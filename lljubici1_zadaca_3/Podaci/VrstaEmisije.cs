using lljubici1_zadaca_3.FactoryMethod;
using lljubici1_zadaca_3.Visitor;

namespace lljubici1_zadaca_3.Podaci
{
    public class VrstaEmisije : Entitet, Visitable
    {
        public int Id { get; set; }
        public string Vrsta { get; set; }
        public bool ImaReklame { get; set; }
        public int TrajanjeReklame { get; set; }

        public VrstaEmisije()
        {

        }

        public override string ToString()
        {
            return Id + " - " + Vrsta;
        }

        public void Accept(Visitor.Visitor visitor)
        {
            visitor.Visit(this);
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
