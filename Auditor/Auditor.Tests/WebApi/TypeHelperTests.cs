using NUnit.Framework;
using System;

namespace Auditor.Tests.WebApi
{
    [TestFixture]
    public class TypeHelperTests
    {
        [Test]
        public void ReturnsTrueForEmptyGuid()
        {
            Assert.IsTrue(Auditor.WebApi.Helpers.TypeHelper.IsNullOrDefault(typeof(Guid), Guid.Empty));
            Assert.IsTrue(Auditor.WebApi.Helpers.TypeHelper.IsNullOrDefault(typeof(Guid), default(Guid)));
        }

        [Test]
        public void ReturnsTrueForEmptyDateTime()
        {
            Assert.IsTrue(Auditor.WebApi.Helpers.TypeHelper.IsNullOrDefault(typeof(DateTime), DateTime.MinValue));
            Assert.IsTrue(Auditor.WebApi.Helpers.TypeHelper.IsNullOrDefault(typeof(DateTime), default(DateTime)));
        }
    }
}
