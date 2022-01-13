namespace Lightcore.Processors.Models
{
    using Lightcore.Common.Models;
    using System;
    using System.Linq;

    public abstract class Processor
    {
        public abstract ProcessorMetadata Metadata { get; }

        public void Process(RenderArgs args)
        {
            RenderStatistic statistic = new RenderStatistic();
            statistic.Name = Metadata.Name;
            statistic.Started = DateTime.Now;

            var entities = args.World.Entities.Where(e => EntityPredicate(e, args)).ToArray();
            var entitiesCount = entities.Count();

            args.Status($"{Metadata.Name}: Preprocessing ...");

            args.CancellationToken.ThrowIfCancellationRequested();

            PreProcessor(args);

            if (RunProcessors())
            { 
                for (int i = 0; i < entitiesCount; i++)
                {
                    if (!EntityPredicate(args.World.Entities[i], args))
                        continue;

                    var polygons = args.World.Entities[i].Elements.ToArray();
                    var polygonsCount = args.World.Entities[i].Elements.Count();

                    for (int j = 0; j < polygons.Count(); j++)
                    {
                        if ((j + 1) % 1000 == 0)
                            args.Status($"{Metadata.Name}: Processing entity {i + 1} of {entitiesCount}, polygon {j + 1} of {polygonsCount} ...");

                        for (int k = 0; k < args.World.Entities[i].Elements[j].Elements.Length; k++)
                        {
                            args.World.Entities[i].Elements[j].Elements[k] = VectorProcessor(args.World.Entities[i].Elements[j].Elements[k], args);
                            statistic.Vectors++;
                        }

                        args.CancellationToken.ThrowIfCancellationRequested();

                        args.World.Entities[i].Elements[j] = PolygonProcessor(args.World.Entities[i].Elements[j], args);
                        statistic.Polygons++;
                    }
                }
            }

            args.CancellationToken.ThrowIfCancellationRequested();

            args.Status($"{Metadata.Name}: Postprocessing ...");

            PostProcessor(args);
            statistic.Ended = DateTime.Now;

            args.RenderMetadata.Statistics.Add(statistic);
        }

        public virtual Vector VectorProcessor(Vector vector, RenderArgs args)
        {
            return vector;
        }

        public virtual Polygon PolygonProcessor(Polygon polygon, RenderArgs args)
        {
            return polygon;
        }

        private bool RunProcessors()
        {
            var polygonProcessor = this.GetType().GetMethod("PolygonProcessor");
            if (polygonProcessor.GetBaseDefinition().DeclaringType != polygonProcessor.DeclaringType)
                return true;

            var vectorProcessor = this.GetType().GetMethod("VectorProcessor");
            if (vectorProcessor.GetBaseDefinition().DeclaringType != vectorProcessor.DeclaringType)
                return true;

            return false;
        }

        public virtual void PreProcessor(RenderArgs args)
        {

        }

        public virtual void PostProcessor(RenderArgs args)
        {

        }

        public bool EntityPredicate(Entity entity, RenderArgs args)
        {
            if (!args.RenderMode.EntityTypeFilter.Contains(entity.EntityType))
                return false;

            if (!Metadata.EntityTypeFilter.Contains(entity.EntityType))
                return false;

            return true;
        }

        public bool Disabled { get; set; }
    }
}
