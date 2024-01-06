using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
using RunGroopApp.Data;
using RunGroopApp.Interfaces;
using RunGroopApp.Models;
using RunGroopApp.ViewModels;

namespace RunGroopApp.Controllers
{
    public class DashBoardController : Controller
    {
        private readonly IDashBoardRepository _dashBoardRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPhotoService _photoService;

        public DashBoardController(IDashBoardRepository dashBoardRepository, IHttpContextAccessor httpContextAccessor, IPhotoService photoService)
        {
            _dashBoardRepository = dashBoardRepository;
            _httpContextAccessor = httpContextAccessor;
            _photoService = photoService;
        }
        private void MapUserEdit(AppUser user, EditUserDashBoardViewModel userDashBoardVM, ImageUploadResult imageUploadResult)
        {
            user.Id = userDashBoardVM.Id;
            user.Pace = userDashBoardVM.Pace;
            user.Mileage = userDashBoardVM.Mileage;
            user.ProfileImageUrl = imageUploadResult.Url.ToString();
            user.City = userDashBoardVM.City;
            user.State = userDashBoardVM.State;

        }

        public async Task<IActionResult> Index()
        {
            var races = await _dashBoardRepository.GetAllUserRaces();
            var clubs = await _dashBoardRepository.GetAllUserClubs();
            var dashBoardViewModel = new DashBoardViewModel
            {
                Clubs = clubs,
                Races = races
            };
            return View(dashBoardViewModel);

        }
        public async Task<IActionResult> EditUserProfile()
        {
            var currentUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            AppUser currentUser = await _dashBoardRepository.GetUserById(currentUserId);
            if (currentUser == null) return View("Error");
            var editUserDashBoardViewModel = new EditUserDashBoardViewModel
            {
                Id = currentUserId,
                Pace = currentUser.Pace,
                Mileage = currentUser.Mileage,
                ProfileImageUrl = currentUser.ProfileImageUrl,
                City = currentUser.City,
                State = currentUser.State,
            };
            return View(editUserDashBoardViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> EditUserProfile(EditUserDashBoardViewModel editUserVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to update profile");
                return View("EditUserProfile", editUserVM);
            }
            var user = await _dashBoardRepository.GetUserByIdNoTracking(editUserVM.Id);
            if (user.ProfileImageUrl == "" || user.ProfileImageUrl == null)
            {
                var photoResult = await _photoService.AddPhotoAsync(editUserVM.Image);
                MapUserEdit(user, editUserVM, photoResult);
                _dashBoardRepository.Update(user);
                return RedirectToAction("Index");

            }
            else
            {
                try
                {
                    await _photoService.DeletePhotoasync(user.ProfileImageUrl);
                }
                catch (Exception e) {
                    ModelState.AddModelError("", "Could not delete photo");
                    return View(editUserVM);
                }
                var photoResult = await _photoService.AddPhotoAsync(editUserVM.Image);
                MapUserEdit(user, editUserVM, photoResult);
                _dashBoardRepository.Update(user);
                return RedirectToAction("Index");
            }

        }

    }
}
