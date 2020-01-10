using lljubici1_zadaca_3.Composite;
using lljubici1_zadaca_3.Podaci;
using System;
using System.Collections.Generic;

namespace lljubici1_zadaca_3.Memento
{
    public class Originator
    {
        private static int redniBroj = 1;
        private List<IRasporedProgramaComponent> _stanje = new List<IRasporedProgramaComponent>();

        public Originator(List<IRasporedProgramaComponent> stanje)
        {
            foreach (Program rasporedProgramaComponent in stanje)
            {
                _stanje.AddRange(rasporedProgramaComponent.VratiRasporedEmisija());
            }

            //this._stanje = stanje;
            Console.WriteLine("Originator: Inicijalno stanje: " + stanje);
        }

        // The Originator's business logic may affect its internal stanje.
        // Therefore, the client should backup the stanje before launching
        // methods of the business logic via the save() method.
        public void ObrisiEmisiju(int redniBroj)
        {
            //TODO: izbrisi
            //Console.WriteLine("Originator: I'm doing something important.");
            //this._stanje = this.GenerateRandomString(30);
            //Console.WriteLine($"Originator: and my stanje has changed to: {_stanje}");

            //////
            //List<Program> listaPrograma = new List<Program>();
            //foreach (Program rasporedProgramaComponent in _stanje)
            //{
            //    var a = rasporedProgramaComponent.VratiRasporedEmisija()();
            //}

            //////
            //foreach (Program program in _stanje)
            //{
            //    foreach (Dan dan in program.RasporedDani)
            //    {
            //        var obrisiElement =
            //            dan.RasporedEmisijaDana.SingleOrDefault(o => ((EmisijePrograma)o).RedniBroj == redniBroj);
            //        if (obrisiElement != null)
            //        {
            //            dan.RasporedEmisijaDana.Remove(obrisiElement);
            //            return;
            //        }
            //    }
            //}

            foreach (EmisijePrograma emisijaProgram in _stanje)
            {
                if (emisijaProgram.RedniBroj == redniBroj)
                {
                    _stanje.Remove(emisijaProgram);
                    return;
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
            Console.Write($"Originator: My stanje has changed to: {_stanje}");
        }

    }
}
