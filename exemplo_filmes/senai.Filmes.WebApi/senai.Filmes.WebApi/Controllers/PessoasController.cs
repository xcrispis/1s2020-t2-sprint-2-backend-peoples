using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using senai.Filmes.WebApi.Domains;
using senai.Filmes.WebApi.Interfaces;
using senai.Filmes.WebApi.Repositories;

namespace senai.Filmes.WebApi.Controllers
{
    /// <summary>
    /// Controller responsável pelos endpoints referentes aos Pessoas
    /// </summary>

    // Define que o tipo de resposta da API será no formato JSON
    [Produces("application/json")]

    // Define que a rota de uma requisição será no formato domínio/api/NomeController
    [Route("api/[controller]")]

    // Define que é um controlador de API
    [ApiController]
    public class PessoasController : ControllerBase
    {
        /// <summary>
        /// Cria um objeto _PessoasRepository que irá receber todos os métodos definidos na interface
        /// </summary>
        private IPessoasRepository _PessoasRepository { get; set; }

        /// <summary>
        /// Instancia este objeto para que haja a referência aos métodos no repositório
        /// </summary>
        public PessoasController()
        {
            _PessoasRepository = new PessoasRepository();
        }

        /// <summary>
        /// Lista todos os Pessoas
        /// </summary>
        /// <returns>Retorna uma lista de Pessoas</returns>
        /// dominio/api/Pessoas
        [HttpGet]
        public IEnumerable<PessoasDomain> Get()
        {
            // Faz a chamada para o método .Listar();
            return _PessoasRepository.Listar();
        }

        /// <summary>
        /// Cadastra um novo Pessoas
        /// </summary>
        /// <param name="novoPessoas">Objeto Pessoas recebido na requisição</param>
        /// <returns>Retorna um status code 201 (created)</returns>
        /// dominio/api/Pessoas
        [HttpPost]
        public IActionResult Post(PessoasDomain novoPessoas)
        {
            // Faz a chamada para o método .Cadastrar();
            _PessoasRepository.Cadastrar(novoPessoas);

            // Retorna um status code 201 - Created
            return StatusCode(201);
        }

        /// <summary>
        /// Busca um Pessoas através do seu ID
        /// </summary>
        /// <param name="id">ID do Pessoas buscado</param>
        /// <returns>Retorna um Pessoas buscado</returns>
        /// dominio/api/Pessoas/1
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            // Cria um objeto PessoasBuscado que irá receber o Pessoas buscado no banco de dados
            PessoasDomain PessoasBuscado = _PessoasRepository.BuscarPorId(id);

            // Verifica se nenhum Pessoas foi encontrado
            if (PessoasBuscado == null)
            {
                // Caso não seja encontrado, retorna um status code 404 com a mensagem personalizada
                return NotFound("Nenhum Pessoas encontrado");
            }

            // Caso seja encontrado, retorna o Pessoas buscado
            return Ok(PessoasBuscado);
        }

        /// <summary>
        /// Atualiza um Pessoas existente passando o ID no corpo da requisição
        /// </summary>
        /// <param name="PessoasAtualizado">Objeto Pessoas que será atualizado</param>
        /// <returns>Retorna um status code 204 - No Content</returns>
        /// dominio/api/Pessoas
        [HttpPut]
        public IActionResult PutIdCorpo(PessoasDomain PessoasAtualizado)
        {
            // Cria um objeto PessoasBuscado que irá receber o Pessoas buscado no banco de dados
            PessoasDomain PessoasBuscado = _PessoasRepository.BuscarPorId(PessoasAtualizado.IdPessoas);

            // Verifica se algum Pessoas foi encontrado
            if (PessoasBuscado != null)
            {
                // Tenta atualizar o registro
                try
                {
                    // Faz a chamada para o método .AtualizarIdCorpo();
                    _PessoasRepository.AtualizarIdCorpo(PessoasAtualizado);

                    // Retorna um status code 204 - No Content
                    return NoContent();
                }
                // Caso ocorra algum erro
                catch (Exception erro)
                {
                    // Retorna BadRequest e o erro
                    return BadRequest(erro);
                }
                
            }

            // Caso não seja encontrado, retorna NotFound com uma mensagem personalizada
            // e um bool para representar que houve erro
            return NotFound
                (
                    new
                    {
                        mensagem = "Pessoas não encontrado",
                        erro = true
                    }
                );
        }

        /// <summary>
        /// Atualiza um Pessoas existente passando o ID no recurso
        /// </summary>
        /// <param name="id">ID do Pessoas que será atualizado</param>
        /// <param name="PessoasAtualizado">Objeto Pessoas que será atualizado</param>
        /// <returns>Retorna um status code</returns>
        /// dominio/api/Pessoas/1
        [HttpPut("{id}")]
        public IActionResult PutIdUrl(int id, PessoasDomain PessoasAtualizado)
        {
            // Cria um objeto PessoasBuscado que irá receber o Pessoas buscado no banco de dados
            PessoasDomain PessoasBuscado = _PessoasRepository.BuscarPorId(id);

            // Verifica se nenhum Pessoas foi encontrado
            if (PessoasBuscado == null)
            {
                // Caso não seja encontrado, retorna NotFound com uma mensagem personalizada
                // e um bool para representar que houve erro
                return NotFound
                    (
                        new
                        {
                            mensagem = "Pessoas não encontrado",
                            erro = true
                        }
                    );
            }

            // Tenta atualizar o registro
            try
            {
                // Faz a chamada para o método .AtualizarIdUrl();
                _PessoasRepository.AtualizarIdUrl(id, PessoasAtualizado);

                // Retorna um status code 204 - No Content
                return NoContent();
            }
            // Caso ocorra algum erro
            catch (Exception erro)
            {
                // Retorna BadRequest e o erro
                return BadRequest(erro);
            }
        }

        /// <summary>
        /// Deleta um Pessoas passando o ID
        /// </summary>
        /// <param name="id">ID da pessoa que será deletado</param>
        /// <returns>Retorna um status code com uma mensagem personalizada</returns>
        /// dominio/api/Pessoas/1
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // Faz a chamada para o método .Deletar();
            _PessoasRepository.Deletar(id);

            // Retorna um status code com uma mensagem personalizada
            return Ok("Pessoa deletado");
        }
    }
}