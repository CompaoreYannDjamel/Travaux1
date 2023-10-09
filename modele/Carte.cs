using Enumeration;
using System;

namespace Modele
{
    public class Carte
    {
        private ValeurCarte valeur;
        private CouleurCarte couleur;

        // Constructeur
        public Carte(ValeurCarte valeur, CouleurCarte couleur)
        {
            this.valeur = valeur;
            this.couleur = couleur;
        }

        // Méthode pour obtenir la valeur de la carte
        public ValeurCarte GetValeur()
        {
            return valeur;
        }

        // Methode pour obtenir la couleur de la carte
        public CouleurCarte GetCouleur()
        {
            return couleur;
        }

        // Methode pour obtenir le nom complet de la carte (ex: AS de TREFLE)
        public string GetNomCarte()
        {
            return valeur + " de " + couleur;
        }
    }
}
