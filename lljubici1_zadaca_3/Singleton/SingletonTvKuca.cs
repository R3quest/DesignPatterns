using lljubici1_zadaca_3.Composite;
using lljubici1_zadaca_3.Decorator;
using lljubici1_zadaca_3.Iterator;
using lljubici1_zadaca_3.Memento;
using lljubici1_zadaca_3.Podaci;
using lljubici1_zadaca_3.Visitor;
using System;
using System.Collections.Generic;
using System.Linq;

namespace lljubici1_zadaca_3.Singleton
{
    public class SingletonTvKuca : IAbstractCollectionEmisijeOdredeneVrste, IRasporedProgramaComponent
    {
        private static SingletonTvKuca _instanca = new SingletonTvKuca();
        public static SingletonTvKuca Instanca => _instanca;

        List<IRasporedProgramaComponent> RasporedPrograma = new List<IRasporedProgramaComponent>();

        public List<IRasporedProgramaComponent> GetRasporedPrograma()
        {
            return RasporedPrograma;
        }

        public void SetRasporedPrograma(List<IRasporedProgramaComponent> programi)
        {
            RasporedPrograma = programi;
        }

        private SingletonTvKuca()
        {

        }

        public void IspisiProgrameTvKuce()
        {
            foreach (Program rasporedProgramaComponent in RasporedPrograma)
            {
                Console.WriteLine(rasporedProgramaComponent.ToString());
            }
        }

        public void DodajElementRasporeda(IRasporedProgramaComponent elementComposite)
        {
            RasporedPrograma.Add(elementComposite);
        }

        public List<IRasporedProgramaComponent> VratiRasporedEmisija()
        {
            //TODO: druga zadaca
            return this.RasporedPrograma;
        }

        public List<IRasporedProgramaComponent> VratiRaspored()
        {
            return this.RasporedPrograma;
        }

        //public void VratiRasporedEmisija()
        //{
        //    throw new NotImplementedException();
        //}

