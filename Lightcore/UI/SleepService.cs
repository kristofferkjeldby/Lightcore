namespace Lightcore.UI
{
    using System;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Threading.Tasks;

    public static class SleepService
    {
        [Flags]
        private enum EXECUTION_STATE : uint
        {
            ES_AWAYMODE_REQUIRED = 0x00000040,
            ES_CONTINUOUS = 0x80000000,
            ES_DISPLAY_REQUIRED = 0x00000002,
            ES_SYSTEM_REQUIRED = 0x00000001
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern uint SetThreadExecutionState(EXECUTION_STATE esFlags);

        private static AutoResetEvent resetEvent;

        public static void Suspend(bool value)
        {
            if (value)
            { 
                resetEvent = new AutoResetEvent(false);

                (new TaskFactory()).StartNew(() =>
                {
                    SetThreadExecutionState(
                        EXECUTION_STATE.ES_CONTINUOUS
                        | EXECUTION_STATE.ES_DISPLAY_REQUIRED
                        | EXECUTION_STATE.ES_SYSTEM_REQUIRED);
                    resetEvent.WaitOne();

                },
                    TaskCreationOptions.LongRunning);
            }
            else
            {
                resetEvent.Set();
            }
        }
    }
}