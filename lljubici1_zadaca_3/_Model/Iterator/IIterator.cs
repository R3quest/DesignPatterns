using lljubici1_zadaca_3._Model.Composite;

namespace lljubici1_zadaca_3._Model.Iterator
{
    public interface IIterator
    {
        IRasporedProgramaComponent Prvi();
        IRasporedProgramaComponent Sljedeci();
        IRasporedProgramaComponent Trenutni { get; }
        bool Gotovo { get; }
    }
}
