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

            app.MapGet("/Categorias/", ([FromServices] DAL<Categorias> DAL) =>
            {
                var lista = DAL.Listar();
                return Results.Ok(lista);
            });

            app.MapGet("/Categorias/{nome}", ([FromServices] DAL<Categorias> DAL, string nome) =>
            {

                var recuperar = DAL.RecuperarPor(c => c.Nome!.Equals(nome));

                if(recuperar != null)
                {
                    return Results.Ok(recuperar);
                }
                else
                {
                    return Results.NotFound();  
                }
            });

            app.MapPost("/Categorias/", ([FromServices] DAL<Categorias> DAL, CategoriasRequest request) =>
            {

                var categorias = new Categorias(request.nome, request.tipo);
                DAL.Adicionar(categorias); 
                
                return Results.Ok();

            });

            app.MapDelete("/Categorias/{id}", ([FromServices] DAL<Categorias> DAL, int id) =>
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

            app.MapPut("/Categorias/", ([FromServices] DAL<Categorias> DAL, [FromBody] CategoriasRequestEdit requestEdit) =>
            {
                var Recuperar = DAL.RecuperarPor(a => a.Id_Categoria == requestEdit.id);

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

        }
    }
}
