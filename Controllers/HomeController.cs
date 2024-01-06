using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RunGroopApp.Helpers;
using RunGroopApp.Interfaces;
using RunGroopApp.Models;
using RunGroopApp.ViewModels;
using System.Diagnostics;
using System.Globalization;
using System.Net;

namespace RunGroopApp.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IClubRepository _clubRepository;
		private readonly IConfiguration _config;

        public HomeController(ILogger<HomeController> logger, IClubRepository clubRepository, IConfiguration config)
        {
            _logger = logger;
            _clubRepository = clubRepository;
			_config = config;
        }

        public async Task<IActionResult> Index()
		{
			var ipInfo = new IpInfo();
			var homeViewModel = new HomeViewModel();
			try
			{
			string url = "https://ipinfo.io?token=" + _config.GetValue<string>("IPInfoToken");
				var info = new WebClient().DownloadString(url);
				ipInfo = JsonConvert.DeserializeObject<IpInfo>(info);
				RegionInfo regionInfo = new RegionInfo(ipInfo.Country);
				ipInfo.Country = regionInfo.EnglishName;
				homeViewModel.City = ipInfo.City;
				homeViewModel.State = ipInfo.Region;
                if (homeViewModel.City != null)
                {
                    homeViewModel.Clubs = await _clubRepository.GetByCity(homeViewModel.City);
				}
				else
				{
					homeViewModel.Clubs = null;
				}
                return View(homeViewModel);
            }
            catch (Exception)
            {
                homeViewModel.Clubs = null;
            }
        
			return View(homeViewModel);
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
