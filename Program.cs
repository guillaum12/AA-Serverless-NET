using Newtonsoft.Json;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.PixelFormats;

// 1. Création de la variable de type Personne
Personne maPersonne = new Personne();

// 2. Assignation du nom et de l'âge
maPersonne.Nom = "Alice";
maPersonne.Age = 30;

// 3. Affichage à l'écran en appelant la méthode
// Test avec isLowercase = true
Console.WriteLine(maPersonne.Hello(true)); 

// (Optionnel) Test avec isLowercase = false pour voir la différence
// Console.WriteLine(maPersonne.Hello(false));

string jsonString = JsonConvert.SerializeObject(maPersonne);

Console.WriteLine(jsonString);

// IMAGE PROCESSING 


string inputPath = "input/duck_test.jpg";
string outputPath = "output/duck_resized.jpg";

using (Image<Rgba32> image = Image.Load<Rgba32>(inputPath))
{
    // Set desired size
    float resize_factor = 0.1f;
    int newWidth = (int)(image.Width * resize_factor);
    int newHeight = (int)(image.Height * resize_factor);

    image.Mutate(ctx =>
    {
        ctx.Resize(newWidth, newHeight);
    });

    image.Save(outputPath);
}

Console.WriteLine("Resized image saved to: " + outputPath);

