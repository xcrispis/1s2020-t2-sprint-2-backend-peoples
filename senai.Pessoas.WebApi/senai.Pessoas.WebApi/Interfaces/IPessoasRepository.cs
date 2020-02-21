using senai.Pessoas.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.Pessoas.WebApi.Interfaces
{
    /// <summary>
    /// Interface responsável pelo repositório Pessoas
    /// </summary>
    interface IPessoasRepository
    {
        /// <summary>
        /// Lista todos os gêneros
        /// </summary>
        /// <returns>Retorna uma lista de pessoas</returns>
        List<PessoasDomain> Listar();

        /// <summary>
        /// Cadastra uma nova pessoa
        /// </summary>
        /// <param name="Pessoas">Objeto Pessoas que será cadastrado</param>
        void Cadastrar(PessoasDomain Pessoas);

        /// <summary>
        /// Atualiza uma pessoa existente passando o id pelo corpo da requisição
        /// </summary>
        /// <param name="Pessoas">Objeto Pessoas que será atualizado</param>
        void AtualizarIdCorpo(PessoasDomain Pessoas);

        /// <summary>
        /// Deleta um gênero
        /// </summary>
        /// <param name="id">ID da pessoa que será deletado</param>
        void Deletar(int id);

        /// <summary>
        /// Busca uma pessoa através do ID
        /// </summary>
        /// <param name="id">ID da pessoa que será buscado</param>
        /// <returns>Retorna um Pessoas</returns>
        PessoasDomain BuscarPorId(int id);


        PessoasDomain ListarPorNome(string Nome);
    }
}
