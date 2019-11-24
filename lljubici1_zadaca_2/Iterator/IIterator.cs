using lljubici1_zadaca_2.Composite;

namespace lljubici1_zadaca_2.Iterator
{
    public interface IIterator
    {
        IRasporedProgramaComponent First();
        IRasporedProgramaComponent Next();
        IRasporedProgramaComponent CurrentItem { get; }
        bool IsDone { get; }
    }
}
