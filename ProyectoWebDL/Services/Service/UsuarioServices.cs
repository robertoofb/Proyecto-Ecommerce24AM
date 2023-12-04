﻿using Microsoft.EntityFrameworkCore;
using ProyectoWebDL.Context;
using ProyectoWebDL.Models.Entities;
using ProyectoWebDL.Services.IServices;
using System.Data;

namespace ProyectoWebDL.Services.Service
{
    public class UsuarioServices : IUsuarioServices
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHost;
        private readonly IHttpContextAccessor _httpContext;
        //Constructor para usar las tablas de base de datos
        public UsuarioServices(ApplicationDbContext context, IHttpContextAccessor httpContext, IWebHostEnvironment webHost)
        {
            _context = context;
            _httpContext = httpContext;
            _webHost = webHost;
        }

        public async Task<List<Usuario>> GetUsuarios()
        {
            try
            {

                return await _context.Usuarios.Include(x => x.Roles).ToListAsync();

            }
            catch (Exception ex)
            {
                throw new Exception("Surgio un error" + ex.Message);
            }

        }

        public async Task<List<Usuario>> GetInvestigadores()
        {
            try
            {

                return await _context.Usuarios.Where(x => x.Roles.Nombre == "Investigador").ToListAsync();

            }
            catch (Exception ex)
            {
                throw new Exception("Surgio un error" + ex.Message);
            }

        }

        public async Task<Usuario> GetByIdUsuario(int id)
        {
            try
            {
                //Articulo response = await _context.Articulos.FindAsync(id);

                Usuario response = await _context.Usuarios.FirstOrDefaultAsync(x => x.PKUsuario == id);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("Surgio un error" + ex.Message);
            }

        }
        public async Task<Usuario> CrearUsuario(Usuario i)
        {
            try
            {
                var urlImagen = i.Img.FileName;
                i.UrlImagenPath = @"Img/usuarios/" + urlImagen;
                Usuario request = new Usuario()
                {
                    Nombre = i.Nombre,
                    Apellido1 = i.Apellido1,
                    Apellido2 = i.Apellido2,
                    NombreUsuario = i.NombreUsuario,
                    Correo = i.Correo,
                    Contraseña = i.Contraseña,
                    UrlImagenPath = i.UrlImagenPath,
                    FkRol = i.FkRol,
                };
                SubirImg(urlImagen);

                var result = await _context.Usuarios.AddAsync(request);
                _context.SaveChanges();

                return request;
            }
            catch (Exception ex)
            {
                throw new Exception("Surgio un error" + ex.Message);
            }
        }
        public async Task<Usuario> RegistrarUsuario(Usuario i)
        {
            try
            {
                Usuario request = new Usuario()
                {
                    Nombre = i.Nombre,
                    Apellido1 = i.Apellido1,
                    Apellido2 = i.Apellido2,
                    NombreUsuario = i.NombreUsuario,
                    Correo = i.Correo,
                    Contraseña = i.Contraseña,
                    UrlImagenPath = "img",
                    FkRol = 2,
                };
                var result = await _context.Usuarios.AddAsync(request);
                _context.SaveChanges();

                return request;
            }
            catch (Exception ex)
            {

                throw new Exception("Surgio un error" + ex.Message);
            }
        }

        public async Task<Usuario> EditarUsuario(Usuario i)
        {
            try
            {
                Usuario usuario = _context.Usuarios.Find(i.PKUsuario);
                var urlImagen = i.Img.FileName;
                i.UrlImagenPath = @"Img/usuarios/" + urlImagen;

                usuario.Nombre = i.Nombre;
                usuario.Apellido1 = i.Apellido1;
                usuario.Apellido2 = i.Apellido2;
                usuario.NombreUsuario = i.NombreUsuario;
                usuario.Correo = i.Correo;
                usuario.Contraseña = i.Contraseña;
                usuario.UrlImagenPath = i.UrlImagenPath;
                usuario.FkRol = i.FkRol;

                _context.Entry(usuario).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                SubirImg(urlImagen);

                return usuario;

            }
            catch (Exception ex)
            {
                throw new Exception("Succedio un error " + ex.Message);
            }
        }
        public bool EliminarUsuario(int id)
        {
            try
            {
                Usuario usuario = _context.Usuarios.Find(id);

                if (usuario != null)
                {
                    var res = _context.Usuarios.Remove(usuario);
                    _context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Surgió un error: " + ex.Message);
            }
        }
        public bool SubirImg(string Img)
        {
            bool res = false;

            try
            {
                string rutaprincipal = _webHost.WebRootPath;
                var archivos = _httpContext.HttpContext.Request.Form.Files;

                if (archivos.Count > 0 && !string.IsNullOrEmpty(archivos[0].FileName))
                {

                    var nombreArchivo = Img;
                    var subidas = Path.Combine(rutaprincipal, "Img", "usuarios");

                    // Asegurarse de que el directorio de destino exista
                    if (!Directory.Exists(subidas))
                    {
                        Directory.CreateDirectory(subidas);
                    }

                    var rutaCompleta = Path.Combine(subidas, nombreArchivo);

                    using (var fileStream = new FileStream(rutaCompleta, FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStream);
                        res = true;
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error al subir la imagen: {ex.Message}");
            }

            return res;
        }
    }
}
