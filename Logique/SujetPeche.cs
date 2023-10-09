using Observateur;
using System;
using System.Collections.Generic;

namespace Logique
{
    public abstract class SujetPeche
    {
        protected List<IObservateur> Observateurs = new List<IObservateur>();

        public void AjouterObservateur(IObservateur observateur)
        {
            Observateurs.Add(observateur);
        }

        public abstract void NotifierObservateurs();
    }
}
