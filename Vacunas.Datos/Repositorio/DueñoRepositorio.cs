using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Vacunas.Datos.Configuracion;
using Vacunas.Datos.Entidades;

namespace Vacunas.Datos.Repositorio
{
    public class DueñoRepositorio :IDueñoRepositorio
    {

        private readonly ConfiguracionConexion _conexion;

        public DueñoRepositorio(IOptions<ConfiguracionConexion> conexion)
        {
            _conexion=conexion.Value;
        }

        public MascotaVM ObtenerDueño(int IdPet)
        {
            var oOwner = new MascotaVM();

            using (var conexion = new SqlConnection(_conexion.CadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_ObtenerDueño", conexion);
                cmd.Parameters.AddWithValue("PetId",IdPet);
                cmd.CommandType=CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oOwner.OwnerId=Convert.ToInt32(dr["Id"]);
                        oOwner.FirstName=dr["FirtsName"].ToString();
                        oOwner.LastName=dr["LastName"].ToString();
                        oOwner.Phone=dr["PhoneNumber"].ToString();
                        oOwner.Email=dr["Email"].ToString();
                        oOwner.ClientAdData=dr["AdData"].ToString();
                    }
                }
                conexion.Close();
            }
            return oOwner ;
        }

        public bool EditarDueño(MascotaVM OwnerEdit)
        {
            bool tf;
            try
            {
                using (var conexion = new SqlConnection(_conexion.CadenaSQL))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_ActualizarDueño", conexion);
                    cmd.Parameters.AddWithValue("Owner",OwnerEdit.OwnerId);
                    cmd.Parameters.AddWithValue("FirstName", OwnerEdit.FirstName);
                    cmd.Parameters.AddWithValue("LastName", OwnerEdit.LastName);
                    cmd.Parameters.AddWithValue("PhoneNum", OwnerEdit.Phone);
                    cmd.Parameters.AddWithValue("Email", OwnerEdit.Email);
                    cmd.Parameters.AddWithValue("AdData", OwnerEdit.ClientAdData);
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
    }
}
