using lljubici1_zadaca_3.Composite;
using System;
using System.Collections.Generic;

namespace lljubici1_zadaca_3.Memento
{
    public interface IMemento
    {
        string GetName();
        List<IRasporedProgramaComponent> GetState();
        void PrintState();
        DateTime GetDate();
    }
}
