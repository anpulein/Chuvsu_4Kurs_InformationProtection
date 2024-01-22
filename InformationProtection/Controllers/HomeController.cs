using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using InformationProtection.Models;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

using lib.Lab;
using lib.Lab.Controllers.Lab1;
using lib.Lab.Models.Enum;
using lib.Lab.Providers;

namespace InformationProtection.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly Worker _worker;
    // private readonly IHostingEnvironment _hostEnvironment;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
        _worker = new Worker();
        // _hostEnvironment = hostEnvironment;
    }

    [HttpGet]
    public IActionResult Index(Labs lab)
    {
        var result = new DataViewModel();

        if (!lab.GetIsInclude())
        {
            return View("Error");
        }
        
        result.SchemaHTML = GetSchema(lab);
        result.LabModel = new LabModel();
        result.LabModel.Type = lab.GetNameEnumLab();
        
        if (HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
        {
            return PartialView("LabCompanentContainer", result);
        }
        
        return View(result);
        
        // return PartialView("LabCompanentContainer", result);
    }
    
    
    [HttpPost("ProcessLabInput")]
    public IActionResult ProcessLabInput([FromBody] Dictionary<string, string> inputData)
    {
        // Извлечение необходимых параметров, включая Type
        string typeCoder = inputData["TypeCoder"];
        string value = inputData["Value"]; // Получение значения из input по имени свойства
        string key = inputData["Key"];
        Labs labType = EnumExtension.GetParseName(inputData["Type"]);

        var lab = _worker.CreateLabInstance(labType, key, value.Length);
        var result = _worker.GetResult(typeCoder, lab, value);

        return Json(result);
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
    
    private string GetSchema(Labs lab) => ResourceProvider.GetJsonSchema(lab.GetNameEnumLab());
}