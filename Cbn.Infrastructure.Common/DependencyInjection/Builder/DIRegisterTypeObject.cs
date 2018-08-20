using System;
using System.Collections.Generic;
using System.Linq;

namespace Cbn.Infrastructure.Common.DependencyInjection.Builder
{
    public class DIRegisterTypeObject : DIRegisterObject
    {
        public DIRegisterTypeObject(
            Type registerType,
            IEnumerable<Type> aliasTypes = null,
            LifetimeType lifetimeType = LifetimeType.Dependency,
            IEnumerable<string> scopeTags = null) : base(aliasTypes)
        {
            this.RegisterType = registerType;
            this.LifetimeType = lifetimeType;
            this.ScopeTags = scopeTags?.ToList() ?? new List<string>();
        }
        public Type RegisterType { get; }
        public LifetimeType LifetimeType { get; set; }
        public IList<string> ScopeTags { get; set; }
        public DIRegisterTypeObject InstancePerDependency()
        {
            this.LifetimeType = LifetimeType.Dependency;
            return this;
        }
        public DIRegisterTypeObject SingleInstance()
        {
            this.LifetimeType = LifetimeType.Singleton;
            return this;
        }
        public DIRegisterTypeObject InstancePerLifetimeScope()
        {
            this.LifetimeType = LifetimeType.Scoped;
            return this;
        }
        public DIRegisterTypeObject InstancePerMatchingLifetimeScope(params string[] tags)
        {
            this.InstancePerLifetimeScope();
            this.ScopeTags = tags.ToList();
            return this;
        }
        public DIRegisterTypeObject As(Type aliasType)
        {
            this.AliasTypes.Add(aliasType);
            return this;
        }
        public DIRegisterTypeObject As<TAlias>() => this.As(typeof(TAlias));
    }
    public class DIRegisterTypeObject<TRegister> : DIRegisterTypeObject
    {
        public DIRegisterTypeObject(
            IEnumerable<Type> aliasTypes = null,
            LifetimeType lifetimeType = LifetimeType.Dependency,
            IEnumerable<string> scopeTags = null) : base(typeof(TRegister), aliasTypes, lifetimeType, scopeTags) { }
    }
}