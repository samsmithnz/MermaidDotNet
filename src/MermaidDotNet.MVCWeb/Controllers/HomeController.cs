using MermaidDotNet.Models;
using MermaidDotNet.MVCWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MermaidDotNet.MVCWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            string direction = "LR";
            List<Node> nodes = new()
            {
                new("node1", "This is node 1"),
                new("node2", "This is node 2", Node.ShapeType.Hexagon),
                new("node3", "This is node 3", Node.ShapeType.Rounded)
            };
                    List<Link> links = new()
            {
                new Link("node1", "node2", "12s"),
                new Link("node1", "node3", "3mins")
            };
            Flowchart flowchart = new(direction, nodes, links);
            string graph = flowchart.CalculateFlowchart();

            return View(model: graph);
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
