using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using dominio;

namespace datos
{
    public class ElementoDatos
    {
        public List<Elemento> listar()
        {
			List<Elemento> lista = new List<Elemento>();
			AccesoDatos acceso = new AccesoDatos();
			try
			{

				acceso.setearConsulta("SELECT Id, Descripcion FROM ELEMENTOS");
				acceso.ejecutarConsulta();


				while (acceso.Lector.Read())
				{
					Elemento aux = new Elemento();
					aux.Id = (int)acceso.Lector["Id"];
					aux.Descripcion = (string)acceso.Lector["Descripcion"];

					lista.Add(aux);

				}


				return lista;
			}
			catch (Exception ex)
			{

				throw ex;
			}
			finally
			{
				acceso.cerrarConexion();
			}


        }


    }
}
