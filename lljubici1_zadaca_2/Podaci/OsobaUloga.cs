namespace lljubici1_zadaca_2.Podaci
{
    public class OsobaUloga
    {
        public int IdOsoba { get; set; }
        public int IdUloga { get; set; }

        public OsobaUloga()
        {

        }

        public OsobaUloga(int idOsoba, int idUloga)
        {
            IdOsoba = idOsoba;
            IdUloga = idUloga;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
