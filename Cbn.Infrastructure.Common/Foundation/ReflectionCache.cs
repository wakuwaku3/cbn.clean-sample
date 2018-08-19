using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Cbn.Infrastructure.Common.Foundation.Interfaces;

namespace Cbn.Infrastructure.Common.Foundation.Reflection
{
    /// <summary>
    /// ReflectionCache
    /// </summary>
    public class ReflectionCache : IReflectionCache
    {
        private readonly IAssemblyHelper assemblyHelper;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ReflectionCache(IAssemblyHelper assemblyHelper)
        {
            this.assemblyHelper = assemblyHelper;
            this.TypeCache = new Lazy<Type[]>(() => GetAllTypeArray(), true);
        }

        /// <inheritDoc/>
        public ConcurrentDictionary<MemberInfo, Func<object, object>> GetterCache { get; } = new ConcurrentDictionary<MemberInfo, Func<object, object>>();
        /// <inheritDoc/>
        public ConcurrentDictionary<MemberInfo, Action<object, object>> SetterCache { get; } = new ConcurrentDictionary<MemberInfo, Action<object, object>>();
        /// <inheritDoc/>
        public Lazy<Type[]> TypeCache { get; }
        /// <inheritDoc/>
        public ConcurrentDictionary<MemberInfo, IEnumerable<Attribute>> AttributeCache { get; } = new ConcurrentDictionary<MemberInfo, IEnumerable<Attribute>>();
        /// <inheritDoc/>
        public ConcurrentDictionary<Type, Func<IEnumerable<object>, IEnumerable>> CasterCache { get; } = new ConcurrentDictionary<Type, Func<IEnumerable<object>, IEnumerable>>();

        private Type[] GetAllTypeArray()
        {
            var hashSet = new HashSet<string>();
            return GetAllTypes(this.assemblyHelper.GetExecuteAssembly()).Distinct().ToArray();

            IEnumerable<Type> GetAllTypes(Assembly assembly)
            {
                foreach (var type in assembly.GetTypes())
                {
                    yield return type;
                }
                var referencedAssemblies = assembly.GetReferencedAssemblies().Where(x => !hashSet.Contains(x.FullName));
                foreach (var referencedAssembly in referencedAssemblies)
                {
                    hashSet.Add(referencedAssembly.FullName);
                    foreach (var type in GetAllTypes(Assembly.Load(referencedAssembly)))
                    {
                        yield return type;
                    }
                }
            }
        }
    }
}