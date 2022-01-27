using Microsoft.AspNetCore.Http;
using MvcCoreCifradoBBDD.Providers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreCifradoBBDD.Helpers
{  
    public class HelperUploadFiles
    {
        private PathProvider pathProvider;

        public HelperUploadFiles(PathProvider pathProvider) 
        {
            this.pathProvider = pathProvider;
        }
        //esto es para subir los ficheros con su propio nombre.
        public async Task<string> UploadFileAsync
            (IFormFile formFile, Folders folder)
        {
            string fileName = formFile.FileName;
            string path = this.pathProvider.MapPath(fileName, folder);
            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                await formFile.CopyToAsync(stream);
            }
            //return path; //ruta del fichero
            return fileName; //nombre del fichero
        }
        /*este metodo seria para poder poner el id al nombre de la imagen*/
        public async Task<string> UploadFileAsync
            (IFormFile formFile, Folders folder,string filename)
        {
            string path = this.pathProvider.MapPath(filename, folder);
            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                await formFile.CopyToAsync(stream);
            }
            //return path; //ruta del fichero
            return filename; //nombre del fichero
        }
    }
}
