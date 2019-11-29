using lljubici1_zadaca_2.Composite;
using lljubici1_zadaca_2.Decorator;
using lljubici1_zadaca_2.Iterator;
using lljubici1_zadaca_2.Podaci;
using System;
using System.Collections.Generic;

namespace lljubici1_zadaca_2.Singleton
{
    public class SingletonTvKuca : IAbstractCollectionEmisijeOdredeneVrste, IRasporedProgramaComponent
    {
        private static SingletonTvKuca _instanca = new SingletonTvKuca();
        public static SingletonTvKuca Instanca => _instanca;

        List<IRasporedProgramaComponent> RasporedPrograma = new List<IRasporedProgramaComponent>();

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

        //public void IspisiRaspored()
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
                ConcreateComponent komponenta = new ConcreateComponent(null, _program.NazivPrograma, _dan.NazivDana);
                sveKomponente.Add(komponenta);
                foreach (EmisijePrograma emisijaPrograma in _dan.RasporedEmisijaDana)
                {
                    komponenta = new ConcreateComponent(emisijaPrograma, null, null);
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
        //        program.IspisiRaspored();
        //    }
        //}

        public int VratiBrojPrograma()
        {
            return RasporedPrograma.Count;
        }

        public void IspisiRaspored()
        {
            foreach (var program in RasporedPrograma)
            {
                program.IspisiRaspored();
            }
        }
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
            var prvi = iterator.Prvi() as EmisijePrograma;
            List<IComponent> sveKomponente = new List<IComponent>();
            ConcreateComponent emisijeVrste = new ConcreateComponent(null, null, null);
            sveKomponente.Add(emisijeVrste);
            while (!iterator.Gotovo)
            {
                var emisijaPrograma = ((EmisijePrograma)iterator.Trenutni);
                if (iterator.NoviProgram)
                {
                    emisijeVrste = new ConcreateComponent(emisijaPrograma, iterator.TrenutniProgram(), iterator.TrenutniDan());
                    //Console.WriteLine(iterator.TrenutniProgram());
                }
                else if (iterator.NoviDan)
                {
                    emisijeVrste = new ConcreateComponent(emisijaPrograma, null, iterator.TrenutniDan());
                    //Console.WriteLine(iterator.TrenutniDan());
                }
                else
                {
                    emisijeVrste = new ConcreateComponent(emisijaPrograma, null, null);
                }
                sveKomponente.Add(emisijeVrste);
                //Console.WriteLine(iterator.Trenutni);
                iterator.Sljedeci();
            }
            Decorator.Decorator dekorator = new Decorator.Decorator(sveKomponente);
            Console.WriteLine(dekorator.Operacija());
        }


        public IIterator KreirajIterator(string vrstaEmisije)
        {
            return new ConcreateIteratorEmisijaZeljeneVrste(RasporedPrograma, vrstaEmisije);
        }
    }
}