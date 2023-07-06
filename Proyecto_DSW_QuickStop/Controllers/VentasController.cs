//IMPORTA
using Proyecto_DSW_QuickStop.Models;
using Newtonsoft.Json;


//

using Microsoft.AspNetCore.Mvc.Rendering;
//


using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Proyecto_DSW_QuickStop.Controllers
{
    public class VentasController : Controller
    {

        //1.
        //Definir las variables privadas y de solo lectura
        //que vamos a utilizar
        private readonly VentasDAO dao;

        //2.
        public VentasController(VentasDAO _dao)
        {
            dao = _dao;
        }

        //3.
        //Acciones para el carrito de Compra
        // GET: VentasController
        public ActionResult ListarProducto(int nropagina = 0)
        {
            //HOY ------> Crear la varibales de sesion si es que no existe (==null)<-------
            if (HttpContext.Session.GetString("carrito") == null)
            {
                HttpContext.Session.SetString("carrito",
                    //Serializando de JSON
                    JsonConvert.SerializeObject(
                        new List<CarritoModel>()
                        ));
            }
            //FIN HOY--------------------------------------------------------


            //comprobar la existencia de la variable de session 
            var listado = dao.GetProductos(); //Esto es el listado del DAO.
            //return View(listado);

            // ---------------------------------------------------------
            int filas_pagina = 6;
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

        //*-------------------------------------------------------------------------------------------------
        //ES un DETAILS
        //4. //viene de VentasDAO
        //Agregar Carrito //CON LA PLANTILLA DE DETALLE.
        //GET: VentasController/Details/
        public ActionResult AgregarCarrito(string id) //el id puede ser int
        {
            return View(dao.BuscarProducto(id));

        }
        //HOY --> Agregar un action de tipo Post AgregarCarrito

        [HttpPost]
        public ActionResult AgregarCarrito(string id, int n_cantidad)
        {
            //Buscar los datos del producto Seleccionado (codido==> id)
            ProductosModel objprod = dao.BuscarProducto(id);
            //Asignar los datos del producto a un objeto de carrito de compra
            CarritoModel cm = new CarritoModel()
            {
                codigo = objprod.codProd,
                nombre = objprod.nomProd,
                precio = objprod.preProd,
                cantidad = n_cantidad,
                //importe = precio * cantidad

            };

            //Recuperar de la session la lista generica que almacena los
            //productos del carrito (deserializar la session)
            var lista_carrito =
                JsonConvert.DeserializeObject<List<CarritoModel>>(
                    HttpContext.Session.GetString("carrito"));

            var buscado = lista_carrito.Find(
                c => c.codigo.Equals(cm.codigo));



            //Si el producto nuevo para para agregar al carrito de compra 
            //NO EXISTE en el carrito. entonces lo agregamos
            if (buscado == null)
            {
                //Agregar el nuevo objeto del carrito a la lista generica
                lista_carrito.Add(cm);
                ViewBag.Mensaje = "Articulo Agregado con Exito al CARRITO DE COMPRA";

            }
            else //si existe en el carrito, actualizamos su cantidad
            {
                buscado.cantidad += cm.cantidad;
                ViewBag.Mensaje = "Cantidad Actualizada al CARRITO DE COMPRA";
            }
            //
            //Guardar la lista generica en la variable se session
            HttpContext.Session.SetString("carrito",
                JsonConvert.SerializeObject(lista_carrito)
                );


            return View(objprod);
        }
        //FIN HOY--------------------------------------------------------


        //GET : VER EL CARRITO
        public ActionResult VerCarrito()
        {
            List<CarritoModel> lista = new List<CarritoModel>();

            //Recuperar desde la session carriot de la lista generica
            //del carrito de compra
            if (HttpContext.Session.GetString("carrito") != null)
            {
                lista = JsonConvert.DeserializeObject<List<CarritoModel>>(
                    HttpContext.Session.GetString("carrito")
                );
            }
            //
            ViewBag.TOTAL = lista.Sum(c => c.importe);
            //
            return View(lista);
        }


        [HttpPost]
        public ActionResult EliminarProducto(string id)
        {
            //Recuperar desde la session carriot de la lista generica
            //del carrito de compra

            var lista = JsonConvert.DeserializeObject<List<CarritoModel>>(
                HttpContext.Session.GetString("carrito")
            );

            //Buscar producto por su id para eliminarlo
            CarritoModel buscado = lista.Find(c => c.codigo.Equals(id));
            lista.Remove(buscado);

            //Grabar la sesion
            HttpContext.Session.SetString("carrito",
                JsonConvert.SerializeObject(lista)
                );
            //Nos dirigimos a la vista del carrito de compra
            return RedirectToAction("VerCarrito");
        }


        //--------------------04-06-2023------------------------------------*/
        List<CarritoModel> TraerCarrito()
        {
            List<CarritoModel>? lista =
                JsonConvert.DeserializeObject<List<CarritoModel>>(
                    HttpContext.Session.GetString("carrito")
                    );
            return lista;
        }


        public ActionResult PagarCarrito()
        {
            //*-------------- LISTADO DE CLIENTE -----------------------*//

            // recuperar y guardar en un viewbag la lista de clientes
            ViewBag.CLIENTES = new SelectList(
              dao.ListaClientes(),
              "cod_cli",
              "nom_cli"
              );
            //*-------------- LISTADO DE CLIENTE ---------------------*//


            //Recuperar la lista de productos del carrito de compra
            var listado = TraerCarrito();

            //enviar los datos a la vista
            return View(listado);

            //return View(TraerCarrito());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PagarCarrito(string cod_cli)
        {
            var listado = TraerCarrito();

            try
            {
                decimal total = listado.Sum(cc => cc.importe);

                //grabando toda la venta de los productos
                string numero = dao.Grabar_Ventas_Web(cod_cli, total, listado);

                ViewBag.MENSAJE = $"La Venta de {numero} fue realizada con exito ";

                ViewBag.GRABO = "Si";

                //destruyendo todas las variables de sesion del usuario
                //HttpContext.Session.Remove("carrito");
                HttpContext.Session.Clear();
            }
            catch (Exception ex)
            {
                ViewBag.MENSAJE = ex.Message;
            }
            //
            // recuperar y guardar en un viewbag la lista de clientes
            ViewBag.CLIENTES = new SelectList(
              dao.ListaClientes(),
              "cod_cli",
              "nom_cli"
              );
            return View(listado);
        }





        // GET: VentasController
        public ActionResult Index()
        {
            return View();
        }

        // GET: VentasController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: VentasController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VentasController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: VentasController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: VentasController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: VentasController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: VentasController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
