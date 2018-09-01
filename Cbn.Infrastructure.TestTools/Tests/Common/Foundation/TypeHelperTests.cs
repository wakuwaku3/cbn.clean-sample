using System.Reflection;
using Cbn.Infrastructure.Common.Foundation;
using Cbn.Infrastructure.Common.Foundation.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Cbn.Infrastructure.TestTools.Tests.Common.Foundation
{
    [TestClass]
    public class TypeHelperTests
    {
        [TestMethod]
        public void GetTypeTest()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var assemblyHelper = new Mock<IAssemblyHelper>();
            assemblyHelper.Setup(x => x.GetExecuteAssembly()).Returns(assembly);
            var instance = new TypeHelper(assemblyHelper.Object);
            Assert.AreEqual(typeof(TypeHelperTests), instance.GetType(x => x.Name == nameof(TypeHelperTests)));
        }
    }
}