using Bannerflow.DbModels;
using Bannerflow.IRepository;
using Bannerflow.Models;
using HtmlAgilityPack;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bannerflow.Repository
{
    public class BannerRepository : IBannerRepository
    {

        private readonly ObjectContext _context = null;
       

        public BannerRepository(IOptions<Settings> settings)
        {
            _context = new ObjectContext(settings);
        }


        //Create
        public async Task<Banner> AddBanner(Banner objBanner)
        {
            objBanner.Created = DateTime.Now;
            objBanner.Modified = DateTime.Now;
            await _context.Banners.InsertOneAsync(objBanner);
            return objBanner;
        }

        //Read
        public async Task<IEnumerable<Banner>> GetBanners(string id)
        {
            var Banner = Builders<Banner>.Filter.Eq("Id", id);
            if (string.IsNullOrEmpty(id)) return await _context.Banners.Find(_ => true).ToListAsync();
            else return await _context.Banners.Find(Banner).ToListAsync(); 
        }

        //Update
        public async Task<bool> UpdateBanner(string id, Banner objBanner)
        {
            var filter = Builders<Banner>.Filter.Eq(s => s.Id, id);
            var update = Builders<Banner>.Update
                            .Set(s => s.Html, objBanner.Html)
                            .Set(s => s.Modified, DateTime.Now)
                            .Set(s => s.BannerName, objBanner.BannerName);
            UpdateResult actionResult = await _context.Banners.UpdateOneAsync(filter, update);

            return actionResult.IsAcknowledged && actionResult.ModifiedCount > 0;
        }

        //Delete
        public async Task<bool> DeleteBanner(string id)
        {
            DeleteResult actionResult = await _context.Banners.DeleteOneAsync(
                        Builders<Banner>.Filter.Eq("Id", id));

            return actionResult.IsAcknowledged && actionResult.DeletedCount > 0;
        }

        //GET HTML
        public async Task<Banner> GetBannerHtml(string id)
        {

            var bannerHtml = Builders<Banner>.Filter.Eq("Id", id);// Filter 
            return await _context.Banners.Find(bannerHtml).FirstOrDefaultAsync();
           
        }


        


    }
}
