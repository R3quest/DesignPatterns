using System;
using System.Collections.Generic;
using System.Linq;

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
            Console.WriteLine("\nCaretaker: Saving Originator's state...");
            this._mementos.Add(this._originator.Save());
        }

        public void Undo()
        {
            if (this._mementos.Count == 0)
            {
                return;
            }

            var memento = this._mementos.Last();
            this._mementos.Remove(memento);

            Console.WriteLine("Caretaker: Restoring state to: " + memento.GetName());

            //try
            //{
            //    this._originator.Restore(memento);
            //}
            //catch (Exception)
            //{
            //    this.Undo();
            //}
        }

        public void ShowHistory()
        {
            Console.WriteLine("Lista mementa:");

            foreach (var memento in this._mementos)
            {
                Console.Write(memento.GetName());
                memento.PrintState();
            }
        }


    }
}
