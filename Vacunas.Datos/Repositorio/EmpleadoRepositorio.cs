using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;
using Vacunas.Datos.Configuracion;
using Vacunas.Datos.Entidades;

namespace Vacunas.Datos.Repositorio
{
    public class EmpleadoRepositorio : IEmpleadoRepositorio
    {
        private readonly ConfiguracionConexion _conexion;

        public EmpleadoRepositorio(IOptions<ConfiguracionConexion> conexion)
        {
            _conexion= conexion.Value;
        }

        public async Task<List<Empleado>> ObtenerEmpleado()
        {
            List<Empleado> lista = new List<Empleado>();

            using (var conexion = new SqlConnection(_conexion.CadenaSQL))
            {
                conexion.Open();

                SqlCommand cmd = new SqlCommand("sp_ObtenerEmpleados", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        lista.Add(new Empleado()
                        {
                            IdEmpleado=Convert.ToInt32(dr["Id"]),
                            FirstName=dr["FirtsName"].ToString(),
                            LastName=dr["LastName"].ToString(),
                            Range=dr["Range"].ToString(),
                            Email=dr["Email"].ToString(),
                            Status=(bool)dr["Status"]
                        });
                    }
                }

                conexion.Close();
            }
            return lista;
        }

        public bool NuevoEmpleado(Empleado NuevoEmpleado)
        {
            bool tf;
            try
            {
                using (var conexion = new SqlConnection(_conexion.CadenaSQL))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_NuevoEmpleado", conexion);
                    cmd.Parameters.AddWithValue("FirtsName", NuevoEmpleado.FirstName);
                    cmd.Parameters.AddWithValue("LastName", NuevoEmpleado.LastName);
                    cmd.Parameters.AddWithValue("Range", NuevoEmpleado.Range);
                    cmd.Parameters.AddWithValue("Email", NuevoEmpleado.Email);
                    cmd.CommandType=CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                    conexion.Close();
                }
                tf = true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                tf  = false;
            }
            return tf;
        }
    }
}
