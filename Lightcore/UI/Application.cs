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
        public World World { get; set; }
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

        private Task task;
        private CancellationTokenSource cancellationTokenSource;

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
            ProcessPreview();
        }

        public void Rotate(int axis, int direction)
        {
            Camera.ReferenceFrame.Passive = CartesianUtils.Rotate(Camera.ReferenceFrame.Passive[axis], direction * Settings.R) * Camera.ReferenceFrame.Passive;
            ProcessPreview();
        }

        public void Start()
        {
            ProcessPreview();
        }

        public Task Process(string filename = null)
        {
            OnSuspendChanged(true);
            cancellationTokenSource = new CancellationTokenSource();
            task = RenderService.ProcessAsync(cancellationTokenSource.Token, filename);
            task.ContinueWith((task) => OnSuspendChanged(false));
            return task;
        }

        public void ProcessPreview()
        {
            OnSuspendChanged(true);
            cancellationTokenSource = new CancellationTokenSource();
            task = RenderService.ProcessPreviewAsync(cancellationTokenSource.Token);
            task.ContinueWith((task) => OnSuspendChanged(false));
        }

        public void OnSuspendChanged(bool suspended)
        {
            SleepService.Suspend(suspended);
            KeyboardService.Suspend(suspended);
            SuspendChanged?.Invoke(this, suspended);
        }

        public void OnStatusChanged(string status)
        {
            StatusChanged?.Invoke(this, status);
        }

        public void Cancel()
        {
            var token = cancellationTokenSource.Token;

            if (task.Status == TaskStatus.Running && !(cancellationTokenSource?.IsCancellationRequested ?? false))
            {
                cancellationTokenSource.Cancel();
                OnStatusChanged("Cancelling ...");
                task.Wait();
                OnSuspendChanged(false);
                OnStatusChanged("Cancelled");

            }
        }
    }
}
