using lljubici1_zadaca_3.FactoryMethod;

namespace lljubici1_zadaca_3.Podaci
{
    public class Uloga : Entitet
    {
        public int Id { get; set; }
        public string Opis { get; set; }

        public Uloga()
        {

        }

        public Uloga(int id, string opis)
        {
            Id = id;
            Opis = opis;
        }

        public override string ToString()
        {
            return Id + ": " + Opis;
        }
    }
}