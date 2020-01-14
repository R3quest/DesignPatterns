using System;

namespace lljubici1_zadaca_3._Model.ChainOfResponsibility
{
    public class BlueHandler : AbstractHandler
    {
        public override bool Handle(string request)
        {
            if (request.ToLower().Equals("plava"))
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                return true;
            }
            else
            {
                return base.Handle(request);
            }
        }
    }
}
