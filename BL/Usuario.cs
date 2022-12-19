using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using ML;
using System.Data;

namespace BL
{
    public class Usuario
    {
        static public ML.Result Add (ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try {
                using (SqlConnection context = new SqlConnection(DL.Conexion.Get()))
                {
                    SqlCommand cmd = new SqlCommand();

                    cmd.Connection = context;


                    cmd.CommandText = "INSERT INTO [Usuarios] ([nombre],[apellidoPaterno],[apellidoMaterno],[correo])VALUES(@Nombre, @ApellidoPaterno, @ApellidoMaterno, @Correo)";

                    //cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.CommandText = "UsuarioAdd";

                    SqlParameter[] collection = new SqlParameter[4];


                    collection[0] = new SqlParameter("Nombre", System.Data.SqlDbType.VarChar);
                    collection[0].Value = usuario.Nombre;
                    collection[1] = new SqlParameter("ApellidoPaterno", System.Data.SqlDbType.VarChar);
                    collection[1].Value = usuario.ApellidoPaterno;
                    collection[2] = new SqlParameter("ApellidoMaterno", System.Data.SqlDbType.VarChar);
                    collection[2].Value = usuario.ApellidoMaterno;
                    collection[3] = new SqlParameter("Correo", System.Data.SqlDbType.VarChar);
                    collection[3].Value = usuario.Email;

                    cmd.Parameters.AddRange(collection);

                    cmd.Connection.Open();

                    int RowsAffected = cmd.ExecuteNonQuery();
                    //int RowsAffected = cmd.ExecuteReader();

                    cmd.Connection.Close();
                    
                    if(RowsAffected > 0)
                    {
                        result.Correct = true;
                    } else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al insertar el alumno";
                    }

                }
            } 
            catch (Exception e) {
                result.Correct = false;
                result.ErrorMessage = e.Message;
                result.Ex = e;
            }

            return result;
        } 

