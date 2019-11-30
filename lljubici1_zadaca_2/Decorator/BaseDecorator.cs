using lljubici1_zadaca_2.Composite;
using System;
using System.Collections.Generic;

namespace lljubici1_zadaca_2.Decorator
{
    public class BaseDecorator : IComponent
    {
        private IComponent wrappee;
        public BaseDecorator(IComponent wrappee)
        {
            this.wrappee = wrappee;
        }

        public List<IRasporedProgramaComponent> IspisVremenskogPlana()
        {
            throw new NotImplementedException();
        }
    }
}
