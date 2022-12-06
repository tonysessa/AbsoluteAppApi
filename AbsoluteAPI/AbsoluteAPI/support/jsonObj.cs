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


        public class elencoNews: genericResponse
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

        public class detailNews: genericResponse
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
        }
    }
}