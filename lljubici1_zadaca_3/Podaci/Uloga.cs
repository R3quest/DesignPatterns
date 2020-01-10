using lljubici1_zadaca_3.FactoryMethod;
using lljubici1_zadaca_3.Prototype;

namespace lljubici1_zadaca_3.Podaci
{
    public class Uloga : Entitet, Kloniraj
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

        public Kloniraj Kloniraj()
        {
            return new Uloga(this.Id, this.Opis);
        }
    }
}