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
    
    public partial class Sp_ProssimeGare_Result
    {
        public int ID_COMPETIZIONE { get; set; }
        public string NOME { get; set; }
        public string NOME_EVENTO { get; set; }
        public int ID_EVENTO { get; set; }
        public string TIPO_EVENTO { get; set; }
        public string ORDINE_CLASSIFICA { get; set; }
        public int ID_INCONTRO { get; set; }
        public Nullable<System.DateTime> DATA_EVENTO { get; set; }
        public Nullable<System.DateTime> DATA_POSTICIPO { get; set; }
        public int ID_CAMPO { get; set; }
        public Nullable<int> ID_CAMPO_RECUPERO { get; set; }
        public string ORA { get; set; }
        public string ORA_POSTICIPO { get; set; }
        public int ID_SQUADRA_1 { get; set; }
        public int ID_SQUADRA_2 { get; set; }
        public int ID_CALENDARIO { get; set; }
        public string NOME_CALENDARIO { get; set; }
    }
}
