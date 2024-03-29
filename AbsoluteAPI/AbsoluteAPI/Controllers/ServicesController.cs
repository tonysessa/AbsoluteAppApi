﻿using AbsoluteAPI.Model;
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
using System.Text.RegularExpressions;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography.Xml;
using System.Security.Permissions;
using System.Web.Http.Cors;
using System.Web.DynamicData;

namespace AbsoluteAPI.Controllers
{
    [DisableCors]
    public class ServicesController : ApiController
    {
        [DisableCors]
        [System.Web.Http.HttpGet]
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
        public elencoNews elencoNews(int? num, int? flHome)
        {
            int numToResponse = num ?? 0;
            int flagHome = flHome ?? 1;
            elencoNews response = new elencoNews();
            response.news = null;
            response.num = 0;
            try
            {

                using (absoluteEntities ctx = new absoluteEntities())
                {
                    IQueryable<APP_NEWS_V> list = null;
                    if (flagHome == 1)
                        list = ctx.APP_NEWS_V.Where(t => t.FL_HOME == 1).OrderByDescending(t => t.DATA_NEWS).ThenByDescending(t => t.ID);
                    else
                        list = ctx.APP_NEWS_V.OrderByDescending(t => t.DATA_NEWS).ThenByDescending(t => t.ID);

                    if (numToResponse > 0)
                        list = list.Take(numToResponse);

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
                            s.testo = !string.IsNullOrEmpty(x.TESTO) ? x.TESTO : null;
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
        [System.Web.Http.ActionName("newsDettaglio")]
        public detailNews getNews(int idnews)
        {

            detailNews response = new detailNews();
            response.news = null;

            try
            {

                using (absoluteEntities ctx = new absoluteEntities())
                {
                    APP_NEWS_V x = ctx.APP_NEWS_V.Where(t => t.ID == idnews).FirstOrDefault();

                    if (x != null)
                    {
                        news s = new news();
                        s.id = x.ID;
                        s.titolo = !string.IsNullOrEmpty(x.TITOLO) ? x.TITOLO : null;
                        s.titolo_breve = !string.IsNullOrEmpty(x.TITOLO_BREVE) ? x.TITOLO_BREVE : null;
                        s.data = x.DATA_NEWS != null ? ((DateTime)x.DATA_NEWS).ToString("dd/MM/yyyy") : null;
                        s.linkEsterno = !string.IsNullOrEmpty(x.LINK_ESTERNO) ? x.LINK_ESTERNO : null;
                        s.immagine = !string.IsNullOrEmpty(x.ImmagineNews) ? x.ImmagineNews : null;
                        s.testo = !string.IsNullOrEmpty(x.TESTO) ? x.TESTO : null;
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
            response.status = 99;
            try
            {
                if (id > 0)
                {

                    using (absoluteEntities ctx = new absoluteEntities())
                    {

                        SQUADRE s = ctx.SQUADRE.Where(t => t.ID == id).FirstOrDefault();
                        if (s != null)
                        {
                            response.nome = s.NOME;
                            response.id = id;

                            var i = ctx.APP_INCONTRI_V.Where(t => t.ID_SQUADRA_1 == id || t.ID_SQUADRA_2 == id)
                            .GroupBy(t => t.ID_COMPETIZIONE)
                            .Select(c => new
                            {
                                name = c.FirstOrDefault().NOME,
                                id = c.Key,
                                elements = c.ToList()
                            })
                            .OrderBy(t => t.id)
                            .AsQueryable();

                            if (i.Count() > 0)
                            {
                                response.competizioni = new List<competizione>();
                                foreach (var x in i)
                                {
                                    competizione c = new competizione();
                                    c.id = x.id;
                                    c.nome = x.name;

                                    var listEvents = x.elements
                                                        .GroupBy(y => y.ID_EVENTO)
                                                        .Select(m => new
                                                        {
                                                            id_evento = m.Key,
                                                            nome_evento = m.FirstOrDefault().NOME_EVENTO
                                                        })
                                                        .OrderBy(t => t.id_evento)
                                                        .AsQueryable();

                                    if (listEvents != null)
                                    {
                                        c.eventi = new List<evento>();
                                        foreach (var z in listEvents)
                                        {
                                            evento e = new evento();
                                            e.id = z.id_evento;
                                            e.nome = z.nome_evento;
                                            //
                                            c.eventi.Add(e);
                                        }
                                    }

                                    response.competizioni.Add(c);
                                }

                                response.status = 1;
                            }
                            else
                            {
                                response.message = constMsg._noEventi;

                            }
                        }
                        else
                        {
                            response.status = 99;
                            response.message = constMsg._noSquadraDetail;
                        }
                    }

                  
                }
                else
                {
                    response.status = 99;
                    response.message = constMsg._noSquadraDetail;
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
        [System.Web.Http.ActionName("prossimeGare")]
        public prossimeGare prossimeGare(int id)
        {
            prossimeGare response = new prossimeGare();
            try
            {
                if (id > 0)
                {
                    using (absoluteEntities ctx = new absoluteEntities())
                    {

                        SQUADRE s = ctx.SQUADRE.Where(t => t.ID == id).FirstOrDefault();
                        if (s != null)
                        {

                            string time = DateTime.Now.Hour.ToString().PadLeft(2, '0') + DateTime.Now.Minute.ToString().PadLeft(2, '0');
                            List<Sp_ProssimeGare_Result> r = ctx.Sp_ProssimeGare(time, id).ToList();
                            if (r.Count > 0)
                            {

                                //vecchio raggruppamento
                                //var query = r.GroupBy(t => t.ID_COMPETIZIONE)
                                //                                            .Select(c => new
                                //                                            {
                                //                                                name = c.FirstOrDefault().NOME,
                                //                                                id = c.Key,
                                //                                                elements = c.ToList()
                                //                                            })
                                //                                    .OrderBy(t =>t.id)
                                //                                    .AsQueryable();


                                //var query = r.OrderBy(t => t.DATA_EVENTO).ThenBy(b => b.DATA_POSTICIPO).AsQueryable();
                                //prendo tutte le squadre che trovo
                                IEnumerable<int> listSquadre = r.Select(t => t.ID_SQUADRA_1);
                                listSquadre = listSquadre.Concat(r.Select(t => t.ID_SQUADRA_2)).Distinct();

                                IQueryable<SQUADRE> squadre = ctx.SQUADRE.Where(sq => listSquadre.Contains(sq.ID)).AsQueryable();


                                response.competizioni = new List<competizione>();
                                foreach (var x in r)
                                {
                                    competizione c = new competizione();
                                    c.id = x.ID_COMPETIZIONE;
                                    c.nome = x.NOME;

                                    //var listEvents = x
                                    //                    //.GroupBy(y => y.ID_EVENTO)
                                    //                    .Select(m => new
                                    //                    {
                                    //                        id_evento = m.Key,
                                    //                        nome_evento = m.FirstOrDefault().NOME_EVENTO,
                                    //                        incontri = m.ToList()
                                    //                    })
                                    //                    .OrderBy(t => t.id_evento)
                                    //                    .AsQueryable();



                                    //if (listEvents != null)
                                    //{
                                        c.eventi = new List<evento>();
                                        //foreach (var z in listEvents)
                                        //{
                                            evento e = new evento();
                                            e.id = x.ID_EVENTO;
                                            e.nome = x.NOME_EVENTO;
                                            //
                                            //if (z.incontri != null)
                                            //{


                                                e.incontri = new List<incontro>();
                                                //foreach (var j in z.incontri)
                                                //{
                                                    incontro i = new incontro();
                                                    i.id = x.ID_INCONTRO;
                                                    //
                                                    #region squadre
                                                    squadra s1 = new squadra();
                                                    squadra s2 = new squadra();

                                                    s1.id = x.ID_SQUADRA_1;
                                                    if (s1.id > 0)
                                                    {
                                                        SQUADRE sq = squadre.Where(b => b.ID == s1.id).FirstOrDefault();
                                                        s1.nome = sq.NomeSquadra;
                                                        s1.logo = sq.LogoSquadra;

                                                    }
                                                    else
                                                        s1.nome = constMsg._squadraVincente;

                                                    s2.id = x.ID_SQUADRA_2;
                                                    if (s2.id > 0)
                                                    {
                                                        SQUADRE sq = squadre.Where(b => b.ID == s2.id).FirstOrDefault();
                                                        s2.nome = sq.NomeSquadra;
                                                        s2.logo = sq.LogoSquadra;

                                                    }
                                                    else
                                                        s2.nome = constMsg._squadraVincente;

                                                    i.squadra1 = s1;
                                                    i.squadra2 = s2;
                                                    #endregion
                                                    #region campo
                                                    campo c1 = new campo();
                                                    c1.id = x.ID_CAMPO;
                                                    if (x.ID_CAMPO_RECUPERO != 0 && !string.IsNullOrEmpty(x.ID_CAMPO_RECUPERO.ToString()))
                                                        c1.id = (int)x.ID_CAMPO_RECUPERO;

                                                    CAMPI campi = ctx.CAMPI.Where(t => t.ID == c1.id).FirstOrDefault();
                                                    c1.nome = campi.NOMECAMPO;
                                                    if (!string.IsNullOrEmpty("" + campi.GMAP))
                                                    {
                                                        string gmap = campi.GMAP.ToString().Replace("&amp;ll=", "&ll=");
                                                        int index = gmap.IndexOf("&ll=");
                                                        if (index > 0)
                                                        {
                                                            gmap = gmap.Substring(index + 4, gmap.Length - (index + 4));
                                                            index = gmap.IndexOf("&");
                                                            if (index > 0)
                                                            {
                                                                char[] separator = new char[] { ',' };
                                                                string[] strArray = gmap.Substring(0, index).Split(separator);
                                                                if (strArray.Length != 0)
                                                                {
                                                                    c1.googleLat = strArray[0];
                                                                    c1.googleLon = strArray[1];
                                                                }
                                                            }
                                                        }
                                                    }
                                                    i.campo = c1;
                                                    #endregion
                                                    #region data e ora
                                                    i.data = x.DATA_POSTICIPO != null ? ((DateTime)x.DATA_POSTICIPO).ToString("dd/MM/yyyy") : ((DateTime)x.DATA_EVENTO).ToString("dd/MM/yyyy");
                                                    i.ora = !string.IsNullOrEmpty(x.ORA_POSTICIPO) ? x.ORA_POSTICIPO : x.ORA;
                                                    #endregion
                                                    #region risultati e live
                                                    i.risultato1 = "-";
                                                    i.risultato2 = "-";
                                                    i.flLive = false;
                                                    #endregion

                                                    e.incontri.Add(i);
                                                //}
                                            //}
                                            c.eventi.Add(e);
                                        //}
                                    //}
                                    response.competizioni.Add(c);
                                }
                                response.competizioni = response.competizioni.OrderBy(t => t.eventi.Select(v => v.incontri.Select(b => b.data))).ToList();                                response.status = 1;
                            }
                            else
                            {
                                response.status = 99;
                                response.message = constMsg._noGare;
                            }
                        }
                        else
                        {
                            response.status = 99;
                            response.message = constMsg._noSquadraDetail;
                        }
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
        [System.Web.Http.ActionName("dettaglioEvento")]

        public dettaglioEvento dettaglioEvento(int idsquadra, int idevento)
        {
            //https://localhost:44322/api/Services/dettaglioEvento?idsquadra=6413&idevento=2718/
            //https://dev.absolute5.it/api/Services/dettaglioEvento?idsquadra=6437&idevento=2722
            //https://dev.absolute5.it/api/Services/dettaglioEvento?idsquadra=1282&idevento=2327
            dettaglioEvento response = new dettaglioEvento();
            try
            {
                if (idevento > 0 && idsquadra > 0)
                {
                    using (absoluteEntities ctx = new absoluteEntities())
                    {

                        EVENTI e = ctx.EVENTI.Where(t => t.ID == idevento).FirstOrDefault();
                        if (e != null)
                        {

                            #region competizione
                            APP_COMPETIZIONI_EVENTI_V c = ctx.APP_COMPETIZIONI_EVENTI_V.Where(t => t.ID_EVENTI == idevento && t.ID == e.ID_COMPETIZIONE).FirstOrDefault();
                            if (c != null)
                            {
                                competizione competizione = new competizione();
                                competizione.id = c.ID;
                                competizione.nome = c.NOME;

                                #region evento
                                competizione.eventi = new List<evento>();
                                evento ev = new evento();
                                ev.id = c.ID_EVENTI;
                                ev.nome = c.NOME_EVENTI;
                                ev.tipoEvento = e.TIPO_GIRONE;
                                ev.OrdineClassifica = e.ORDINE_CLASSIFICA;
                                if (!string.IsNullOrEmpty(ev.tipoEvento))
                                {
                                    if (ev.tipoEvento.Equals("2"))
                                    {
                                        IQueryable<APP_INCONTRI_V> query = ctx.APP_INCONTRI_V
                                                                                            .Where(t => (t.ID_SQUADRA_1 == idsquadra
                                                                                                                            || t.ID_SQUADRA_2 == idsquadra)
                                                                                                                            && t.ID_EVENTO == idevento)
                                                                                            .OrderBy(t => t.DATA_POSTICIPO).ThenBy(t => t.DATA_EVENTO).AsQueryable();

                                        IEnumerable<int> listSquadre = query.Select(t => t.ID_SQUADRA_1).AsEnumerable();
                                        listSquadre = listSquadre.Concat(query.Select(t => t.ID_SQUADRA_2)).Distinct();
                                        IQueryable<SQUADRE> squadre = ctx.SQUADRE.Where(x => listSquadre.Contains(x.ID)).AsQueryable();

                                        ev.incontri = new List<incontro>();
                                        foreach (var j in query)
                                        {

                                            incontro i = new incontro();
                                            i.id = j.ID_INCONTRO;
                                            i.calendario = j.NOME_CALENDARIO;
                                            //
                                            #region squadre
                                            squadra s1 = new squadra();
                                            squadra s2 = new squadra();

                                            s1.id = j.ID_SQUADRA_1;
                                            if (s1.id > 0)
                                            {
                                                SQUADRE sq = squadre.Where(b => b.ID == s1.id).FirstOrDefault();
                                                s1.nome = sq.NomeSquadra;
                                                s1.logo = sq.LogoSquadra;

                                            }
                                            else
                                                s1.nome = constMsg._squadraVincente;

                                            s2.id = j.ID_SQUADRA_2;
                                            if (s2.id > 0)
                                            {
                                                SQUADRE sq = squadre.Where(b => b.ID == s2.id).FirstOrDefault();
                                                s2.nome = sq.NomeSquadra;
                                                s2.logo = sq.LogoSquadra;

                                            }
                                            else
                                                s2.nome = constMsg._squadraVincente;

                                            i.squadra1 = s1;
                                            i.squadra2 = s2;
                                            #endregion
                                            #region campo
                                            campo c1 = new campo();
                                            c1.id = j.ID_CAMPO;
                                            if (j.ID_CAMPO_RECUPERO != 0 && !string.IsNullOrEmpty(j.ID_CAMPO_RECUPERO.ToString()))
                                                c1.id = (int)j.ID_CAMPO_RECUPERO;

                                            CAMPI campi = ctx.CAMPI.Where(t => t.ID == c1.id).FirstOrDefault();
                                            c1.nome = campi.NOMECAMPO;

                                            if (!string.IsNullOrEmpty("" + campi.GMAP))
                                            {
                                                string gmap = campi.GMAP.ToString().Replace("&amp;ll=", "&ll=");
                                                int index = gmap.IndexOf("&ll=");
                                                if (index > 0)
                                                {
                                                    gmap = gmap.Substring(index + 4, gmap.Length - (index + 4));
                                                    index = gmap.IndexOf("&");
                                                    if (index > 0)
                                                    {
                                                        char[] separator = new char[] { ',' };
                                                        string[] strArray = gmap.Substring(0, index).Split(separator);
                                                        if (strArray.Length != 0)
                                                        {
                                                            c1.googleLat = strArray[0];
                                                            c1.googleLon = strArray[1];
                                                        }
                                                    }
                                                }
                                            }

                                            i.campo = c1;
                                            #endregion
                                            #region data e ora
                                            i.data = j.DATA_POSTICIPO != null ? ((DateTime)j.DATA_POSTICIPO).ToString("dd/MM/yyyy") : ((DateTime)j.DATA_EVENTO).ToString("dd/MM/yyyy");
                                            i.ora = !string.IsNullOrEmpty(j.ORA_POSTICIPO) ? j.ORA_POSTICIPO : j.ORA;
                                            #endregion
                                            #region risultati e live
                                            i = SetRisultatoLive(ctx, i);
                                            #endregion
                                            ev.incontri.Add(i);
                                        }
                                    }
                                    else
                                    {
                                        ev.classifiche = new List<classifica>();
                                        #region classifica non Azzerata

                                        //Prendo tutte le squadre presenti in quel girone
                                        List<int> listIdSquadre = ctx.EVENTI_SQUADRE.Where(t => t.ID_EVENTO == idevento && t.STATO == 1).Select(t => t.ID_SQUADRA).Distinct().ToList();
                                        List<SQUADRE> listaSquadre = ctx.SQUADRE.Where(t => listIdSquadre.Contains(t.ID)).ToList();
                                        //

                                        IQueryable<CLASSIFICHE_SQUADRE_V> classificaNoAzzerata = ctx.CLASSIFICHE_SQUADRE_V
                                                                                                                    .Where(t => t.ID_EVENTO == idevento
                                                                                                                    && t.AZZERATA == 0
                                                                                                                    && t.ID_SQUADRA != 6420 //squadra fake
                                                                                                           ).AsQueryable();
                                        if (ev.OrdineClassifica.Equals("0"))
                                            classificaNoAzzerata = classificaNoAzzerata.OrderByDescending(t => t.PUNTI).ThenByDescending(t => (t.GF - t.GS)).ThenBy(t => t.NOME);
                                        else
                                            classificaNoAzzerata = classificaNoAzzerata.OrderBy(t => t.ORDINE).ThenByDescending(t => t.PUNTI).ThenByDescending(t => (t.GF - t.GS)).ThenBy(t => t.NOME);


                                        classifica classifica = new classifica();
                                        classifica.nome = ev.tipoEvento.Equals("1") ? constMsg._clsGenerale : constMsg._cls;

                                        classifica.righe = new List<rigaClassifica>();
                                        List<SQUADRE> listaSquadreToEmpty = new List<SQUADRE>();
                                        listaSquadreToEmpty.AddRange(listaSquadre);
                                            
                                        foreach (var x in classificaNoAzzerata)
                                        {
                                            rigaClassifica r = new rigaClassifica();
                                            SQUADRE sq = listaSquadre.Where(t => t.ID == x.ID_SQUADRA).FirstOrDefault();
                                            r.squadra = new squadra() { id = sq.ID, logo = sq.LogoSquadra, nome = sq.NomeSquadra };
                                            r.perse = x.PERSE;
                                            r.giocate = x.GIOCATE;
                                            r.punti = x.PUNTI;
                                            r.dr = (x.GF - x.GS);
                                            r.vinte = x.VINTE;
                                            r.pareggiate = x.PAREGGIATE;
                                            r.punti_penalita = Math.Abs(x.punti_penalita);
                                            r.gf = x.GF;
                                            r.gs = x.GS;
                                            classifica.righe.Add(r);

                                            listaSquadreToEmpty.Remove(sq);
                                        }


                                        //Aggiungo record per squadre che non hanno ancora giocato
                                        foreach (var x in listaSquadreToEmpty)
                                        {
                                            rigaClassifica r = new rigaClassifica();

                                            r.squadra = new squadra() { id = x.ID, logo = x.LogoSquadra, nome = x.NomeSquadra };
                                            r.perse = 0;
                                            r.giocate = 0;
                                            r.punti = 0;
                                            r.dr = 0;
                                            r.vinte = 0;
                                            r.pareggiate = 0;
                                            r.punti_penalita = 0;
                                            r.gs = 0;
                                            r.gf = 0;
                                            classifica.righe.Add(r);

                                        }

                                        ev.classifiche.Add(classifica);
                                        #endregion

                                        #region Classifica clausura

                                        IQueryable<CLASSIFICHE_SQUADRE_V> classificaClausura = ctx.CLASSIFICHE_SQUADRE_V
                                                                                                                    .Where(t => t.ID_EVENTO == idevento
                                                                                                                    && t.AZZERATA == 1
                                                                                                                    && t.ID_SQUADRA != 6420 //squadra fake
                                                                                                           ).AsQueryable();
                                        if (classificaClausura.Count() > 0)
                                        {
                                            if (ev.OrdineClassifica.Equals("0"))
                                                classificaClausura = classificaClausura.OrderByDescending(t => t.PUNTI).ThenByDescending(t => (t.GF - t.GS)).ThenBy(t => t.NOME);
                                            else
                                                classificaClausura = classificaClausura.OrderBy(t => t.ORDINE).ThenByDescending(t => t.PUNTI).ThenByDescending(t => (t.GF - t.GS)).ThenBy(t => t.NOME);




                                            classifica = new classifica();
                                            classifica.nome = constMsg._clsClausura;
                                            classifica.righe = new List<rigaClassifica>();
                                            listaSquadreToEmpty = listaSquadre;
                                            foreach (var x in classificaClausura)
                                            {
                                                rigaClassifica r = new rigaClassifica();
                                                SQUADRE sq = listaSquadre.Where(t => t.ID == x.ID_SQUADRA).FirstOrDefault();
                                                r.squadra = new squadra() { id = sq.ID, logo = sq.LogoSquadra, nome = sq.NomeSquadra };
                                                r.perse = x.PERSE;
                                                r.giocate = x.GIOCATE;
                                                r.punti = x.PUNTI;
                                                r.dr = (x.GF - x.GS);
                                                r.vinte = x.VINTE;
                                                r.pareggiate = x.PAREGGIATE;
                                                r.punti_penalita = Math.Abs(x.punti_penalita);
                                                r.gf = x.GF;
                                                r.gs = x.GS;
                                                classifica.righe.Add(r);

                                                listaSquadreToEmpty.Remove(sq);
                                            }
                                            //Aggiungo record per squadre che non hanno ancora giocato
                                            foreach (var x in listaSquadreToEmpty)
                                            {
                                                rigaClassifica r = new rigaClassifica();

                                                r.squadra = new squadra() { id = x.ID, logo = x.LogoSquadra, nome = x.NomeSquadra };
                                                r.perse = 0;
                                                r.giocate = 0;
                                                r.punti = 0;
                                                r.dr = 0;
                                                r.vinte = 0;
                                                r.pareggiate = 0;
                                                r.punti_penalita = 0;
                                                r.gs = 0;
                                                r.gf = 0;
                                                classifica.righe.Add(r);

                                            }

                                            ev.classifiche.Add(classifica);

                                        }
                                        #endregion
                                    }
                                }
                                competizione.eventi.Add(ev);


                                #endregion
                                response.competizione = competizione;
                                response.status = 1;
                            }
                            else
                            {
                                response.status = 99;
                                response.message = constMsg._noDettaglio;

                            }
                            #endregion
                        }
                        else
                        {
                            response.status = 99;
                            response.message = constMsg._noDettaglio;
                        }
                    }

                }
                else
                {
                    response.status = 99;
                    response.message = constMsg._noDettaglio;
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
        [System.Web.Http.ActionName("dettaglioGara")]
        public dettaglioGara dettaglioGara(int id)
        {
            dettaglioGara response = new dettaglioGara();
            try
            {
                if (id > 0)
                {
                    using (absoluteEntities ctx = new absoluteEntities())
                    {
                        APP_INCONTRI_V i = ctx.APP_INCONTRI_V.Where(t => t.ID_INCONTRO == id).FirstOrDefault();
                        if (i != null)
                        {
                            IQueryable<SQUADRE> squadre = ctx.SQUADRE.Where(t => t.ID == i.ID_SQUADRA_1 || t.ID == i.ID_SQUADRA_2).AsQueryable();
                            IQueryable<CAMPI> campi = ctx.CAMPI.Where(t => t.ID == i.ID_CAMPO || t.ID == i.ID_CAMPO_RECUPERO).AsQueryable();

                            competizione cmp = new competizione();

                            cmp.id = i.ID_COMPETIZIONE;
                            cmp.nome = i.NOME;
                            cmp.eventi = new List<evento>();

                            evento e = new evento();
                            e.id = i.ID_EVENTO;
                            e.nome = i.NOME_EVENTO;
                            e.incontri = new List<incontro>();

                            incontro ic = new incontro();
                            ic.id = i.ID_INCONTRO;

                            ic.calendario = i.NOME_CALENDARIO;
                            #region squadre
                            squadra s1 = new squadra();
                            squadra s2 = new squadra();

                            s1.id = i.ID_SQUADRA_1;
                            if (s1.id > 0)
                            {
                                SQUADRE sq = squadre.Where(b => b.ID == s1.id).FirstOrDefault();
                                s1.nome = sq.NomeSquadra;
                                s1.logo = sq.LogoSquadra;

                            }
                            else
                                s1.nome = constMsg._squadraVincente;

                            s2.id = i.ID_SQUADRA_2;
                            if (s2.id > 0)
                            {
                                SQUADRE sq = squadre.Where(b => b.ID == s2.id).FirstOrDefault();
                                s2.nome = sq.NomeSquadra;
                                s2.logo = sq.LogoSquadra;

                            }
                            else
                                s2.nome = constMsg._squadraVincente;

                            ic.squadra1 = s1;
                            ic.squadra2 = s2;
                            #endregion
                            #region campo
                            campo c1 = new campo();
                            c1.id = i.ID_CAMPO;
                            if (i.ID_CAMPO_RECUPERO != 0 && !string.IsNullOrEmpty(i.ID_CAMPO_RECUPERO.ToString()))
                                c1.id = (int)i.ID_CAMPO_RECUPERO;
                            CAMPI campo = campi.Where(t => t.ID == c1.id).FirstOrDefault();
                            c1.nome = campo.NOMECAMPO;

                            if (!string.IsNullOrEmpty("" + campo.GMAP))
                            {
                                string gmap = campo.GMAP.ToString().Replace("&amp;ll=", "&ll=");
                                int index = gmap.IndexOf("&ll=");
                                if (index > 0)
                                {
                                    gmap = gmap.Substring(index + 4, gmap.Length - (index + 4));
                                    index = gmap.IndexOf("&");
                                    if (index > 0)
                                    {
                                        char[] separator = new char[] { ',' };
                                        string[] strArray = gmap.Substring(0, index).Split(separator);
                                        if (strArray.Length != 0)
                                        {
                                            c1.googleLat = strArray[0];
                                            c1.googleLon = strArray[1];
                                        }
                                    }
                                }
                            }

                            ic.campo = c1;
                            #endregion
                            #region data e ora
                            ic.data = i.DATA_POSTICIPO != null ? ((DateTime)i.DATA_POSTICIPO).ToString("dd/MM/yyyy") : ((DateTime)i.DATA_EVENTO).ToString("dd/MM/yyyy");
                            ic.ora = !string.IsNullOrEmpty(i.ORA_POSTICIPO) ? i.ORA_POSTICIPO : i.ORA;
                            #endregion
                            #region risultati e live
                            ic = SetRisultatoLive(ctx, ic);
                            #endregion

                            e.incontri.Add(ic);
                            cmp.eventi.Add(e);


                            response.competizione = cmp;
                            response.status = 1;
                        }
                        else
                        {
                            response.status = 99;
                            response.message = constMsg._noDettaglio;
                        }
                    }
                }
                else
                {
                    response.status = 99;
                    response.message = constMsg._noDettaglio;
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
        [System.Web.Http.ActionName("elencoGare")]
        public elencoGare elencoGare(string periodo)
        {
            elencoGare response = new elencoGare();
            try
            {
                using (absoluteEntities ctx = new absoluteEntities())
                {
                    if (!"o,i,d".ToLower().Contains(periodo))
                    {
                        response.status = 99;
                        response.message = constMsg._paramError;
                    }
                    else
                    {
                        DateTime dIeri = DateTime.Today.Date.AddDays(-1);
                        DateTime dDomani = DateTime.Today.Date.AddDays(1);
                        //
                        periodo = periodo.ToLower();
                        var i = ctx.APP_INCONTRI_V
                                        .Where(t => (periodo.Equals("o") && (t.DATA_EVENTO.Value == DateTime.Today.Date || t.DATA_POSTICIPO.Value == DateTime.Today.Date))
                                                    || (periodo.Equals("i") && (t.DATA_EVENTO.Value == dIeri || t.DATA_POSTICIPO.Value == dIeri))
                                                    || (periodo.Equals("d") && (t.DATA_EVENTO.Value == dDomani || t.DATA_POSTICIPO.Value == dDomani))
                                        )
                                .GroupBy(t => t.ID_COMPETIZIONE)
                                .Select(c => new
                                {
                                    name = c.FirstOrDefault().NOME,
                                    id = c.Key,
                                    elements = c.ToList()
                                })
                                .OrderBy(t => t.id)
                                .AsQueryable();
                        //

                        response.competizioni = new List<competizione>();
                        foreach (var x in i)
                        {
                            competizione c = new competizione();
                            c.id = x.id;
                            c.nome = x.name;

                            var listEvents = x.elements
                                                .GroupBy(y => y.ID_EVENTO)
                                                .Select(m => new
                                                {
                                                    id_evento = m.Key,
                                                    nome_evento = m.FirstOrDefault().NOME_EVENTO,
                                                    incontri = m.OrderBy(t => t.ORA_POSTICIPO).ThenBy(t => t.ORA).ToList()
                                                })
                                                .OrderBy(t => t.id_evento)
                                                .AsQueryable();

                            if (listEvents != null)
                            {
                                c.eventi = new List<evento>();
                                foreach (var z in listEvents)
                                {
                                    evento e = new evento();
                                    e.id = z.id_evento;
                                    e.nome = z.nome_evento;
                                    //
                                    if (z.incontri != null)
                                    {
                                        IEnumerable<int> listSquadre = z.incontri.Select(t => t.ID_SQUADRA_1).AsEnumerable();
                                        listSquadre = listSquadre.Concat(z.incontri.Select(t => t.ID_SQUADRA_2)).Distinct();
                                        IQueryable<SQUADRE> squadre = ctx.SQUADRE.Where(sq => listSquadre.Contains(sq.ID)).AsQueryable();

                                        e.incontri = new List<incontro>();
                                        foreach (var j in z.incontri)
                                        {
                                            incontro inc = new incontro();
                                            inc.calendario = j.NOME_CALENDARIO;
                                            inc.id = j.ID_INCONTRO;
                                            //
                                            #region squadre
                                            squadra s1 = new squadra();
                                            squadra s2 = new squadra();

                                            s1.id = j.ID_SQUADRA_1;
                                            if (s1.id > 0)
                                            {
                                                SQUADRE sq = squadre.Where(b => b.ID == s1.id).FirstOrDefault();
                                                s1.nome = sq.NomeSquadra;
                                                s1.logo = sq.LogoSquadra;

                                            }
                                            else
                                                s1.nome = constMsg._squadraVincente;

                                            s2.id = j.ID_SQUADRA_2;
                                            if (s2.id > 0)
                                            {
                                                SQUADRE sq = squadre.Where(b => b.ID == s2.id).FirstOrDefault();
                                                s2.nome = sq.NomeSquadra;
                                                s2.logo = sq.LogoSquadra;

                                            }
                                            else
                                                s2.nome = constMsg._squadraVincente;

                                            inc.squadra1 = s1;
                                            inc.squadra2 = s2;
                                            #endregion
                                            #region campo
                                            campo c1 = new campo();
                                            c1.id = j.ID_CAMPO;
                                            if (j.ID_CAMPO_RECUPERO != 0 && !string.IsNullOrEmpty(j.ID_CAMPO_RECUPERO.ToString()))
                                                c1.id = (int)j.ID_CAMPO_RECUPERO;

                                            CAMPI campi = ctx.CAMPI.Where(t => t.ID == c1.id).FirstOrDefault();
                                            c1.nome = campi.NOMECAMPO;



                                            if (!string.IsNullOrEmpty("" + campi.GMAP))
                                            {
                                                string gmap = campi.GMAP.ToString().Replace("&amp;ll=", "&ll=");
                                                int index = gmap.IndexOf("&ll=");
                                                if (index > 0)
                                                {
                                                    gmap = gmap.Substring(index + 4, gmap.Length - (index + 4));
                                                    index = gmap.IndexOf("&");
                                                    if (index > 0)
                                                    {
                                                        char[] separator = new char[] { ',' };
                                                        string[] strArray = gmap.Substring(0, index).Split(separator);
                                                        if (strArray.Length != 0)
                                                        {
                                                            c1.googleLat = strArray[0];
                                                            c1.googleLon = strArray[1];
                                                        }
                                                    }
                                                }
                                            }
                                            inc.campo = c1;

                                            #endregion
                                            #region data e ora
                                            inc.data = j.DATA_POSTICIPO != null ? ((DateTime)j.DATA_POSTICIPO).ToString("dd/MM/yyyy") : ((DateTime)j.DATA_EVENTO).ToString("dd/MM/yyyy");
                                            inc.ora = !string.IsNullOrEmpty(j.ORA_POSTICIPO) ? j.ORA_POSTICIPO : j.ORA;
                                            #endregion
                                            #region risultati e live
                                            inc = SetRisultatoLive(ctx, inc);
                                            #endregion

                                            e.incontri.Add(inc);
                                        }
                                    }
                                    c.eventi.Add(e);
                                }
                            }

                            response.competizioni.Add(c);
                        }
                        //
                        response.status = 1;
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


        private incontro SetRisultatoLive(absoluteEntities c, incontro iInput)
        {
            #region risultati e live
            LS_INCONTRI ls = c.LS_INCONTRI
                                    .Where(t => t.ID_INCONTRO == iInput.id && t.ABILITATO == 1).FirstOrDefault();
            if (ls == null)
            {

                iInput.risultato1 = "-";
                iInput.risultato2 = "-";
                iInput.flLive = false;
            }
            else
            {
                iInput.flLive = ls.FL_LIVE == 1 ? true : false;
                iInput.risultato1 = ls.FL_LIVE == 1 ? "0" : "-";
                iInput.risultato2 = ls.FL_LIVE == 1 ? "0" : "-";


                iInput.risultato1 = ls.PUNTEGGIO_1 != null ? ls.PUNTEGGIO_1.ToString() : "0";
                iInput.risultato2 = ls.PUNTEGGIO_2 != null ? ls.PUNTEGGIO_2.ToString() : "0";
            }

            INCONTRI_RISULTATI_V ir = c.INCONTRI_RISULTATI_V.Where(t => t.ID == iInput.id).FirstOrDefault();
            if (ir != null)
            {
                iInput.risultato1 = ir.PUNTEGGIO_1.ToString();
                iInput.risultato2 = ir.PUNTEGGIO_2.ToString();
            }

            #endregion
            return iInput;
        }

    }
}