using lljubici1_zadaca_3._Model.Composite;
using lljubici1_zadaca_3._Model.Decorator;
using lljubici1_zadaca_3._Model.Iterator;
using lljubici1_zadaca_3._Model.Podaci;
using System;
using System.Collections.Generic;

namespace lljubici1_zadaca_3._Model.Singleton
{
    public class SingletonTvKuca
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



        public List<IRasporedProgramaComponent> VratiRaspored()
        {
            return this.RasporedPrograma;
        }

        //public void VratiRasporedEmisija()
        //{
        //    throw new NotImplementedException();
        //}




        //public void IspisisRasporedZaSveDaneIPrograme()
        //{
        //    foreach (var program in RasporedPrograma)
        //    {
        //        program.VratiRasporedEmisija();
        //    }
        //}



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


        //TODO: vidi
        public void IspisiTjednogPlana(List<IRasporedProgramaComponent> listaPrograma)
        {
            ConcreateIteratorEmisijaTjednogPlana iterator = new ConcreateIteratorEmisijaTjednogPlana(listaPrograma);
            //ConcreateIteratorEmisijaZeljeneVrste iterator = KreirajIterator(vrstaEmisije) as ConcreateIteratorEmisijaZeljeneVrste;
            List<IComponent> sveKomponente = new List<IComponent>();
            ConcreateComponentProgramDanEmisija emisija = new ConcreateComponentProgramDanEmisija(null, null, null);
            sveKomponente.Add(emisija);
            while (!iterator.Gotovo)
            {
                var emisijaPrograma = ((EmisijePrograma)iterator.Trenutni);
                if (iterator.NoviProgram)
                {
                    emisija = new ConcreateComponentProgramDanEmisija(emisijaPrograma, iterator.TrenutniProgram(), iterator.TrenutniDan());
                }
                else if (iterator.NoviDan)
                {
                    emisija = new ConcreateComponentProgramDanEmisija(emisijaPrograma, null, iterator.TrenutniDan());
                }
                else
                {
                    emisija = new ConcreateComponentProgramDanEmisija(emisijaPrograma, null, null);
                }
                sveKomponente.Add(emisija);
                iterator.Sljedeci();
            }
            Decorator.Decorator dekorator = new Decorator.Decorator(sveKomponente);
            Console.WriteLine(dekorator.Operacija());

            //foreach (EmisijePrograma rasporedProgramaComponent in listaPrograma)
            //{
            //    Console.WriteLine(rasporedProgramaComponent.ToString());
            //}

        }










    }
}