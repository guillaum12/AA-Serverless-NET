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

    // TODO N'accepter que le POST
    [FunctionName("ResizeHttpTrigger")]
    public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
        ILogger log)
    {
        if (!int.TryParse(req.Query["w"], out int w) ||
            !int.TryParse(req.Query["h"], out int h))
        {
            return new BadRequestObjectResult("Invalid w or h");
        }

        byte[]  targetImageBytes;
        using(var  msInput = new MemoryStream())
        {
            // Récupère le corps du message en mémoire
            await req.Body.CopyToAsync(msInput);
            msInput.Position = 0;

            // Charge l'image       
            using (var image = Image.Load(msInput)) 
            {
                // Effectue la transformation
                image.Mutate(x => x.Resize(w, h));

                // Sauvegarde en mémoire               
                using (var msOutput = new MemoryStream())
                {
                    image.SaveAsJpeg(msOutput);
                    targetImageBytes = msOutput.ToArray();
                }
            }
        }
        // Renvoie le contenu avec le content-type correspondant à une image jpeg
        // TODO renvoyer les octets de l'image
        // TODO ... ainsi que le content-type correspondant à une image Jpeg
        return new FileContentResult(targetImageBytes, "image/jpeg");
    }

}