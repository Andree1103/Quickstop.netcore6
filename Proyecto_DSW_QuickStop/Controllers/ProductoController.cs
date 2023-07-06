using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto_DSW_QuickStop.Models;

using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Proyecto_DSW_QuickStop.Controllers
{
    public class ProductoController : Controller
    {
        //CONEXION A BD --------------------------------------->
        private readonly IConfiguration _configuration;
        private string cad_cn = "";

        //Constructor ctor y tab
        public ProductoController(IConfiguration _config)
        {
            _configuration = _config;
            //recuperar la cadena de conexion almacenada con el nombre cn1
            cad_cn = _configuration.GetConnectionString("cn1");
        }
        //CONEXION A BD --------------------------------------->

        List<ProductosModel> ProductosPorNombre(string nom)
        {
            List<ProductosModel> lista = new List<ProductosModel>();
            using (SqlConnection cnx = new SqlConnection(cad_cn))
            {
                cnx.Open();
                //
                SqlCommand cmd = new SqlCommand("pa_listar_productos_nombre", cnx);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //
                cmd.Parameters.AddWithValue("@nom", nom);
                //
                ProductosModel obj = null;
                //
                SqlDataReader dr = cmd.ExecuteReader();
                //
                //
                while (dr.Read())
                {
                    obj = new ProductosModel()
                    {
                        codProd = dr.GetString(0),
                        nomProd = dr.GetString(1),
                        codCat = dr.GetString(2),
                        preProd = dr.GetDecimal(3),
                        stokProd = dr.GetInt32(4)
                        //eliProd = dr.GetString(5)

                    };
                    lista.Add(obj);
                }
                dr.Close();
            }//despues de cerrar la llave , Todos los objetos creados dentro del using seran destruidos por
            //visual
            return lista;
        }

        public IActionResult IndexProductosPorNombre(string nom = "", int nropagina = 0)
        {

            var listado = ProductosPorNombre(nom);
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

        //lista de Categorias 

        private List<CategoriaCombo> ListaCategorias()
        {
            List<CategoriaCombo> lista = new List<CategoriaCombo>();
            //
            SqlDataReader dr =
        SqlHelper.ExecuteReader(cad_cn, "PA_LISTAR_CATEGORIA");
            //
            CategoriaCombo obj = null;
            while (dr.Read())
            {
                obj = new CategoriaCombo()
                {
                    codCate = dr.GetString(0),
                    nomCate = dr.GetString(1)
                };
                //
                lista.Add(obj);
            }
            dr.Close();
            //
            return lista;
        }


        // GET: ProductoController/Create
        public ActionResult CreateProductos()
        {
            ProductosModel obj = new ProductosModel();

            ViewBag.Categorias = new SelectList(ListaCategorias(), "codCate", "nomCate");
            return View(obj);
        }

        // POST: ProductoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProductos(ProductosModel obj)

        {
            //
            if (ModelState.IsValid == true)
            {
                SqlHelper.ExecuteNonQuery(cad_cn, "PA_GRABAR_PRODUCTO",
                   obj.codProd, obj.nomProd, obj.codCat, obj.preProd, obj.stokProd
                    );

                //
                //
                ViewBag.MENSAJE = "Producto Registrado correctamente";
            }

            ViewBag.Categorias = new SelectList(ListaCategorias(), "codCate", "nomCate");

            return View(obj);
        }

        // GET: ClienteController/Edit/5
        public ActionResult EditProductos(string id, string nom)
        {
            //buscamos al cliente por su codigo
            ProductosModel? buscado = ProductosPorNombre(nom).Find(cli => cli.codProd.Equals(id));

            ViewBag.Categorias =  new SelectList(ListaCategorias(), "codCate", "nomCate");


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
        public ActionResult EditProductos(string id, ProductosModel obj)
        {
            try
            {
                //
                if (ModelState.IsValid == true)
                {
                    SqlHelper.ExecuteNonQuery(cad_cn, "PA_GRABAR_PRODUCTO",
                      obj.codProd, obj.nomProd, obj.codCat, obj.preProd, obj.stokProd
                    );
                    //
                    TempData["MENSAJE"] = $"El Producto con el Codigo {obj.codProd} " +
                              "Fue Actualizado correctamente";
                }
                //
                //return RedirectToAction(nameof(IndexClientes)); // ASP NET CORE MVC
                return RedirectToAction("IndexProductosPorNombre"); // ASP NET MVC
            }
            catch (Exception ex)
            {
                ViewBag.MENSAJE = "Error: " + ex.Message;
            }
            //

            ViewBag.Categorias = new SelectList(ListaCategorias(), "codCate", "nomCate");

            return View(obj);
        }

        // GET: ProductoController/Details/5

        public ActionResult DetailsProductos(string id)
        {
            ProductosModel? buscado = ProductosPorNombre("").Find(c => c.codProd.Equals(id));
            return View(buscado);
        }


        // GET: ClientesController/Delete/5
        public ActionResult DeleteProductos(string id, string nom)
        {
            ProductosModel? buscado = ProductosPorNombre(nom).Find(c => c.codProd.Equals(id));
            return View(buscado);
        }

        // POST: ClientesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteProductos(string id, string nom, IFormCollection collection)
        {//
            ProductosModel? buscado = ProductosPorNombre(nom).Find(c => c.codProd.Equals(id));

            //
            try
            {
                SqlHelper.ExecuteNonQuery(cad_cn, "PA_ELIMINAR_PRODUCTO", id);
                //
                TempData["MENSAJE"] = $"El Producto con el Codigo {id} " +
                            "Fue Eliminado de forma logica";
                //
                return RedirectToAction("IndexProductosPorNombre");
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
