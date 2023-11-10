
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

        //public ActionResult CompareImages(string imageData, string staffId)
        //{
        //    var user = _userHelper.FindById(staffId);
        //    // Load the reference image (the image you registered)
        //    // var referenceImagePath = Server.MapPath("~/Content/reference.jpg")
        //    //var referenceImagePath = Path.Combine(_env.ContentRootPath, "wwwroot", "Content", "reference.jpg");
        //    var referenceImagePath = _context.ApplicationUser.Where(h => h.Id == staffId && h.FaceImageData == imageData).FirstOrDefault();
        //    if (referenceImagePath == null)
        //    {
        //        return Json(new { isError = true, msg = "Image compare failed, make sure there is image saved for comparison" });
        //    }
        //    var referenceImage = Image.FromFile(referenceImagePath.FaceImageData);
           
        //    // Convert the captured image data to an image
        //    var imageDataBytes = Convert.FromBase64String(imageData.Split(',')[1]);
        //    var capturedImage = Image.FromStream(new MemoryStream(imageDataBytes));

        //    // Perform image comparison and decide whether to grant access
        //    bool accessGranted = _accountHelper.CompareImages(referenceImage, capturedImage);

        //    if (accessGranted)
        //    {
        //        return Json(new { isError = false, msg = "Successful" }); 
        //    }
        //    else
        //    {
        //        return Json(new { isError = true, msg = "Something went wrong, try again" });
        //    }
        //}

        public ActionResult FaceClockIn(string imageData, string staffId)
        {
            // Find the user by staffId
            var user = _userHelper.FindById(staffId);

            // Check if the user is found and has a valid reference image path
            if (user != null && !string.IsNullOrEmpty(user.FaceImageData))
            {
                try
                {
                    // Load the reference image (the image you registered)
                    var referenceImage = Image.FromFile(user.FaceImageData);

                    // Convert the captured image data to an image
                    var imageDataBytes = Convert.FromBase64String(imageData.Split(',')[1]);
                    var capturedImage = Image.FromStream(new MemoryStream(imageDataBytes));

                    // Perform image comparison and decide whether to grant access
                    bool accessGranted = _userHelper.CompareImages(referenceImage, capturedImage);

                    if (accessGranted)
                    {
                        return Json(new { isError = false, msg = "Successful" });
                    }
                    else
                    {
                        return Json(new { isError = true, msg = "Something went wrong, try again" });
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions (e.g., if there's an issue loading images)
                    return Content($"Error: {ex.Message}");
                }
            }
            else
            {
                // Handle the case when the user is not found or has no valid reference image
                return Content("User not found or has no valid reference image");
            }
        }


    }
} 
