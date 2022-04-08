using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentiPolizia
{
    interface IManagerAgenti
    {
        List<Agente> GetAll();
        List<Agente> GetByAreaGeografica(string areaGeografica);
        List<Agente> GetByAnniDiServizio(int anniDiServizio);
        bool Add(Agente agente);

        List<string> GetAllAreeGeografiche();
        bool EsisteArea(string areaGeografica);
        Agente GetByCF(string cf);
    }
}
