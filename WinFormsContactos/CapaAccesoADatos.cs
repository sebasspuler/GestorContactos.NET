using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace WinFormsContactos
{
    public class CapaAccesoADatos
    {
        private SqlConnection conn = new SqlConnection("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Formulario Contactos;Data Source=DESKTOP-IIGG7JF");

        #region Consulta Directa  
        /*
        public void InsertarContacto(Contacto contacto)
        {
            try
            {
                conn.Open();
                string query = @"
                                 INSERT INTO Contactos (Nombre, Apellido, Telefono, Direccion) 
                                 VALUES (@Nombre, @Apellido, @Telefono, @Direccion)";

                SqlParameter nombre = new SqlParameter("@Nombre", contacto.Nombre);
                SqlParameter apellido = new SqlParameter("@Apellido", contacto.Apellido);
                SqlParameter telefono = new SqlParameter("@Telefono", contacto.Telefono);
                SqlParameter direccion = new SqlParameter("@Direccion", contacto.Direccion);

                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.Add(nombre);
                command.Parameters.Add(apellido);
                command.Parameters.Add(telefono);
                command.Parameters.Add(direccion);

                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public void ActualizarContacto(Contacto contacto)
        {
            try
            {
                conn.Open();
                string query = @"UPDATE Contactos
                                 SET Nombre = @Nombre,
                                     Apellido = @Apellido,
                                     Telefono = @Telefono,
                                     Direccion = @Direccion
                                 WHERE Id = @Id ";

                SqlParameter id = new SqlParameter("@Id", contacto.Id);
                SqlParameter nombre = new SqlParameter("@Nombre", contacto.Nombre);
                SqlParameter apellido = new SqlParameter("@Apellido", contacto.Apellido);
                SqlParameter telefono = new SqlParameter("@Telefono", contacto.Telefono);
                SqlParameter direccion = new SqlParameter("@Direccion", contacto.Direccion);

                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.Add(id);
                command.Parameters.Add(nombre);
                command.Parameters.Add(apellido);
                command.Parameters.Add(telefono);
                command.Parameters.Add(direccion);

                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public void BorrarContacto(int id)
        {
            try
            {
                conn.Open();
                string query = @"DELETE FROM Contactos WHERE Id = @Id";

                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.Add(new SqlParameter("@Id", id));

                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public List<Contacto> GetContactos(string searchText = null)
        {
            List<Contacto> contactos = new List<Contacto>();
            try
            {
                conn.Open();
                string query = @"SELECT Id, Nombre, Apellido, Telefono, Direccion 
                                 From Contactos";

                SqlCommand command = new SqlCommand();

                if (!string.IsNullOrEmpty(searchText))
                {
                    query += @" WHERE Nombre LIKE @Search OR Apellido LIKE @Search OR Telefono LIKE @Search OR 
                             Direccion LIKE @Search";
                    command.Parameters.Add(new SqlParameter("@Search", $"%{searchText}"));
                }

                command.CommandText = query;
                command.Connection = conn;

                SqlDataReader reader = command.ExecuteReader(); 

                while (reader.Read())
                {
                    contactos.Add(new Contacto
                    {
                        Id = int.Parse(reader["Id"].ToString()),
                        Nombre = reader["Nombre"].ToString(),
                        Apellido = reader["Apellido"].ToString(),
                        Telefono = reader["Telefono"].ToString(),
                        Direccion = reader["Direccion"].ToString()
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return contactos;
        }
        */
        #endregion

        #region Stored Procedures

        public void InsertarContacto(Contacto contacto)
        {
            try
            {
                conn.Open();
                SqlCommand command = new SqlCommand("InsertarContacto", conn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Nombre", contacto.Nombre);
                command.Parameters.AddWithValue("@Apellido", contacto.Apellido);
                command.Parameters.AddWithValue("@Telefono", contacto.Telefono);
                command.Parameters.AddWithValue("@Direccion", contacto.Direccion);

                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public void ActualizarContacto(Contacto contacto)
        {
            try
            {
                conn.Open();
                SqlCommand command = new SqlCommand("ActualizarContacto", conn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Id", contacto.Id);
                command.Parameters.AddWithValue("@Nombre", contacto.Nombre);
                command.Parameters.AddWithValue("@Apellido", contacto.Apellido);
                command.Parameters.AddWithValue("@Telefono", contacto.Telefono);
                command.Parameters.AddWithValue("@Direccion", contacto.Direccion);

                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public void BorrarContacto(int id)
        {
            try
            {
                conn.Open();
                SqlCommand command = new SqlCommand("BorrarContacto", conn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Id", id);

                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public List<Contacto> GetContactos(string searchText = null)
        {
            List<Contacto> contactos = new List<Contacto>();
            try
            {
                conn.Open();
                SqlCommand command = new SqlCommand("GetContactos", conn);
                command.CommandType = CommandType.StoredProcedure;

                if (!string.IsNullOrEmpty(searchText))
                {
                    command.Parameters.AddWithValue("@Search", $"%{searchText}%");
                }
                else
                {
                    command.Parameters.AddWithValue("@Search", DBNull.Value);
                }

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    contactos.Add(new Contacto
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Nombre = reader["Nombre"].ToString(),
                        Apellido = reader["Apellido"].ToString(),
                        Telefono = reader["Telefono"].ToString(),
                        Direccion = reader["Direccion"].ToString()
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return contactos;
        }

        #endregion

    }
}



