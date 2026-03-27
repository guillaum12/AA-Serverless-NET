using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace ResizeHttpTrigger;

public class OuchnaMachabertProject
{
    private readonly ILogger<OuchnaMachabertProject> _logger;

    public OuchnaMachabertProject(ILogger<OuchnaMachabertProject> logger)
    {
        _logger = logger;
    }

    [Function("OuchnaMachabertProject")]
    public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");
        return new OkObjectResult("Welcome to Azure Functions!");
    }
}