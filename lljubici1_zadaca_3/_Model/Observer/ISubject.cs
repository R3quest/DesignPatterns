using lljubici1_zadaca_3._Model.Podaci;

namespace lljubici1_zadaca_3._Model.Observer
{
    public interface ISubject
    {
        void Prikaci(IObserver observer);
        void Odvoji(IObserver observer);
        void Obavijesti();
        void PostaviStanje(Uloga trenutna, Uloga buduca);
    }
}