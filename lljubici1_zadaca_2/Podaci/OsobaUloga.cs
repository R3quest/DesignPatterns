using lljubici1_zadaca_2.Observer;
using System;

namespace lljubici1_zadaca_2.Podaci
{
    public class OsobaUloga : IObserver
    {
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

        public void Azuriraj(ISubject subject)
        {
            //TODO: VIDI
            Console.WriteLine("AZURIRANO");
        }
    }
}
