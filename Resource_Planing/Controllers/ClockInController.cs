
using Core.Db;
using Core.ViewModels;
using Grpc.Core;
using Logic.IHelpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Drawing;

namespace Resource_Planing.Controllers
{
    public class ClockInController : Controller
    {
        private AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private IUserHelper _userHelper;
        private IAccountHelper _accountHelper;
        private IRotaHelper _rotaHelper;
        private IDropdownHelper _dropdownHelper;
        public ClockInController(AppDbContext context, IUserHelper userHelper, IRotaHelper rotaHelper, IDropdownHelper dropdownHelper, IAccountHelper accountHelper, IWebHostEnvironment env)
        {
            _context = context;
            _userHelper = userHelper;
            _accountHelper = accountHelper;
            _rotaHelper = rotaHelper;
            _dropdownHelper = dropdownHelper;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Shifts = _dropdownHelper.GetShifts();
            ViewBag.State = await _dropdownHelper.GetState();
            ViewBag.Country = await _dropdownHelper.GetCountry();
            var shiftLocationList = _userHelper.GetShiftLocationList();
            return View(shiftLocationList);
        }
        [HttpPost]
        public async Task<JsonResult> CreateShiftLocation(string shiftDetails)
        {
            try
            {
                if (shiftDetails != null)
                {
                    var shiftModel = JsonConvert.DeserializeObject<ShiftLocationViewModel>(shiftDetails);
                    if (shiftModel != null)
                    {
                        var shiftLocation = _userHelper.AddShiftLocation(shiftModel);
                        if (shiftLocation)
                        {
                            return Json(new { isError = false, msg = "Shift Location Added successfully" });
                        }
                        return Json(new { isError = true, msg = "Shift Location already exist" });
                    }
                    return Json(new { isError = true, msg = "Unable to Add Shift Location" });
                }
                return Json(new { isError = true, msg = "Network failure, please try again." });
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
		[HttpPost]
		public JsonResult EditShiftLocation(string shiftDetails)
		{
			if (shiftDetails != null)
			{
				var shiftViewModel = JsonConvert.DeserializeObject<ShiftLocationViewModel>(shiftDetails);
				if (shiftViewModel != null)
				{
					var editTreatment = _userHelper.EditShiftLocation(shiftViewModel);
					if (editTreatment)
					{
						return Json(new { isError = false, msg = "Shift Location Updated successfully", url = "/Treatment/Index" });
					}
				}
				return Json(new { isError = false, msg = "Unable to update Treatment" });
			}
			return Json(new { isError = false, msg = "Network failure, please try again." });
		}
		[HttpPost]
		public JsonResult DeleteShiftLocation(string shiftDetails)
		{
			if (shiftDetails != null)
			{
				var shiftViewModel = JsonConvert.DeserializeObject<ShiftLocationViewModel>(shiftDetails);
				if (shiftViewModel != null)
				{
					var deleteShift = _userHelper.DeleteShiftLocation(shiftViewModel);
					if (deleteShift)
					{
						return Json(new { isError = false, msg = "Treatment Deleted successfully" });

					}
				}
				return Json(new { isError = true, msg = "Unable to Delete Treatment" });
			}
			return Json(new { isError = true, msg = "Network failure, please try again." });
		}
        public JsonResult GetShiftLocationById(int shiftLocationID)
        {
            if (shiftLocationID != 0)
            {
                var treatment = _userHelper.GetShiftLocation(shiftLocationID);
                if (treatment != null)
                {
                    return Json(new { isError = false, data = treatment });
                }
            }
            return Json(new { isError = true, msg = "No Result Found" });

        }
        [HttpGet]
        public IActionResult AddShiftLocation(int shiftId)
        {
            var shift = _userHelper.GetShiftById(shiftId).Result;
            if (shift != null)
            {
                return PartialView(shift);
            }
            return PartialView();
        }
        [HttpGet]
        public JsonResult AddLocationShift(int shiftId, string latitude, string longitude, string radius)
        {
            try
            {
                if (shiftId < 0 || latitude == null || longitude == null)
                {
                    return Json(new { isError = true, msg = "ensure that all details are entered correctly" });
                }
                var shift = _userHelper.GetShiftById(shiftId).Result;
                if (shift == null)
                {
                    return Json(new { isError = true, msg = "ensure that all details are entered correctly" });
                }
                shift.Latitude = Convert.ToDouble(latitude);
                shift.Longitude = Convert.ToDouble(longitude);
                shift.AcceptedRadius = Convert.ToDouble(radius);
                _context.ShiftsLocations.Update(shift);
                _context.SaveChanges();
                return Json(new { isError = false, msg = "Successfully added location coordinate to this branch" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        public IActionResult ClockInView()
        {
            var loggedInUser = _userHelper.FindByUserName(User.Identity.Name);
            ViewBag.LoggedInUser = loggedInUser.Id;
            return View();
        }
        [HttpPost]
       
        [HttpGet] 
        public IActionResult GetProfilePicture(string userId)
        {
            try
            {
                var user = _userHelper.FindById(userId);
                if (user != null && !string.IsNullOrEmpty(user.FaceImageData))
                {
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", user.FaceImageData);
                    var imageBytes = System.IO.File.ReadAllBytes(imagePath);
                    var base64String = Convert.ToBase64String(imageBytes);
                    var imageDataUrl = $"data:image/png;base64,{base64String}";

                    return Json(new { imageDataUrl });
                }

                return Json(new { isError = true, msg = "User not found or has no valid profile picture" });
            }
            catch (Exception ex)
            {
                return Json(new { isError = true, msg = $"Error: {ex.Message}" });
            }
        }

        public IActionResult FaceClockIn(string imageData, string staffId)
        {
            try
            {
                 var user = _userHelper.FindById(staffId);
                if (user != null && user.FaceImageData != null)
                {
                    var base64Data = user.FaceImageData.Split(',')[1];
                    var imageDataBytes = Convert.FromBase64String(base64Data);
                    var referenceImage = Image.FromStream(new MemoryStream(imageDataBytes));
                    // Convert the captured image data to an image
                    var newImageDataBytes = Convert.FromBase64String(imageData.Split(',')[1]);
                    var newCapturedImage = Image.FromStream(new MemoryStream(newImageDataBytes));

                    // Specify the similarity threshold for image comparison
                    float similarityThreshold = 0.9f;
                    // Perform image comparison and decide whether to grant access
                    bool accessGranted = _userHelper.CompareImages(referenceImage, newCapturedImage, similarityThreshold);
                    if (accessGranted)
                    {
                        return Json(new { isError = false, msg = "Access granted" });
                    }
                    else
                    {
                        return Json(new { isError = true, msg = "Access denied. Face recognition failed." });
                    }
                }
                else
                {
                    return Json(new { isError = true, msg = "User not found or has no valid reference image" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { isError = true, msg = $"Error: {ex.Message}" });
            }
        }

        private bool IsBase64String(string s)
        {
            // Check if the string is a valid Base64 string
            try
            {
                Convert.FromBase64String(s);
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
} 
