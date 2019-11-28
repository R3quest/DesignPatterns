using lljubici1_zadaca_2.Composite;

namespace lljubici1_zadaca_2.Iterator
{
    public interface IIterator
    {
        IRasporedProgramaComponent Prvi();
        IRasporedProgramaComponent Sljedeci();
        IRasporedProgramaComponent Trenutni { get; }
        bool Gotovo { get; }
    }
}
