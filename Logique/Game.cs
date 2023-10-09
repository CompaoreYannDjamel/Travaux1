using Logique;
using Modele;
using Observateur;

namespace logique
{

    class Game : SujetPeche
    {
        private JeuDeCartes pioches;
        private Paquet depot;
        private int nbrJoueurs;
        private Random random;

        private int indexJoueur;

        public Game()
        {
            pioches = new JeuDeCartes();
            depot = new Paquet();
            nbrJoueurs = 0;
            random = new Random();
        }

        public Paquet GetDepot()
        {
            return depot;
        }

        private int EntrerNbrJoueurs()
        {
            Console.WriteLine("Donner le nombre de joueurs : ");
            string reponse;
            int entier;
            do
            {
                Console.Write("> ");
                reponse = Console.ReadLine();
                if (int.TryParse(reponse, out entier) && (entier >= 2 && entier <= 4))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Entrer un nombre valide (2, 3 ou 4)");
                }
            } while (true);
            return entier;
        }

        private void InscrirJoueurs()
        {
            for (int i = 0; i < nbrJoueurs; i++)
            {
                string nom, prenom;
                Console.WriteLine("Entrer les infos du joueur " + (i + 1) + " :");
                Console.Write("Nom> ");
                nom = Console.ReadLine();
                Console.Write("Prénom> ");
                prenom = Console.ReadLine();
                AjouterObservateur(new Joueur(nom, prenom));
            }
        }

        private void DistribuerCartesAuJoueurs(int nbrCartes)
        {
            for (int i = 0; i < nbrJoueurs; i++)
            {
                pioches.Distribuer(nbrCartes, (Joueur)Observateurs[i]);
            }
        }

        public void LancerJeu()
        {
            Console.WriteLine("************** Jeu de Pêche|Pioche **************");
            /* Commencer le jeu */
            // introduire le nombre de joueurs
            nbrJoueurs = EntrerNbrJoueurs();
            // introduire les informations des joueurs
            InscrirJoueurs();
            Console.WriteLine("La partie commence...");
            // choisir aléatoirement le nombre de cartes à distribuer (<=8)
            int nbrCartes = random.Next(1, 9);
            // distribuer les cartes au joueurs
            DistribuerCartesAuJoueurs(nbrCartes);
            // choisir aléatoirement le joueur qui commence
            indexJoueur = random.Next(nbrJoueurs);
            Joueur premierJoueur = JoueurCourrant();
            Console.WriteLine("Le joueur " + premierJoueur.GetNomComplet() + " commence...");
            depot.AjouterCarte(premierJoueur.PoserCarteOuverte());
            Console.WriteLine("et dépose la carte " + depot.GetFace().GetNomCarte() + " sur la pile de dépôt");
            // Notifier les observateurs lorsque le jeu commence
            NotifierObservateurs();
        }

        public Joueur JoueurCourrant()
        {
            return (Joueur)Observateurs[indexJoueur];
        }

        public Joueur JoueurSuivant()
        {
            indexJoueur = (indexJoueur + 1) % nbrJoueurs;
            return (Joueur)Observateurs[indexJoueur];
        }

        public Carte PiocherCarte()
        {
            return pioches.PiocherCarte();
        }

        public bool PiocheVide()
        {
            return pioches.GetCartes().Count == 0;
        }

        public void ReutiliserDepot()
        {
            pioches.AjouterToutesCartes(depot);
        }

        public override void NotifierObservateurs()
        {
            int delaiEntreTour = 2000;
            while (true)
            {
                IObservateur observateur = JoueurSuivant();
                observateur.MettreAJour(this);
                // retarder les traitements en utilisant Task.Delay
                System.Threading.Tasks.Task.Delay(delaiEntreTour).Wait();
            }
        }
    }
}
