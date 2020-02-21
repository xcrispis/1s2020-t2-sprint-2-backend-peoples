using senai.Filmes.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.Filmes.WebApi.Interfaces
{
    /// <summary>
    /// Interface responsável pelo repositório Pessoas
    /// </summary>
    interface IPessoasRepository
    {
        /// <summary>
        /// Lista todos os gêneros
        /// </summary>
        /// <returns>Retorna uma lista de gêneros</returns>
        List<PessoasDomain> Listar();

        /// <summary>
        /// Cadastra um novo gênero
        /// </summary>
        /// <param name="Pessoas">Objeto Pessoas que será cadastrado</param>
        void Cadastrar(PessoasDomain Pessoas);

        /// <summary>
        /// Atualiza um gênero existente passando o id pelo corpo da requisição
        /// </summary>
        /// <param name="Pessoas">Objeto Pessoas que será atualizado</param>
        void AtualizarIdCorpo(PessoasDomain Pessoas);

        /// <summary>
        /// Atualiza um gênero existente passando o id pela url da requisição
        /// </summary>
        /// <param name="id">ID do gênero que será atualizado</param>
        /// <param name="Pessoas">Objeto Pessoas que será atualizado</param>
        void AtualizarIdUrl(int id, PessoasDomain Pessoas);

        /// <summary>
        /// Deleta um gênero
        /// </summary>
        /// <param name="id">ID do gênero que será deletado</param>
        void Deletar(int id);

        /// <summary>
        /// Busca um gênero através do ID
        /// </summary>
        /// <param name="id">ID do gênero que será buscado</param>
        /// <returns>Retorna um Pessoas</returns>
        PessoasDomain BuscarPorId(int id);
    }
}
