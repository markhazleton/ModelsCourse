using AutoMapper;
using JurisTempus.Data;
using JurisTempus.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace JurisTempus.Controllers
{
  public class HomeController : Controller
  {
    private readonly ILogger<HomeController> _logger;
    private readonly BillingContext _context;
    readonly IMapper _mapper;

    public HomeController(ILogger<HomeController> logger, BillingContext context, IMapper mapper)
    {
      _mapper = mapper;
      _logger = logger;
      _context = context;
    }

    public IActionResult Index()
    {
      var result = _context.Clients
        .Include(c => c.Address)
        .Include(c => c.Cases)
        .ToArray();

      var vms = _mapper.Map<ClientViewModel[]>(result);

      return View(vms);
    }
    [HttpGet("editor/{id:int}")]
    public async Task<IActionResult> ClientEditor(int id)
    {
      var result = await _context.Clients
        .Include(c => c.Address)
        .Where(c => c.Id == id)
        .FirstOrDefaultAsync().ConfigureAwait(true);

      var vms = _mapper.Map<ClientViewModel>(result);
      return View(vms);
    }

    [HttpPost("editor/{id:int}")]
    public async Task<IActionResult> ClientEditor(int id, ClientViewModel model)
    {
      // Save Changes to the Database
      var oldclient = await _context.Clients.Where(c => c.Id == id)
        .Include(c=>c.Address)
        .FirstOrDefaultAsync().ConfigureAwait(true);
      if (oldclient != null)
      {
        // Update the Database
        _mapper.Map(model, oldclient); // Copy Changes

        if (await _context.SaveChangesAsync().ConfigureAwait(true) > 0)
        {
          return RedirectToAction("Index");
        }
      }
      return View();
    }

    [HttpGet("timesheet")]
    public IActionResult Timesheet()
    {
      return View();
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
