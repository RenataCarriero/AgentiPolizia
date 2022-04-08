using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentiPolizia
{
    class Agente: Persona
    {
        public string AreaGeografica { get; set; }
        public int AnnoInizioAttivita { get; set; }
        public Agente()
        {

        }
        public Agente(string nome, string cognome, string codiceFiscale, string areaGeografica, int annoInizioAttivita)
            : base(nome, cognome, codiceFiscale)
        {
            AreaGeografica = areaGeografica;
            AnnoInizioAttivita = annoInizioAttivita;
        }


        internal int CalcolaAnniDiServizioDaAnnoDiInizioAttivita()
        {
            return DateTime.Now.Year - AnnoInizioAttivita;
        }

        public override string ToString()
        {
            return base.ToString() + $"- AnniDiServizio: {CalcolaAnniDiServizioDaAnnoDiInizioAttivita()}";
        }
    }

}
