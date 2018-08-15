using System.Collections.Generic;

namespace Updater.Apis.Dtos
{
    public class StageDtoMod : StageDto
    {
        public StageDtoModA A { get; set; }
        
        public sealed class StageDtoModA
        {
            public IList<StageDtoModC> C { get; set; } = new List<StageDtoModC>();

            public sealed class StageDtoModC
            {
                public IList<StageDtoModR> R { get; set; } = new List<StageDtoModR>();

                public sealed class StageDtoModR
                {
                    public TeamDto T { get; set; }
                }
            }
        }

        public StageDtoModD D { get; set; }

        public sealed class StageDtoModD 
        {
            public IList<StageDtoModC> C { get; set; } = new List<StageDtoModC>();

            public sealed class StageDtoModC 
            {
                public TeamDto A { get; set; }

                public TeamDto H { get; set; }
            }
        }
    }
}