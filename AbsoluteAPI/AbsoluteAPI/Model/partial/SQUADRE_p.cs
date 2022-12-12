using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AbsoluteAPI.support;
using System.Web.Configuration;

namespace AbsoluteAPI.Model
{
    public partial class SQUADRE
    {
        private string Url = WebConfigurationManager.AppSettings["baseurl"];
        public string LogoSquadra
        {
            get
            {

                string path = !string.IsNullOrEmpty(this.LOGO) ? "public/squadre/"+this.LOGO : "assets/images/placeholder-squadra.png";
                return string.Format("{0}/{1}", Url, path);
            }
        }


        public string NomeSquadra
        {
            get
            {
                return this.NOME ?? constMsg._noName;
            }
        }

        public string NomeSquadraEventoFuturoTorneo
        {
            get
            {
                return this.NOME ?? constMsg._squadraVincente;
            }
        }
    }
}