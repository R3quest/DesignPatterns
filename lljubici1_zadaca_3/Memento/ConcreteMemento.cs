using lljubici1_zadaca_3.Composite;
using System;
using System.Collections.Generic;

namespace lljubici1_zadaca_3.Memento
{
    public class ConcreteMemento : IMemento
    {
        private List<IRasporedProgramaComponent> _stanje;
        private DateTime _vrijemeDatum;
        public int RedniBrojPohrane { get; set; }


        public ConcreteMemento(List<IRasporedProgramaComponent> raspored)
        {
            //KLONIRAJ
            _stanje = raspored;
            _vrijemeDatum = DateTime.Now;
        }

        public string GetName()
        {
            //TODO ISPISI CIJELI RASPORED
            return _vrijemeDatum.ToString("dd.MM.yyyy. HH:mm:ss") + "\nPohrana: " + RedniBrojPohrane + "\nPodaci::\n";
        }

        public List<IRasporedProgramaComponent> GetState()
        {
            return _stanje;
        }

        public void PrintState()
        {
            Singleton.SingletonTvKuca.Instanca.IspisiTjednogPlana(_stanje);
        }


        public DateTime GetDate()
        {
            return _vrijemeDatum;
        }
    }
}
