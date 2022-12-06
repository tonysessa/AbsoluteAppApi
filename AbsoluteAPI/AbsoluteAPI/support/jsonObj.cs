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
        }
    }
}