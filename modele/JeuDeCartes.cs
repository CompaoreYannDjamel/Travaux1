using Enumeration;
using System;
using System.Collections.Generic;

namespace Modele
{
    public class JeuDeCartes : Paquet
    {
        public JeuDeCartes() : base()
        {
            // Ajouter des cartes avec toutes les 52 combinaisons possibles
            foreach (CouleurCarte couleur in Enum.GetValues(typeof(CouleurCarte)))
            {
                foreach (ValeurCarte valeur in Enum.GetValues(typeof(ValeurCarte)))
                {
                    this.AjouterCarte(new Carte(valeur, couleur));
                }
            }
            Melanger();
        }

        // Methode pour distribuer un nombre de cartes à un joueur
        public void Distribuer(int nombreDeCartes, Joueur joueur)
        {
            for (int i = 0; i < nombreDeCartes; i++)
            {
                Carte carte = this.PiocherCarte();
                joueur.AjouterCarte(carte);
            }
        }
    }
}
