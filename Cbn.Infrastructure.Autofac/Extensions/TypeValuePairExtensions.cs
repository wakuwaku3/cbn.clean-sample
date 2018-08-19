using Autofac;
using Cbn.Infrastructure.Common.ValueObjects;

namespace Cbn.Infrastructure.Autofac.Extensions
{
    public static class TypeValuePairExtensions
    {
        public static TypedParameter Convert(this TypeValuePair pair) => new TypedParameter(pair.Type, pair.Value);
    }
}