using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AbsoluteAPI.support;

namespace AbsoluteAPI.Model
{
    public partial class APP_NEWS_V
    {

        public string ImmagineNews
        {
            get
            {
                return !string.IsNullOrEmpty(this.IMMAGINE) ? string.Format("{0}/{1}", "https://www.absolute5.it/public/app/news", this.IMMAGINE) : null;
            }
        }


        public string TitoloShare
        {
            get
            {
                string ret = this.TITOLO;
                if (!string.IsNullOrEmpty(ret))
                {
                    ret = ret.Replace("'", @"\'").Replace("\n", "").Replace("<br />", @"\n").Replace("\"", @"\'");
                }
                return ret;
            }
        }

        public string TestoShare
        {
            get
            {
                string ret = this.TESTO;
                if (!string.IsNullOrEmpty(ret))
                {
                    ret = ret.Replace("\r", "<br />");
                    ret = ret.Replace("'", @"\'").Replace("\n", "").Replace("<br />", @"\n").Replace("\"", @"\'");
                }
                return ret;
            }
        }



    }
}