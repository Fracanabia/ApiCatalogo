using ApiCatalogoJogos.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoJogos.Repositories
{
    public class JogoRepository : IJogoRepository
    {

        private static Dictionary<Guid, Jogo> jogos = new Dictionary<Guid, Jogo>()
        {
            { Guid.Parse("43c4ba1a-5251-4562-9787-54dd7e5214c2"), new Jogo { Id = Guid.Parse("43c4ba1a-5251-4562-9787-54dd7e5214c2"), Nome = "Fifa 21", Produtora = "EA", Preco = 200 } },
            { Guid.Parse("02f5b4aa-e1e7-4f6d-a411-56efe97bf3f8"), new Jogo { Id = Guid.Parse("02f5b4aa-e1e7-4f6d-a411-56efe97bf3f8"), Nome = "Fifa 20", Produtora = "EA", Preco = 190 } },
            { Guid.Parse("de80c984-17fc-11ec-9621-0242ac130002"), new Jogo { Id = Guid.Parse("de80c984-17fc-11ec-9621-0242ac130002"), Nome = "Fifa 19", Produtora = "EA", Preco = 180 } },
            { Guid.Parse("14b24762-2db5-4743-932f-91be556db93c"), new Jogo { Id = Guid.Parse("14b24762-2db5-4743-932f-91be556db93c"), Nome = "Fifa 18", Produtora = "EA", Preco = 170 } },
            { Guid.Parse("5fe4679f-79aa-4795-a8d5-09e179eb7c46"), new Jogo { Id = Guid.Parse("5fe4679f-79aa-4795-a8d5-09e179eb7c46"), Nome = "Street Fighter V", Produtora = "Capcom", Preco = 170 } },
            { Guid.Parse("48b48598-1347-4cd7-9695-d624bbb0518a"), new Jogo { Id = Guid.Parse("48b48598-1347-4cd7-9695-d624bbb0518a"), Nome = "Grand Theft Auto V", Produtora = "Rockstar", Preco = 170 } },
        };

        public Task<List<Jogo>> Obter(int pagina, int quantidade)
        {
            return Task.FromResult(jogos.Values.Skip((pagina - 1) * quantidade).Take(quantidade).ToList());
        }

        public Task<Jogo> Obter(Guid id)
        {
            if (!jogos.ContainsKey(id))
                return null;
            return Task.FromResult(jogos[id]);
        }


        public Task<List<Jogo>> Obter(string nome, string produtora)
        {
            return Task.FromResult(jogos.Values.Where(jogo => jogo.Nome.Equals(nome) && jogo.Produtora.Equals(produtora)).ToList());
        }

        //Está função é a mesma coisa de Obter(string nome, string produtora), porém sem usar lambda;
        public Task<List<Jogo>> ObterSemLambda(string nome, string produtora)
        {
            var retorno = new List<Jogo>();

            foreach (var jogo in jogos.Values)
            {
                if (jogo.Nome.Equals(nome) && jogo.Produtora.Equals(produtora))
                    retorno.Add(jogo);
            }

            return Task.FromResult(retorno);
        }


        public Task Inserir(Jogo jogo)
        {
            jogos.Add(jogo.Id, jogo);
            return Task.CompletedTask;
        }

        public Task Atualizar(Jogo jogo)
        {
            jogos[jogo.Id] = jogo;
            return Task.CompletedTask;
        }


        public Task Remover(Guid id)
        {
            jogos.Remove(id);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            //Fechar conexão com o banco;
        }
    }
}
