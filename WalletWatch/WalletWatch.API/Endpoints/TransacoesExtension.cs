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

            app.MapGet("/Transacoes/", ([FromServices] DAL<Transacoes> DAL) =>{

                return Results.Ok(DAL.Listar());
            });

            app.MapGet("/Transacoes/{nome}", ([FromServices] DAL<Transacoes> DAL, string nome) =>
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


            app.MapGet("/Transacoes/{id:int}", ([FromServices] DAL<Transacoes> DAL, int id) =>
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


            app.MapPost("/Transacoes/", ([FromServices] DAL<Transacoes> DAL, TransacoesRequest request) =>
            {

                var transacoes = new Transacoes(request.id_usuario, request.id_categoria, request.descricao, request.valor, request.data);

                DAL.Adicionar(transacoes);
                return Results.Ok(transacoes);
            });

            app.MapDelete("/Transacoes/{id}", ([FromServices] DAL<Transacoes> DAL, int id) =>
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

            app.MapPut("/Transacoes/{id_transacao}", ([FromServices] DAL<Transacoes> DAL, int id_transacao, TransacoesRequestEdit requestEdit) =>
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

        }

    }
}
