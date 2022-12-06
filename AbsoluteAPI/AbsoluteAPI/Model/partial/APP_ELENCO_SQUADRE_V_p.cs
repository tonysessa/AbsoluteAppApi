using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AbsoluteAPI.support;

namespace AbsoluteAPI.Model
{
    public partial class APP_ELENCO_SQUADRE_V
    {

        public string LogoSquadra
        {
            get
            {

                string path = !string.IsNullOrEmpty(this.LOGO) ? this.LOGO : "/assets/images/placeholder-squadra.png";
                return string.Format("{0}/{1}", "https://www.absolute5.it/public/squadre/", path);
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