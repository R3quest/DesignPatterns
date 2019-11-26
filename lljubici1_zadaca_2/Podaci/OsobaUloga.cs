namespace lljubici1_zadaca_2.Podaci
{
    public class OsobaUloga
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
    }
}
