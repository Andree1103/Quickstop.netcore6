using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

//IMPORTAR
using Proyecto_DSW_QuickStop.Models;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Proyecto_DSW_QuickStop.Controllers
{
    public class ClienteController : Controller
    {
        //CONEXION A BD --------------------------------------->
        private readonly IConfiguration _configuration;
        private string cad_cn = "";

        //Constructor ctor y tab
        public ClienteController(IConfiguration _config)
        {
            _configuration = _config;
            //recuperar la cadena de conexion almacenada con el nombre cn1
            cad_cn = _configuration.GetConnectionString("cn1");
        }
        //CONEXION A BD --------------------------------------->

        List<ClienteModelv2> ClientesPorNombre(string nom)
        {
            List<ClienteModelv2> lista = new List<ClienteModelv2>();
            using (SqlConnection cnx = new SqlConnection(cad_cn))
            {
                cnx.Open();
                //
                SqlCommand cmd = new SqlCommand("PA_LISTAR_CLIENTES_NOMBRE", cnx);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //
                cmd.Parameters.AddWithValue("@nom", nom);
                //
                ClienteModelv2 obj = null;
                //
                SqlDataReader dr = cmd.ExecuteReader();
                //
                //
                while (dr.Read())
                {
                    obj = new ClienteModelv2()
                    {
                        codCliente = dr.GetString(0),
                        nomCliente = dr.GetString(1),
                        telCliente = dr.GetString(2),
                        corCliente = dr.GetString(3),
                        dirCliente = dr.GetString(4),
                        credCliente = dr.GetInt32(5),
                        fechaNac = dr.GetDateTime(6),
                        codDistrito = dr.GetInt32(7),
                        //eliCliente = dr.GetString(8),
                    };
                    lista.Add(obj);
                }
                dr.Close();
            }//despues de cerrar la llave , Todos los objetos creados dentro del using seran destruidos por
            //visual
            return lista;
        }
        public IActionResult IndexClientesPorNombre(string nom = "", int nropagina = 0)
        {

            var listado = ClientesPorNombre(nom);
            ViewBag.NOM = nom;
            //PAGINACION
            // datos para la paginación
            // cantidad de filas por página
            int filas_pagina = 5;
            // cantidad total de filas del listado
            int cantidad = listado.Count;
            // obtener el numero de página a mostrar
            int paginas = 0;
            // si el resto sale mayor que cero, división no es exacta
            if (cantidad % filas_pagina > 0)
                paginas = (cantidad / filas_pagina) + 1;
            else
                paginas = cantidad / filas_pagina;
            //
            ViewBag.PAGINAS = paginas;
            //
            return View(listado.Skip(nropagina * filas_pagina).Take(filas_pagina));
        }

        //LISTA COMBO DE DISTRITOS
        private List<DistritoCombo> ListaDistritos()
        {
            List<DistritoCombo> lista = new List<DistritoCombo>();
            //
            SqlDataReader dr =
        SqlHelper.ExecuteReader(cad_cn, "PA_LISTAR_DISTRITO");
            //
            DistritoCombo obj = null;
            while (dr.Read())
            {
                obj = new DistritoCombo()
                {
                    codDistrito = dr.GetInt32(0),
                    nomDistrito = dr.GetString(1)
                };
                //
                lista.Add(obj);
            }
            dr.Close();
            //
            return lista;
        }



        // GET: ClienteController/Create
        public ActionResult CreateClientes()
        {
            ClienteModelv2 obj = new ClienteModelv2();

            ViewBag.Distritos = new SelectList(ListaDistritos(), "codDistrito", "nomDistrito");
            return View(obj);
        }


        // POST: ClienteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateClientes(ClienteModelv2 obj)

        {
            //
            if (ModelState.IsValid == true)
            {
                SqlHelper.ExecuteNonQuery(cad_cn, "PA_GRABAR_CLIENTE",
                   obj.codCliente, obj.nomCliente, obj.telCliente, obj.corCliente, obj.dirCliente, obj.credCliente, obj.fechaNac,
                            obj.codDistrito
                    );

                //
                //
                ViewBag.MENSAJE = "Cliente Registrado correctamente";
            }

            ViewBag.Distritos = new SelectList(ListaDistritos(), "codDistrito", "nomDistrito");

            return View(obj);
        }

        // GET: ClienteController/Edit/5
        public ActionResult EditClientes(string id, string nom)
        {
            //buscamos al cliente por su codigo
            ClienteModelv2? buscado = ClientesPorNombre(nom).Find(cli => cli.codCliente.Equals(id));

            ViewBag.Distritos = new SelectList(ListaDistritos(), "codDistrito", "nomDistrito");

            //ClientesModel buscado = null;
            // foreach (var item in listaClientes()) { 
            //    if (item.cod_cli.Equals(id))
            //        buscado= item;
            //    break;
            // }


            //enviamos los datos a la vista
            return View(buscado);
        }

        // POST: ClienteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditClientes(string id, ClienteModelv2 obj)
        {
            try
            {
                //
                if (ModelState.IsValid == true)
                {
                    SqlHelper.ExecuteNonQuery(cad_cn, "PA_GRABAR_CLIENTE",
                      obj.codCliente, obj.nomCliente, obj.telCliente, obj.corCliente, obj.dirCliente, obj.credCliente, obj.fechaNac,
                            obj.codDistrito
                    );
                    //
                    TempData["MENSAJE"] = $"El Cliente con el Codigo {obj.codCliente} " +
                              "Fue Actualizado correctamente";
                }
                //
                //return RedirectToAction(nameof(IndexClientes)); // ASP NET CORE MVC
                return RedirectToAction("IndexClientesPorNombre"); // ASP NET MVC
            }
            catch (Exception ex)
            {
                ViewBag.MENSAJE = "Error: " + ex.Message;
            }
            //

            ViewBag.Distritos = new SelectList(ListaDistritos(), "codDistrito", "nomDistrito");

            return View(obj);
        }

        // GET: ProductoController/Details/5

        public ActionResult DetailsClientes(string id)
        {
            ClienteModelv2? buscado = ClientesPorNombre("").Find(c => c.codCliente.Equals(id));
            return View(buscado);
        }


        // GET: ClientesController/Delete/5
        public ActionResult DeleteClientes(string id, string nom)
        {
            ClienteModelv2? buscado = ClientesPorNombre(nom).Find(c => c.codCliente.Equals(id));
            return View(buscado);
        }

        // POST: ClientesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteClientes(string id, string nom, IFormCollection collection)
        {//
            ClienteModelv2? buscado = ClientesPorNombre(nom).Find(c => c.codCliente.Equals(id));

            //
            try
            {
                SqlHelper.ExecuteNonQuery(cad_cn, "PA_ELIMINAR_CLIENTE", id);
                //
                TempData["MENSAJE"] = $"El Cliente con el Codigo {id} " +
                            "Fue Eliminado de forma logica";
                //
                return RedirectToAction("IndexClientesPorNombre");
            }
            catch (Exception ex)
            {
                ViewBag.MENSAJE = ex.Message;
            }
            //
            return View(buscado);
        }



    }
}
