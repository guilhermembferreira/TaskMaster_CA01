using System.Data;

namespace TM_App01
{
    internal class Program
    {
        static List<Trabalhadores> ListTrabalhadores = new List<Trabalhadores>();
        static int idpr = 1;
        static int idproj = 1;

        #region Criar Trabalhadorr
        static Trabalhadores.Regioes LerRegiao()
        {
            Console.WriteLine("Selecione a região do trabalhador:");
            foreach (Trabalhadores.Regioes regiao in Enum.GetValues(typeof(Trabalhadores.Regioes)))
            {
                Console.WriteLine($"{(int)regiao}: {regiao}");
            }

            int escolha = Convert.ToInt32(Console.ReadLine());
            if (Enum.IsDefined(typeof(Trabalhadores.Regioes), escolha))
            {
                return (Trabalhadores.Regioes)escolha;
            }
            else
            {
                Console.WriteLine("Opção inválida. Escolha novamente.");
                return LerRegiao();
            }
        }
        static Trabalhadores CriarTrabalhador()
        {
            int id = idpr++;

            Console.Write("Primeiro nome: ");
            string primeiroNome = Console.ReadLine();

            Console.Write("Último nome: ");
            string ultimoNome = Console.ReadLine();

            Console.Write("Email: ");
            string email = Console.ReadLine();

            Console.Write("Password: ");
            string password = Console.ReadLine();

            Console.Write("Data de Nascimento (yyyy-MM-dd): ");
            DateTime dataNascimento = DateTime.Parse(Console.ReadLine());

            Trabalhadores.Regioes regiao = LerRegiao();

            DateTime dataRegistro = DateTime.Now;

            return new Trabalhadores(id, primeiroNome, ultimoNome, email, password, dataNascimento, regiao, dataRegistro);
        }
        #endregion

        #region Criar Projetos
        static Projetos.EstadoProjeto LerEstadoProjeto()
        {
            Console.WriteLine("Selecione Estado do Projeto: ");
            foreach (Projetos.EstadoProjeto estado in Enum.GetValues(typeof(Projetos.EstadoProjeto)))
            {
                Console.WriteLine($"{(int)estado}: {estado}");
            }

            int escolha = Convert.ToInt32(Console.ReadLine());
            if (Enum.IsDefined(typeof(Projetos.EstadoProjeto), escolha))
            {
                return (Projetos.EstadoProjeto)escolha;
            }
            else
            {
                Console.WriteLine("Opção inválida. Escolha novamente.");
                return LerEstadoProjeto();
            }
        }

        static Projetos CriarProjeto()
        {
            int id = idproj++;

            Console.Write("Nome do Projeto: ");
            string nomeProjeto = Console.ReadLine();

            Console.Write("Descrição do Projeto: ");
            string descricaoProjeto = Console.ReadLine();

            DateTime dataCriacao = DateTime.Now;

            DateTime dataFim = DateTime.Parse(Console.ReadLine());

            Projetos.EstadoProjeto estadoProjeto = LerEstadoProjeto();

            DateTime dataRegistro = DateTime.Now;

            return new Projetos(id, nomeProjeto, descricaoProjeto, dataCriacao, dataFim, estadoProjeto);
        }
        #endregion

        #region Menus

        #region Menu Prinicipal
        static int Menu()
        {
            Console.Write("Esreve algo Menu Pr: ");
            int opcao = Convert.ToInt32(Console.ReadLine());
            return opcao;
        }
        #endregion

        #region SubMenu Criações (Trabalhadores, Projetos, Equipas)
        static void SubMenuCriar()
        {
            Trabalhadores trabalhadores = new Trabalhadores();
            Projetos projetos = new Projetos();

            int criarOpcao = 0;

            do
            {
                Console.WriteLine("Submenu de Opção 1:");
                Console.WriteLine("1. Criar Trabalhadores");
                Console.WriteLine("2. Criar Projetos");
                Console.WriteLine("3. Criar Tarefas");
                Console.WriteLine("4. Criar Equipas");
                Console.WriteLine("5. Voltar ao Menu Principal");
                criarOpcao = Convert.ToInt32(Console.ReadLine());

                switch(criarOpcao) 
                {
                    case 1:
                        Console.Write("Quantos Trabalhadores deseja criar: ");
                        int nTrabalhadores = Convert.ToInt32(Console.ReadLine());

                        for (int i = 0; i < nTrabalhadores; i++)
                        {
                            Trabalhadores trabalhador = CriarTrabalhador();
                            ListTrabalhadores.Add(trabalhador);
                            Console.Clear();                           
                            Console.WriteLine("Trabalhador criado com sucesso!");
                        }
                        break;
                    case 5:
                        return;

                    default:
                        Console.WriteLine("Opção Inválida!");
                        break;

                }
            } while (criarOpcao != 0);
        }
        #endregion

        #region SubMenu Ler (Trabalhadores, Projetos, Equipas)
        static void SubMenuLer()
        {
            Trabalhadores trabalhadores = new Trabalhadores();
            Projetos projetos = new Projetos();

            int lerOpcao = 0;

            do
            {
                Console.Clear();
                Console.WriteLine("Submenu de Opção 1:");
                Console.WriteLine("1. Ler Trabalhadores");
                Console.WriteLine("2. Ler Projetos");
                Console.WriteLine("3. Ler Tarefas");
                Console.WriteLine("4. Ler Equipas");
                Console.WriteLine("5. Voltar ao Menu Principal");
                lerOpcao = Convert.ToInt32(Console.ReadLine());

                switch (lerOpcao)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("Lista de Trabalhadores:");
                        foreach (Trabalhadores trabalhador in ListTrabalhadores)
                        {
                            Console.WriteLine($"Trabalhador:");
                            Console.WriteLine(trabalhador.ToString());
                        }
                        break;
                    case 5:
                        return;

                    default: 
                        Console.WriteLine("Opção Inválida!");
                        break;

                }
            } while (lerOpcao != 0);
        }
        #endregion

        #endregion
        static void Main(string[] args)
        {
            Trabalhadores trabalhadores = new Trabalhadores();
            Projetos projetos = new Projetos();

            int opcao = 0;
            do
            {
                opcao = Menu();
                switch (opcao)
                {
                    case 1:
                        SubMenuCriar();
                        break;
                    case 2:
                        SubMenuLer();
                        break;

                } 
            } while (opcao != 0);
        }
    }
}