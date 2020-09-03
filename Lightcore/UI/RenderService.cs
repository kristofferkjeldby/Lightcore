namespace Lightcore.UI
{
    using Lightcore.Common;
    using Lightcore.Processors;
    using Lightcore.Processors.Models;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class RenderService
    {
        Application application;
        readonly Action<string> status;

        public RenderService(Application application, Action<string> status)
        {
            this.application = application;
            this.status = status;
        }

        private void Preprocess(CancellationToken cancellationToken)
        {
            status("Preprocessing world ...");
            var worldToProcess = application.World.Clone();
            var renderMode = RenderModeFactory.Create(false, Settings.Debug);
            var args = new RenderArgs(worldToProcess, application.Camera, renderMode, cancellationToken, status);
            application.PreprocessorStack.Process(args);
            application.PreprocessedWorld = args.World;
            status($"Preprocessing world done {args.RenderMetadata.Statistics.Select(statistics => statistics.Time.TotalMilliseconds).Sum()}ms)");
        }

        private void PreprocessPreview(CancellationToken cancellationToken)
        {
            status("Preprocessing preview world ...");
            var worldToProcess = application.World.Clone();
            var renderMode = RenderModeFactory.Create(true, Settings.Debug);
            var args = new RenderArgs(worldToProcess, application.Camera, renderMode, cancellationToken, status);
            application.PreprocessorStack.Process(args);
            application.PreprocessedPreviewWorld = args.World;
            status($"Preprocessing preview world done {args.RenderMetadata.Statistics.Select(statistics => statistics.Time.TotalMilliseconds).Sum()}ms");
        }

        private void Process(CancellationToken cancellationToken, string filename = null)
        {
            if (application.PreprocessedWorld == null || !Settings.StorePreprocessed)
                Preprocess(cancellationToken);

            if (cancellationToken.IsCancellationRequested)
                return;

            status("Processing world ...");
            var worldToProcess = (Settings.StorePreprocessed) ? application.PreprocessedWorld.Clone() : application.PreprocessedWorld;
            var renderMode = RenderModeFactory.Create(false, Settings.Debug);
            var args = new RenderArgs(worldToProcess, application.Camera, renderMode, cancellationToken, status);

            if (!String.IsNullOrEmpty(filename))
                args.RenderMetadata.Filename = filename;

            application.ProcessorStack.Process(args);

            if (cancellationToken.IsCancellationRequested)
                return;

            status($"Processing world done {CommonUtils.ToInt(args.RenderMetadata.Statistics.Select(statistics => statistics.Time.TotalMilliseconds).Sum())}ms");
        }

        private void ProcessPreview(CancellationToken cancellationToken)
        {
            if (application.PreprocessedPreviewWorld == null || !Settings.StorePreprocessed)
                PreprocessPreview(cancellationToken);

            if (cancellationToken.IsCancellationRequested)
                return;

            var worldToProcess = (Settings.StorePreprocessed) ? application.PreprocessedPreviewWorld.Clone() : application.PreprocessedPreviewWorld;
            var renderMode = RenderModeFactory.Create(true, Settings.Debug);
            var args = new RenderArgs(worldToProcess, application.Camera, renderMode, cancellationToken, status);

            application.ProcessorStack.Process(args);

            if (cancellationToken.IsCancellationRequested)
                return;

            status("Previewing, press Render to process");
        }

        public Task ProcessAsync(CancellationToken cancellationToken, string filename = null)
        {
            var task = new Task(() => Process(cancellationToken, filename), TaskCreationOptions.LongRunning);
            task.Start();
            return task;
        }

        public Task ProcessPreviewAsync(CancellationToken cancellationToken)
        {
            var task = new Task(() => ProcessPreview(cancellationToken), TaskCreationOptions.LongRunning);
            task.Start();
            return task;
        }
    }
}
