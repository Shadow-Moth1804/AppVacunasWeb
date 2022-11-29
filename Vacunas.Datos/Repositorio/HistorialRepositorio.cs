using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;
using Vacunas.Datos.Configuracion;
using Vacunas.Datos.Entidades;

namespace Vacunas.Datos.Repositorio
{
    public class HistorialRepositorio : IHistorialRepositorio
    {
        private readonly ConfiguracionConexion _conexion;

        public HistorialRepositorio(IOptions<ConfiguracionConexion> conexion)
        {
            _conexion = conexion.Value;
        }

        public async Task<List<Historial>> ObtenerHistorial()
        {
            List<Historial> lista = new List<Historial>();

            using (var conexion = new SqlConnection(_conexion.CadenaSQL))
            {
                conexion.Open();

                SqlCommand cmd = new SqlCommand("sp_ObtenerHistorial", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        lista.Add(new Historial()
                        {
                            HId=Convert.ToInt32(dr["Id"]),
                            PetName=dr["PetName"].ToString(),
                            Vaccine=dr["Vaccine"].ToString(),
                            Weight=Convert.ToDecimal(dr["PetWeight"]),
                            Date=(DateTime)dr["DateApli"],
                            Satage=dr["Stage"].ToString(),
                            NameDoctor=dr["FirstName"].ToString(),
                            Status=(bool)dr["Status"]
                        });
                    }
                }

                conexion.Close();
            }
            return lista;
        }

        public bool NuevoRegistro(Historial historial)
        {
            bool tf;
            try
            {
                using (var conexion = new SqlConnection(_conexion.CadenaSQL))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_InsertarVacunacion", conexion);
                    cmd.Parameters.AddWithValue("PetName", historial.PetName);
                    cmd.Parameters.AddWithValue("Weight", (historial.Weight));
                    cmd.Parameters.AddWithValue("Stage", historial.Satage);
                    cmd.Parameters.AddWithValue("Nvaccine", historial.Vaccine);
                    cmd.Parameters.AddWithValue("ApDate", historial.Date);
                    cmd.Parameters.AddWithValue("DocName", historial.NameDoctor);
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

        public Vacuna FiltrarVacuna(int idVacuna)
        {
            var oVacuna = new Vacuna();

            using (var conexion = new SqlConnection(_conexion.CadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_FilltrarVacuna", conexion);
                cmd.Parameters.AddWithValue("@PetId", idVacuna);
                cmd.CommandType=CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oVacuna.Id=Convert.ToInt32(dr["Id"]);
                        oVacuna.Name=dr["Name"].ToString();
                    }
                }
                conexion.Close();
            }
            return oVacuna;
        }

        public Historial FiltrarMascota(int IdMascota)
        {
            var oMascota = new Historial();

            using (var conexion = new SqlConnection(_conexion.CadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_FiltrarMascota", conexion);
                cmd.Parameters.AddWithValue("@PetId", IdMascota);
                cmd.CommandType=CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oMascota.IdPet=Convert.ToInt32(dr["Id"]);
                        oMascota.PetName=dr["Name"].ToString();
                    }
                }
                conexion.Close();
            }
            return oMascota;
        }
    }
}