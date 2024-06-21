using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using WalletWatch.API.Requests;
using WalletWatch.Banco;
using WalletWatch.Modelos;

namespace WalletWatch.API.Endpoints
{
    public static class CategoriasExtension
    {
        public static void AddEndpointsCategorias(this WebApplication app)
        {
            var GroupBuilder = app.MapGroup("Categorias").RequireAuthorization().WithTags("Categorias");

            GroupBuilder.MapGet("", ([FromServices] DAL<Categorias> DAL) =>
            {
                var lista = DAL.Listar();
                return Results.Ok(lista);
            });



            GroupBuilder.MapGet("/{nome}", ([FromServices] DAL<Categorias> DAL, string nome) =>
            {
                var recuperar = DAL.RecuperarPor(c => c.Nome!.Equals(nome));
                return recuperar != null ? Results.Ok(recuperar) : Results.NotFound();
            });


            GroupBuilder.MapGet("/{id:int}", ([FromServices] DAL<Categorias> DAL, int id) =>
            {
                var idRecuperado = DAL.RecuperarPor(i => i.Id_Categoria == id);
                return idRecuperado != null ? Results.Ok(idRecuperado) : Results.NotFound();
            });



            GroupBuilder.MapPost("", ([FromServices] DAL<Categorias> DAL, CategoriasRequest request) =>
            {
                var categorias = new Categorias(request.nome, request.tipo);
                DAL.Adicionar(categorias);
                return Results.Ok();
            });


            GroupBuilder.MapDelete("/{id:int}", ([FromServices] DAL<Categorias> DAL, int id) =>
            {
                var recuperarID = DAL.RecuperarPor(i => i.Id_Categoria == id);
                if (recuperarID != null)
                {
                    DAL.Deletar(recuperarID);
                    return Results.NoContent();
                }
                else
                {
                    return Results.NotFound();
                }
            });
 

            GroupBuilder.MapPut("/{id}", ([FromServices] DAL<Categorias> DAL, int id, [FromBody] CategoriasRequestEdit requestEdit) =>
            {
                var Recuperar = DAL.RecuperarPor(a => a.Id_Categoria == id);

                if (Recuperar != null)
                {
                    Recuperar.Nome = requestEdit.nome;
                    Recuperar.Tipo = requestEdit.tipo;
                    DAL.Atualizar(Recuperar);

                    return Results.Ok(Recuperar);
                }
                else
                {
                    return Results.NotFound();
                }
            });


            GroupBuilder.MapGet("/ExportCategorias", ([FromServices] DAL<Categorias> DAL) =>
            {
                var categorias = DAL.Listar();

                string path = "C:\\WalletWatch-Files\\";
                string FullPath = path + "ListaCategorias.csv";

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                using (FileStream fs = File.Create(FullPath))
                {
                    fs.Close();
                }

                if (File.Exists(FullPath))
                {
                    using (StreamWriter sw = new StreamWriter(FullPath))
                    {
                        sw.WriteLine("Código da Categoria\t  Descrição\t   Tipo\t");
                        foreach (var cataegoria in categorias)
                        {
                            sw.WriteLine($"{cataegoria.Id_Categoria}\t,{cataegoria.Nome}\t,{cataegoria.Tipo}\t");

                        }
                    }

                }
            });


        }

    }
}
