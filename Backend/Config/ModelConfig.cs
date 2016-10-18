using FluentNHibernate.Automapping;
using System;

namespace Backend.Config
{
    public class ModelConfig : DefaultAutomappingConfiguration
    {
        public override bool ShouldMap(Type type)
        {
            return type.Namespace=="Common.Model";
        }
    }
}
