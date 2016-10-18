using FluentNHibernate.Automapping.Alterations;
using FluentNHibernate.Automapping;
using Common.Model.Enum;

namespace Common.Model.Constraints
{
    public class ArtistConstraints : IAutoMappingOverride<Artist>
    {
        public void Override(AutoMapping<Artist> mapping)
        {
            mapping.Map(a => a.Genre).CustomType<Genre>();
            mapping.Map(a => a.Name).Unique();
        }
    }
}