        public void IspisiRasporedZaDan(int program, int dan)
        {
            try
            {
                //((Program)RasporedPrograma[program - 1]).IspisZaDan(dan);
                var _program = (Program)RasporedPrograma[program - 1];
                var _dan = (Dan)_program.RasporedDani[dan - 1];
                List<IComponent> sveKomponente = new List<IComponent>();
                ConcreateComponentProgramDanEmisija komponenta = new ConcreateComponentProgramDanEmisija(null, null, null);
                sveKomponente.Add(komponenta);
                for (int i = 0; i < _dan.RasporedEmisijaDana.Count; i++)
                {
                    EmisijePrograma emisijaPrograma = (EmisijePrograma)_dan.RasporedEmisijaDana[i];
                    if (i == 0)
                    {
                        komponenta = new ConcreateComponentProgramDanEmisija(emisijaPrograma, _program.NazivPrograma, _dan.NazivDana);
                        sveKomponente.Add(komponenta);
                        continue;
                    }
                    komponenta = new ConcreateComponentProgramDanEmisija(emisijaPrograma, null, null);
                    sveKomponente.Add(komponenta);
                }
                Decorator.Decorator dekorator = new Decorator.Decorator(sveKomponente);
                Console.WriteLine(dekorator.Operacija());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }


        //public void IspisisRasporedZaSveDaneIPrograme()
        //{
        //    foreach (var program in RasporedPrograma)
        //    {
        //        program.VratiRasporedEmisija();
        //    }
        //}

        public int VratiBrojPrograma()
        {
            return RasporedPrograma.Count;
        }

        //public void IspisiRaspored()
        //{
        //    foreach (var program in RasporedPrograma)
        //    {
        //        program.VratiRasporedEmisija();
        //    }
        //}
        //trenutni < count -1



        //while (!iterator.Gotovo)
        //{
        //    if (iterator.NoviProgram)
        //    {
        //        Console.WriteLine(iterator.TrenutniProgram());
        //    }
        //    if (iterator.NoviDan)
        //    {
        //        Console.WriteLine(iterator.TrenutniDan());
        //    }
        //    Console.WriteLine(iterator.Trenutni);
        //    iterator.Sljedeci();
        //}
        public void IspisiTjedniPlanVrsteEmisija(string vrstaEmisije)
        {
            ConcreateIteratorEmisijaZeljeneVrste iterator = KreirajIterator(vrstaEmisije) as ConcreateIteratorEmisijaZeljeneVrste;
            List<IComponent> sveKomponente = new List<IComponent>();
            ConcreateComponentProgramDanEmisija emisijeVrste = new ConcreateComponentProgramDanEmisija(null, null, null);
            sveKomponente.Add(emisijeVrste);
            while (!iterator.Gotovo)
            {
                var emisijaPrograma = ((EmisijePrograma)iterator.Trenutni);
                if (iterator.NoviProgram)
                {
                    emisijeVrste = new ConcreateComponentProgramDanEmisija(emisijaPrograma, iterator.TrenutniProgram(), iterator.TrenutniDan());
                }
                else if (iterator.NoviDan)
                {
                    emisijeVrste = new ConcreateComponentProgramDanEmisija(emisijaPrograma, null, iterator.TrenutniDan());
                }
                else
                {
                    emisijeVrste = new ConcreateComponentProgramDanEmisija(emisijaPrograma, null, null);
                }
                sveKomponente.Add(emisijeVrste);
                iterator.Sljedeci();
            }
            Decorator.Decorator dekorator = new Decorator.Decorator(sveKomponente);
            Console.WriteLine(dekorator.Operacija());
        }

        //TODO: vidi
        public void IspisiTjednogPlana(List<IRasporedProgramaComponent> listaPrograma)
        {
            ConcreateIteratorEmisijaTjednogPlana iterator = new ConcreateIteratorEmisijaTjednogPlana(listaPrograma);
            //ConcreateIteratorEmisijaZeljeneVrste iterator = KreirajIterator(vrstaEmisije) as ConcreateIteratorEmisijaZeljeneVrste;
            List<IComponent> sveKomponente = new List<IComponent>();
            ConcreateComponentProgramDanEmisija emisijeVrste = new ConcreateComponentProgramDanEmisija(null, null, null);
            sveKomponente.Add(emisijeVrste);
            while (!iterator.Gotovo)
            {
                var emisijaPrograma = ((EmisijePrograma)iterator.Trenutni);
                if (iterator.NoviProgram)
                {
                    emisijeVrste = new ConcreateComponentProgramDanEmisija(emisijaPrograma, iterator.TrenutniProgram(), iterator.TrenutniDan());
                }
                else if (iterator.NoviDan)
                {
                    emisijeVrste = new ConcreateComponentProgramDanEmisija(emisijaPrograma, null, iterator.TrenutniDan());
                }
                else
                {
                    emisijeVrste = new ConcreateComponentProgramDanEmisija(emisijaPrograma, null, null);
                }
                sveKomponente.Add(emisijeVrste);
                iterator.Sljedeci();
            }
            Decorator.Decorator dekorator = new Decorator.Decorator(sveKomponente);
            Console.WriteLine(dekorator.Operacija());

            //foreach (EmisijePrograma rasporedProgramaComponent in listaPrograma)
            //{
            //    Console.WriteLine(rasporedProgramaComponent.ToString());
            //}

        }

        public void IspisiPrihodeOdReklama(int program, int dan)
        {
            KalkulirajPrihodVisitor kalkulirajVisitor = new KalkulirajPrihodVisitor();
            //int prihod = 0;
            var _program = (Program)RasporedPrograma[program - 1];
            var _dan = (Dan)_program.RasporedDani[dan - 1];
            List<IComponent> sveKomponente = new List<IComponent>();
            ConcreateComponentPrihodiReklama komponenta = new ConcreateComponentPrihodiReklama(null, null, null);
            sveKomponente.Add(komponenta);
            for (int i = 0; i < _dan.RasporedEmisijaDana.Count; i++)
            {
                EmisijePrograma emisijaPrograma = (EmisijePrograma)_dan.RasporedEmisijaDana[i];

                kalkulirajVisitor.Visit(emisijaPrograma.Emisija.VrstaEmisije);
                if (i == 0)
                {
                    komponenta = new ConcreateComponentPrihodiReklama(emisijaPrograma, _program.NazivPrograma, _dan.NazivDana);
                    sveKomponente.Add(komponenta);
                    continue;
                }
                komponenta = new ConcreateComponentPrihodiReklama(emisijaPrograma, null, null);
                sveKomponente.Add(komponenta);
            }
            komponenta = new ConcreateComponentPrihodiReklama(null, _program.NazivPrograma, null, kalkulirajVisitor.UkupanPrihod);
            sveKomponente.Add(komponenta);
            Decorator.Decorator dekorator = new Decorator.Decorator(sveKomponente);
            Console.WriteLine(dekorator.Operacija());
        }

        public List<Osoba> VratiOsobu(int osobaId)
        {
            List<Osoba> osobe = new List<Osoba>();
            var iterator = new ConcreateIteratorEmisijaTjednogPlana(RasporedPrograma);
            while (!iterator.Gotovo)
            {
                EmisijePrograma emisijaPrograma = (EmisijePrograma)iterator.Trenutni;
                Osoba _osoba = emisijaPrograma.OsobeUloge.Find(ou => ou.Id == osobaId);
                if (_osoba != null)
                {
                    osobe.Add(_osoba);
                }
                iterator.Sljedeci();
            }
            return osobe;
        }
        public List<Uloga> VratiUlogePojedineOsobe(int osobaId)
        {
            List<Uloga> ulogeOsobe = new List<Uloga>();
            var iterator = new ConcreateIteratorEmisijaTjednogPlana(RasporedPrograma);
            while (!iterator.Gotovo)
            {
                EmisijePrograma emisijaPrograma = (EmisijePrograma)iterator.Trenutni;
                Osoba osoba = emisijaPrograma.OsobeUloge.Find(ou => ou.Id == osobaId);
                if (osoba != null)
                {
                    List<Uloga> listaUlogaOsobe = osoba.Uloge;
                    ulogeOsobe.AddRange(listaUlogaOsobe);

                }
                iterator.Sljedeci();
            }

            return ulogeOsobe.Distinct().ToList();
        }
        public IIterator KreirajIterator(string vrstaEmisije)
        {
            return new ConcreateIteratorEmisijaZeljeneVrste(RasporedPrograma, vrstaEmisije);
        }


        public void ObrisiEmisijuNaTemeljuJednoznacnogRednogBroja(int obrisiID, Originator o)
        {
            o.ObrisiEmisiju(obrisiID);
        }

    }
}