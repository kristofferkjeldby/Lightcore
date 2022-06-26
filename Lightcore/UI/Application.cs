namespace Lightcore.UI
{
    using Lightcore.Common.Cartesian;
    using Lightcore.Common.Models;
    using Lightcore.Processors;
    using Lightcore.Worlds.Models;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class Application
    {
        public World PreprocessedWorld { get; set; }
        public World PreprocessedPreviewWorld { get; set; }
        public World ProcessedPreviewWorld { get; set; }
        public World Camera { get; set; }
        public WorldBuilder WorldBuilder { get; set; }

        public KeyboardService KeyboardService { get; set; }
        public RenderService RenderService { get; set; }
        public ButtonService ButtonService { get; set; }

        public ProcessorStack ProcessorStack { get; set; }
        public ProcessorStack PreprocessorStack { get; set; }

        public event EventHandler<string> StatusChanged;
        public event EventHandler<bool> SuspendChanged;

        private Task<bool> task;
        private CancellationTokenSource CancellationTokenSource;

        public Func<WorldBuilder> GetWorldBuilder { get; set; }

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
            Camera.ReferenceFrame.Origon = Camera.ReferenceFrame.Origon + direction * Camera.ReferenceFrame.Base.Transpose()[axis];
            ProcessPreview(0);
        }

        public void Rotate(int axis, int direction)
        {
            Camera.ReferenceFrame.Base = CartesianUtils.RotateTransformation(Camera.ReferenceFrame.Base[axis], direction * Settings.R).Matrix * Camera.ReferenceFrame.Base;
            ProcessPreview(0);
        }

        public void Start()
        {
            ProcessPreview(0);
        }

        public void Process(int animateStep = 0, string filename = null)
        {
            CancellationTokenSource = new CancellationTokenSource();
            OnSuspendChanged(true);
            task = Task.Run(() => RenderService.Process(CancellationTokenSource.Token, animateStep, filename), CancellationTokenSource.Token);
            task.ContinueWith(t => OnSuspendChanged(false, t));
        }

        public void Animate()
        {
            CancellationTokenSource = new CancellationTokenSource();
            OnSuspendChanged(true);
            task = Task.Run(() =>
            {
                var result = false;
                for (int animateStep = 0; animateStep < Settings.AnimateMaxSteps; animateStep++)
                {
                    var filename = string.Concat(Settings.AnimateFilename, animateStep, ".jpg");
                    result = RenderService.Process(CancellationTokenSource.Token, animateStep, filename);
                }
                return result;
            }, CancellationTokenSource.Token);
            task.ContinueWith(t => OnSuspendChanged(false, t));
        }

        public void ProcessPreview(int animateStep)
        {
            CancellationTokenSource = new CancellationTokenSource();
            OnSuspendChanged(true);
            task = Task.Run(() => RenderService.ProcessPreview(CancellationTokenSource.Token, animateStep));
            task.ContinueWith(t => OnSuspendChanged(false, t));
        }

        public bool OnSuspendChanged(bool suspended, Task task = null)
        {
            if (task?.IsFaulted ?? false)
            {
                OnStatusChanged("Failed: " + task.Exception.InnerExceptions[0]?.Message);
                System.Diagnostics.Debug.WriteLine(task.Exception.InnerExceptions[0]?.Message);
                System.Diagnostics.Debug.WriteLine(task.Exception.InnerExceptions[0]?.StackTrace);
            }

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
                task.ContinueWith(t => OnStatusChanged("Cancelled ..."));
            }
        }
    }
}
