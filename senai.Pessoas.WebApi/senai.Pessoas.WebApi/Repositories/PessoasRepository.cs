using senai.Pessoas.WebApi.Domains;
using senai.Pessoas.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace senai.Pessoas.WebApi.Repositories
{
    /// <summary>
    /// Repositório dos Pessoas
    /// </summary>
    public class PessoasRepository : IPessoasRepository
    {
        /// <summary>
        /// String de conexão com o banco de dados que recebe os parâmetros
        /// Data Source - Nome do Servidor
        /// initial catalog - Nome do Banco de Dados
        /// integrated security=true - Faz a autenticação com o usuário do sistema
        /// user Id=sa; pwd=sa@132 - Faz a autenticação com um usuário específico, passando o logon e a senha
        /// </summary>
        //private string StringConexao = "Data Source=DESKTOP-NJ6LHN1\\SQLDEVELOPER; initial catalog=Filmes; integrated security=true;";
        private string stringConexao = "Data Source=DEV901\\SQLEXPRESS; initial catalog=M_Peoples; user Id=sa; pwd=sa@132";

        /// <summary>
        /// Atualiza um Pessoas passando o ID pelo corpo da requisição
        /// </summary>
        /// <param name="Pessoas">Objeto Pessoas que será atualizado</param>
        public void AtualizarIdCorpo(PessoasDomain Pessoas)
        {
            // Declara a conexão passando a string de conexão
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                // Declara a query que será executada
                string queryUpdate = "UPDATE funcionarios SET Nome = @Nome, Sobrenome = @Sobrenome WHERE IdPessoas = @ID";

                // Declara o SqlCommand passando o comando a ser executado e a conexão
                using (SqlCommand cmd = new SqlCommand(queryUpdate, con))
                {
                    // Passa os valores dos parâmetros
                    cmd.Parameters.AddWithValue("@ID", Pessoas.IdPessoas);
                    cmd.Parameters.AddWithValue("@Nome", Pessoas.Nome);
                    cmd.Parameters.AddWithValue("@Sobrenome", Pessoas.Sobrenome);

                    // Abre a conexão com o banco de dados
                    con.Open();

                    // Executa o comando
                    cmd.ExecuteNonQuery();
                }
            }
        }



        /// <summary>
        /// Busca um Pessoas pelo ID
        /// </summary>
        /// <param name="id">ID do Pessoas que será buscado</param>
        /// <returns>Retorna um Pessoas buscado ou null caso não seja encontrado</returns>
        public PessoasDomain BuscarPorId(int id)
        {
            // Declara a conexão passando a string de conexão
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                // Declara a query que será executada
                string querySelectById = "SELECT IdPessoas, Nome, Sobrenome FROM funcionarios WHERE IdPessoas = @ID";

                // Abre a conexão com o banco de dados
                con.Open();

                // Declara o SqlDataReader fazer a leitura no banco de dados
                SqlDataReader rdr;

                // Declara o SqlCommand passando o comando a ser executado e a conexão
                using (SqlCommand cmd = new SqlCommand(querySelectById, con))
                {
                    // Passa o valor do parâmetro
                    cmd.Parameters.AddWithValue("@ID", id);

                    // Executa a query
                    rdr = cmd.ExecuteReader();

                    // Caso a o resultado da query possua registro
                    if (rdr.Read())
                    {
                        // Cria um objeto Pessoas
                        PessoasDomain Pessoas = new PessoasDomain
                        {
                            // Atribui à propriedade IdPessoas o valor da coluna "IdPessoas" da tabela do banco
                            IdPessoas = Convert.ToInt32(rdr["IdPessoas"])

                            // Atribui à propriedade Nome o valor da coluna "Nome" da tabela do banco
                            ,
                            Nome = rdr["Nome"].ToString(),
                            Sobrenome = rdr["Sobrenome"].ToString()
                        };

                        // Retorna o Pessoas com os dados obtidos
                        return Pessoas;
                    }

                    // Caso o resultado da query não possua registros, retorna null
                    return null;
                }
            }
        }

        /// <summary>
        /// Cadastra um novo Pessoas
        /// </summary>
        /// <param name="Pessoas">Objeto Pessoas que será cadastrado</param>
        public void Cadastrar(PessoasDomain Pessoas)
        {
            // Declara a conexão passando a string de conexão
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                // Declara a query que será executada
                // string queryInsert = "INSERT INTO Pessoas(Nome) VALUES ('" + Pessoas.Nome + "')";
                // Não usar dessa forma pois pode causar o efeito Joana D'arc
                // Além de permitir SQL Injection
                // Por exemplo
                // "nome" : "')DROP TABLE Filmes--'"
                // Ao tentar cadastrar o comando acima, irá deletar a tabela Filmes do banco de dados
                // https://www.devmedia.com.br/sql-injection/6102

                // Declara a query que será executada passando o valor como parâmetro, evitando assim os problemas acima
                string queryInsert = "INSERT INTO funcionarios(Nome, sobrenome, datanascimento) VALUES (@Nome, @Sobrenome, @DataNascimento)";

                // Declara o comando passando a query e a conexão
                SqlCommand cmd = new SqlCommand(queryInsert, con);

                // Passa o valor do parâmetro
                cmd.Parameters.AddWithValue("@Nome", Pessoas.Nome);
                cmd.Parameters.AddWithValue("@Sobrenome", Pessoas.Sobrenome);
                cmd.Parameters.AddWithValue("@DataNascimento", Pessoas.datanascimento);

                // Abre a conexão com o banco de dados
                con.Open();

                // Executa o comando
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Deleta um Pessoas através do seu ID
        /// </summary>
        /// <param name="id">ID do Pessoas que será deletado</param>
        public void Deletar(int id)
        {
            // Declara a conexão passando a string de conexão
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                // Declara a query que será executada passando o valor como parâmetro
                string queryDelete = "DELETE FROM funcionarios WHERE IdPessoas = @ID";

                // Declara o comando passando a query e a conexão
                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
                    // Passa o valor do parâmetro
                    cmd.Parameters.AddWithValue("@ID", id);

                    // Abre a conexão com o banco de dados
                    con.Open();

                    // Executa o comando
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Lista todos os Pessoas
        /// </summary>
        /// <returns>Retorna uma lista de Pessoas</returns>
        public List<PessoasDomain> Listar()
        {
            // Cria uma lista Pessoas onde serão armazenados os dados
            List<PessoasDomain> Pessoas = new List<PessoasDomain>();

            // Declara a SqlConnection passando a string de conexão
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                // Declara a instrução a ser executada
                string querySelectAll = "SELECT IdPessoas, Nome, Sobrenome, datanascimento from funcionarios";

                // Abre a conexão com o banco de dados
                con.Open();

                // Declara o SqlDataReader para percorrer a tabela do banco
                SqlDataReader rdr;

                // Declara o SqlCommand passando o comando a ser executado e a conexão
                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {
                    // Executa a query
                    rdr = cmd.ExecuteReader();

                    // Enquanto houver registros para ler, o laço se repete
                    while (rdr.Read())
                    {
                        // Cria um objeto Pessoas do tipo PessoasDomain
                        PessoasDomain Pessoa = new PessoasDomain
                        {
                            // Atribui à propriedade IdPessoas o valor da primeira coluna da tabela do banco
                            IdPessoas = Convert.ToInt32(rdr[0]),

                            // Atribui à propriedade Nome o valor da coluna "Nome" da tabela do banco
                            Nome = rdr["Nome"].ToString(),
                            Sobrenome = rdr["Sobrenome"].ToString(),
                            datanascimento = rdr["datanascimento"].ToString()
                        };

                        // Adiciona o Pessoas criado à tabela Pessoas
                        Pessoas.Add(Pessoa);
                    }
                }
            }

            // Retorna a lista de Pessoas
            return Pessoas;
        }

        public PessoasDomain ListarPorNome(string Nome)
        {
            {
                // Declara a conexão passando a string de conexão
                using (SqlConnection con = new SqlConnection(stringConexao))
                {
                    // Declara a query que será executada
                    string querySelectById = "SELECT IdPessoas, Nome, Sobrenome, datanascimento from funcionarios WHERE Nome LIKE %@Nome1%";

                    // Abre a conexão com o banco de dados
                    con.Open();

                    // Declara o SqlDataReader fazer a leitura no banco de dados
                    SqlDataReader rdr;

                    // Declara o SqlCommand passando o comando a ser executado e a conexão
                    using (SqlCommand cmd = new SqlCommand(querySelectById, con))
                    {
                        // Passa o valor do parâmetro
                        cmd.Parameters.AddWithValue("@Nome1", Nome);

                        // Executa a query
                        rdr = cmd.ExecuteReader();

                        // Caso a o resultado da query possua registro
                        if (rdr.Read())
                        {
                            // Cria um objeto Pessoas
                            PessoasDomain Pessoas = new PessoasDomain
                            {
                                // Atribui à propriedade IdPessoas o valor da coluna "IdPessoas" da tabela do banco
                                IdPessoas = Convert.ToInt32(rdr["IdPessoas"])

                                // Atribui à propriedade Nome o valor da coluna "Nome" da tabela do banco
                                ,
                                Nome = rdr["Nome"].ToString(),
                                Sobrenome = rdr["Sobrenome"].ToString()
                            };

                            // Retorna o Pessoas com os dados obtidos
                            return Pessoas;
                        }

                        // Caso o resultado da query não possua registros, retorna null
                        return null;
                    }
                }
            }
        }
    }
}
