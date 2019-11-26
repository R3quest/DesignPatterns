using lljubici1_zadaca_2.FactoryMethod;

namespace lljubici1_zadaca_2.Podaci
{
    public class VrstaEmisije : Entitet
    {
        public int Id { get; set; }
        public string Vrsta { get; set; }
        public bool ImaReklame { get; set; }
        public int TrajanjeReklame { get; set; }

        public VrstaEmisije()
        {

        }

        public VrstaEmisije(int id, string vrsta, int imaReklame, int trajanjeReklame)
        {
            Id = id;
            Vrsta = vrsta;
            //TODO: vidi
            if (imaReklame == 0)
            {
                ImaReklame = false;
            }
            else if (imaReklame == 1)
            {
                ImaReklame = true;
            }
            //ImaReklame = imaReklame;
            TrajanjeReklame = trajanjeReklame;
        }
    }
}
