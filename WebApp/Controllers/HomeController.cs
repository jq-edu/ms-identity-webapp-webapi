using System.Diagnostics;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using Microsoft.Identity.Abstractions;
using Microsoft.Identity.Web;
using WebApp.Models;

namespace ms_identity_webapp_multitenant.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IConfiguration _configuration;
    private readonly GraphServiceClient _graphClient;
    private IDownstreamApi _downstreamApi;
    private readonly ITokenAcquisition _tokenAcquisition;

    public HomeController(ILogger<HomeController> logger, IConfiguration configuration, GraphServiceClient graphClient, ITokenAcquisition tokenAcquisition, IDownstreamApi downstreamApi)
    {
        _logger = logger;
        _configuration = configuration;
        _graphClient = graphClient;
        _tokenAcquisition = tokenAcquisition;
        _downstreamApi = downstreamApi;
    }

    [AuthorizeForScopes(Scopes = ["User.Read"])]
    public async Task<IActionResult> Index()
    {
        _logger.LogInformation("Getting role from user claims");
        var roles = HttpContext.User.Claims.Where(c => c.Type == "roles").ToList();
        ViewData["UserRoles"] = roles;

        _logger.LogInformation("Getting logged user info from Graph");
        var userInfo = await _graphClient.Me.GetAsync();
        var serializerOption = new JsonSerializerOptions 
        { 
            WriteIndented = true,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };
        ViewData["UserInfo"] = JsonSerializer.Serialize(userInfo, serializerOption);

        _logger.LogInformation("Getting user token for api calls");
        
        _logger.LogInformation("Token for Graph API");
        var graphScopes = _configuration.GetSection("GraphApi:Scopes").Get<string[]>();
        var graphToken = await _tokenAcquisition.GetAccessTokenForUserAsync(graphScopes);
        ViewData["UserGraphToken"] = graphToken;
        
        _logger.LogInformation("Token for School API");
        var schoolScope = _configuration.GetSection("SchoolApi:Scopes").Get<string[]>();
        var schoolToken = await _tokenAcquisition.GetAccessTokenForUserAsync(schoolScope);
        ViewData["UserSchoolToken"] = schoolToken;

        _logger.LogInformation("Calling SchoolAPI");
        var groups = await _downstreamApi.GetForUserAsync<IEnumerable<Group>>("SchoolApi");
        ViewData["SchoolApiData"] = groups;

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
