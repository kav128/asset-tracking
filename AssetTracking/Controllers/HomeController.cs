using AssetTracking.Models;
using System.Web.Mvc;

namespace AssetTracking.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var config = MongoDBConfiguration.RetrieveAppSettings();
            if (config.Validate())
            {
                MongoDBService service = new MongoDBService(config);
                return View(new AssetViewModel
                {
                    Ready = true,
                    Assets = service.GetDocuments(),
                    Count = service.CountDocuments(),
                    MostExpensiveCost = service.MostExpensiveCost(),
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
            var config = MongoDBConfiguration.RetrieveAppSettings();
            return View(new ConfigurationViewModel
            {
                Saved = false,
                Configuration = config
            });
        }

        [HttpPost]
        public ActionResult Configure(ConfigurationViewModel viewModel)
        {
            MongoDBConfiguration.SaveAppSettings(viewModel.Configuration);
            viewModel.Saved = true;
            return View(viewModel);
        }
    }
}