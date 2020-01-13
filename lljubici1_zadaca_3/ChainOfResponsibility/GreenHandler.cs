using System;

namespace lljubici1_zadaca_3.ChainOfResponsibility
{
    public class GreenHandler : AbstractHandler
    {
        public override bool Handle(string request)
        {
            if (request.ToLower().Equals("zelena"))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                return true;
            }
            else
            {
                return base.Handle(request);
            }
        }
    }
}
