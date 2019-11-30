namespace lljubici1_zadaca_2.Observer
{
    interface ISubject
    {
        void Prikaci(IObserver observer);
        void Odvoji(IObserver observer);
        void Obavjesti();
    }
}