
public class Personne
{
    // Généré avec "prop" + TAB + TAB
    public string Nom { get; set; }
    public int Age { get; set; }

    // Méthode optionnelle
    public string Hello(bool isLowercase)
    {
        // On prépare la chaîne de base
        string message = $"hello {Nom}, you are {Age}";

        // On vérifie le booléen pour retourner la bonne casse
        if (isLowercase)
        {
            return message.ToLower();
        }
        else
        {
            return message.ToUpper();
        }
    }
}