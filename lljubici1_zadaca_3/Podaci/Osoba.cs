using lljubici1_zadaca_3.FactoryMethod;
using lljubici1_zadaca_3.Observer;
using lljubici1_zadaca_3.Prototype;
using System.Collections.Generic;

namespace lljubici1_zadaca_3.Podaci
{
    public class Osoba : Entitet, ISubject, Kloniraj
    {
        private List<IObserver> observers = new List<IObserver>();
        public int Id { get; set; }
        public string ImeIPrezime { get; set; }

        public List<Uloga> Uloge { get; set; } = new List<Uloga>();

        public Osoba()
        {

        }

        public Osoba(int id, string imeIPrezime)
        {
            Id = id;
            ImeIPrezime = imeIPrezime;
        }

        public Osoba(int id, string imeIPrezime, Uloga uloga)
        {
            Id = id;
            ImeIPrezime = imeIPrezime;
            Uloge.Add(uloga);
        }

        public override string ToString()
        {
            return Id + ": " + ImeIPrezime;
        }

        public Kloniraj Kloniraj()
        {
            Osoba osoba = new Osoba();
            osoba.observers = observers;
            osoba.Id = Id;
            osoba.ImeIPrezime = ImeIPrezime;

            foreach (var uloga in Uloge)
            {
                osoba.Uloge.Add((Uloga)uloga.Kloniraj());
            }

            return osoba;
        }


        public void Prikaci(IObserver observer)
        {
            observers.Add(observer);
        }

        public void Odvoji(IObserver observer)
        {
            observers.Remove(observer);
        }

        public void Obavijesti()
        {
            foreach (var observer in observers)
            {
                observer.Azuriraj(this);
            }
        }

        public void PostaviStanje(Uloga trenutna, Uloga buduca)
        {
            var indexUloge = this.Uloge.FindIndex(u => u.Id == trenutna.Id);
            if (indexUloge != -1) Uloge[indexUloge] = buduca;
            Obavijesti();
        }

    }
}