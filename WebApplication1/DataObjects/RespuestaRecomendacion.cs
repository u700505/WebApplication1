using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplication1.DataObjects
{
    public class RespuestaRecomendacion
    {
        public decimal Global1 { get; set; }
        public decimal Global2 { get; set; }
        public decimal Superficie1 { get; set; }
        public decimal Superficie2 { get; set; }
        public decimal Directo1 { get; set; }
        public decimal Directo2 { get; set; }
    }
}