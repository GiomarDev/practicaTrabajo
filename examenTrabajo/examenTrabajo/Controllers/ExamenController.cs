using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using examenTrabajo.Models;

namespace examenTrabajo.Controllers
{
    public class ExamenController : Controller
    {
        //Cambian de Conexion
        SqlConnection con = new SqlConnection("server=52.3.159.208;database=BDSALI_TEST;uid=User_Test;pwd=Test2021$$");

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult iniciarSesion(string codigo, string pass)
        {
            int resultado = -1;

            SqlCommand sqlCommand = new SqlCommand("SP_BUSCAR_USUARIO", con);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@username_usu", codigo);
            sqlCommand.Parameters.AddWithValue("@userpassword_usu", pass);

            con.Open();
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            if (sqlDataReader.Read())
            {
                Usuario usuario = new Usuario();
                usuario.codigo = sqlDataReader.GetString(0);
                usuario.pass = sqlDataReader.GetString(1);
                resultado = 1;
            }
            sqlDataReader.Close();
            con.Close();

            if (resultado == 1)
            {
                return RedirectToAction("listadoProducto", "Producto", new { USUARIO = codigo });
            }
            else
            {
                TempData["Error"] = "Usuario y/o contraseña incorrecta";
                return RedirectToAction("Index");
            }
        }
    }
}