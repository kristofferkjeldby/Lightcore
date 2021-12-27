namespace Lightcore.UI
{
    using Lightcore.Common;
    using Lightcore.Processors;
    using Lightcore.Processors.Models;
    using System;
    using System.Linq;
    using System.Threading;

    public class RenderService
    {
        Application application;
        readonly Action<string> status;

        public RenderService(Application application, Action<string> status)
        {
            this.application = application;
            this.status = status;
        }

        private bool Preprocess(CancellationToken cancellationToken, int animateStep)
        {
            status("Preprocessing world ...");
            var worldToProcess = application.GetWorld(animateStep).Clone();
            var renderMode = RenderModeFactory.Create(false, Settings.Debug);
            var args = new RenderArgs(worldToProcess, application.Camera, renderMode, cancellationToken, status);
            application.PreprocessorStack.Process(args);
            application.PreprocessedWorld = args.World;
            status($"Preprocessing world done {args.RenderMetadata.Statistics.Select(statistics => statistics.Time.TotalMilliseconds).Sum()}ms)");
            return true;
        }

        public bool PreprocessPreview(CancellationToken cancellationToken, int animateStep)
        {
            status("Preprocessing preview world ...");
            var worldToProcess = application.GetWorld(animateStep).Clone();
            var renderMode = RenderModeFactory.Create(true, Settings.Debug);
            var args = new RenderArgs(worldToProcess, application.Camera, renderMode, cancellationToken, status);
            application.PreprocessorStack.Process(args);
            application.PreprocessedPreviewWorld = args.World;
            status($"Preprocessing preview world done {args.RenderMetadata.Statistics.Select(statistics => statistics.Time.TotalMilliseconds).Sum()}ms");
            return true;
        }

        public bool Process(CancellationToken cancellationToken, int animateStep, string filename = null)
        {
            if (application.PreprocessedWorld == null || !Settings.StorePreprocessed)
                Preprocess(cancellationToken, animateStep);

            if (cancellationToken.IsCancellationRequested)
                return false;

            status("Processing world ...");
            var worldToProcess = (Settings.StorePreprocessed) ? application.PreprocessedWorld.Clone() : application.PreprocessedWorld;
            var renderMode = RenderModeFactory.Create(false, Settings.Debug);
            var args = new RenderArgs(worldToProcess, application.Camera, renderMode, cancellationToken, status);

            if (!String.IsNullOrEmpty(filename))
                args.RenderMetadata.Filename = filename;

            application.ProcessorStack.Process(args);

            if (cancellationToken.IsCancellationRequested)
                return false;

            status($"Processing world done {CommonUtils.ToInt(args.RenderMetadata.Statistics.Select(statistics => statistics.Time.TotalMilliseconds).Sum())}ms");

            return true;
        }

        public bool ProcessPreview(CancellationToken cancellationToken, int animateStep)
        {
            if (application.PreprocessedPreviewWorld == null || !Settings.StorePreprocessed)
                PreprocessPreview(cancellationToken, animateStep);

            if (cancellationToken.IsCancellationRequested)
                return false;

            var worldToProcess = (Settings.StorePreprocessed) ? application.PreprocessedPreviewWorld.Clone() : application.PreprocessedPreviewWorld;
            var renderMode = RenderModeFactory.Create(true, Settings.Debug);
            var args = new RenderArgs(worldToProcess, application.Camera, renderMode, cancellationToken, status);

            application.ProcessorStack.Process(args);

            if (cancellationToken.IsCancellationRequested)
                return false;

            status("Previewing, press Render to process");

            return true;
        }
    }
}
