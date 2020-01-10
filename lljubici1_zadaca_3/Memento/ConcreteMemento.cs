using lljubici1_zadaca_3.Composite;
using lljubici1_zadaca_3.Podaci;
using System;
using System.Collections.Generic;

namespace lljubici1_zadaca_3.Memento
{
    public class ConcreteMemento : IMemento
    {
        private List<IRasporedProgramaComponent> _stanje = new List<IRasporedProgramaComponent>();
        private DateTime _vrijemeDatum;
        public int RedniBrojPohrane { get; set; }


        public ConcreteMemento(List<IRasporedProgramaComponent> raspored)
        {
            //KLONIRAJ
            foreach (EmisijePrograma emisijePrograma in raspored)
            {
                _stanje.Add((EmisijePrograma)emisijePrograma.Kloniraj());
            }


            //_stanje = raspored;
            _vrijemeDatum = DateTime.Now;
        }

        public string GetName()
        {

            return _vrijemeDatum.ToString("dd.MM.yyyy. HH:mm:ss") + "\nPohrana (stanje): " + RedniBrojPohrane + "\n";
        }

        public List<IRasporedProgramaComponent> GetState()
        {
            return _stanje;
        }

        public void PrintState()
        {
            Console.WriteLine("Podaci::");
            Singleton.SingletonTvKuca.Instanca.IspisiTjednogPlana(_stanje);
        }


        public DateTime GetDate()
        {
            return _vrijemeDatum;
        }
    }
}
