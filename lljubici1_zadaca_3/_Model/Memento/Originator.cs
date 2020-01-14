using System;
using System.Collections.Generic;
using System.Linq;
using lljubici1_zadaca_3._Model.Composite;
using lljubici1_zadaca_3._Model.Podaci;
using lljubici1_zadaca_3._Model.Singleton;

namespace lljubici1_zadaca_3._Model.Memento
{
    public class Originator
    {
        private static int redniBroj = 1;
        private List<IRasporedProgramaComponent> _stanje = new List<IRasporedProgramaComponent>();

        public Originator(List<IRasporedProgramaComponent> stanje)
        {
            //foreach (Program program in stanje)
            //{
            //    _stanje.Add((Program)program.Kloniraj());
            //}
            this._stanje = stanje;
        }

        // The Originator's business logic may affect its internal stanje.
        // Therefore, the client should backup the stanje before launching
        // methods of the business logic via the save() method.


        //foreach (EmisijePrograma emisijaProgram in _stanje)
        //{
        //    if (emisijaProgram.RedniBroj == redniBroj)
        //    {
        //        _stanje.Remove(emisijaProgram);
        //        return;
        //    }
        //}

        public void ObrisiEmisiju(int redniBroj)
        {
            foreach (Program program in _stanje)
            {
                foreach (Dan dan in program.RasporedDani)
                {
                    var obrisiElement =
                        dan.RasporedEmisijaDana.SingleOrDefault(o => ((EmisijePrograma)o).RedniBroj == redniBroj);
                    if (obrisiElement != null)
                    {
                        dan.RasporedEmisijaDana.Remove(obrisiElement);
                        return;
                    }
                }
            }
            Console.WriteLine("Neispravan broj!");
        }

        // Saves the current stanje inside a memento.
        public IMemento Save()
        {
            //ConcreteMemento cm = new ConcreteMemento(this._stanje);
            ConcreteMemento cm = new ConcreteMemento(_stanje);
            cm.RedniBrojPohrane = redniBroj++;
            return cm;
        }

        //Restores the Originator's stanje from a memento object.
        public void Restore(IMemento memento)
        {
            if (!(memento is ConcreteMemento))
            {
                throw new Exception("Unknown memento class " + memento.ToString());
            }

            this._stanje = memento.GetState();
            SingletonTvKuca.Instanca.SetRasporedPrograma(this._stanje);
            Console.Write($"Originator: My stanje has changed to: {_stanje}");
        }

    }
}
