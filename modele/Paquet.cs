using System;
using System.Collections.Generic;
using System.Linq;

namespace Modele
{
    public class Paquet
    {
        protected List<Carte> cartes;

        public Paquet()
        {
            cartes = new List<Carte>();
        }

        public List<Carte> GetCartes()
        {
            return cartes;
        }

        public void Melanger()
        {
            Random random = new Random();
            cartes = cartes.OrderBy(x => random.Next()).ToList();
        }

        public Carte PiocherCarte()
        {
            if (cartes.Count > 0)
            {
                Carte carte = cartes[0];
                cartes.RemoveAt(0);
                return carte;
            }
            return null;
        }

        public void AjouterCarte(Carte carte)
        {
            cartes.Add(carte);
        }

        public void AjouterToutesCartes(Paquet paquet)
        {
            // On doit garder la dernière carte de la pile dépôt sur la table
            Carte derniereCarte = paquet.GetFace();
            paquet.GetCartes().Remove(derniereCarte);
            paquet.Melanger();
            foreach (Carte carte in paquet.GetCartes())
            {
                AjouterCarte(carte);
            }
            paquet.AjouterCarte(derniereCarte);
        }

        public Carte GetFace()
        {
            return cartes.LastOrDefault();
        }
    }
}
