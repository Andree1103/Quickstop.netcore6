using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

//IMPORTAR
using Proyecto_DSW_QuickStop.Models;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Proyecto_DSW_QuickStop.Controllers
{
    public class CateController : Controller
    {

      
    // -------------------------------------------------------------------------//

    private string cad_cn = "";
        public CateController(IConfiguration config)
        {
            cad_cn = config.GetConnectionString("cn1");
        }
        // -------------------------------------------------------------------------//

        // -------------------------------------------------------------------------//
        List<CategoriaModel> ListaCategoriaNombre(string nombre)
        {
            List<CategoriaModel> lista = new List<CategoriaModel>();
            SqlDataReader dr = SqlHelper.ExecuteReader(cad_cn, "PA_LISTAR_categoria_nombre", nombre);
            CategoriaModel obj = null;
            while (dr.Read())
            {
                obj = new CategoriaModel()
                {
                    cod_cat = dr.GetString(0),
                    nombre_categoria = dr.GetString(1),
                    cod_marca = dr.GetInt32(2)
                };
                lista.Add(obj);
            }
            dr.Close();
            return lista;
        }

        public IActionResult IndexCategoriaNombre(string nombre = "", int nropagina = 0)
        {
            if (TempData["MENSAJE"] != null)
                ViewBag.Mensaje = TempData["MENSAJE"].ToString();
            var listado = ListaCategoriaNombre(nombre);
            ViewBag.NOMBRE = nombre;
            int filas_pagina = 5;
            // cantidad total de filas del listado
            int cantidad = listado.Count;
            // obtener el numero de página a mostrar
            int paginas = 1;
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
        // --------------------------------------------------------


        //LISTA COMBO DE DISTRITOS
        private List<MarcaModel> ListaMarcas()
        {
            List<MarcaModel> lista = new List<MarcaModel>();
            //
            SqlDataReader dr =
        SqlHelper.ExecuteReader(cad_cn, "PA_LISTAR_MARCA");
            //
            MarcaModel obj = null;
            while (dr.Read())
            {
                obj = new MarcaModel()
                {
                    cod_marca= dr.GetInt32(0),
                    nombre_marca = dr.GetString(1)
                };
                //
                lista.Add(obj);
            }
            dr.Close();
            //
            return lista;
        }

        // GET: CateController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CateController/Details/5
        public ActionResult DetailsCat(String id,string nombre)
        {
            CategoriaModel? buscado = ListaCategoriaNombre(nombre).
                                 Find(cli => cli.cod_cat.Equals(id));

            return View(buscado);
        }
        // GET: CateController/Create
        public ActionResult CreateCat()
        {
            CategoriaModel obj = new CategoriaModel();

            ViewBag.DISTRITOS = new SelectList(ListaMarcas(), "cod_marca", "nombre_marca");
            return View(obj);
        }

        // POST: CateController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCat(CategoriaModel obj)
        {
            try
            {
                if (ModelState.IsValid == true)
                {
                    SqlHelper.ExecuteNonQuery(cad_cn, "PA_GRABAR_CATEGORIA",
                        obj.cod_cat, obj.nombre_categoria, obj.cod_marca
                        );
                    //
                    ViewBag.MENSAJE = "Categoria Registrada correctamente";
                }
            }
            catch (Exception ex)
            {
                ViewBag.MENSAJE = ex.Message;
            }

            ViewBag.DISTRITOS = new SelectList(ListaMarcas(), "cod_marca", "nombre_marca");

            return View(obj);
        }

        // GET: CateController/Edit/5
        public ActionResult EditCat(string id,string nombre)
        {
            CategoriaModel? buscado = ListaCategoriaNombre(nombre).
                                 Find(cli => cli.cod_cat.Equals(id));

            ViewBag.DISTRITOS = new SelectList(ListaMarcas(), "cod_marca", "nombre_marca");

            return View(buscado);
        }

        // POST: CateController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCat(String id, CategoriaModel obj)
        {
            try
            {
                if (ModelState.IsValid == true)
                {
                    SqlHelper.ExecuteNonQuery(cad_cn, "PA_GRABAR_CATEGORIA",
                        obj.cod_cat, obj.nombre_categoria, obj.cod_marca
                        );
                    //
                    TempData["MENSAJE"] = $"La Categoria con el Codigo {obj.cod_cat} " +
                         "Fue Actualizado correctamente";
                }
                return RedirectToAction("IndexCategoriaNombre"); //para que vuelva a la lista
            }
            catch (Exception ex)
            {
                ViewBag.MENSAJE = "Error" + ex.Message;
            }
            ViewBag.DISTRITOS = new SelectList(ListaMarcas(), "cod_marca", "nombre_marca");

            //
            return View(obj);
        }

        // GET: CateController/Delete/5
        public ActionResult DeleteCat(String id,string nombre)
        {
            CategoriaModel? buscado = ListaCategoriaNombre(nombre).
                                 Find(cli => cli.cod_cat.Equals(id));

            return View(buscado);
        }

        // POST: CateController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCat(string id , string nombre,IFormCollection collection)
        {
            CategoriaModel? buscado = ListaCategoriaNombre(nombre).
                                Find(cli => cli.cod_cat.Equals(id));

            try
            {
                SqlHelper.ExecuteNonQuery(cad_cn, "PA_ELIMINAR_CATEGORIA", id);
                //
                TempData["MENSAJE"] = $"La Categoria con el Codigo {id} " +
                        "!Fue Eliminado de Forma Logica!";
                //
                return RedirectToAction("IndexCategoriaNombre");
            }
            catch (Exception ex)
            {
                ViewBag.MENSAJE = "Error" + ex.Message;
            }
            return View(buscado);
        }


    }
}
