using AssetTracking.Models;
using System.Web.Mvc;

namespace AssetTracking.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var config = DocumentDBConfiguration.RetrieveAppSettings();
            if (config.Validate())
            {
                DocumentDBService service = new DocumentDBService(config);
                return View(new AssetViewModel
                {
                    Ready = true,
                    Assets = service.GetDocuments(),
                    Count = service.CountDocuments(),
                    AverageCost = service.AverageCost(),
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
            var config = DocumentDBConfiguration.RetrieveAppSettings();
            return View(new ConfigurationViewModel
            {
                Saved = false,
                Configuration = config
            });
        }

        [HttpPost]
        public ActionResult Configure(ConfigurationViewModel viewModel)
        {
            DocumentDBConfiguration.SaveAppSettings(viewModel.Configuration);
            viewModel.Saved = true;
            return View(viewModel);
        }
    }
}