using System;
using System.Collections.Generic;

namespace lljubici1_zadaca_3.Memento
{
    public class Caretaker
    {
        private List<IMemento> _mementos = new List<IMemento>();

        private Originator _originator = null;

        public Caretaker(Originator originator)
        {
            this._originator = originator;
        }

        public void Backup()
        {
            Console.WriteLine("Caretaker: Spremanje stanja originatora...");
            this._mementos.Add(this._originator.Save());
        }

        public void Restore(int stanje)
        {
            if (this._mementos.Count == 0)
            {
                return;
            }
            //TODO: pazi na index
            var memento = this._mementos[stanje - 1];
            //this._mementos.Remove(memento);
            Console.WriteLine("Caretaker: Vračanje stanja na: " + memento.GetName());

            try
            {
                this._originator.Restore(memento);
            }
            catch (Exception)
            {
                this.Restore(stanje);
            }
        }

        public void ShowHistory()
        {
            if (_mementos.Count != 0)
            {
                Console.WriteLine("Lista mementa:");
            }
            foreach (var memento in this._mementos)
            {
                Console.Write(memento.GetName());
                memento.PrintState();
            }
        }

        public void ShowHistoryDates()
        {
            if (_mementos.Count != 0)
            {
                Console.WriteLine("Lista mementa:");
            }

            foreach (var memento in this._mementos)
            {
                Console.WriteLine(memento.GetName());
            }
        }

        public int GetListCount()
        {
            return _mementos.Count;
        }

    }
}
