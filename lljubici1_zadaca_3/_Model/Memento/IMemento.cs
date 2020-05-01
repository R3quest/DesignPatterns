using lljubici1_zadaca_3._Model.Composite;
using System;
using System.Collections.Generic;

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
