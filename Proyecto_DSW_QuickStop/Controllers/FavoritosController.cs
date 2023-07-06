using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Proyecto_DSW_QuickStop.Models;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using Proyecto_DSW_QuickStop.Modelos;


namespace Proyecto_DSW_QuickStop.Controllers
{
    public class FavoritosController : Controller
    {
        // GET: MedicosController
        public async Task<ActionResult> IndexFavoritos()
        {
            var listado = new List<Favoritos>();

            using (HttpClient cliente = new HttpClient()) 
            {
                // realizar la solicitud GET
                var respuesta =
                    await cliente.GetAsync("http://localhost:5182/api/FavoritosAPI/GetFavoritos");

                // convertir el contenido a una cadena
                string respuestaAPI = await respuesta.Content.ReadAsStringAsync();

                // deserializar la cadena (Json) a Lista Genérica de Medicos
                listado = JsonConvert.DeserializeObject<List<Favoritos>>(respuestaAPI);
            }

            return View(listado);
        }

        // GET: MedicosController/Details/5
        public async Task<ActionResult> DetailsMedicos(string id)
        {
            Favoritos? obj = new Favoritos();
            // permite realizar una solicitud al servicio web api
            using (var cliente = new HttpClient())
            {
                // realizamos una solicitud Get
                var respuesta =
                       await cliente.GetAsync("http://localhost:5182/api/FavoritosAPI/GetFavoritos/" + id);

                // convertimos el contenido de la variable respuesta a una cadena                
                string respuestaPI = await respuesta.Content.ReadAsStringAsync();

                // para despues deserializarlo al formato Json de un objeto Medicos
                obj = JsonConvert.DeserializeObject<Favoritos>(respuestaPI);
            }
            //               
            return View(obj);
        }

        // GET: MedicosController/Create
        public async Task<ActionResult> CreateMedicos()
        {
            // para el dropdownlist de la Especialidad
            var listaesp = new List<ClienteModel>();

            using (HttpClient cliente = new HttpClient())
            {
                // realizar la solicitud GET
                var respuesta =
                    await cliente.GetAsync("http://localhost:5182/api/FavoritosAPI/GetClientesFavorito");

                // convertir el contenido a una cadena
                string respuestaAPI = await respuesta.Content.ReadAsStringAsync();

                // deserializar la cadena (Json) a Lista Genérica de Especialidades
                listaesp = JsonConvert.DeserializeObject<List<ClienteModel>>(respuestaAPI);
            }


            var listaesp2 = new List<Producto>();

            using (HttpClient cliente = new HttpClient())
            {
                // realizar la solicitud GET
                var respuesta =
                    await cliente.GetAsync("http://localhost:5182/api/FavoritosAPI/GetProductosFavorito");

                // convertir el contenido a una cadena
                string respuestaAPI = await respuesta.Content.ReadAsStringAsync();

                // deserializar la cadena (Json) a Lista Genérica de Especialidades
                listaesp2 = JsonConvert.DeserializeObject<List<Producto>>(respuestaAPI);
            }

            //
            ViewBag.Cliente = new SelectList( 
                                     listaesp, "cod_cli", "nom_cli");

            ViewBag.Producto = new SelectList(
                                     listaesp2, "cod_prod", "nom_prod");

            // para el dropdownlist de los distritos

            return View(new Favoritos());
        }






