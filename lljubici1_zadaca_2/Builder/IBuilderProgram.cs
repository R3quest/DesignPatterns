using lljubici1_zadaca_2.Podaci;
using System.Collections.Generic;

namespace lljubici1_zadaca_2.Builder
{
    public interface IBuilderProgram
    {
        List<EmisijePrograma> DodajEmisijeSaDanimaIPocetkom(Program p,
            List<EmisijePrograma> emisijeKojeImajuDaneUTjednuIPocetak, List<EmisijePrograma> emisijeZaDodati);

        List<EmisijePrograma> DodajEmisijeSaDanimaBezPocetka(Program p, List<EmisijePrograma> emisijeSaDanimaBezPocetka,
            List<EmisijePrograma> emisijeZaDodati);

        List<EmisijePrograma> DodajEmisijeBezDanaIPocetka(Program p, List<EmisijePrograma> emisijeBezDanaIPocetka,
            List<EmisijePrograma> emisijeZaDodati);
    }
}
