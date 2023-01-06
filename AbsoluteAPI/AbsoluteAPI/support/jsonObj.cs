using AbsoluteAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbsoluteAPI.support
{
    public class jsonObj
    {

        public class genericResponse
        {
            public int status { get; set; }
            public string message { get; set; }
            public string messageOri { get; set; }
        }

        public class elencoSquadre : genericResponse
        {
            public int num { get; set; }
            public List<squadra> squadre { get; set; }
        }

        public class squadra
        {
            public int id { get; set; }
            public string nome { get; set; }
            public string logo { get; set; }
        }


        public class elencoNews : genericResponse
        {
            public int num { get; set; }
            public List<news> news { get; set; }
        }

        public class news
        {
            public int id { get; set; }
            public string titolo { get; set; }
            public string titolo_breve { get; set; }
            public string data { get; set; }
            public string testo { get; set; }
            public string immagine { get; set; }
            public string linkEsterno { get; set; }
            public string titolo_share { get; set; }
            public string testo_share { get; set; }
        }

        public class detailNews : genericResponse
        {
            public news news { get; set; }

        }


        public class banner
        {
            public int id { get; set; }
            public string titolo { get; set; }
            public string immagine { get; set; }
            public string linkEsterno { get; set; }

        }

        public class elencoBanner : genericResponse
        {
            public int num { get; set; }
            public List<banner> banners { get; set; }
        }


        public class eventiSquadra : genericResponse
        {
            public int id { get; set; }
            public string nome { get; set; }
            public List<competizione> competizioni { get; set; }
        }

        public class competizione
        {
            public int id { get; set; }
            public string nome { get; set; }
            public List<evento> eventi { get; set; }
        }
        public class evento
        {
            public int id { get; set; }
            public string nome { get; set; }
            public string tipoEvento { get; set; }
            public string OrdineClassifica { get; set; }
            public List<incontro> incontri { get; set; }

            public List<classifica> classifiche { get; set; }
        }

        public class prossimeGare: genericResponse
        {
            public List<competizione> competizioni { get; set; }
        }

        public class incontro
        {
            public int id { get; set; }
            public string calendario { get; set; }
            public squadra squadra1 { get; set; }
            public squadra squadra2 { get; set; }
            public campo campo { get; set; }
            public string data { get; set; }
            public string ora { get; set; }
            public string risultato1 { get; set; }
            public string risultato2 { get; set; }
            public bool flLive { get; set; }

        }

        public class campo
        {
            public int id { get; set; }
            public string nome { get; set; }
            public string googleLat { get; set; }
            public string googleLon { get; set; }
        }

        public class dettaglioEvento:genericResponse
        {
           
            //public squadra squadra { get; set; }
            public competizione competizione { get; set; }
        }

        public class dettaglioGara : genericResponse
        {
            public competizione competizione { get; set; }
        }

        public class elencoGare : genericResponse
        {
            public List<competizione> competizioni { get; set; }
        }

        public class classifica
        {
            public string nome { get; set; }
            public List<rigaClassifica> righe { get; set; }


        }
        public class rigaClassifica
        {
           
            public squadra squadra { get; set;}
            public int punti_penalita { get; set; }
            public int punti { get; set; }
            public int giocate { get; set; }
            public int vinte { get; set; }
            public int pareggiate { get; set; }
            public int perse { get; set; }
            public int dr { get; set; }
        }
    }
}