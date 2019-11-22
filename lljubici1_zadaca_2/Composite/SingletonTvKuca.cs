using lljubici1_zadaca_2.Podaci;
using System.Collections.Generic;

namespace lljubici1_zadaca_2.Composite
{
    public class SingletonTvKuca : IRasporedProgramaComponent
    {
        private static SingletonTvKuca _instanca = new SingletonTvKuca();
        public static SingletonTvKuca Instanca => _instanca;
        public List<Program> Programi { get; set; } = new List<Program>();
        private SingletonTvKuca()
        {

        }
        public void DodajProgram(Program program)
        {
            Programi.Add(program);
        }
        public void DodajPrograme(List<Program> programi)
        {
            Programi.AddRange(programi);
        }

        public void DodajElementRasporeda(IRasporedProgramaComponent elementComposite)
        {
            throw new System.NotImplementedException();
        }

        public void IspisiRaspored()
        {
            throw new System.NotImplementedException();
        }
    }
}