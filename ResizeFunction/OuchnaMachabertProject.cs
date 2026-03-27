using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace ResizeHttpTrigger;

public class OuchnaMachabertProject
{
    private readonly ILogger<OuchnaMachabertProject> _logger;

    public OuchnaMachabertProject(ILogger<OuchnaMachabertProject> logger)
    {
        _logger = logger;
    }

    // On accepte uniquement les requêtes POST
    [Function("OuchnaMachabertProject")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req)
    {
        _logger.LogInformation("Traitement de la requête de redimensionnement d'image.");

        // Récupération et conversion des paramètres w et h depuis la chaîne de requête (Query String)
        // Attention au typage : req.Query renvoie des chaînes de caractères (string), il faut les parser en int.
         if (!int.TryParse(req.Query["w"], out int w) ||
            !int.TryParse(req.Query["h"], out int h))
        {
            return new BadRequestObjectResult("Invalid w or h");
        }

        byte[] targetImageBytes;
        
        using (var msInput = new MemoryStream())
        {
            // Récupère le corps du message (l'image envoyée) en mémoire
            await req.Body.CopyToAsync(msInput);
            msInput.Position = 0; // Réinitialise la position du flux pour le lire depuis le début

            // Charge l'image dans ImageSharp
            using (var image = Image.Load(msInput)) 
            {
                // Effectue la transformation (redimensionnement avec les paramètres w et h)
                image.Mutate(x => x.Resize(w, h));

                // Sauvegarde en mémoire au format JPEG
                using (var msOutput = new MemoryStream())
                {
                    image.SaveAsJpeg(msOutput);
                    targetImageBytes = msOutput.ToArray();
                }
            }
        }

        // Renvoie les octets de la nouvelle image ainsi que le content-type correspondant à une image Jpeg
        return new FileContentResult(targetImageBytes, "image/jpeg");
    }
}