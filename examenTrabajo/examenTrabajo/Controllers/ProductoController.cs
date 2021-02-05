using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using examenTrabajo.Models;

namespace examenTrabajo.Controllers
{
    public class ProductoController : Controller
    {
        //Cambian de Conexion
        SqlConnection con = new SqlConnection("server=52.3.159.208;database=BDSALI_TEST;uid=User_Test;pwd=Test2021$$");

        public ActionResult Index()
        {
            return View();
        }

        List<Producto> ListProducto()
        {
            List<Producto> aProd = new List<Producto>();
            con.Open();
            SqlCommand cmd = new SqlCommand("SP_LISTA_PRODUCTOS", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                aProd.Add(new Producto()
                {
                    productID = int.Parse(dr[0].ToString()),
                    productCode = dr[1].ToString(),
                    description = dr[2].ToString(),
                    quantity = int.Parse(dr[3].ToString())

                });
            }

            dr.Close();
            con.Close();
            return aProd;
        }

        public ActionResult listadoProducto()
        {
            return View(ListProducto());
        }


        void Crud(string proceso, List<SqlParameter> parametros)
        {
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand(proceso, con);
                cmd.CommandType = CommandType.StoredProcedure;
                //Soportar todos los parametros y hacer un match
                cmd.Parameters.AddRange(parametros.ToArray());
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
            }
            cn.Close();
        }

        //public ActionResult nuevoProducto(Producto objP, HttpPostedFileBase f)
        //{
        //    if (f == null)
        //    {
        //        return View(objP);
        //    }

        //    List<SqlParameter> parametros = new List<SqlParameter>()
        //    { 
        //        //Parametros de registro            
        //        new SqlParameter(){ParameterName="@PRODUCTID", SqlDbType=SqlDbType.VarChar,Value=objP.productID},
        //        new SqlParameter(){ParameterName="@PRODCODE", SqlDbType=SqlDbType.Int,Value=objP.productCode},
        //        new SqlParameter(){ParameterName="@DESCR", SqlDbType=SqlDbType.Int,Value=objP.description},
        //        new SqlParameter(){ParameterName="@QUANTITY", SqlDbType=SqlDbType.Int,Value=objP.quantity}
        //    };
           
        //    Crud("SP_NUEVO_PROD", parametros);
        //    return RedirectToAction("listadoPc");
        //}

    }
}