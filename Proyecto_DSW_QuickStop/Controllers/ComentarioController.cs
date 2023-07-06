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
    public class ComentarioController : Controller
    {
        // GET: MedicosController
        public async Task<ActionResult> IndexComentarios()
        {
            var listado = new List<Comentarios>();

            using (HttpClient cliente = new HttpClient()) 
            {
                // realizar la solicitud GET
                var respuesta =
                    await cliente.GetAsync("http://localhost:5182/api/ComentariosAPI/GetComentario");

                // convertir el contenido a una cadena
                string respuestaAPI = await respuesta.Content.ReadAsStringAsync();

                // deserializar la cadena (Json) a Lista Genérica de Medicos
                listado = JsonConvert.DeserializeObject<List<Comentarios>>(respuestaAPI);
            }

            return View(listado);
        }

        // GET: MedicosController/Details/5
        public async Task<ActionResult> DetailsMedicos(string id)
        {
            Comentarios? obj = new Comentarios();
            // permite realizar una solicitud al servicio web api
            using (var cliente = new HttpClient())
            {
                // realizamos una solicitud Get
                var respuesta =
                       await cliente.GetAsync("http://localhost:5182/api/ComentariosAPI/GetComentario/" + id);

                // convertimos el contenido de la variable respuesta a una cadena                
                string respuestaPI = await respuesta.Content.ReadAsStringAsync();

                // para despues deserializarlo al formato Json de un objeto Medicos
                obj = JsonConvert.DeserializeObject<Comentarios>(respuestaPI);
            }
            //               
            return View(obj);
        }

        // GET: MedicosController/Create
        public async Task<ActionResult> CreateComentarios()
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

            ViewBag.Cliente = new SelectList( 
                                     listaesp, "cod_cli", "nom_cli");
            // para el dropdownlist de los distritos

            return View(new Comentarios());
        }




        // POST: MedicosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateComentarios(Comentarios obj)
        {
            // permite realizar una solicitud al servicio web api
            using (var cliente = new HttpClient())
            {
                obj.eliminado = "No";
                StringContent contenido = new StringContent(
                   JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");

                var respuesta =
                      await cliente.PostAsync("http://localhost:5182/api/ComentariosAPI/PostComentario", contenido);

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

            //
            ViewBag.Cliente = new SelectList(
                                     listaesp, "cod_cli", "nom_cli");
            //
            return View(obj);
        }

        // GET: MedicosController/Edit/5
        public async Task<ActionResult> EditComentarios(string id)
        {
            Comentarios? obj = new Comentarios();
            // permite realizar una solicitud al servicio web api
            using (var cliente = new HttpClient())
            {
                // realizamos una solicitud Get
                var respuesta = await cliente.GetAsync("http://localhost:5182/api/ComentariosAPI/GetComentario/" + id);

                // convertimos el contenido de la variable respuesta a una cadena
                string respuestaPI = await respuesta.Content.ReadAsStringAsync();

                // para despues deserializarlo al formato Json de un objeto Medicos
                obj = JsonConvert.DeserializeObject<Comentarios>(respuestaPI);
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

            ViewBag.Cliente = new SelectList(
                                     listaesp, "cod_cli", "nom_cli");

       

            return View(obj);
        }

        // POST: MedicosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditComentarios(string id, Comentarios obj)
        {
            //Medicos? obj = new Medicos();
            // permite realizar una solicitud al servicio web api
            using (var cliente = new HttpClient())
            {
                obj.eliminado = "No";
                StringContent contenido = new StringContent(
                    JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");

                var respuesta =
                      await cliente.PutAsync("http://localhost:5182/api/ComentariosAPI/PutComentario", contenido);

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


            

            //
            //
            ViewBag.Cliente = new SelectList(
                                     listaesp, "cod_cli", "nom_cli");

            //
            return View(obj);
        }


        // GET: MedicosController/Delete/5
        public async Task<ActionResult> DeleteComentarios(string id)
        {
            Comentarios? obj = new Comentarios();
            // permite realizar una solicitud al servicio web api
            using (var cliente = new HttpClient())
            {
                // realizamos una solicitud Get
                var respuesta = 
                    await cliente.GetAsync("http://localhost:5182/api/ComentariosAPI/GetComentario/" + id);

                // convertimos el contenido de la variable respuesta a una cadena
                string respuestaPI = await respuesta.Content.ReadAsStringAsync();

                // para despues deserializarlo al formato Json de un objeto Medicos
                obj = JsonConvert.DeserializeObject<Comentarios>(respuestaPI);
            }
            //
            return View(obj);
        }

        // POST: MedicosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteComentarios(string id, IFormCollection collection)
        {
            // permite realizar una solicitud al servicio web api
            using (var cliente = new HttpClient())
            {
                var respuesta =
                      await cliente.DeleteAsync("http://localhost:5182/api/ComentariosAPI/DeleteComentario/" + id);

                string respuestaAPI = await respuesta.Content.ReadAsStringAsync();

                ViewBag.MENSAJE = respuestaAPI;
            }
            //
            return View();
        }



    }
}
