using MvcCoreCifradoBBDD.Data;
using MvcCoreCifradoBBDD.Helpers;
using MvcCoreCifradoBBDD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreCifradoBBDD.Repositories
{
    public class RepositoryUsuarios
    {
        private UsuariosContext context;
        public RepositoryUsuarios(UsuariosContext context) 
        {
            this.context = context;
        }
        private int GetMaxIdUsuario() 
        {
            if (this.context.Usuarios.Count() == 0)
            {
                return 1;
            }
            else 
            {
                return this.context.Usuarios.Max(z => z.IdUsuario) + 1;
            }
        }
        public int RegistrarUsuario(string nombre,string email,string password,string imagen) 
        {
            int idusuario = this.GetMaxIdUsuario();
            Usuario usuario = new Usuario();
            usuario.IdUsuario = this.GetMaxIdUsuario();
            usuario.Nombre = nombre;
            usuario.Email = email;
            usuario.Imagen = idusuario + "_" + imagen;
            //GENERAMOS UN SALT ALEATORIO PARA CADA USUARIO
            usuario.Salt = HelperCryptography.GenerateSalt();
            //GENERAMOS SU PASSWORD 
            usuario.Password = HelperCryptography.EncriptarPassword(password, usuario.Salt);
            this.context.Usuarios.Add(usuario);
            this.context.SaveChanges();
            return idusuario;
        }

        public Usuario LogInUsuario(string email, string password) 
        {
            //para buscar el usuario por su email
            Usuario usuario = this.context.Usuarios.SingleOrDefault(x => x.Email == email);
            if (usuario == null)
            {
                return null;
            }
            else 
            {
                //DEBEMOS COMPARAR CON LA BBDD EL PASSWORD 
                //HACIENDO DE NUEVO EL CIFRADO CON CADA SALT DE USUARIO
                byte[] passUsuario = usuario.Password;
                string salt = usuario.Salt;
                //CIFRAMOS DE NUEVO PARA COMPARAR 
                byte[] temporal = HelperCryptography.EncriptarPassword(password, salt);
                //COMPARAMOS LOS ARRAY PARA COMPROBAR SI EL CIFRADO ES EL MISMO 
                bool respuesta = HelperCryptography.CompareArrays(passUsuario, temporal);
                if (respuesta == true)
                {
                    return usuario;
                }
                else 
                {
                    return null;
                }
            }
        }
    }
}
