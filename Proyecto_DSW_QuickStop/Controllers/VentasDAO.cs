using Proyecto_DSW_QuickStop.Models;
using System.Data.SqlClient;

namespace Proyecto_DSW_QuickStop.Controllers
{
    public class VentasDAO
    {


        //1.
        //crear la variable String para Obtener la
        //cadena de conexion desde la appsetting.json
        private readonly string cad_conexion;

        //2.
        //desde el constructor que recibe una variable del 
        // tipo IConfiguration obtendremos la cadena de conexion
        public VentasDAO(IConfiguration configuration)
        {
            cad_conexion = configuration.GetConnectionString("cn1");
        }

        //3.
        //Listado de Productos
        public List<ProductosModel> GetProductos()
        {
            List<ProductosModel> lista = new List<ProductosModel>();
            //
            SqlDataReader dr = SqlHelper.ExecuteReader(cad_conexion, "PA_LISTAR_PRODUCTOS_CC");
            //
            while (dr.Read())
            {
                lista.Add(new ProductosModel()
                {
                    codProd = dr.GetString(0),
                    nomProd = dr.GetString(1),
                    codCat = dr.GetString(2),
                    preProd = dr.GetDecimal(3),
                    stokProd = dr.GetInt32(4),
                    eliProd = dr.GetString(5)
                });
            }
            dr.Close();
            //    //4. crear el Controlador de MVC con acciones de lectura o escritura
            return lista;    //POSTERIORMENTE CREAMOS EL VENTASController.cs     
        }

        //4.
        //BUSCAR EL PRODUCTO 
        public ProductosModel BuscarProducto(string cod_prod)
        {
            //recuerda usar el "?"
            ProductosModel? buscado =
                GetProductos().Find(p => p.codProd.Equals(cod_prod)); //esto viene de cod_prod

            if (buscado == null)
                buscado = new ProductosModel();

            return buscado;
        }

        //--------------------04-06-2023------------------------------------*/
        public string Grabar_Ventas_Web(string cod_cli, decimal tot_vta,
            List<CarritoModel> listacar)

        {
            //1.
            //Grabar en la tabla Ventas_Cab
            string? numero = SqlHelper.ExecuteScalar(cad_conexion,
                "PA_GRABAR_WEB_VENTAS_CAB", cod_cli, tot_vta).ToString();

            //2.
            //Grabar en la tabla Ventas Deta
            foreach (var item in listacar)
            {
                //Grabar cada producto del carrito a la tabla
                SqlHelper.ExecuteNonQuery(cad_conexion,
                    "PA_GRABAR_WEB_VENTAS_DETA", numero, item.codigo, item.cantidad, item.precio);
            }
            return numero;
        }
        //--------------------04-06-2023------------------------------------*/

        public List<ClienteModel> ListaClientes()
        {
            List<ClienteModel> lista = new List<ClienteModel>();
            //
            SqlDataReader lector = SqlHelper.ExecuteReader(cad_conexion, "PA_LISTAR_CLIENTES_CC");
            //
            while (lector.Read())
            {
                lista.Add(new ClienteModel()
                {
                    cod_cli = lector.GetString(0),
                    nom_cli = lector.GetString(1),
                    tel_cli = lector.GetString(2),
                    cred_cli = lector.GetInt32(3)


                });
            }
            lector.Close();
            return lista;
        }

        //--------------------04-06-2023------------------------------------*/









    }
}
