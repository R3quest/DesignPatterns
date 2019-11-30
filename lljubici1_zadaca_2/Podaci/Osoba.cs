using lljubici1_zadaca_2.FactoryMethod;
using System.Collections.Generic;

namespace lljubici1_zadaca_2.Podaci
{
    public class Osoba : Entitet
    {
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
            return ImeIPrezime;
        }
    }
}