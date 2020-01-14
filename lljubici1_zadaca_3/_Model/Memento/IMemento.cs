using System;
using System.Collections.Generic;
using lljubici1_zadaca_3._Model.Composite;

namespace lljubici1_zadaca_3._Model.Memento
{
    public interface IMemento
    {
        string GetName();
        List<IRasporedProgramaComponent> GetState();
        void PrintState();
        DateTime GetDate();
    }
}
