using lljubici1_zadaca_2.FactoryMethod;

namespace lljubici1_zadaca_2.Podaci
{
    public class Osoba : Entitet
    {
        public int Id { get; set; }
        public string ImeIPrezime { get; set; }

        public Osoba()
        {

        }

        public Osoba(int id, string imeIPrezime)
        {
            Id = id;
            ImeIPrezime = imeIPrezime;
        }

        public override string ToString()
        {
            return ImeIPrezime;
        }
    }
}