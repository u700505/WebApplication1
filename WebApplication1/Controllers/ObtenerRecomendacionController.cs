using Microsoft.Azure.Mobile.Server.Config;
using System.Collections.Generic;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.DataObjects;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [MobileAppController]
    public class ObtenerRecomendacionController : ApiController
    {
        public HttpResponseMessage Post(PeticionRecomendacion peticionRecomendacion)
        {
            MobileServiceContext context = new MobileServiceContext();
            Partidos GanadosGlobal1 = context.Database.SqlQuery<Partidos>("SELECT COUNT(Ganador) " + "AS 'n' " + "FROM Partidos " + "WHERE Ganador = " + "@TenistaUno" + ";",
            new SqlParameter("@TenistaUno", peticionRecomendacion.TenistaUno)).FirstOrDefault();
            Partidos PerdidosGlobal1 = context.Database.SqlQuery<Partidos>("SELECT COUNT(Perdedor) " + "AS 'n' " + "FROM Partidos " + "WHERE Perdedor = " + "@TenistaUno" + ";",
            new SqlParameter("@TenistaUno", peticionRecomendacion.TenistaUno)).FirstOrDefault();
            Partidos GanadosGlobal2 = context.Database.SqlQuery<Partidos>("SELECT COUNT(Ganador) " + "AS 'n' " + "FROM Partidos " + "WHERE Ganador = " + "@TenistaDos" + ";",
            new SqlParameter("@TenistaDos", peticionRecomendacion.TenistaDos)).FirstOrDefault();
            Partidos PerdidosGlobal2 = context.Database.SqlQuery<Partidos>("SELECT COUNT(Perdedor) " + "AS 'n' " + "FROM Partidos " + "WHERE Perdedor = " + "@TenistaDos" + ";",
            new SqlParameter("@TenistaDos", peticionRecomendacion.TenistaDos)).FirstOrDefault();

            Partidos GanadosSuperficie1 = context.Database.SqlQuery<Partidos>("SELECT COUNT(Ganador) " + "AS 'n' " + "FROM Partidos " + "WHERE Ganador = " + "@TenistaUno" + " AND Superficie = " + "@Superficie" + ";",
            new SqlParameter("@TenistaUno", peticionRecomendacion.TenistaUno), new SqlParameter("@Superficie", peticionRecomendacion.Superficie)).FirstOrDefault();
            Partidos PerdidosSuperficie1 = context.Database.SqlQuery<Partidos>("SELECT COUNT(Perdedor) " + "AS 'n' " + "FROM Partidos " + "WHERE Perdedor = " + "@TenistaUno" + " AND Superficie = " + "@Superficie" + ";",
            new SqlParameter("@TenistaUno", peticionRecomendacion.TenistaUno), new SqlParameter("@Superficie", peticionRecomendacion.Superficie)).FirstOrDefault();
            Partidos GanadosSuperficie2 = context.Database.SqlQuery<Partidos>("SELECT COUNT(Ganador) " + "AS 'n' " + "FROM Partidos " + "WHERE Ganador = " + "@TenistaDos" + " AND Superficie = " + "@Superficie" + ";",
            new SqlParameter("@TenistaDos", peticionRecomendacion.TenistaDos), new SqlParameter("@Superficie", peticionRecomendacion.Superficie)).FirstOrDefault();
            Partidos PerdidosSuperficie2 = context.Database.SqlQuery<Partidos>("SELECT COUNT(Perdedor) " + "AS 'n' " + "FROM Partidos " + "WHERE Perdedor = " + "@TenistaDos" + " AND Superficie = " + "@Superficie" + ";",
            new SqlParameter("@TenistaDos", peticionRecomendacion.TenistaDos), new SqlParameter("@Superficie", peticionRecomendacion.Superficie)).FirstOrDefault();

            Partidos GanadosDirecto1 = context.Database.SqlQuery<Partidos>("SELECT COUNT(Ganador) " + "AS 'n' " + "FROM Partidos " + "WHERE Ganador = " + "@TenistaUno" + " AND Perdedor = " + "@TenistaDos" + ";",
            new SqlParameter("@TenistaUno", peticionRecomendacion.TenistaUno), new SqlParameter("@TenistaDos", peticionRecomendacion.TenistaDos)).FirstOrDefault();
            Partidos GanadosDirecto2 = context.Database.SqlQuery<Partidos>("SELECT COUNT(Ganador) " + "AS 'n' " + "FROM Partidos " + "WHERE Ganador = " + "@TenistaDos" + " AND Perdedor = " + "@TenistaUno" + ";",
            new SqlParameter("@TenistaDos", peticionRecomendacion.TenistaDos), new SqlParameter("@TenistaUno", peticionRecomendacion.TenistaUno)).FirstOrDefault();



            decimal Perdidos_Global1 = PerdidosGlobal1.n;
            decimal Ganados_Global1 = GanadosGlobal1.n;
            decimal global1 = Ganados_Global1 / (Ganados_Global1 + Perdidos_Global1) * 100;

            decimal Perdidos_Global2 = PerdidosGlobal2.n;
            decimal Ganados_Global2 = GanadosGlobal2.n;
            decimal global2 = Ganados_Global2 / (Ganados_Global2 + Perdidos_Global2) * 100;

            decimal Perdidos_Superficie1 = PerdidosSuperficie1.n;
            decimal Ganados_Superficie1 = GanadosSuperficie1.n;
            decimal superficie1;
            if (Ganados_Superficie1 != 0 | Perdidos_Superficie1 != 0){
                superficie1 = Ganados_Superficie1 / (Ganados_Superficie1 + Perdidos_Superficie1) * 100;
            }
            else{
                superficie1 = 0;
            }

            decimal Perdidos_Superficie2 = PerdidosSuperficie2.n;
            decimal Ganados_Superficie2 = GanadosSuperficie2.n;
            decimal superficie2;
            if (Ganados_Superficie2 != 0 | Perdidos_Superficie2 != 0)
            {
                superficie2 = Ganados_Superficie2 / (Ganados_Superficie2 + Perdidos_Superficie2) * 100;
            }
            else
            {
                superficie2 = 0;
            }

            
            decimal Ganados_Directo1 = GanadosDirecto1.n;
            decimal Ganados_Directo2 = GanadosDirecto2.n;
            decimal directos1;
            decimal directos2;
            if (Ganados_Directo1 != 0 | Ganados_Directo2 != 0)
            {
                directos1 = Ganados_Directo1 / (Ganados_Directo1 + Ganados_Directo2) * 100;
                directos2 = Ganados_Directo2 / (Ganados_Directo1 + Ganados_Directo2) * 100;
            }
            else
            {
                directos1 = 0;
                directos2 = 0;
            }



            RespuestaRecomendacion respuestaRecomendacion = new RespuestaRecomendacion();
            respuestaRecomendacion.Global1 = global1;
            respuestaRecomendacion.Global2 = global2;
            respuestaRecomendacion.Superficie1 = superficie1;
            respuestaRecomendacion.Superficie2 = superficie2;
            respuestaRecomendacion.Directo1 = directos1;
            respuestaRecomendacion.Directo2 = directos2;
            return this.Request.CreateResponse(HttpStatusCode.OK, respuestaRecomendacion);
        }
    }
}
