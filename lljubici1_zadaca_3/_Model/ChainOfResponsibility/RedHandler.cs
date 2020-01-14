using System;

namespace lljubici1_zadaca_3._Model.ChainOfResponsibility
{
    public class RedHandler : AbstractHandler
    {
        public override bool Handle(string request)
        {
            if (request.ToLower().Equals("crvena"))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                return true;
            }
            else
            {
                return base.Handle(request);
            }
        }
    }
}
