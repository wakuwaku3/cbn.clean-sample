using System;
using System.Collections.Generic;
using Cbn.Infrastructure.Common.Foundation.Interfaces;
using Cbn.Infrastructure.Common.ValueObjects;

namespace Cbn.Infrastructure.Common.Foundation
{
    public class MapRegister : IMapRegister
    {
        private Dictionary<MapKey, Action<object, object>> map = new Dictionary<MapKey, Action<object, object>>();
        public Dictionary<MapKey, Action<object, object>> CreateMap()
        {
            return this.map;
        }

        public void Register<TSource, TDestination>(Action<TSource, TDestination> action)
        where TSource : class
        where TDestination : class
        {
            this.map.Add(new MapKey<TSource, TDestination>(), (s, d) => action(s as TSource, d as TDestination));
        }

        public void RegisterDefinition<TSource, TDestination>(IMapDefinition<TSource, TDestination> mapDefinition)
        where TSource : class
        where TDestination : class
        {
            this.map.Add(new MapKey<TSource, TDestination>(), (s, d) => mapDefinition.Map(s as TSource, d as TDestination));
            this.map.Add(new MapKey<TDestination, TSource>(), (s, d) => mapDefinition.MapReverse(s as TDestination, d as TSource));
        }
    }
}