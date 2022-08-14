using System.Collections.Generic;

namespace CarUtils.Entities
{
    public class RegionEntity
    {
        public readonly string InternationalName;
        public readonly string NationalName;
        public readonly IEnumerable<string> Regions;

        public RegionEntity(
            string internationalName,
            string nationalName,
            params string[] regions
        )
        {
            this.InternationalName = internationalName;
            this.NationalName = nationalName;
            this.Regions = regions;
        }
    }
}