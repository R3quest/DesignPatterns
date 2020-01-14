using lljubici1_zadaca_3._Controller;
using lljubici1_zadaca_3._Model;
using lljubici1_zadaca_3._View;
using System;

namespace lljubici1_zadaca_3
{
    class MainProgram
    {
        static void Main(string[] args)
        {
            Model model = new Model(args);
            View1 view = new View1();
            Controller controller = new Controller(model, view);
            controller.KorisnikovUnos();
            Console.ReadLine();
        }
    }
}
