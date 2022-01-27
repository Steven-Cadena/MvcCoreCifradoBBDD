using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcCoreCifradoBBDD.Helpers;
using MvcCoreCifradoBBDD.Models;
using MvcCoreCifradoBBDD.Providers;
using MvcCoreCifradoBBDD.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreCifradoBBDD.Controllers
{
    public class UsuariosController : Controller
    {
        private RepositoryUsuarios repo;
        private HelperUploadFiles helperUpload;
        public UsuariosController(RepositoryUsuarios repo, HelperUploadFiles helperUpload) 
        {
            this.repo = repo;
            this.helperUpload = helperUpload;
        }
        public IActionResult Register()
        {
            return View();
        }
        /*importante poner async para subir ficheros*/
        [HttpPost]
        public async Task<IActionResult> Register(string nombre, string email,string password,IFormFile fichero)
        {
            //buscar un metodo para sanear los simbolos especiales
            string fileName = fichero.FileName;
            int idusuario = this.repo.RegistrarUsuario(nombre, email, password, fileName);
            fileName = idusuario + "_" + fileName;
            await this.helperUpload.UploadFileAsync(fichero, Folders.Images, fileName);
            ViewData["MENSAJE"] = "Usuario registrado correctamente";
            return View();
        }

        public IActionResult Login() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            Usuario usuario = this.repo.LogInUsuario(email, password);
            if (usuario == null)
            {
                ViewData["MENSAJE"] = "No tienes credenciales correcta";
                return View();
            }
            else
            {
                return View(usuario);
            } 
        }
    }
}
