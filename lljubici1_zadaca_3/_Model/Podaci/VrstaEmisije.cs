using lljubici1_zadaca_3._Model.FactoryMethod;
using lljubici1_zadaca_3._Model.Prototype;
using lljubici1_zadaca_3._Model.Visitor;

namespace lljubici1_zadaca_3._Model.Podaci
{
    public class VrstaEmisije : Entitet, Visitable, Kloniraj
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

        public Kloniraj Kloniraj()
        {
            VrstaEmisije ve = new VrstaEmisije();
            ve.Id = Id;
            ve.ImaReklame = ImaReklame;
            ve.TrajanjeReklame = TrajanjeReklame;
            ve.Vrsta = Vrsta;
            return ve;
        }


        public void Accept(Visitor.Visitor visitor)
        {
            visitor.Visit(this);
        }

        public VrstaEmisije(int id, string vrsta, int imaReklame, int trajanjeReklame)
        {
            Id = id;
            Vrsta = vrsta;
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
