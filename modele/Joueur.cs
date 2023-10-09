using System;
using System.Collections.Generic;
using logique;
using Logique;
using Observateur;

namespace Modele
{
    public class Joueur : IObservateur
    {
        private string nom;
        private string prenom;
        private int id;
        private List<Carte> main;

        private static int nextId = 0;

        public Joueur(string nom, string prenom)
        {
            this.nom = nom;
            this.prenom = prenom;
            this.id = ++nextId;
            this.main = new List<Carte>(); // Initialisation de la main du joueur
        }

        public string GetNomComplet()
        {
            return prenom + " " + nom;
        }

        public int GetId()
        {
            return id;
        }

        public void AjouterCarte(Carte carte)
        {
            main.Add(carte);
        }

        public List<Carte> GetMain()
        {
            return main;
        }

        public Carte PoserCarteOuverte()
        {
            if (main.Count > 0)
                return main[0];
            return null;
        }

        private Carte PoserCarteValide(Carte faceDepot)
        {
            for (int i = 0; i < main.Count; i++)
            {
                if (main[i].GetCouleur() == faceDepot.GetCouleur() ||
                    main[i].GetValeur() == faceDepot.GetValeur())
                {
                    Carte carte = main[i];
                    main.RemoveAt(i);
                    return carte;
                }
            }
            return null;
        }

        private bool ATermine()
        {
            return main.Count == 0;
        }

        public void AfficherMain()
        {
            Console.Write("Main : ");
            if (main.Count == 0)
                Console.WriteLine("Vide");
            else
            {
                for (int i = 0; i < main.Count - 1; i++)
                {
                    Console.Write(main[i].GetNomCarte() + " | ");
                }
                Console.WriteLine(main[main.Count - 1].GetNomCarte());
            }
        }

        public void MettreAJour(SujetPeche sujet)
        {
            Console.WriteLine("Le tour est à " + GetNomComplet());

            Game game = (Game)sujet;
            Paquet depotActuel = game.GetDepot();
            Carte faceDepot = depotActuel.GetFace();

            Carte carteAPoser = PoserCarteValide(faceDepot);
            if (carteAPoser != null)
            {
                depotActuel.AjouterCarte(carteAPoser);
                Console.WriteLine(GetNomComplet() + " a posé la carte : " + carteAPoser.GetNomCarte());

                if (ATermine())
                {
                    Console.WriteLine("Le Jeu est terminé et le joueur " + GetNomComplet() + " a gagné la partie!");
                    Environment.Exit(0);
                }
            }
            else
            {
                Carte cartePiochee = game.PiocherCarte();
                AjouterCarte(cartePiochee);
                Console.WriteLine(GetNomComplet() + " n'a pas de carte valide à poser, et il pioche!");

                if (game.PiocheVide())
                {
                    Console.WriteLine("La pile de pioches est finie, la pile de dépôt va être réutilisée comme pile de pioches!");
                    game.ReutiliserDepot();
                }
            }

            Console.WriteLine("La dernière carte sur la pile de dépôt est : " + depotActuel.GetFace().GetNomCarte());
        }
    }
}
