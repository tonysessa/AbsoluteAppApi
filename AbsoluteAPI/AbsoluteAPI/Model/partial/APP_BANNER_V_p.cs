using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AbsoluteAPI.support;

namespace AbsoluteAPI.Model
{
    public partial class APP_BANNER_V
    {

        public string Immagine
        {
            get
            {
                return !string.IsNullOrEmpty(this.IMMAGINE) ? string.Format("{0}/{1}", "https://www.absolute5.it/public/app/banner", this.IMMAGINE) : null;
            }
        }


        

       

    }
}