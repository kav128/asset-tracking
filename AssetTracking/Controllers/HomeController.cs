using AssetTracking.Models;
using System.Web.Mvc;

namespace AssetTracking.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var config = StorageConfiguration.RetrieveAppSettings();
            if (config.Validate())
            {
                StorageService service = new StorageService(config);
                return View(new AssetViewModel
                {
                    Ready = true,
                    Assets = service.GetDocuments(),
                    Count = service.CountDocuments(),
                    AverageCost = service.AverageCost(),
                    MostExpensiveAssetName = service.MostExpensiveAssetName()
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
            var config = StorageConfiguration.RetrieveAppSettings();
            return View(new ConfigurationViewModel
            {
                Saved = false,
                Configuration = config
            });
        }

        [HttpPost]
        public ActionResult Configure(ConfigurationViewModel viewModel)
        {
            StorageConfiguration.SaveAppSettings(viewModel.Configuration);
            viewModel.Saved = true;
            return View(viewModel);
        }
    }
}