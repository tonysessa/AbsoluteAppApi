//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AbsoluteAPI.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class CAMPI
    {
        public int ID { get; set; }
        public Nullable<System.DateTime> DATA_INS { get; set; }
        public string LOGIN_INS { get; set; }
        public Nullable<System.DateTime> DATA_AGG { get; set; }
        public string LOGIN_AGG { get; set; }
        public string NOMECAMPO { get; set; }
        public string CITTA { get; set; }
        public string DESCRIZIONE { get; set; }
        public int STATO { get; set; }
        public string LATITUDINE_GMAP { get; set; }
        public string LONGITUDINE_GMAP { get; set; }
        public string ZOOM_GMAP { get; set; }
        public string GMAP { get; set; }
        public string CONVENZIONATO { get; set; }
    }
}