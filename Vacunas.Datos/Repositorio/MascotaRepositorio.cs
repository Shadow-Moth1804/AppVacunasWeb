using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;
using Vacunas.Datos.Configuracion;
using Vacunas.Datos.Entidades;

namespace Vacunas.Datos.Repositorio
{
    public class MascotaRepositorio : IMascotaRepositorio
    {
        private readonly ConfiguracionConexion _conexion;

        public MascotaRepositorio(IOptions<ConfiguracionConexion> conexion)
        {
            _conexion = conexion.Value;
        }

        public async Task<List<Mascota>> ObtenerMascota()
        {
            List<Mascota> lista = new List<Mascota>();

            using (var conexion = new SqlConnection(_conexion.CadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_ObtenerMascotas", conexion);
                cmd.CommandType=CommandType.StoredProcedure;
                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        lista.Add(new Mascota()
                        {
                            PetId=Convert.ToInt32(dr["PId"]),
                            PetName=dr["PName"].ToString(),
                            OwnerName=dr["OwnerN"].ToString(),
                            Breed=dr["PBreed"].ToString(),
                            Gender=dr["Gender"].ToString(),
                            Dateb=(DateTime)dr["DateBirth"],
                            AddData=dr["AdData"].ToString(),
                            Status=(bool)dr["Pstatus"]
                        });
                    }
                }
                conexion.Close();
            }
            return lista;
        }

        public bool GuardarMascota(MascotaVM NuevaMascota)
        {
            bool tf;
            try
            {
                using (var conexion = new SqlConnection(_conexion.CadenaSQL))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_NuevaMascota", conexion);
                    cmd.Parameters.AddWithValue("FirtsName", NuevaMascota.FirstName);
                    cmd.Parameters.AddWithValue("LastName", NuevaMascota.LastName);
                    cmd.Parameters.AddWithValue("PhoneNumber", NuevaMascota.Phone);
                    cmd.Parameters.AddWithValue("Email", NuevaMascota.Email);
                    cmd.Parameters.AddWithValue("AdData", NuevaMascota.ClientAdData);
                    cmd.Parameters.AddWithValue("Name", NuevaMascota.PetName);
                    cmd.Parameters.AddWithValue("Breed", NuevaMascota.Breed);
                    cmd.Parameters.AddWithValue("Gender", NuevaMascota.Gender);
                    cmd.Parameters.AddWithValue("DateBirt", NuevaMascota.Dateb);
                    cmd.Parameters.AddWithValue("PetAdData", NuevaMascota.PetAddData);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                    conexion.Close();
                }
                tf=true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                tf=false;
            }
            return tf;
        }

        public bool EliminarMascota(int IdMascota)
        {
            bool tf;
            try
            {
                using (var conexion = new SqlConnection(_conexion.CadenaSQL))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_EliminarMascota", conexion);
                    cmd.Parameters.AddWithValue("@PetId", IdMascota);
                    cmd.CommandType=CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                    conexion.Close();
                }
                tf=true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                tf=false;
            }
            return tf;
        }

        public Mascota FiltrarMascota(int IdMascota)
        {
            var oMascota = new Mascota();

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
                        oMascota.PetId=Convert.ToInt32(dr["Id"]);
                        oMascota.PetName=dr["Name"].ToString();
                    }
                }
                conexion.Close();
            }
            return oMascota;
        }
    }
}