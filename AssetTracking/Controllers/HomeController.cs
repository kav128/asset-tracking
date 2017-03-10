using AssetTracking.Models;
using System.Web.Mvc;

namespace AssetTracking.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var searchConfig = SearchConfiguration.RetrieveAppSettings();
            var cacheConfig = CacheConfiguration.RetrieveAppSettings();
            if (searchConfig.Validate() && cacheConfig.Validate())
            {
                SearchService service = new SearchService(searchConfig, cacheConfig);
                return View(new AssetViewModel
                {
                    Ready = true,
                    Assets = service.GetDocuments(),
                    Count = service.CountDocuments(),
                    CacheLastUpdated = service.CacheLastUpdated(),
                    LatestAssetName = service.GetLatestAssetName()
                });
            }
            else
            {
                return View(new AssetViewModel
                {
                    Ready = false
                });
            }
        }

        public ActionResult Configure()
        {
            var searchConfig = SearchConfiguration.RetrieveAppSettings();
            var cacheConfig = CacheConfiguration.RetrieveAppSettings();
            return View(new ConfigurationViewModel
            {
                Saved = false,
                SearchConfiguration = searchConfig,
                CacheConfiguration = cacheConfig
            });
        }

        [HttpPost]
        public ActionResult Configure(ConfigurationViewModel viewModel)
        {
            SearchConfiguration.SaveAppSettings(viewModel.SearchConfiguration);
            CacheConfiguration.SaveAppSettings(viewModel.CacheConfiguration);
            viewModel.Saved = true;
            return View(viewModel);
        }
    }
}