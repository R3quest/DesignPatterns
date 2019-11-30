using lljubici1_zadaca_2.Observer;
using System.Collections.Generic;

namespace lljubici1_zadaca_2.Podaci
{
    public class OsobaUloga
    {
        private List<IObserver> observers = new List<IObserver>();

        public Osoba Osoba { get; set; } = new Osoba();
        public Uloga Uloga { get; set; } = new Uloga();

        public OsobaUloga()
        {

        }

        public OsobaUloga(Osoba osoba, Uloga uloga)
        {
            Osoba = osoba;
            Uloga = uloga;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
