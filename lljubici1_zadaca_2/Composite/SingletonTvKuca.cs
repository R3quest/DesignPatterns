using System.Collections.Generic;

namespace lljubici1_zadaca_2.Composite
{
    public class SingletonTvKuca : IRasporedProgramaComponent
    {
        private static SingletonTvKuca _instanca = new SingletonTvKuca();
        public static SingletonTvKuca Instanca => _instanca;
        public List<Program> Programi { get; set; } = new List<Program>();

        public List<IRasporedProgramaComponent> RasporedPrograma { get; set; } = new List<IRasporedProgramaComponent>();

        private SingletonTvKuca()
        {

        }
        public void DodajProgram(Program program)
        {
            Programi.Add(program);
        }

        public void DodajElementRasporeda(IRasporedProgramaComponent elementComposite)
        {
            RasporedPrograma.Add(elementComposite);
        }

        public void IspisiRaspored()
        {
            foreach (var program in RasporedPrograma)
            {
                program.IspisiRaspored();
            }
        }
    }
}