        // POST: MedicosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateMedicos(Favoritos obj)
        {
            // permite realizar una solicitud al servicio web api
            using (var cliente = new HttpClient())
            {
                obj.eliminado = "No";
                StringContent contenido = new StringContent(
                   JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");

                var respuesta =
                      await cliente.PostAsync("http://localhost:5182/api/FavoritosAPI/PostFavoritos", contenido);

                string respuestaAPI = await respuesta.Content.ReadAsStringAsync();
                ViewBag.MENSAJE = respuestaAPI;
            }

            // para el dropdownlist de la Especialidad
            var listaesp = new List<ClienteModel>();

            using (HttpClient cliente = new HttpClient())
            {
                // realizar la solicitud GET
                var respuesta =
                    await cliente.GetAsync("http://localhost:5182/api/FavoritosAPI/GetClientesFavorito");

                // convertir el contenido a una cadena
                string respuestaAPI = await respuesta.Content.ReadAsStringAsync();

                // deserializar la cadena (Json) a Lista Genérica de Especialidades
                listaesp = JsonConvert.DeserializeObject<List<ClienteModel>>(respuestaAPI);
            }


            var listaesp2 = new List<Producto>();

            using (HttpClient cliente = new HttpClient())
            {
                // realizar la solicitud GET
                var respuesta =
                    await cliente.GetAsync("http://localhost:5182/api/FavoritosAPI/GetProductosFavorito");

                // convertir el contenido a una cadena
                string respuestaAPI = await respuesta.Content.ReadAsStringAsync();

                // deserializar la cadena (Json) a Lista Genérica de Especialidades
                listaesp2 = JsonConvert.DeserializeObject<List<Producto>>(respuestaAPI);
            }

            //
            //
            ViewBag.Cliente = new SelectList(
                                     listaesp, "cod_cli", "nom_cli");

            ViewBag.Producto = new SelectList(
                                     listaesp2, "cod_prod", "nom_prod");

            //
            return View(obj);
        }

        // GET: MedicosController/Edit/5
        public async Task<ActionResult> EditFavoritos(string id)
        {
            Favoritos? obj = new Favoritos();
            // permite realizar una solicitud al servicio web api
            using (var cliente = new HttpClient())
            {
                // realizamos una solicitud Get
                var respuesta = await cliente.GetAsync("http://localhost:5182/api/FavoritosAPI/GetFavoritos/" + id);

                // convertimos el contenido de la variable respuesta a una cadena
                string respuestaPI = await respuesta.Content.ReadAsStringAsync();

                // para despues deserializarlo al formato Json de un objeto Medicos
                obj = JsonConvert.DeserializeObject<Favoritos>(respuestaPI);
            }
            //
            // para el dropdownlist de la Especialidad
            var listaesp = new List<ClienteModel>();

            using (HttpClient cliente = new HttpClient())
            {
                // realizar la solicitud GET
                var respuesta =
                    await cliente.GetAsync("http://localhost:5182/api/FavoritosAPI/GetClientesFavorito");

                // convertir el contenido a una cadena
                string respuestaAPI = await respuesta.Content.ReadAsStringAsync();

                // deserializar la cadena (Json) a Lista Genérica de Especialidades
                listaesp = JsonConvert.DeserializeObject<List<ClienteModel>>(respuestaAPI);
            }


            var listaesp2 = new List<Producto>();

            using (HttpClient cliente = new HttpClient())
            {
                // realizar la solicitud GET
                var respuesta =
                    await cliente.GetAsync("http://localhost:5182/api/FavoritosAPI/GetProductosFavorito");

                // convertir el contenido a una cadena
                string respuestaAPI = await respuesta.Content.ReadAsStringAsync();

                // deserializar la cadena (Json) a Lista Genérica de Especialidades
                listaesp2 = JsonConvert.DeserializeObject<List<Producto>>(respuestaAPI);
            }

            //
            //
            ViewBag.Cliente = new SelectList(
                                     listaesp, "cod_cli", "nom_cli");

            ViewBag.Producto = new SelectList(
                                     listaesp2, "cod_prod", "nom_prod");


            return View(obj);
        }

        // POST: MedicosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditFavoritos(string id, Favoritos obj)
        {
            //Medicos? obj = new Medicos();
            // permite realizar una solicitud al servicio web api
            using (var cliente = new HttpClient())
            {
                obj.eliminado = "No";
                StringContent contenido = new StringContent(
                    JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");

                var respuesta =
                      await cliente.PutAsync("http://localhost:5182/api/FavoritosAPI/PutFavoritos", contenido);

                string respuestaAPI = await respuesta.Content.ReadAsStringAsync();
                ViewBag.MENSAJE = respuestaAPI;
                //
                ViewBag.Exito_update = "Si";
            }
            //
            // para el dropdownlist de la Especialidad
            var listaesp = new List<ClienteModel>();

            using (HttpClient cliente = new HttpClient())
            {
                // realizar la solicitud GET
                var respuesta =
                    await cliente.GetAsync("http://localhost:5182/api/FavoritosAPI/GetClientesFavorito");

                // convertir el contenido a una cadena
                string respuestaAPI = await respuesta.Content.ReadAsStringAsync();

                // deserializar la cadena (Json) a Lista Genérica de Especialidades
                listaesp = JsonConvert.DeserializeObject<List<ClienteModel>>(respuestaAPI);
            }


            var listaesp2 = new List<Producto>();

            using (HttpClient cliente = new HttpClient())
            {
                // realizar la solicitud GET
                var respuesta =
                    await cliente.GetAsync("http://localhost:5182/api/FavoritosAPI/GetProductosFavorito");

                // convertir el contenido a una cadena
                string respuestaAPI = await respuesta.Content.ReadAsStringAsync();

                // deserializar la cadena (Json) a Lista Genérica de Especialidades
                listaesp2 = JsonConvert.DeserializeObject<List<Producto>>(respuestaAPI);
            }

            //
            //
            ViewBag.Cliente = new SelectList(
                                     listaesp, "cod_cli", "nom_cli");

            ViewBag.Producto = new SelectList(
                                     listaesp2, "cod_prod", "nom_prod");
            //
            //
            return View(obj);
        }


        // GET: MedicosController/Delete/5
        public async Task<ActionResult> DeleteMedicos(string id)
        {
            Favoritos? obj = new Favoritos();
            // permite realizar una solicitud al servicio web api
            using (var cliente = new HttpClient())
            {
                // realizamos una solicitud Get
                var respuesta = 
                    await cliente.GetAsync("http://localhost:5182/api/FavoritosAPI/GetFavoritos/" + id);

                // convertimos el contenido de la variable respuesta a una cadena
                string respuestaPI = await respuesta.Content.ReadAsStringAsync();

                // para despues deserializarlo al formato Json de un objeto Medicos
                obj = JsonConvert.DeserializeObject<Favoritos>(respuestaPI);
            }
            //
            return View(obj);
        }

        // POST: MedicosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteMedicos(string id, IFormCollection collection)
        {
            // permite realizar una solicitud al servicio web api
            using (var cliente = new HttpClient())
            {
                var respuesta =
                      await cliente.DeleteAsync("http://localhost:5182/api/FavoritosAPI/DeleteFavoritos/" + id);

                string respuestaAPI = await respuesta.Content.ReadAsStringAsync();

                ViewBag.MENSAJE = respuestaAPI;
            }
            //
            return View();
        }



    }
}
