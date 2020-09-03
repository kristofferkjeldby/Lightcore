namespace Lightcore.UI
{
    using System.Windows.Forms;

    public class KeyboardService
    {
        Application application;
        bool suspended = false;

        public KeyboardService(Application application)
        {
            this.application = application;
        }

        public void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (suspended)
                return;

            switch (e.KeyCode)
            {
                case Keys.D1: // 1 - Rotate up
                    application.Rotate(0, 1);
                    break;

                case Keys.D2: // 2 - Rotate down
                    application.Rotate(0, -1);
                    break;

                case Keys.D3: // 3 - Rotate left
                    application.Rotate(1, 1);
                    break;

                case Keys.D4: // 4 - Rotate right
                    application.Rotate(1, -1);
                    break;

                case Keys.D5: // 5 - Spin left
                    application.Rotate(2, 1);
                    break;

                case Keys.D6: // 6 - Spin right
                    application.Rotate(2, -1);
                    break;

                case Keys.D: // D - move right
                    application.Move(0, 1);
                    break;

                case Keys.A: // A - move left
                    application.Move(0, -1);
                    break;

                case Keys.W: // W - move up
                    application.Move(1, 1);
                    break;

                case Keys.S: // S - move down
                    application.Move(1, -1);
                    break;

                case Keys.O: // O - move forward
                    application.Move(2, 1);
                    break;

                case Keys.L: // L - move backwards
                    application.Move(2, -1);
                    break;
            }
        }

        public void Suspend(bool value)
        {
            suspended = value;
        }
    }
}
