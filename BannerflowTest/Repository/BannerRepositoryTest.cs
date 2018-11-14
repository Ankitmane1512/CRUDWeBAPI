using Bannerflow.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace BannerflowTest.Repository
{
    [TestClass]
    public class BannerRepositoryTest
    {
        

        [TestMethod]
        public void Valid_HtmlString_Returns_True()
        {
            string validHtml = "<!DOCType html><html><head></head><body><p>Hello World</p></body></html>";
            Vaildator _validator = new Vaildator();

            bool isValidHtml = _validator.HtmlValidator(validHtml);

            Assert.IsTrue(isValidHtml);
        }

        [TestMethod]
        public void Invalid_HtmlString_Returns_False()
        {
            string brokenHtml = "<!DOCType html><html><head></head><body><p>Hello World</p></body>";
            Vaildator _validator = new Vaildator();

            bool isValidHtml = _validator.HtmlValidator(brokenHtml);

            Assert.IsFalse(isValidHtml);
        }
    }
}
