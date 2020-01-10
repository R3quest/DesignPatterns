using lljubici1_zadaca_3.Composite;
using lljubici1_zadaca_3.Podaci;
using System.Collections.Generic;

namespace lljubici1_zadaca_3.Builder
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
