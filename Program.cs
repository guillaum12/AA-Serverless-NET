using Newtonsoft.Json;

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