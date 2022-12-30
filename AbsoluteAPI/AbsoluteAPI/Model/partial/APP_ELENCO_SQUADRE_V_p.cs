using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AbsoluteAPI.support;
using System.Security.Policy;
using System.Configuration;
using System.Web.Configuration;

namespace AbsoluteAPI.Model
{
    public partial class APP_ELENCO_SQUADRE_V
    {
        private string Url = WebConfigurationManager.AppSettings["baseurl"];
        public string LogoSquadra
        {
            get
            {
                string path = !string.IsNullOrEmpty(this.LOGO) ? "public/squadre/" + this.LOGO : "assets/images/soccer/placeholder-squadra.png";
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
    }
}