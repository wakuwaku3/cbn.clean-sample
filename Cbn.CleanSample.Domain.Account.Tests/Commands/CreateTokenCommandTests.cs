using System;
using System.Threading.Tasks;
using Cbn.CleanSample.Domain.Account.Commands;
using Cbn.CleanSample.Domain.Common.Models;
using Cbn.Infrastructure.Common.Claims.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Cbn.CleanSample.Domain.Account.Tests.Commands
{
    [TestClass]
    public class CreateTokenCommandTests
    {
        [TestMethod]
        public async Task ExecuteAsync()
        {
            var userClaim = new UserClaim();
            var expected = Guid.NewGuid().ToString();
            var mock = new Mock<IJwtFactory>();
            mock.Setup(x => x.Create(userClaim)).Returns(expected);
            var command = new CreateTokenCommand(mock.Object);
            var actual = await command.ExecuteAsync(userClaim);
            actual.Is(expected);
            "actual".Is("expected");
        }
    }
}