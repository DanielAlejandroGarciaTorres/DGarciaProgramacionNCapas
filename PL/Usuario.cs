using ML;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    internal class Usuario
    {
        static public void Add()
        {
            ML.Usuario usuario = new ML.Usuario();
            ML.Result result = new ML.Result();

            Console.WriteLine("Ingrese los datos del usuario a insertar: \n");

            Console.WriteLine("Ingrese el User Name del usuario");
            usuario.UserName = Console.ReadLine();
            Console.WriteLine("Ingrese el nombre del usuario");
            usuario.Nombre = Console.ReadLine();
            Console.WriteLine("Ingrese el apellido paterno del usuario");
            usuario.ApellidoPaterno = Console.ReadLine();
            Console.WriteLine("Ingrese el apellido materno del usuario");
            usuario.ApellidoMaterno = Console.ReadLine();
            Console.WriteLine("Ingrese el correo del usuario");
            usuario.Email = Console.ReadLine();
            Console.WriteLine("Ingrese el fecha de nacimiento del usuario");
            usuario.FechaNacimiento = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            Console.WriteLine("Ingrese la contraseña del usuario");
            usuario.Password = Console.ReadLine();
            Console.WriteLine("Ingrese el genero del usuario");
            usuario.Sexo = Console.ReadLine();
            Console.WriteLine("Ingrese el telefono del usuario");
            usuario.Telefono = Console.ReadLine();
            Console.WriteLine("Ingrese el celular del usuario");
            usuario.Celular = Console.ReadLine();
            Console.WriteLine("Ingrese el CURP del usuario");
            usuario.CURP = Console.ReadLine();

            result = BL.Usuario.AddSP(usuario);

            if (result.Correct)
            {
                Console.WriteLine("El usaurio fue insertado");
            }
            else
            {
                Console.WriteLine("El usuario no pudo ser insertado");
            }
        }

        static public void Update()
        {
            string idUsuario;
            ML.Result result = new ML.Result();
            ML.Usuario usuario = new ML.Usuario();  

            
            Console.WriteLine("Ingrese el Id del usuario a modificar:");
            idUsuario = Console.ReadLine();

            Console.WriteLine("Modificación de usuario: \n");

            Console.WriteLine("Ingrese el User Name del usuario");
            usuario.UserName = Console.ReadLine();
            Console.WriteLine("Ingrese el nombre del usuario");
            usuario.Nombre = Console.ReadLine();
            Console.WriteLine("Ingrese el apellido paterno del usuario");
            usuario.ApellidoPaterno = Console.ReadLine();
            Console.WriteLine("Ingrese el apellido materno del usuario");
            usuario.ApellidoMaterno = Console.ReadLine();
            Console.WriteLine("Ingrese el correo del usuario");
            usuario.Email = Console.ReadLine();
            Console.WriteLine("Ingrese el fecha de nacimiento del usuario");
            usuario.FechaNacimiento = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            Console.WriteLine("Ingrese la contraseña del usuario");
            usuario.Password = Console.ReadLine();
            Console.WriteLine("Ingrese el genero del usuario");
            usuario.Sexo = Console.ReadLine();
            Console.WriteLine("Ingrese el telefono del usuario");
            usuario.Telefono = Console.ReadLine();
            Console.WriteLine("Ingrese el celular del usuario");
            usuario.Celular = Console.ReadLine();
            Console.WriteLine("Ingrese el CURP del usuario");
            usuario.CURP = Console.ReadLine();

            result = BL.Usuario.UpdateSP(int.Parse(idUsuario), usuario);

            if (result.Correct)
            {
                Console.WriteLine("El usuario fue modificado");
            }
            else
            {
                Console.WriteLine("El usuario no pudo ser modificado");
            }

        }

        static public void Delete()
        {
            string idUsuario;
            ML.Result result = new ML.Result();

            Console.WriteLine("\t\tUsuarios registrados:");

            Console.WriteLine("Ingrese el Id del usuario a eliminar:");
            idUsuario = Console.ReadLine();

            result = BL.Usuario.DeleteSP(int.Parse(idUsuario));

            if (result.Correct)
            {
                Console.WriteLine("El usuario fue eliminado");
            }
            else
            {
                Console.WriteLine("El usuario no pudo ser eliminado");
                Console.WriteLine(result.ErrorMessage);
            }
        }

        static public void GetAll()
        {
            ML.Result result = new ML.Result();

            result = BL.Usuario.GetAllSP();

            Console.WriteLine("Los datos del usuario son los siguientes:\n");
            
            foreach( ML.Usuario usuario in result.Objects){
                Console.WriteLine("Id Usuario: " + usuario.IdUsuario);
                Console.WriteLine("User Name: " + usuario.UserName);
                Console.WriteLine("Nombre: " + usuario.Nombre);
                Console.WriteLine("Apellido Paterno: " + usuario.ApellidoPaterno);
                Console.WriteLine("Apellido Materno: " + usuario.ApellidoMaterno);
                Console.WriteLine("Correo: " + usuario.Email);
                Console.WriteLine("Contrasena: " + usuario.Password);
                Console.WriteLine("Fecha nacimiento: " + usuario.FechaNacimiento.ToString());
                Console.WriteLine("Genero: " + usuario.Sexo);
                Console.WriteLine("Telefono: " + usuario.Telefono);
                Console.WriteLine("Celular: " + usuario.Celular);
                Console.WriteLine("CURP: " + usuario.CURP);
                Console.WriteLine("Imagen: " + usuario.Imagen);
                Console.WriteLine("--------------------------------------------------");
            }
        }

        static public void GetById()
        {
            ML.Result result = new ML.Result();
            int idUsuario;

            Console.WriteLine("Ingresa el id del usuario que deseas visualizar");
            idUsuario = int.Parse(Console.ReadLine());

            result = BL.Usuario.GetByIdSP(idUsuario);

            if (result.Correct)
            {
                ML.Usuario usuario = new ML.Usuario();
                usuario = (ML.Usuario) result.Object; //unboxing 

                Console.WriteLine("Id Usuario: " + usuario.IdUsuario);
                Console.WriteLine("User Name: " + usuario.UserName);
                Console.WriteLine("Nombre: " + usuario.Nombre);
                Console.WriteLine("Apellido Paterno: " + usuario.ApellidoPaterno);
                Console.WriteLine("Apellido Materno: " + usuario.ApellidoMaterno);
                Console.WriteLine("Correo: " + usuario.Email);
                Console.WriteLine("Contrasena: " + usuario.Password);
                Console.WriteLine("Fecha nacimiento: " + usuario.FechaNacimiento.ToString());
                Console.WriteLine("Genero: " + usuario.Sexo);
                Console.WriteLine("Telefono: " + usuario.Telefono);
                Console.WriteLine("Celular: " + usuario.Celular);
                Console.WriteLine("CURP: " + usuario.CURP);
                Console.WriteLine("Imagen: " + usuario.Imagen);
                Console.WriteLine("--------------------------------------------------");
            } 
            else
            {
                Console.WriteLine(result.ErrorMessage);
            }
        }

    }
}
