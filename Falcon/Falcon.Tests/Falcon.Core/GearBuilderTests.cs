using System.Collections.Generic;
using Falcon.Core.Builder;
using Falcon.Core.Model.Loadouts;
using NUnit.Framework;

namespace Falcon.Tests.Falcon.Core
{
    [TestFixture]
    public class GearBuilderTests
    {
        [Test]
        public void Test()
        {
            GearBuilder sut = new GearBuilder();
            var result = sut.BuildUnitgear(new List<Loadout>());

            Assert.NotNull(result);
        }
    }
}