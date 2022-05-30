using AcademicInfo.Config;
using Microsoft.AspNetCore.Mvc;

namespace AcademicInfo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            this.context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var adues = context.Students.FirstOrDefault();
            context.Entry(adues).CurrentValues.SetValues(new
            {
                FirstName = "Zagiga",
                LastName = "Coyos",
                Email = "h@nmail.com",
                Password = "fdanuf",
                City = "zgard",
                Year = "1999"
            });

            context.SaveChanges();
            return Json("Works");
            //return Json(context.Students.Where(item => item.FirstName.StartsWith("A")));
        }
    }
}