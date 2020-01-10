using lljubici1_zadaca_3.Composite;

namespace lljubici1_zadaca_3.Iterator
{
    public interface IIterator
    {
        IRasporedProgramaComponent Prvi();
        IRasporedProgramaComponent Sljedeci();
        IRasporedProgramaComponent Trenutni { get; }
        bool Gotovo { get; }
    }
}
