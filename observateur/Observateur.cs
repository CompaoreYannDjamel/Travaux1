using System;
using Logique;

namespace Observateur
{
    public interface IObservateur
    {
        void MettreAJour(SujetPeche sujet);
    }
}
