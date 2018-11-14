using Bannerflow.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bannerflow.IRepository
{
    public interface IBannerRepository
    {

        Task<Banner> AddBanner(Banner objBanner);
        Task<IEnumerable<Banner>> GetBanners(string id);
        Task<bool> UpdateBanner(string id, Banner objBanner);
        Task<bool> DeleteBanner(string id);

        Task<Banner> GetBannerHtml(string id);
       
    }
}
