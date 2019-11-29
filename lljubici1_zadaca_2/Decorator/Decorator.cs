using System.Collections.Generic;

namespace lljubici1_zadaca_2.Decorator
{
    abstract class Decorator : IComponent
    {
        protected List<IComponent> components;

        public void SetComponent(List<IComponent> components)
        {
            this.components = components;
        }
        public string Operacija()
        {
            if (components != null)
            {
                foreach (var component in components)
                {
                    component.Operacija();
                }
            }
            return null;
        }
    }
}
