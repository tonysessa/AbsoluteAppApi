using AbsoluteAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using System.Xml;
using static AbsoluteAPI.support.jsonObj;
using AbsoluteAPI.support;
using System.Configuration;

namespace AbsoluteAPI.Controllers
{
    public class ServicesController : ApiController
    {

        [System.Web.Http.HttpPost]
        [System.Web.Http.ActionName("elencoSquadre")]
        public elencoSquadre elencoSquadre(string _text)
        {
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
                                s.nome = x.NomeSquadra;
                                s.logo = x.LogoSquadra;

                                response.squadre.Add(s);
                            }

                            response.status = 1;
                        }
                        else
                        {
                            response.status = 99;
                            response.message = constMsg._noSquadra;
                        }

                    }

                }
                else
                {
                    response.status = 99;
                    response.message = constMsg._digit2;
                }


            }
            catch (Exception ex)
            {
                response.status = -1;
                response.messageOri = ex.Message;
                response.message = constMsg._genericError;
            }

            return response;
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.ActionName("news")]
        public elencoNews elencoNews(int? num)
        {
            int numToResponse = num ?? 0;
            elencoNews response = new elencoNews();
            response.news = null;
            response.num = 0;
            try
            {

                using (absoluteEntities ctx = new absoluteEntities())
                {
                    IQueryable<APP_NEWS_V> list = ctx.APP_NEWS_V.Where(t => t.FL_HOME == 1).OrderByDescending(t => t.DATA_NEWS);
                    if (numToResponse > 0)
                        list = list.Where(t => t.FL_HOME == 1).Take(numToResponse);

                    int Tot = list.Count();
                    if (Tot > 0)
                    {
                        response.news = new List<news>();
                        response.num = Tot;

                        foreach (APP_NEWS_V x in list)
                        {
                            news s = new news();
                            s.id = x.ID;
                            s.titolo = !string.IsNullOrEmpty(x.TITOLO) ? x.TITOLO : null;
                            s.titolo_breve = !string.IsNullOrEmpty(x.TITOLO_BREVE) ? x.TITOLO_BREVE : null;
                            s.data = x.DATA_NEWS != null ? ((DateTime)x.DATA_NEWS).ToString("dd/MM/yyyy") : null;
                            s.linkEsterno = !string.IsNullOrEmpty(x.LINK_ESTERNO) ? x.LINK_ESTERNO : null;
                            s.immagine = !string.IsNullOrEmpty(x.ImmagineNews) ? x.ImmagineNews : null;

                            response.news.Add(s);
                        }

                        response.status = 1;
                    }
                    else
                    {
                        response.status = 99;
                        response.message = constMsg._noNews;
                    }
                }
            }
            catch (Exception ex)
            {
                response.status = -1;
                response.messageOri = ex.Message;
                response.message = constMsg._genericError;
            }

            return response;
        }


        [System.Web.Http.HttpGet]
        [System.Web.Http.ActionName("news")]
        public detailNews getNews(int idnews)
        {

            detailNews response = new detailNews();
            response.news = null;
            
            try
            {

                using (absoluteEntities ctx = new absoluteEntities())
                {
                    APP_NEWS_V x = ctx.APP_NEWS_V.Where(t => t.ID == idnews).FirstOrDefault();

                    if (x!= null)
                    {
                        news s = new news();
                        s.id = x.ID;
                        s.titolo = !string.IsNullOrEmpty(x.TITOLO) ? x.TITOLO : null;
                        s.titolo_breve = !string.IsNullOrEmpty(x.TITOLO_BREVE) ? x.TITOLO_BREVE : null;
                        s.data = x.DATA_NEWS != null ? ((DateTime)x.DATA_NEWS).ToString("dd/MM/yyyy") : null;
                        s.linkEsterno = !string.IsNullOrEmpty(x.LINK_ESTERNO) ? x.LINK_ESTERNO : null;
                        s.immagine = !string.IsNullOrEmpty(x.ImmagineNews) ? x.ImmagineNews : null;
                        s.testo = !string.IsNullOrEmpty(x.ImmagineNews) ? x.ImmagineNews : null;
                        s.titolo_share = x.TitoloShare;
                        s.testo_share = x.TestoShare;

                        response.news = s;
                        response.status = 1;
                    }
                    else
                    {
                        response.status = 99;
                        response.message = constMsg._noDetailNews;
                    }

                }
            }
            catch (Exception ex)
            {
                response.status = -1;
                response.messageOri = ex.Message;
                response.message = constMsg._genericError;
            }

            return response;
        }


        [System.Web.Http.HttpGet]
        [System.Web.Http.ActionName("banners")]
        public elencoBanner elencoBanner(int? num)
        {
            int numToResponse = num ?? 0;
            elencoBanner response = new elencoBanner();
            response.banners = null;
            response.num = 0;
            try
            {
                using (absoluteEntities ctx = new absoluteEntities())
                {
                    IQueryable<APP_BANNER_V> list = ctx.APP_BANNER_V.OrderByDescending(t => t.ID);
                    if (numToResponse > 0)
                        list = list.Take(numToResponse);


                    int Tot = list.Count();
                    if (Tot > 0)
                    {
                        response.banners = new List<banner>();
                        response.num = Tot;

                        foreach (APP_BANNER_V x in list)
                        {
                            banner s = new banner();
                            s.id = x.ID;
                            s.titolo = !string.IsNullOrEmpty(x.TITOLO) ? x.TITOLO : null;
                            s.linkEsterno = !string.IsNullOrEmpty(x.LINK_ESTERNO) ? x.LINK_ESTERNO : null;
                            s.immagine = !string.IsNullOrEmpty(x.Immagine) ? x.Immagine : null;

                            response.banners.Add(s);
                        }

                        response.status = 1;
                    }
                    else
                    {
                        response.status = 99;
                        response.message = constMsg._noBanners;
                    }
                }
            }
            catch (Exception ex)
            {
                response.status = -1;
                response.messageOri = ex.Message;
                response.message = constMsg._genericError;
            }

            return response;
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.ActionName("eventiSquadra")]
        public eventiSquadra elencoEventiSquadra(int id)
        {
            eventiSquadra response = new eventiSquadra();

            try
            {
                if (id > 0)
                {

                    using (absoluteEntities ctx = new absoluteEntities())
                    {
                        var i = ctx.APP_INCONTRI_V.Where(t=>t.ID_SQUADRA_1 == id || t.ID_SQUADRA_2 == id)
                            .GroupBy(t=>t.ID_COMPETIZIONE)
                            .Select(c => new
                            {
                                name = c.FirstOrDefault().NOME,
                                id = c.Key
                            })
                            .OrderBy(t=>t.id)
                            .AsQueryable();
                    }
                }
            }
            catch (Exception ex)
            {
                response.status = -1;
                response.messageOri = ex.Message;
                response.message = constMsg._genericError;
            }

            return response;
        }

    }
}