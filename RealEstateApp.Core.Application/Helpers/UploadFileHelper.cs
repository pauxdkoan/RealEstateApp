

using Microsoft.AspNetCore.Http;
using RealEstateApp.Core.Application.Enums;

namespace RealEstateApp.Core.Application.Helpers
{
    public static class UploadFileHelper
    {

        /*Nota: le agregue un parametro mas para q indique el nombre
         de la carpeta en la cual se va guardar la imagen, ejemplo: Cliente o Agente
         y en vez de utilizar el id utilizamos el correo ya q el id de identity es muy 
         largo*/
        public static string UploadFile(int? rol,IFormFile file, string codeOrEmail,string? name,bool isEditMode=false, string photo = "") 
        {
            if (isEditMode && file == null)
            {
                return photo;
            }

            string basePath="";
            //Se crea el path donde queremos guardar las imagenes
            if(rol != null) 
            { 
                string Rol = rol == 1 ? Roles.Cliente.ToString() : Roles.Agente.ToString();
                basePath = $"/Images/{Rol}/{codeOrEmail}";
            }

            if (name != null) 
            {
                basePath = $"/Images/{name}/{codeOrEmail}";
            }

           


            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            //Luego creamos el folder si no existe
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            /*Esto genera un id unico para las ft q se van a subir
             asi cuando un usuario de casualidad suba una misma ft
             o archivos con el mismo nombre no explote*/

            Guid guid = Guid.NewGuid(); 
            FileInfo fileInfo = new(file.FileName);
            string filename = guid + fileInfo.Extension;

            string fileNameWithPath = Path.Combine(path, filename); //Ruta completa

            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                file.CopyTo(stream); //Copia el archivo subido a la ruta de destino
            }

            if (isEditMode && photo != null)
            {
                string[] oldImagePart = photo.Split("/");
                string oldImageName = oldImagePart[^1];//Toma el ultimo dato del arreglo, es decir el nombre del archivo
                string completeImageOldPath = Path.Combine(path, oldImageName);

                if (System.IO.File.Exists(completeImageOldPath))
                {
                    System.IO.File.Delete(completeImageOldPath);
                }
            }
            return $"{basePath}/{filename}";
        }

        /*Este metodo lo cree para evitar primero registrar el usuario y luegoo agregarle
         la ft, prefiero q usuario ingrese con su ft y si de casualidad no se da el registro
         por q no cumple con algo, eliminamos la ft del proyecto*/
        public static void DeleteFile(string photoPath) 
        { 
            if(string.IsNullOrEmpty(photoPath)) return;

            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{photoPath}");
            if (File.Exists(fullPath)) 
            { 
                File.Delete(fullPath);

                //Se elimina la carpeta si queda vacia
                string directoryPath=Path.GetDirectoryName(fullPath);
                if(Directory.Exists(directoryPath) && !Directory.EnumerateFiles(directoryPath).Any()) 
                { 
                    Directory.Delete(directoryPath);
                }
            }
        }
    }
}
