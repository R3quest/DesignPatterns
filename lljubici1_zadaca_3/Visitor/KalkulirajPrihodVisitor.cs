using lljubici1_zadaca_3.Podaci;

namespace lljubici1_zadaca_3.Visitor
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
