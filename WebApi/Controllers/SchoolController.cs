using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using WebApi.Models;

namespace WebApi.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class SchoolController : ControllerBase
{
    private readonly ILogger<SchoolController> _logger;
    private readonly GraphServiceClient _graphClient;

    public SchoolController(ILogger<SchoolController> logger, GraphServiceClient graphClient)
    {
        _logger = logger;
        _graphClient = graphClient;
    }

    [HttpGet(Name = "GetGroups")]
    public async Task<IEnumerable<Group>> Get()
    {
        _logger.LogInformation("Calling Graph to get user group membership");
        var graphUserMemberOf = await _graphClient.Me.MemberOf.GetAsync();
        var userGroupList = graphUserMemberOf?.Value?.OfType<Microsoft.Graph.Models.Group>().Select(g => new Group
        {
            Id = g.Id,
            Name = g.DisplayName,
            Description = g.Description
        }) ?? Enumerable.Empty<Group>();

        return userGroupList; 
    }
}
