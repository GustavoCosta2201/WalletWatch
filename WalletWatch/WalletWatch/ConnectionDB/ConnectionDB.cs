﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletWatch.Modelos;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace WalletWatch.Banco;

public class ConnectionDB : IdentityDbContext<PessoaComAcesso, PerfilDeAcesso, int>
{

    public DbSet<Usuarios> Usuarios { get; set; }
    public DbSet<Categorias> Categorias { get; set; }
    public DbSet<Transacoes> Transacoes { get; set; }

    private string ConnectionString = "Data Source=GustavoPC;Initial Catalog=WALLETWATCH;User ID=sa;Password=masterkey;" +
        "Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

        optionsBuilder.UseSqlServer(ConnectionString).UseLazyLoadingProxies();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        base.OnModelCreating(modelBuilder);
    }
}
