using lljubici1_zadaca_3._Model.Podaci;

namespace lljubici1_zadaca_3._Model.Visitor
{
    public class KalkulirajPrihodVisitor : Visitor
    {
        public int UkupanPrihod { get; private set; }
        public void Visit(VrstaEmisije vrstaEmisije)
        {
            if (vrstaEmisije.ImaReklame)
            {
                UkupanPrihod += vrstaEmisije.TrajanjeReklame;
            }
        }
    }
}
