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

        private bool BuildWorld(CancellationToken cancellationToken)
        {
            status("Building world ...");
            application.WorldBuilder = application.GetWorldBuilder();
            status($"Building world done");
            return true;
        }

        private bool Preprocess(CancellationToken cancellationToken, int animateStep)
        {
            if (application.WorldBuilder == null)
                BuildWorld(cancellationToken);

            status("Preprocessing world ...");
            var renderMode = RenderModeFactory.Create(false, Settings.Debug);
            var worldToProcess = application.WorldBuilder.CreateWorld(renderMode, animateStep);
            var args = new RenderArgs(worldToProcess, application.Camera, renderMode, cancellationToken, status);
            application.PreprocessorStack.Process(args);
            application.PreprocessedWorld = args.World;
            status($"Preprocessing world done {args.RenderMetadata.Statistics.Select(statistics => statistics.Time.TotalMilliseconds).Sum()}ms)");
            return true;
        }

        public bool PreprocessPreview(CancellationToken cancellationToken, int animateStep)
        {
            if (application.WorldBuilder == null)
                BuildWorld(cancellationToken);

            status("Preprocessing preview world ...");
            var renderMode = RenderModeFactory.Create(true, Settings.Debug);
            var worldToProcess = application.WorldBuilder.CreateWorld(renderMode, animateStep);
            var args = new RenderArgs(worldToProcess, application.Camera, renderMode, cancellationToken, status);
            application.PreprocessorStack.Process(args);
            application.PreprocessedPreviewWorld = args.World;
            status($"Preprocessing preview world done {args.RenderMetadata.Statistics.Select(statistics => statistics.Time.TotalMilliseconds).Sum()}ms");
            return true;
        }

        public bool Process(CancellationToken cancellationToken, int animateStep, string filename = null)
        {
            if (application.PreprocessedWorld == null)
                Preprocess(cancellationToken, animateStep);

            status("Processing world ...");
            var worldToProcess = (Settings.StorePreprocessed) ? application.PreprocessedWorld.Clone() : application.PreprocessedWorld;
            var renderMode = RenderModeFactory.Create(false, Settings.Debug);
            var args = new RenderArgs(worldToProcess, application.Camera, renderMode, cancellationToken, status);

            if (!String.IsNullOrEmpty(filename))
                args.RenderMetadata.Filename = filename;

            application.ProcessorStack.Process(args);

            status($"Processing world done {CommonUtils.ToInt(args.RenderMetadata.Statistics.Select(statistics => statistics.Time.TotalMilliseconds).Sum())} ms");

            if (!Settings.StorePreprocessed)
                application.PreprocessedWorld = null;
            args = null;
            GC.Collect();

            return true;
        }

        public bool ProcessPreview(CancellationToken cancellationToken, int animateStep)
        {
            if (application.PreprocessedPreviewWorld == null)
                PreprocessPreview(cancellationToken, animateStep);

            status("Processing preview world ...");
            var worldToProcess = (Settings.StorePreprocessed) ? application.PreprocessedPreviewWorld.Clone() : application.PreprocessedPreviewWorld;
            var renderMode = RenderModeFactory.Create(true, Settings.Debug);
            var args = new RenderArgs(worldToProcess, application.Camera, renderMode, cancellationToken, status);

            application.ProcessorStack.Process(args);

            status($"Processing preview done {CommonUtils.ToInt(args.RenderMetadata.Statistics.Select(statistics => statistics.Time.TotalMilliseconds).Sum())} ms");

            if (!Settings.StorePreprocessed)
                application.PreprocessedPreviewWorld = null;
            args = null;
            GC.Collect();

            return true;
        }
    }
}