        static public ML.Result Update( int idUsuario, ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.Get()))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = context;

                    cmd.CommandText = "UPDATE Usuarios SET nombre = @Nombre, apellidoPaterno = @ApellidoPaterno, apellidoMaterno = @ApellidoMaterno, correo = @Correo WHERE idAlumno = @IdAlumno";
                    
                    SqlParameter[] collection = new SqlParameter[5];


                    collection[0] = new SqlParameter("Nombre", System.Data.SqlDbType.VarChar);
                    collection[0].Value = usuario.Nombre;
                    collection[1] = new SqlParameter("ApellidoPaterno", System.Data.SqlDbType.VarChar);
                    collection[1].Value = usuario.ApellidoPaterno;
                    collection[2] = new SqlParameter("ApellidoMaterno", System.Data.SqlDbType.VarChar);
                    collection[2].Value = usuario.ApellidoMaterno;
                    collection[3] = new SqlParameter("Correo", System.Data.SqlDbType.VarChar);
                    collection[3].Value = usuario.Email;
                    collection[4] = new SqlParameter("idAlumno", System.Data.SqlDbType.Int);
                    collection[4].Value = idUsuario;

                    cmd.Parameters.AddRange(collection);

                    cmd.Connection.Open();

                    int RowsAffected = cmd.ExecuteNonQuery();

                    if (RowsAffected > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al insertar el alumno";
                    }
                }
            }
            catch (Exception e)
            {
                result.Correct = false;
                result.ErrorMessage = e.Message;
                result.Ex = e;
            }

            return result;
        }

        static public ML.Result Delete(int idUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.Get()))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = context;

                    cmd.CommandText = "DELETE FROM Usuarios WHERE idAlumno = @IdUsuario";

                    SqlParameter[] collection = new SqlParameter[1];
                    collection[0] = new SqlParameter("IdUsuario", System.Data.SqlDbType.Int);
                    collection[0].Value = idUsuario;


                    cmd.Parameters.AddRange(collection);

                    cmd.Connection.Open();

                    int RowsAffected = cmd.ExecuteNonQuery();

                    if (RowsAffected > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al insertar el alumno";
                    }
                }
            }
            catch (Exception e)
            {
                result.Correct = false;
                result.ErrorMessage = e.Message;
                result.Ex = e;
            }

            return result;
        }


        static public ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.Get()))
                {

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = context;
                    cmd.CommandText = "SELECT [idAlumno], [nombre] , [apellidoPaterno], [apellidoMaterno], [correo] FROM [Usuarios]";

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable usuarioTable = new DataTable();

                    da.Fill(usuarioTable);

                    if (usuarioTable.Rows.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (DataRow row in usuarioTable.Rows)
                        {
                            ML.Usuario usuario = new ML.Usuario();

                            usuario.IdUsuario = int.Parse(row[0].ToString());
                            usuario.Nombre = row[1].ToString();
                            usuario.ApellidoPaterno = row[2].ToString();
                            usuario.ApellidoMaterno = row[3].ToString();
                            usuario.Email = row[4].ToString();

                            result.Objects.Add(usuario);
                        }
                        result.Correct = true;
                    } 
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No tengo registros que mostrar ";
                    }
                }
            }
            catch (Exception e)
            {
                result.Correct = false;
                result.ErrorMessage = e.Message;
                result.Ex = e;
            }
            return result;
        }

        static public ML.Result GetById(int idUsuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.Get()))
                {

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = context;
                    cmd.CommandText = "SELECT [idAlumno], [nombre] , [apellidoPaterno], [apellidoMaterno], [correo] FROM [Usuarios]";
                    cmd.Connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        ML.Usuario usuario = new ML.Usuario();

                        while (reader.Read())
                        {
                            if (reader.GetInt32(0) == idUsuario)
                            {
                                result.Object = new object();

                                usuario.IdUsuario = reader.GetInt32(0);
                                usuario.Nombre = reader.GetString(1);
                                usuario.ApellidoPaterno = reader.GetString(2);
                                usuario.ApellidoMaterno = reader.GetString(3);
                                usuario.Email = reader.GetString(4);

                                result.Object = usuario;
                                result.Correct = true;

                                break;
                            }
                        }   
                    }
                }
            }
            catch (Exception e)
            {
                result.Correct = false;
                result.ErrorMessage = e.Message;
                result.Ex = e;
            }

            return result;
        }

        static public ML.Result AddSP(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.Get()))
                {
                    SqlCommand cmd = new SqlCommand();

                    cmd.Connection = context;


                    //cmd.CommandText = "INSERT INTO [Usuarios] ([nombre],[apellidoPaterno],[apellidoMaterno],[correo])VALUES(@Nombre, @ApellidoPaterno, @ApellidoMaterno, @Correo)";

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "UsuarioAdd";

                    SqlParameter[] collection = new SqlParameter[11];


                    collection[0] = new SqlParameter("UserName", System.Data.SqlDbType.VarChar);
                    collection[0].Value = usuario.UserName;
                    collection[1] = new SqlParameter("Nombre", System.Data.SqlDbType.VarChar);
                    collection[1].Value = usuario.Nombre;
                    collection[2] = new SqlParameter("ApellidoPaterno", System.Data.SqlDbType.VarChar);
                    collection[2].Value = usuario.ApellidoPaterno;
                    collection[3] = new SqlParameter("ApellidoMaterno", System.Data.SqlDbType.VarChar);
                    collection[3].Value = usuario.ApellidoMaterno;
                    collection[4] = new SqlParameter("Email", System.Data.SqlDbType.VarChar);
                    collection[4].Value = usuario.Email;
                    collection[5] = new SqlParameter("Password", System.Data.SqlDbType.VarChar);
                    collection[5].Value = usuario.Password;
                    collection[6] = new SqlParameter("FechaNacimiento", System.Data.SqlDbType.Date);
                    collection[6].Value = usuario.FechaNacimiento;
                    collection[7] = new SqlParameter("Sexo", System.Data.SqlDbType.Char);
                    collection[7].Value = usuario.Sexo;
                    collection[8] = new SqlParameter("Telefono", System.Data.SqlDbType.VarChar);
                    collection[8].Value = usuario.Telefono;
                    collection[9] = new SqlParameter("Celular", System.Data.SqlDbType.VarChar);
                    collection[9].Value = usuario.Celular;
                    collection[10] = new SqlParameter("CURP", System.Data.SqlDbType.VarChar);
                    collection[10].Value = usuario.CURP;
                    //collection[11] = new SqlParameter("Imagen", System.Data.SqlDbType.VarChar);
                    //collection[11].Value = null;

                    cmd.Parameters.AddRange(collection);

                    cmd.Connection.Open();

                    int RowsAffected = cmd.ExecuteNonQuery();
                    //int RowsAffected = cmd.ExecuteReader();

                    cmd.Connection.Close();

                    if (RowsAffected > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al insertar el alumno";
                    }

                }
            }
            catch (Exception e)
            {
                result.Correct = false;
                result.ErrorMessage = e.Message;
                result.Ex = e;
            }

            return result;
        }

        static public ML.Result UpdateSP(int idUsuario, ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.Get()))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = context;

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "UsuarioUpdate";

                    SqlParameter[] collection = new SqlParameter[12];


                    collection[0] = new SqlParameter("UserName", System.Data.SqlDbType.VarChar);
                    collection[0].Value = usuario.UserName;
                    collection[1] = new SqlParameter("Nombre", System.Data.SqlDbType.VarChar);
                    collection[1].Value = usuario.Nombre;
                    collection[2] = new SqlParameter("ApellidoPaterno", System.Data.SqlDbType.VarChar);
                    collection[2].Value = usuario.ApellidoPaterno;
                    collection[3] = new SqlParameter("ApellidoMaterno", System.Data.SqlDbType.VarChar);
                    collection[3].Value = usuario.ApellidoMaterno;
                    collection[4] = new SqlParameter("Email", System.Data.SqlDbType.VarChar);
                    collection[4].Value = usuario.Email;
                    collection[5] = new SqlParameter("Password", System.Data.SqlDbType.VarChar);
                    collection[5].Value = usuario.Password;
                    collection[6] = new SqlParameter("FechaNacimiento", System.Data.SqlDbType.Date);
                    collection[6].Value = usuario.FechaNacimiento;
                    collection[7] = new SqlParameter("Sexo", System.Data.SqlDbType.Char);
                    collection[7].Value = usuario.Sexo;
                    collection[8] = new SqlParameter("Telefono", System.Data.SqlDbType.VarChar);
                    collection[8].Value = usuario.Telefono;
                    collection[9] = new SqlParameter("Celular", System.Data.SqlDbType.VarChar);
                    collection[9].Value = usuario.Celular;
                    collection[10] = new SqlParameter("CURP", System.Data.SqlDbType.VarChar);
                    collection[10].Value = usuario.CURP;
                    collection[11] = new SqlParameter("IdUsuario", System.Data.SqlDbType.Int);
                    collection[11].Value = idUsuario;

                    cmd.Parameters.AddRange(collection);

                    cmd.Connection.Open();

                    int RowsAffected = cmd.ExecuteNonQuery();

                    if (RowsAffected > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al insertar el alumno";
                    }
                }
            }
            catch (Exception e)
            {
                result.Correct = false;
                result.ErrorMessage = e.Message;
                result.Ex = e;
            }

            return result;
        }

        static public ML.Result DeleteSP(int idUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.Get()))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = context;

                    cmd.CommandType = CommandType.StoredProcedure; 
                    cmd.CommandText = "UsuarioDelete";

                    SqlParameter[] collection = new SqlParameter[1];
                    collection[0] = new SqlParameter("IdUsuario", System.Data.SqlDbType.Int);
                    collection[0].Value = idUsuario;

                    cmd.Parameters.AddRange(collection);

                    cmd.Connection.Open();

                    int RowsAffected = cmd.ExecuteNonQuery();

                    if (RowsAffected > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al insertar el alumno";
                    }
                }
            }
            catch (Exception e)
            {
                result.Correct = false;
                result.ErrorMessage = e.Message;
                result.Ex = e;
            }

            return result;
        }

        static public ML.Result GetAllSP()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.Get()))
                {

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = context;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "UsuarioGetAll";
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable usuarioTable = new DataTable();

                    da.Fill(usuarioTable);

                    if (usuarioTable.Rows.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (DataRow row in usuarioTable.Rows)
                        {
                            ML.Usuario usuario = new ML.Usuario();

                            usuario.IdUsuario = int.Parse(row[0].ToString());
                            usuario.UserName = row[1].ToString();
                            usuario.Nombre = row[2].ToString();
                            usuario.ApellidoPaterno = row[3].ToString();
                            usuario.ApellidoMaterno = row[12].ToString();
                            usuario.Email = row[4].ToString();
                            usuario.Password = row[5].ToString();
                            usuario.FechaNacimiento = DateTime.Parse(row[6].ToString());
                            usuario.Sexo = row[7].ToString();
                            usuario.Telefono = row[8].ToString();
                            usuario.Celular = row[9].ToString();
                            usuario.CURP = row[10].ToString();
                            //usuario.Image = row[11].ToString();
                            

                            result.Objects.Add(usuario);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No tengo registros que mostrar ";
                    }
                }
            }
            catch (Exception e)
            {
                result.Correct = false;
                result.ErrorMessage = e.Message;
                result.Ex = e;
            }
            return result;
        }

        static public ML.Result GetByIdSP(int idUsuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.Get()))
                {

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = context;

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "UsuarioGetById";

                    SqlParameter[] collection = new SqlParameter[1];
                    collection[0] = new SqlParameter("IdUsuario", System.Data.SqlDbType.Int);
                    collection[0].Value = idUsuario;

                    cmd.Parameters.AddRange(collection);

                    cmd.Connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        ML.Usuario usuario = new ML.Usuario();

                        usuario.IdUsuario = reader.GetInt32(0);
                        usuario.UserName = reader.GetString(1);
                        usuario.Nombre = reader.GetString(2);
                        usuario.ApellidoPaterno = reader.GetString(3);
                        usuario.ApellidoMaterno = reader.GetString(12);
                        usuario.Email = reader.GetString(4);
                        usuario.Password = reader.GetString(5);
                        usuario.FechaNacimiento = reader.GetDateTime(6);
                        usuario.Sexo = reader.GetString(7);
                        usuario.Telefono = reader.GetString(8);
                        usuario.Celular = reader.GetString(9);
                        usuario.CURP = reader.GetString(10);
                        //usuario.Image = row[11].ToString();

                        result.Object = usuario; //BOXING

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No entontre al usuario deseado";
                    }

                    //SqlDataAdapter da = new SqlDataAdapter(cmd);
                    //DataTable alumnoTable = new DataTable();

                    //da.Fill(alumnoTable);


                    //if (alumnoTable.Rows.Count > 0)
                    //{
                    //DataRow row = alumnoTable.Rows[0];

                    //ML.Usuario usuario = new ML.Usuario();
                    //usuario.IdUsuario = int.Parse(row[0].ToString());
                    //usuario.Nombre = row[1].ToString();
                    //usuario.UserName = row[2].ToString();
                    //usuario.ApellidoPaterno = row[3].ToString();
                    //usuario.ApellidoMaterno = row[12].ToString();
                    //usuario.Email = row[4].ToString();
                    //usuario.Password = row[5].ToString();
                    //usuario.FechaNacimiento = DateTime.Parse(row[6].ToString());
                    //usuario.Sexo = row[7].ToString();
                    //usuario.Telefono = row[8].ToString();
                    //usuario.Celular = row[9].ToString();
                    //usuario.CURP = row[10].ToString();
                    //usuario.Imagen

                    //        result.Object = usuario; //BOXING

                    //        result.Correct = true;
                    //    }
                    //    else
                    //    {
                    //        result.Correct = false;
                    //        result.ErrorMessage = "No contiene registros la tabla Alumno";
                    //    }
                    //}
                }
            }
            catch (Exception e)
            {
                result.Correct = false;
                result.ErrorMessage = e.Message;
                result.Ex = e;
            }

            return result;
        }
    }
}
