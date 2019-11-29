using lljubici1_zadaca_2.Composite;
using lljubici1_zadaca_2.Podaci;
using System;
using System.Collections.Generic;

namespace lljubici1_zadaca_2.Iterator
{
    public class ConcreateIteratorEmisijaZeljeneVrste : IIterator
    {
        //https://www.dofactory.com/net/iterator-design-pattern

        private List<IRasporedProgramaComponent> _kolekcija = new List<IRasporedProgramaComponent>();
        private int _trenutni = 0;
        private int _korak = 1;

        private int _trenutniProgram = 0;
        private int _trenutniDan = 0;

        private List<Tuple<string, int>> listaPrograma = new List<Tuple<string, int>>();
        private List<Tuple<string, int>> listaDana = new List<Tuple<string, int>>();

        public bool NoviDan { get; set; } = true;
        public bool NoviProgram { get; set; } = true;
        // Constructor
        public ConcreateIteratorEmisijaZeljeneVrste(List<IRasporedProgramaComponent> kolekcija, string vrsta)
        {
            foreach (Program program in kolekcija)
            {
                foreach (Dan dan in program.RasporedDani)
                {
                    bool postojiEmisijaUDanu = false;
                    foreach (EmisijePrograma emisija in dan.RasporedEmisijaDana)
                    {
                        if (emisija.Emisija.VrstaEmisije.Vrsta.Equals(vrsta))
                        {
                            _kolekcija.Add(emisija);
                            postojiEmisijaUDanu = true;
                        }
                    }
                    if (_kolekcija.Count != 0 && postojiEmisijaUDanu)
                    {
                        Tuple<string, int> _dan = new Tuple<string, int>(dan.NazivDana, _kolekcija.Count - 1);
                        listaDana.Add(_dan);
                    }
                }
                if (_kolekcija.Count != 0)
                {
                    Tuple<string, int> _program = new Tuple<string, int>(program.NazivPrograma, _kolekcija.Count - 1);
                    listaPrograma.Add(_program);
                }
            }
        }

        // Gets first item
        public IRasporedProgramaComponent Prvi()
        {
            _trenutni = 0;
            _trenutniProgram = 0;
            _trenutniDan = 0;
            NoviDan = true;
            NoviProgram = true;

            return _kolekcija[_trenutni];
        }

        // Gets next item


        private bool KrajDana()
        {
            return _trenutni >= listaDana[_trenutniDan].Item2 + 1;
        }

        private bool KrajPrograma()
        {
            return _trenutni >= listaPrograma[_trenutniProgram].Item2 + 1;
        }

        public IRasporedProgramaComponent Sljedeci()
        {
            _trenutni += _korak;
            if (!Gotovo)
            {
                if (_trenutniProgram < listaPrograma.Count - 1 && KrajPrograma())
                {
                    _trenutniProgram++;
                    NoviProgram = true;
                    //NoviDan = true;
                }
                else
                {
                    NoviProgram = false;
                }
                if (_trenutniDan < listaDana.Count - 1 && KrajDana())
                {
                    _trenutniDan++;
                    NoviDan = true;
                }
                else
                {
                    NoviDan = false;
                }

                return _kolekcija[_trenutni];
            }

            return null;
        }

        // Gets or sets stepsize

        public int Korak
        {
            get { return _korak; }
            set { _korak = value; }
        }

        // Gets current iterator item

        public IRasporedProgramaComponent Trenutni
        {
            get { return _kolekcija[_trenutni] as IRasporedProgramaComponent; }
        }

        // Gets whether iteration is complete

        public bool Gotovo
        {
            get { return _trenutni >= _kolekcija.Count; }
        }

        public string TrenutniProgram()
        {
            return listaPrograma[_trenutniProgram].Item1;
        }
        public string TrenutniDan()
        {
            return listaDana[_trenutniDan].Item1;
        }
    }
}
