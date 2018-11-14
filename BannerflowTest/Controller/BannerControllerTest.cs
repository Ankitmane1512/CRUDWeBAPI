using Bannerflow.Controllers;
using Bannerflow.IRepository;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Net;
using System.Threading.Tasks;

namespace BannerflowTest.Controller
{
    
        [TestClass]
        public class BannerControllerTest
        {
            private Mock<IBannerRepository> _bannerRepository;
            private BannerController _bannerController;
             

            [TestInitialize]
            public void Init()
            {
                _bannerRepository = new Mock<IBannerRepository>();
                _bannerController = new BannerController(_bannerRepository.Object);
            }

            //Testing Dependency Injection
            [TestMethod]
            public void  Calls_IBannerRepository()
            {
            //arrange
            string id = "";
               
                //act
                var response =  _bannerController.DeleteBanner(id);
                
               //assert
               _bannerRepository.Verify( br => br.DeleteBanner(id) , Times.Once);
            }

            [TestMethod]
            public async Task Returns_503_When_IBannerRepository_Throws_Exception()
            {
            string id ="" ;
            _bannerRepository.Setup(br => br.GetBannerHtml(id)).Throws(new Exception("DB down"));
            
            var response =  await _bannerController.GetBannerHtml(id);

            Assert.IsTrue(response is StatusCodeResult);
            var statusCode = (StatusCodeResult)response;
            statusCode.StatusCode.Should().Be((int)HttpStatusCode.ServiceUnavailable);
            }

            [TestMethod]
            public async Task Returns_400_When_GetBanners_Called()
            {
            string id = "";

            var response = await _bannerController.GetBanners(id);

            Assert.IsTrue(response is StatusCodeResult);
            var statusCode = (StatusCodeResult)response;
            statusCode.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);

            }

      
        }

}
    