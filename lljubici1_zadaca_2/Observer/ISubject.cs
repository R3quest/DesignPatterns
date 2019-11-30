namespace lljubici1_zadaca_2.Observer
{
    public interface ISubject
    {
        void Prikaci(IObserver observer);
        void Odvoji(IObserver observer);
        void Obavijesti();
    }
}