using lljubici1_zadaca_3.Composite;
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
            foreach (Program program in raspored)
            {
                _stanje.Add((Program)program.Kloniraj());
            }
            //_stanje = raspored;
            _vrijemeDatum = DateTime.Now;
        }

        public string GetName()
        {
            return _vrijemeDatum.ToString("dd.MM.yyyy. HH:mm:ss") + ",\tPohrana (stanje): " + RedniBrojPohrane;
        }

        public List<IRasporedProgramaComponent> GetState()
        {
            return _stanje;
        }

        public void PrintState()
        {
            Console.WriteLine("\nPodaci::");
            Singleton.SingletonTvKuca.Instanca.IspisiTjednogPlana(_stanje);
        }


        public DateTime GetDate()
        {
            return _vrijemeDatum;
        }
    }
}
