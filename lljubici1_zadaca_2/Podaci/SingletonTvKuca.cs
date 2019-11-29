using lljubici1_zadaca_2.Composite;
using lljubici1_zadaca_2.Iterator;
using System;
using System.Collections.Generic;

namespace lljubici1_zadaca_2.Podaci
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
                ((Program)RasporedPrograma[program - 1]).IspisZaDan(dan);
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


        public void IspisiTjedniPlanVrsteEmisija(string vrstaEmisije)
        {
            ConcreateIteratorEmisijaZeljeneVrste iterator = KreirajIterator(vrstaEmisije) as ConcreateIteratorEmisijaZeljeneVrste;
            while (!iterator.Gotovo)
            {
                if (iterator.NoviProgram)
                {
                    Console.WriteLine(iterator.TrenutniProgram());
                }
                if (iterator.NoviDan)
                {
                    Console.WriteLine(iterator.TrenutniDan());
                }
                Console.WriteLine(iterator.Trenutni);
                iterator.Sljedeci();
            }
        }


        public IIterator KreirajIterator(string vrstaEmisije)
        {
            return new ConcreateIteratorEmisijaZeljeneVrste(RasporedPrograma, vrstaEmisije);
        }
    }
}