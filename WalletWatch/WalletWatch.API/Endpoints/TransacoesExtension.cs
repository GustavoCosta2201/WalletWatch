using Microsoft.AspNetCore.Mvc;
using WalletWatch.API.Requests;
using WalletWatch.Banco;
using WalletWatch.Modelos;

namespace WalletWatch.API.Endpoints
{
    public static class TransacoesExtension
    {
        public static void AddEndpointsTransacoes(this WebApplication app)
        {

            var GroupBuilder = app.MapGroup("Transacoes").RequireAuthorization().WithTags("Transacoes");

            GroupBuilder.MapGet("", ([FromServices] DAL<Transacoes> DAL) =>{

                return Results.Ok(DAL.Listar());
            });

            GroupBuilder.MapGet("/{nome}", ([FromServices] DAL<Transacoes> DAL, string nome) =>
            {
                var recuperar = DAL.RecuperarPor(a => a.Descricao!.Equals(nome));

                if(recuperar != null)
                {
                    return Results.Ok(recuperar);
                }
                else
                {
                    return Results.NotFound();
                }
            });


            GroupBuilder.MapGet("/{id:int}", ([FromServices] DAL<Transacoes> DAL, int id) =>
            {
                var recuperarid = DAL.RecuperarPor(i => i.Id_Transacao == id);

                if (recuperarid != null)
                {
                    return Results.Ok(recuperarid);
                }
                else
                {
                    return Results.NotFound(id);
                }
            });


            GroupBuilder.MapPost("", ([FromServices] DAL<Transacoes> DAL, TransacoesRequest request) =>
            {

                var transacoes = new Transacoes(request.id_usuario, request.id_categoria, request.descricao, request.valor, request.data);

                DAL.Adicionar(transacoes);
                return Results.Ok(transacoes);
            });

            GroupBuilder.MapDelete("/{id}", ([FromServices] DAL<Transacoes> DAL, int id) =>
            {

                var recuperar = DAL.RecuperarPor(a => a.Id_Transacao.Equals(id));   

                if(recuperar != null)
                {
                    DAL.Deletar(recuperar);
                    return Results.NoContent();
                }
                else{
                    return Results.NotFound();
                }

            });

            GroupBuilder.MapPut("/{id_transacao}", ([FromServices] DAL<Transacoes> DAL, int id_transacao, TransacoesRequestEdit requestEdit) =>
            {

                var recuperar = DAL.RecuperarPor(a => a.Id_Transacao == id_transacao);

                if(recuperar != null)
                {
                    recuperar.Id_Transacao = requestEdit.id_transacao;
                    recuperar.Id_Usuario = requestEdit.id_usuario;
                    recuperar.Id_Categoria = requestEdit.id_categoria;
                    recuperar.Descricao = requestEdit.descricao;
                    recuperar.Valor = requestEdit.valor;
                    recuperar.Data_Transacao = requestEdit.data;

                    DAL.Atualizar(recuperar);

                    return Results.Ok(recuperar);
                }
                else
                {
                    return Results.NotFound();
                }

            });

            GroupBuilder.MapGet("/ExportTransacoes", ([FromServices] DAL<Transacoes> DAL) =>
            {
                var transacoes = DAL.Listar();

                string path = "C:\\WalletWatch-Files\\";
                string FullPath = path + "ListaTransacoes.csv";

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


                        sw.WriteLine("Código da transacão\t  Descrição\t   Tipo\t");
                        foreach (var transacao in transacoes)
                        {
                            var recuperarUsuario = DAL.RecuperarPor(u => u.Id_Usuario == transacao.Id_Usuario);
                            var recuperarCategoria = DAL.RecuperarPor(u => u.Id_Categoria == transacao.Id_Categoria);

                            if (recuperarUsuario != null)
                            {
                                sw.WriteLine($"{transacao.Id_Transacao}\t,{recuperarUsuario.Id_Usuario}\t,{recuperarCategoria?.Id_Categoria}\t, {transacao.Descricao}\t, {transacao.Valor}\t, {transacao.Data_Transacao}\t");
                            }
                            else
                            {
                                sw.WriteLine($"{transacao.Id_Transacao}\t, Usuário não encontrado \t,{recuperarCategoria?.Id_Categoria}\t, {transacao.Descricao}\t, {transacao.Valor}\t, {transacao.Data_Transacao}\t");
                            }

                            if (recuperarCategoria != null)
                            {
                                sw.WriteLine($"{transacao.Id_Transacao}\t,{recuperarUsuario?.Id_Usuario}\t,{recuperarCategoria?.Id_Categoria}\t, {transacao.Descricao}\t, {transacao.Valor}\t, {transacao.Data_Transacao}\t");
                            }
                            else
                            {
                                sw.WriteLine($"{transacao.Id_Transacao}\t, {recuperarUsuario?.Id_Usuario}\t,Categoria não encontrada\t, {transacao.Descricao}\t, {transacao.Valor}\t, {transacao.Data_Transacao}\t");
                            }


                        }
                    }

                }
            });

        }

    }
}
