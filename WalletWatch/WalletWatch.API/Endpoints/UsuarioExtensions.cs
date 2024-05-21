using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using WalletWatch.API.Requests;
using WalletWatch.Banco;
using WalletWatch.Modelos;

namespace WalletWatch.API.Endpoints
{
    public static class UsuarioExtensions
    {

        public static void AddEndpointsUsuarios(this WebApplication app)
        {
            app.MapGet("/Usuarios", ([FromServices] DAL<Usuarios> dal) =>
            {
                return Results.Ok(dal.Listar());
            });

            app.MapGet("/Usuarios/{nome}", ([FromServices] DAL<Usuarios> DAL, string nome) =>
            {
                var recuperar = DAL.RecuperarPor(a => a.Nome!.Equals(nome));

                if (recuperar != null)
                {
                    return Results.Ok(recuperar);
                }
                else
                {
                    return Results.NotFound();
                }
            });

            app.MapPost("/Usuarios/", ([FromServices] DAL<Usuarios> DAL, UsuariosRequest request) =>
            {
                var usuarios = new Usuarios(request.nome, request.senha, request.email);

                DAL.Adicionar(usuarios);

                return Results.Ok(usuarios);
            });

            app.MapDelete("/Usuarios/{id}", ([FromServices] DAL<Usuarios> DAL, int id) => {

                var recuperarID = DAL.RecuperarPor(i => i.Id_Usuario.Equals(id));

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


            app.MapPut("/Usuarios/", ([FromServices] DAL<Usuarios> DAL, [FromBody] UsuariosRequestEdit requestEdit) =>
            {
            var UsuarioAtualizar = DAL.RecuperarPor(u => u.Id_Usuario == requestEdit.id);

                if (UsuarioAtualizar is null)
                {
                    return Results.NotFound();
                }
                else
                {
                    UsuarioAtualizar.Nome = requestEdit.nome;
                    UsuarioAtualizar.Senha = requestEdit.senha;
                    UsuarioAtualizar.Email = requestEdit.email;

                    DAL.Atualizar(UsuarioAtualizar);
                    return Results.Ok();
                }
            });




        }
    }
}
