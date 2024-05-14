using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Infraestructura.Models
{
    public partial class Tienda_MusicaContext : DbContext
    {
        public Tienda_MusicaContext()
        {
        }

        public Tienda_MusicaContext(DbContextOptions<Tienda_MusicaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<ProductoMusical> ProductoMusicals { get; set; } = null!;
        public virtual DbSet<Transaccion> Transaccions { get; set; } = null!;

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Server=DESKTOP-AUG9306\\SQLEXPRESS02;Database=Tienda_Musica;Trusted_Connection=True;");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.IdCliente)
                    .HasName("PK__Cliente__E005FBFF157EF489");

                entity.ToTable("Cliente");

                entity.Property(e => e.IdCliente)
                    .ValueGeneratedNever()
                    .HasColumnName("ID_Cliente");

                entity.Property(e => e.Apellido).HasMaxLength(50);

                entity.Property(e => e.CorreoElectronico)
                    .HasMaxLength(100)
                    .HasColumnName("Correo_Electronico");

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.Property(e => e.Pais).HasMaxLength(50);
            });

            modelBuilder.Entity<ProductoMusical>(entity =>
            {
                entity.HasKey(e => e.IdProducto)
                    .HasName("PK__Producto__9B4120E25F420CDF");

                entity.ToTable("ProductoMusical");

                entity.Property(e => e.IdProducto)
                    .ValueGeneratedNever()
                    .HasColumnName("ID_Producto");

                entity.Property(e => e.Artista).HasMaxLength(100);

                entity.Property(e => e.Formato).HasMaxLength(50);

                entity.Property(e => e.Genero).HasMaxLength(50);

                entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Titulo).HasMaxLength(100);

                entity.HasMany(d => d.IdTransaccions)
                    .WithMany(p => p.IdProductos)
                    .UsingEntity<Dictionary<string, object>>(
                        "ProductoTransaccion",
                        l => l.HasOne<Transaccion>().WithMany().HasForeignKey("IdTransaccion").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Producto___ID_Tr__3F466844"),
                        r => r.HasOne<ProductoMusical>().WithMany().HasForeignKey("IdProducto").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Producto___ID_Pr__3E52440B"),
                        j =>
                        {
                            j.HasKey("IdProducto", "IdTransaccion").HasName("PK__Producto__02F46121B587389E");

                            j.ToTable("Producto_Transaccion");

                            j.IndexerProperty<int>("IdProducto").HasColumnName("ID_Producto");

                            j.IndexerProperty<int>("IdTransaccion").HasColumnName("ID_Transaccion");
                        });
            });

            modelBuilder.Entity<Transaccion>(entity =>
            {
                entity.HasKey(e => e.IdTransaccion)
                    .HasName("PK__Transacc__9B541C38AC594544");

                entity.ToTable("Transaccion");

                entity.Property(e => e.IdTransaccion)
                    .ValueGeneratedNever()
                    .HasColumnName("ID_Transaccion");

                entity.Property(e => e.FechaHora)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Hora");

                entity.Property(e => e.IdCliente).HasColumnName("ID_Cliente");

                entity.Property(e => e.TotalCompra)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("Total_Compra");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.Transaccions)
                    .HasForeignKey(d => d.IdCliente)
                    .HasConstraintName("FK__Transacci__ID_Cl__3B75D760");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
