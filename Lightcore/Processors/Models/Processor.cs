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

            var entityCount = args.World.Entities.Where(e => EntityPredicate(e, args)).Count();

            args.Status($"{Metadata.Name}: Preprocessing ...");

            PreProcessor(args);

            if (RunProcessors())
            { 
                for (int i = 0; i < args.World.Entities.Count; i++)
                {
                    if (!EntityPredicate(args.World.Entities[i], args))
                        continue;

                    for (int j = 0; j < args.World.Entities[i].Elements.Count(); j++)
                    {
                        args.Status($"{Metadata.Name}: Processing entity {entityCount - i} of {entityCount}, polygon {j + 1} of {args.World.Entities[i].Elements.Count()} ...");

                        for (int k = 0; k < args.World.Entities[i].Elements[j].Elements.Length; k++)
                        {
                            args.World.Entities[i].Elements[j].Elements[k] = VectorProcessor(args.World.Entities[i].Elements[j].Elements[k], args);
                            statistic.Vectors++;
                        }

                        if (args.CancellationToken.IsCancellationRequested)
                            return;

                        args.World.Entities[i].Elements[j] = PolygonProcessor(args.World.Entities[i].Elements[j], args);
                        statistic.Polygons++;
                    }
                }
            }

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
