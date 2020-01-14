using System;

namespace lljubici1_zadaca_3._Model.Pomagala
{
    public class Konverzija
    {
        public static int PretvoriVrijemeUSekunde(string vrijeme)
        {
            return (int)TimeSpan.Parse(vrijeme).TotalSeconds;
        }
        public static string PretvoriSekundeUVrijeme(int sekunde)
        {
            TimeSpan t = TimeSpan.FromSeconds(sekunde);
            return t.ToString(@"hh\:mm\:ss");
        }
    }
}
