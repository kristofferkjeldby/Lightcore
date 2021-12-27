namespace Lightcore.UI
{
    using Lightcore.Common.Cartesian;
    using Lightcore.Common.Models;
    using Lightcore.Processors;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class Application
    {
        public World PreprocessedWorld { get; set; }
        public World ProcessedWorld { get; set; }
        public World PreprocessedPreviewWorld { get; set; }
        public World ProcessedPreviewWorld { get; set; }
        public World Camera { get; set; }

        public KeyboardService KeyboardService { get; set; }
        public RenderService RenderService { get; set; }
        public ButtonService ButtonService { get; set; }

        public ProcessorStack ProcessorStack { get; set; }
        public ProcessorStack PreprocessorStack { get; set; }

        public event EventHandler<string> StatusChanged;
        public event EventHandler<bool> SuspendChanged;

        public Func<int, World> GetWorld;

        private Task task;
        private CancellationTokenSource CancellationTokenSource = new CancellationTokenSource();

        public Application()
        {
            ProcessorStack = new ProcessorStack();
            PreprocessorStack = new ProcessorStack();
            KeyboardService = new KeyboardService(this);
            RenderService = new RenderService(this, OnStatusChanged);
            ButtonService = new ButtonService(this);
        }

        public void Move(int axis, int direction)
        {
            Camera.ReferenceFrame.Origon = Camera.ReferenceFrame.Origon + direction * Camera.ReferenceFrame.Active[axis];
            ProcessPreview(0);
        }

        public void Rotate(int axis, int direction)
        {
            Camera.ReferenceFrame.Passive = CartesianUtils.Rotate(Camera.ReferenceFrame.Passive[axis], direction * Settings.R) * Camera.ReferenceFrame.Passive;
            ProcessPreview(0);
        }

        public void Start()
        {
            ProcessPreview(0);
        }

        public void Process(int animateStep = 0, string filename = null)
        {
            OnSuspendChanged(true);
            task = new Task(() => RenderService.Process(CancellationTokenSource.Token, animateStep, filename), CancellationTokenSource.Token);
            task.ContinueWith(t => OnSuspendChanged(false));
            task.Start();
        }

        public void Animate()
        {
            OnSuspendChanged(true);
            task = new Task(() =>
            {
                for (int animateStep = 1; animateStep < Settings.AnimateMaxSteps; animateStep++)
                {
                    var filename = string.Concat(Settings.AnimateFilename, animateStep, ".jpg");
                    RenderService.Process(CancellationTokenSource.Token, animateStep, filename);
                }
            }, CancellationTokenSource.Token);
            task.ContinueWith(t => OnSuspendChanged(false));
            task.Start();
        }

        public void ProcessPreview(int animateStep)
        {
            OnSuspendChanged(true);
            task = new Task(() => RenderService.ProcessPreview(CancellationTokenSource.Token, animateStep));
            task.ContinueWith(t => OnSuspendChanged(false));
            task.Start();
        }

        public bool OnSuspendChanged(bool suspended)
        {
            SleepService.Suspend(suspended);
            KeyboardService.Suspend(suspended);
            SuspendChanged?.Invoke(this, suspended);
            return true;
        }

        public void OnStatusChanged(string status)
        {
            StatusChanged?.Invoke(this, status);
        }

        public void Cancel()
        {
            if (task.Status == TaskStatus.Running && !(CancellationTokenSource?.IsCancellationRequested ?? false))
            {
                CancellationTokenSource.Cancel();
                OnStatusChanged("Cancelling ...");
                OnSuspendChanged(false);
                OnStatusChanged("Cancelled");

            }
        }
    }
}
