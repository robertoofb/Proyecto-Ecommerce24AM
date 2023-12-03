using Microsoft.EntityFrameworkCore;
using ProyectoWebDL.Models.Entities;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace ProyectoWebDL.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }


        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<Rol> Roles { get; set; }
        public virtual DbSet<Categoria> Categorias { get; set; }
        public virtual DbSet<Articulo> Articulos { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Insert en la tabla Rol

            modelBuilder.Entity<Rol>().HasData(
                new Rol
                {
                    PkRoles = 1,
                    Nombre = "SA"
                },
                new Rol
                {
                    PkRoles = 2,
                    Nombre = "Usuario"
                },
                new Rol
                {
                    PkRoles = 3,
                    Nombre = "Investigador"
                }
                );

            //Insert en la tabla Categoria

            modelBuilder.Entity<Categoria>().HasData(
                new Categoria
                {
                    PkCategoria = 1,
                    Nombre = "Software"
                },
                new Categoria
                {
                    PkCategoria = 2,
                    Nombre = "Biomédica"
                },
                new Categoria
                {
                    PkCategoria = 3,
                    Nombre = "Biotecnología"
                },
                new Categoria
                {
                    PkCategoria = 4,
                    Nombre = "Finanzas"
                },
                new Categoria
                {
                    PkCategoria = 5,
                    Nombre = "Administración"
                },
                new Categoria
                {
                    PkCategoria = 6,
                    Nombre = "Terapia Física"
                }
                );


            //Insert en la tabla usuario

            modelBuilder.Entity<Usuario>().HasData(
                new Usuario
                {
                    PKUsuario = 1,
                    Nombre = "Roberto",
                    Apellido1 = "Fierro",
                    Apellido2 = "Ballote",
                    NombreUsuario = "robertofb",
                    Correo = "roberto@gmail.com",
                    Contraseña = "1234",
                    FkRol = 1,
                    UrlImagenPath = "imagen/siu"

                });
        }
    }
}
