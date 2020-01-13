namespace lljubici1_zadaca_3.ChainOfResponsibility
{
    public interface IHandler
    {
        IHandler SetNext(IHandler handler);
        bool Handle(string color);
    }
}
