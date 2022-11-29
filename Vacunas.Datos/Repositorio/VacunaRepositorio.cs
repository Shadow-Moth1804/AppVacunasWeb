using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;
using Vacunas.Datos.Configuracion;
using Vacunas.Datos.Entidades;

namespace Vacunas.Datos.Repositorio
{
    public class VacunaRepositorio : IVacunaRepositorio
    {
        private readonly ConfiguracionConexion _conexion;

        public VacunaRepositorio(IOptions<ConfiguracionConexion> conexion)
        {
            _conexion=conexion.Value;
        }

        public async Task<List<Vacuna>> ObtenerVacuna()
        {
            List<Vacuna> lista = new List<Vacuna>();

            using (var conexion = new SqlConnection(_conexion.CadenaSQL))
            {
                conexion.Open();

                SqlCommand cmd = new SqlCommand("sp_ObtenerVacuna", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        lista.Add(new Vacuna()
                        {
                            Id=Convert.ToInt32(dr["Id"]),
                            Name=dr["Name"].ToString(),
                            Pathogen=dr["Pathogen"].ToString(),
                            Status=(bool)dr["Status"]
                        });
                    }
                }

                conexion.Close();
            }
            return lista;
        }

        public bool NuevaVacuna(Vacuna NuevaVacuna)
        {
            bool tf;
            try
            {
                using (var conexion = new SqlConnection(_conexion.CadenaSQL))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_NuevaVacuna", conexion);
                    cmd.Parameters.AddWithValue("Name", NuevaVacuna.Name);
                    cmd.Parameters.AddWithValue("Pathogen", NuevaVacuna.Pathogen);
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