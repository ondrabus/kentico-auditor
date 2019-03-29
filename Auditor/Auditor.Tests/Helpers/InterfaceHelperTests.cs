
using NUnit.Framework;
using System;

namespace Auditor.Tests.Helpers
{
    [TestFixture]
    public class InterfaceHelperTests
    {
        public interface Interface1 { string TextField { get; set; } }
        public abstract class BaseClass1 : Interface1 { public abstract string TextField { get; set; } }
        public class Class1 : BaseClass1 { public override string TextField { get; set; } }

        [Test]
        public void ShouldGetCountOfDerivedClasses()
        {
            var implementingClasses = Auditor.Core.Helpers.InterfaceHelper.GetImplementingClassesInstances<Interface1>();

            Assert.AreEqual(1, implementingClasses.Count);
            Assert.AreEqual(nameof(Class1), implementingClasses[0].GetType().Name);
        }
    }
}
