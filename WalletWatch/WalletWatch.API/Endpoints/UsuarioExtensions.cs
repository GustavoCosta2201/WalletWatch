using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using WalletWatch.API.Requests;
using WalletWatch.Banco;
using WalletWatch.Modelos;

namespace WalletWatch.API.Endpoints
{
    public static class UsuarioExtensions
    {

        public static void AddEndpointsUsuarios(this WebApplication app)
        {
            var GroupBuilder = app.MapGroup("Usuarios").RequireAuthorization().WithTags("Usuarios");

            GroupBuilder.MapGet("", ([FromServices] DAL<Usuarios> dal) =>
            {
                return Results.Ok(dal.Listar());
            });



            GroupBuilder.MapGet("/{nome}", ([FromServices] DAL<Usuarios> DAL, string nome) =>
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


            GroupBuilder.MapGet("/{id:int}", ([FromServices] DAL<Usuarios> DAL, int id) =>
            {
                var recuperarid = DAL.RecuperarPor(i => i.Id_Usuario == id);

                if (recuperarid != null)
                {
                    return Results.Ok(recuperarid);
                }
                else
                {
                    return Results.NotFound(id);
                }
            });



            GroupBuilder.MapPost("", ([FromServices] DAL<Usuarios> DAL, UsuariosRequest request) =>
            {
                var usuarios = new Usuarios(request.nome, request.senha, request.email);

                DAL.Adicionar(usuarios);

                return Results.Ok(usuarios);
            });



            GroupBuilder.MapDelete("/{id}", ([FromServices] DAL<Usuarios> DAL, int id) =>
            {

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




            GroupBuilder.MapPut("/{id}", ([FromServices] DAL<Usuarios> DAL, int id, [FromBody] UsuariosRequestEdit requestEdit) =>
            {
                var UsuarioAtualizar = DAL.RecuperarPor(u => u.Id_Usuario == id);

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
                    return Results.Ok(UsuarioAtualizar);
                }
            });


            GroupBuilder.MapGet("/Export", ([FromServices] DAL<Usuarios> dal) =>
            {
                var usuarios = dal.Listar();

                string path = "C:\\WalletWatch-Files\\";
                string FullPath = path + "ListaUsuarios.csv";

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
                        foreach (var usuario in usuarios)
                        {
                            sw.WriteLine($"{usuario.Id_Usuario},{usuario.Nome},{usuario.Email}");

                        }
                    }

                }
            });




        }
    }
}
