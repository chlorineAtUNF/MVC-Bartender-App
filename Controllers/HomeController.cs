using Microsoft.AspNetCore.Mvc;
using BartenderApp.Models;

namespace BartenderApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly BartenderLogic _model;

        public HomeController(BartenderLogic model)
        {
            _model = model;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Menu()
        {
            return View(_model.GetMenu());
        }

        [HttpPost]
        public IActionResult PlaceOrder(int cocktailId, string patronName)
        {
            _model.PlaceOrder(cocktailId, patronName);
            return RedirectToAction("Menu");
        }

        [HttpGet]
        public IActionResult OrderQueue()
        {
            return View(_model.GetOrderQueue());
        }

        [HttpPost]
        public IActionResult PrepareOrder(int orderId)
        {
            _model.SetOrderPrepared(orderId);
            return RedirectToAction("OrderQueue");
        }
    }
}