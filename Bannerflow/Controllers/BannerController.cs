using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Bannerflow.IRepository;
using Bannerflow.Models;
using Bannerflow.Repository;
using log4net;
using Microsoft.AspNetCore.Mvc;

namespace Bannerflow.Controllers
{

    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class BannerController : Controller
    {

        private readonly IBannerRepository _bannerRepository;
        private string VaildationMessage = "";
        private readonly ILog _log;
        private readonly Vaildator _validator;

        public BannerController(IBannerRepository bannerRepository)
        {
            _bannerRepository = bannerRepository;
            _log = LogManager.GetLogger(typeof(BannerController));
            _validator = new Vaildator();
        }


        [Route("createbanner")]
        //Create
        [HttpPost]
        public async Task<IActionResult> CreateBanner([FromBody] Banner objBanner)
        {
            VaildationMessage = "";
            //Flag for Checking whether new entry has being made
            bool IsAdded = false;
          
            try
            {
                

                //Validating Html and if not valid HTMl then  Message is sent back  With Errors 
                VaildationMessage = _validator.HtmlValidationMessage(objBanner.Html);
                
                //Save Method
                // If there is no validation messsge, i.e HTMl is valid and so entry is being made into database 
                if (string.IsNullOrEmpty(VaildationMessage))
                {
                    objBanner = await _bannerRepository.AddBanner(objBanner);
                    if (!string.IsNullOrEmpty(objBanner.Id)) IsAdded = true;
                }
                
            }
            catch (Exception ex)
            {
                _log.Error("Error in CreateBanner method of BannerController :" + Environment.NewLine + ex.StackTrace);
            }

            //Response 
            if (!string.IsNullOrEmpty(VaildationMessage)) // Bad request with  Errors in Html is sent back
                return BadRequest(VaildationMessage);
            else if (IsAdded)
                return Ok(objBanner); // Newly created entry
            else
                return new StatusCodeResult((int)HttpStatusCode.ServiceUnavailable);

        }

        // id is not Compulsory, if id is not passed all data will be returned else data relevant to id is returned
        [Route("getbanner/{id?}")]
        //Read 
        [HttpGet]
        public async Task<IActionResult> GetBanners(string id)
        {
            List<Banner> objbanner = new List<Banner>();

            try
            {
                var bannerlist = await GetAllBanners(id); //await _bannerRepository.GetBanners(id);
                if (bannerlist != null && bannerlist.Count() > 0)
                    objbanner = bannerlist.ToList();
            }
            catch (Exception ex)
            {
                _log.Error("Error in GetBanners method of BannerController :" + Environment.NewLine + ex.StackTrace);
            }
            //Response
            if (objbanner.Count > 0)
                return Ok(objbanner);
            else
                return BadRequest();
            
        }

        private Task<IEnumerable<Banner>> GetAllBanners(string id)
        {
            var banners = _bannerRepository.GetBanners(id);
            return banners;
        }


        [Route("updatebanner/{id}")]
        //Update
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBanner(string id, [FromBody] Banner objBanner)
        {
            VaildationMessage = "";

            //Flag for Checking whether new entry has being made
            bool IsUpdated = false;

            try
            {
                //Validating Html and if not valid HTMl then  Message is sent back  With Errors 
                VaildationMessage = _validator.HtmlValidationMessage(objBanner.Html);

                // If there is no validation messsge, i.e HTMl is valid and so entry is being made updated into database 
                if (string.IsNullOrEmpty(VaildationMessage))
                {
                    IsUpdated = await _bannerRepository.UpdateBanner(id, objBanner);
                }
            }catch(Exception ex)
            {
                _log.Error("Error in UpdateBanner method of BannerController :" + Environment.NewLine + ex.StackTrace);
            }

            if (!string.IsNullOrEmpty(VaildationMessage))
                return BadRequest(VaildationMessage); // if there is error in html
            else if (IsUpdated)
                return Ok();
            else if (IsUpdated == false)
                return BadRequest(); // if there is no data relevant to ID 
            else
                return new StatusCodeResult((int)HttpStatusCode.ServiceUnavailable);


        }


        [Route("deletebanner/{id}")]
        //Delete
        [HttpDelete]
        public async Task<IActionResult> DeleteBanner(string id)
        {
            bool IsDeleted = false;
            try
            {
                IsDeleted = await _bannerRepository.DeleteBanner(id);
            }
            catch (Exception ex)
            {
                _log.Error("Error in DeleteBanner method of BannerController :" + Environment.NewLine + ex.StackTrace);
            }
            if (IsDeleted)
                return Ok();
            else 
                return BadRequest(); // if there is no data relevant to ID
        }


        [Route("getbannerhtml/{id}")]
        //Get Html
        [HttpGet]
        public async Task<IActionResult> GetBannerHtml(string id)
        {
           string  strBannerHtml = "";
            try
            {
                var bannerHtml = await GetSingleBanner(id) ;
                strBannerHtml = bannerHtml.Html;
            }
            catch (Exception ex)
            {
                _log.Error("Error in GetBannerHtml method of BannerController :" + Environment.NewLine + ex.StackTrace);
            }

            if (!string.IsNullOrEmpty(strBannerHtml))
                return Ok(strBannerHtml);
            else return new StatusCodeResult((int)HttpStatusCode.ServiceUnavailable); 


        }

        private Task<Banner> GetSingleBanner(string id)
        {

            var bannerHtml=  _bannerRepository.GetBannerHtml(id);
            return bannerHtml;
        }


    }
}