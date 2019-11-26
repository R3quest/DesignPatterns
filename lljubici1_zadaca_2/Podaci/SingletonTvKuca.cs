using lljubici1_zadaca_2.Composite;
using System;
using System.Collections.Generic;

namespace lljubici1_zadaca_2.Podaci
{
    public class SingletonTvKuca /*: IRasporedProgramaComponent*/
    {
        private static SingletonTvKuca _instanca = new SingletonTvKuca();
        public static SingletonTvKuca Instanca => _instanca;

        List<IRasporedProgramaComponent> RasporedPrograma = new List<IRasporedProgramaComponent>();

        private SingletonTvKuca()
        {

        }

        public void IspisiProgrameTvKuce()
        {
            foreach (var rasporedProgramaComponent in RasporedPrograma)
            {
                Console.WriteLine(((Program)rasporedProgramaComponent).ToString());
            }
        }

        public void DodajElementRasporeda(IRasporedProgramaComponent elementComposite)
        {
            RasporedPrograma.Add(elementComposite);
        }

        public void IspisiRasporedZaDan(int program, int dan)
        {
            ((Program)RasporedPrograma[program - 1]).IspisZaDan(dan);
        }


        public void IspisisRasporedZaSveDaneIPrograme()
        {
            foreach (var program in RasporedPrograma)
            {
                program.IspisiRaspored();
            }
        }

        public int VratiBrojPrograma()
        {
            return RasporedPrograma.Count;
        }

        //public void IspisiRaspored()
        //{
        //    foreach (var program in RasporedPrograma)
        //    {
        //        program.IspisiRaspored();
        //    }
        //}
    }
}