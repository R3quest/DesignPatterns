using System.Collections.Generic;

namespace lljubici1_zadaca_2.Decorator
{
    public /*abstract*/ class Decorator : IComponent
    {
        public string TablicaEmisija { get; set; }

        protected List<IComponent> components;
        public Decorator(List<IComponent> components)
        {
            this.components = components;
        }

        public string Operacija()
        {
            if (components != null)
            {
                foreach (var component in components)
                {
                    TablicaEmisija += component.Operacija();
                }
            }
            return TablicaEmisija;
        }
    }
}
