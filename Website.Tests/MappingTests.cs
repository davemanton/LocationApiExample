using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Website.Config;

namespace Website.Tests
{
    [TestClass]
    public class MappingTests
    {
        [TestMethod]
        public void MappingProfileTest()
        {
			Mapping.Initialize();

			Mapper.AssertConfigurationIsValid();
		}
    }
}
