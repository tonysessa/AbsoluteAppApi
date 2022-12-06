using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using System.Xml;
using static AbsoluteAPI.support.jsonObj;

namespace AbsoluteAPI.Controllers
{
    public class ServicesController : ApiController
    {

        [System.Web.Http.HttpGet]
        [System.Web.Http.ActionName("squadre/{_text}")]
        public genericResponse elencoSquadre1(string _text)
        {
            //string _text = "abs";
            elencoSquadre response = new elencoSquadre();
            response.squadre = null;
            response.num = 0;
            try
            {
                if (!string.IsNullOrEmpty(_text) && _text.Length >= 2)
                {
                    using (absoluteEntities ctx = new absoluteEntities())
                    {
                        List<APP_ELENCO_SQUADRE_V> list = ctx.APP_ELENCO_SQUADRE_V.Where(t => t.NOME.ToLower().Contains(_text.ToLower())).ToList();
                        int Tot = list.Count;
                        if (Tot > 0)
                        {
                            response.squadre = new List<squadra>();
                            response.num = Tot;

                            foreach (APP_ELENCO_SQUADRE_V x in list)
                            {
                                squadra s = new squadra();
                                s.id = x.ID_SQUADRA;
                                s.nome = x.NOME;

                                response.squadre.Add(s);
                            }
                        }
                    }

                }

                response.status = 1;
            }
            catch(Exception ex)
            {
                response.status = -1;
                response.messageOri = ex.Message;
                response.message = "Errore durante il caricamento dei dati";
            }

            return response;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.ActionName("elencoSquadre")]
        public genericResponse elencoSquadre(string _text)
        {
            //string _text = "abs";
            elencoSquadre response = new elencoSquadre();
            response.squadre = null;
            response.num = 0;
            try
            {
                if (!string.IsNullOrEmpty(_text) && _text.Length >= 2)
                {
                    using (absoluteEntities ctx = new absoluteEntities())
                    {
                        List<APP_ELENCO_SQUADRE_V> list = ctx.APP_ELENCO_SQUADRE_V.Where(t => t.NOME.ToLower().Contains(_text.ToLower())).ToList();
                        int Tot = list.Count;
                        if (Tot > 0)
                        {
                            response.squadre = new List<squadra>();
                            response.num = Tot;

                            foreach (APP_ELENCO_SQUADRE_V x in list)
                            {
                                squadra s = new squadra();
                                s.id = x.ID_SQUADRA;
                                s.nome = x.NOME;

                                response.squadre.Add(s);
                            }
                        }
                    }

                }

                response.status = 1;
            }
            catch (Exception ex)
            {
                response.status = -1;
                response.messageOri = ex.Message;
                response.message = "Errore durante il caricamento dei dati";
            }

            return response;
        }


    }
}