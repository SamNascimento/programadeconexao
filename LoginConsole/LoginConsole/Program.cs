using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            // variável para verificar se o login é válido
            bool resultado;
            // variável para o programa se repetir enquanto o usuário quiser continuar
            string continuar = "Sim";
            // variável para ser usada no switch/case
            int escolha;
            // variáveis de armazenamento local para consulta no banco
            string nome, usuario, senha;
            // variável com o nome para conexão
            string servidor = "Data Source=DESKTOP-3L5OF48\\ROCKY;Initial Catalog=TESTE;Integrated Security=True;";

            // loop de repetição para repetir o programa quantas vezes forem desejadas
            while (continuar == "Sim" || continuar == "sim")
            {
                Console.Clear();
                Console.WriteLine("--- Programa simples para cadastro e login em C# ---\n\n");

                Console.WriteLine("Opção 1 - Login");
                Console.WriteLine("Opção 2 - Cadastro");
                Console.Write("Qual Opção deseja efetuar? ");
                escolha = int.Parse(Console.ReadLine());

                switch (escolha)
                {
                    case 1:
                        Console.WriteLine("\n----------\n");
                        Console.Write("Login: ");
                        usuario = Console.ReadLine();
                        Console.Write("Senha: ");
                        senha = Console.ReadLine();
                        // verifica o login e armazena o resultado da verificação
                        resultado = verificarLogin(usuario, senha, servidor);
                        // se a verificação foi um sucesso então executar função de logar 
                        if (resultado)
                            logar(usuario, senha, servidor);
                        else
                            Console.WriteLine("Usuário ou senha incorretos");
                        break;
                    case 2:
                        Console.WriteLine("\n----------\n");
                        Console.Write("Digite seu nome: ");
                        nome = Console.ReadLine();
                        Console.Write("Digite seu login: ");
                        usuario = Console.ReadLine();
                        Console.Write("Digite sua senha: ");
                        senha = Console.ReadLine();
                        // mandando os dados e cadastrando com a função cadastrar
                        cadastrar(nome, usuario, senha, servidor);
                        break;
                    default:
                        Console.WriteLine("Comando inválido");
                        break;
                }
                Console.WriteLine("Se deseja fazer outra operação digite 'Sim', caso não queira digite qualquer outra palavra");
                continuar = Console.ReadLine();
            }
            // função com uma mensagem para a finalização do programa
            finalizarPrograma();
        }

        public static bool verificarLogin(string user, string password, string server)
        {
            bool result = false;
            string sql = "SELECT * FROM tblUsuario WHERE (usuario = '" + user + "') AND (senha = '" + password + "');";
            // instanciando a variável de conexão
            SqlConnection conexao = new SqlConnection(server);
            // enviando comando sql
            SqlCommand comando = new SqlCommand(sql, conexao);
            comando.CommandType = CommandType.Text;
            // abrindo conexão
            conexao.Open();
            try
            {
                SqlDataReader leitor = comando.ExecuteReader();
                // HasRows retorna verdadeiro caso haja linhas e caso não haja ele retorna falso
                // em caso de retorno verdadeiro significa que existem dados na tabela referentes a esse usuário e essa senha, então o login pode ser feito
                // em caso de retorno falso significa que esse usuário e essa senha não contam no banco de dados
                result = leitor.HasRows;
            }
            catch (SqlException e)
            {
                // exibe uma mensagem em caso de erro
                throw new Exception(e.Message);
            }
            finally
            {
                // fecha conexão
                conexao.Close();
            }
            // retorna verdadeiro ou falso para a variável resultado
            return result;
        }

        public static void logar(string user, string password, string server)
        {
            string name;
            string sql = "SELECT NOME FROM tblUsuario WHERE (usuario = '" + user +"') AND (senha = '"+ password +"');";
            SqlConnection conexao = new SqlConnection(server);
            SqlCommand comando = new SqlCommand(sql, conexao);
            comando.CommandType = CommandType.Text;
            SqlDataReader leitor;
            conexao.Open();
            try
            {
                // Lê o resultado da query e armazena
                leitor = comando.ExecuteReader();
                if (leitor.Read())
                {
                    // define que o valor da váriavel name é o primeiro (e único) valor retornado da query
                    name = leitor[0].ToString();
                    Console.WriteLine("Você logou com sucesso! Bem vindo/a " + name);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.ToString());
            }
            finally
            {
                conexao.Close();
            }
        }

        public static void cadastrar(string name, string user, string password, string server)
        {
            string sql = "INSERT INTO tblUsuario (nome, usuario, senha) VALUES ('" + name + "', '" + user + "', '" + password + "')";
            SqlConnection conexao = new SqlConnection(server);
            SqlCommand comando = new SqlCommand(sql, conexao);
            comando.CommandType = CommandType.Text;
            conexao.Open();
            try
            {
                // executa o query e armazena o valor de linhas afetadas
                int i = comando.ExecuteNonQuery();
                // caso mais de uma linha tenha sido afetada ele retorna uma mensagem de êxito no cadastro
                if (i > 0)
                    Console.WriteLine("Cadastro realizado com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.ToString());
            }
            finally
            {
                conexao.Close();
            }
        }

        // Função para exibir mensagem de finalizar programa
        public static void finalizarPrograma()
        {
            Console.Write("\nAperte qualquer botão para finalizar o programa");
            Console.ReadKey();
        }
    }
}
