using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cbn.Infrastructure.Common.Foundation.Extensions;
using Cbn.Infrastructure.TestTools.Exceptions;

namespace Microsoft.VisualStudio.TestTools.UnitTesting
{
    /// <summary>
    /// 検証用クラス
    /// </summary>
    public static class Assertion
    {
        private const string DefaultName = "target";
        /// <inheritDoc/>
        public static void Is(this object actual, object expected, string name = DefaultName)
        {
            var errors = new List<Exception>();
            IsInner(expected, actual, name, new List<object>(), errors);
            if (errors.Any())
            {
                throw new AssertException(errors);
            }
        }
        /// <inheritDoc/>
        public static List<Exception> IsNot(this object actual, object expected, string name = DefaultName)
        {
            var errors = new List<Exception>();
            IsInner(expected, actual, name, new List<object>(), errors);
            if (errors.Any())
            {
                return errors;
            }
            throw new AssertException($"{name}は同一の内容です。");
        }

        private static bool IsInner(object actual, object expected, string name, List<Exception> errors)
        {
            try
            {
                Assert.AreEqual(expected, actual, name);
                return true;
            }
            catch (Exception ex)
            {
                errors.Add(ex);
            }
            return false;
        }

        private static void IsInner(object actual, object expected, string name, List<object> verified, List<Exception> errors)
        {
            if (CheckNullBoth(expected, actual, name, errors))
            {
                return;
            }
            var eType = expected.GetType();
            if (eType.IsValueType || eType == typeof(string))
            {
                IsInner(expected, actual, name, errors);
                return;
            }
            if (eType.IsEnumerable())
            {
                AreSequenceEqual(expected as IEnumerable, actual as IEnumerable, name, verified, errors);
                return;
            }

            if (verified.Any(x => x == expected))
            {
                return;
            }
            verified.Add(expected);
            IsPropertyEqual(expected, actual, name, verified, errors);
        }

        private static void IsPropertyEqual(object actual, object expected, string name, List<object> verified, List<Exception> errors)
        {
            if (CheckNullBoth(expected, actual, name, errors))
            {
                return;
            }

            var eProps = expected.GetType().GetProperties();
            var aProps = actual.GetType().GetProperties();
            foreach (var eProp in eProps)
            {
                var aProp = aProps.FirstOrDefault(x => x.Name == eProp.Name);
                if (aProp == null)
                {
                    errors.Add(new NotFoundPropertyAssertException(name, eProp.Name));
                    continue;
                }
                var e = expected.Get(eProp);
                var a = actual.Get(aProp);
                IsInner(e, a, $"{name}.{eProp.Name}", verified, errors);
            }
        }

        private static bool CheckNullBoth(object actual, object expected, string name, List<Exception> errors)
        {
            if (expected == null || actual == null)
            {
                if (expected != null || actual != null)
                {
                    errors.Add(new OneSideOnlyNullAssertException(expected == null, name));
                }
                return true;
            }
            return false;
        }

        private static void AreSequenceEqual(IEnumerable expected, IEnumerable actual, string name, List<object> verified, List<Exception> errors)
        {
            if (CheckNullBoth(expected, actual, name, errors))
            {
                return;
            }
            var eList = expected.Cast<object>().ToList();
            var aList = actual.Cast<object>().ToList();
            if (!IsInner(eList.Count, aList.Count, $"{name}.{nameof(eList.Count)}", errors))
            {
                return;
            }

            for (int i = 0; i < eList.Count; i++)
            {
                var e = eList[i];
                var a = aList[i];
                IsInner(e, a, $"{name}[{i}]", verified, errors);
            }
        }

        /// <inheritDoc/>
        public static T ThrowsException<T>(this Action action) where T : Exception
        {
            return Assert.ThrowsException<T>(action);
        }

        /// <inheritDoc/>
        public static T ThrowsException<T>(this Func<object> action) where T : Exception
        {
            return Assert.ThrowsException<T>(action);
        }

        /// <inheritDoc/>
        public static async Task<T> ThrowsExceptionAsync<T>(this Func<Task> action) where T : Exception
        {
            return await Assert.ThrowsExceptionAsync<T>(action);
        }
    }
}