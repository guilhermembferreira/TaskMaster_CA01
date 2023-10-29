using System.Data;
using System.Text.RegularExpressions;
using static TM_App01.Projetos;
using static TM_App01.Trabalhadores;

namespace TM_App01
{
    internal class Program
    {
        #region Listagem dos Objetos
        static List<Trabalhadores> ListTrabalhadores = new List<Trabalhadores>();
        static List<Projetos> ListProjetos = new List<Projetos>();
        #endregion

        #region Criar 
        #region Criação dos ID's
        static int idpr = 1;
        static int idproj = 1;
        #endregion

        #region Criar Trabalhador

        #region Enum Regiões
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
        #endregion

        #region Criar Trabalhador
        static Trabalhadores CriarTrabalhador()
        {
            int id = idpr++;

            string primeiroNome, ultimoNome, email, password;
            DateTime dataNascimento;

            do
            {
                Console.Write("Primeiro nome: ");
                primeiroNome = Console.ReadLine();
                if (string.IsNullOrEmpty(primeiroNome) || !char.IsUpper(primeiroNome[0]))
                    Console.WriteLine("Erro: O primeiro nome deve começar com uma letra maiúscula e não pode estar vazio.");
            } while (string.IsNullOrEmpty(primeiroNome) || !char.IsUpper(primeiroNome[0]));

            do
            {
                Console.Write("Último nome: ");
                ultimoNome = Console.ReadLine();
                if (string.IsNullOrEmpty(ultimoNome) || !char.IsUpper(ultimoNome[0]))
                    Console.WriteLine("Erro: O último nome deve começar com uma letra maiúscula e não pode estar vazio.");
            } while (string.IsNullOrEmpty(ultimoNome) || !char.IsUpper(ultimoNome[0]));

            do
            {
                Console.Write("Email: ");
                email = Console.ReadLine();
                if (string.IsNullOrEmpty(email) || !email.Contains("@"))
                    Console.WriteLine("Erro: O email deve conter pelo menos um '@'.");
            } while (string.IsNullOrEmpty(email) || !email.Contains("@"));

            do
            {
                Console.Write("Password: ");
                password = Console.ReadLine();
                if (string.IsNullOrEmpty(password) || !TerNumero(password) || !TerLetraM(password) || !TerCaracterEspecial(password))
                    Console.WriteLine("Erro: A senha deve conter pelo menos 1 número, 1 letra maiúscula e 1 caractere especial.");
            } while (string.IsNullOrEmpty(password) || !TerNumero(password) || !TerLetraM(password) || !TerCaracterEspecial(password));

            do
            {
                Console.Write("Data de Nascimento (dd/MM/yyyy): ");
            } while (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out dataNascimento));

            Trabalhadores.Regioes regiao = LerRegiao();

            DateTime dataRegistro = DateTime.Now;

            return new Trabalhadores(id, primeiroNome, ultimoNome, email, password, dataNascimento, regiao, dataRegistro);
        }
        #endregion

        #endregion

        #region Criar / Associar -> Projetos

        #region Enum Estado Projeto
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
        #endregion

        #region Criar Projeto
        static Projetos CriarProjeto()
        {
            int id = idproj++;

            string nomeProjeto = "";
            string descricaoProjeto = "";
            DateTime dataFim = DateTime.Now;

            do
            {
                Console.Write("Nome do Projeto: ");
                nomeProjeto = Console.ReadLine();

                if (!Regex.IsMatch(nomeProjeto, @"^[A-Z0-9][a-zA-Z0-9 ]*$"))
                {
                    Console.WriteLine("O nome do projeto deve começar com letra maiúscula ou número e conter apenas letras, números e espaços.");
                }
            } while (!Regex.IsMatch(nomeProjeto, @"^[A-Z0-9][a-zA-Z0-9 ]*$"));

            do
            {
                Console.Write("Descrição do Projeto: ");
                descricaoProjeto = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(descricaoProjeto))
                {
                    Console.WriteLine("A descrição do projeto não pode estar em branco.");
                }
            } while (string.IsNullOrWhiteSpace(descricaoProjeto));

            do
            {
                Console.Write("Data Termino projeto do Projeto (dd/MM/yyyy): ");
                if (DateTime.TryParse(Console.ReadLine(), out dataFim))
                {
                    if (dataFim.Date < DateTime.Today)
                    {
                        Console.WriteLine("A data de término não pode ser anterior à data de hoje.");
                    }
                }
                else
                {
                    Console.WriteLine("Data inválida. Use o formato dd/MM/yyyy.");
                }
            } while (dataFim.Date < DateTime.Today);

            // Dados que não precisam de verificação 

            DateTime dataCriacao = DateTime.Now;

            Projetos.EstadoProjeto estadoProjeto = LerEstadoProjeto();

            return new Projetos(id, nomeProjeto, descricaoProjeto, dataCriacao, dataFim, estadoProjeto);
        }
        #endregion

        #region Associar Trabalhadores a Projetos
        static void SubMenuAssociarProjetos()
        {
            Console.WriteLine("Associar Trabalhador a Projetos");
            Console.WriteLine("Lista de Trabalhadores:");
            foreach (Trabalhadores trabalhador in ListTrabalhadores)
            {
                Console.WriteLine($"{trabalhador.IdTrabalhador}: Nome:{trabalhador.PrimeiroNome} Email:{trabalhador.Email}");
            }
            Console.Write("\nSelecione o ID do Trabalhador para associar a um projeto: ");
            int trabalhadorId = Convert.ToInt32(Console.ReadLine());

            Trabalhadores trabalhadorSelecionado = ListTrabalhadores.FirstOrDefault(t => t.IdTrabalhador == trabalhadorId);

            if (trabalhadorSelecionado == null)
            {
                Console.WriteLine("Trabalhador não encontrado.");
                return;
            }

            Console.WriteLine("Lista de Projetos:");
            foreach (Projetos projeto in ListProjetos)
            {
                Console.WriteLine($"{projeto.IdProjeto}: {projeto.NomeProjeto}");
            }

            Console.Write("Selecione o ID do Projeto para associar ao Trabalhador: ");
            int projetoId = Convert.ToInt32(Console.ReadLine());

            Projetos projetoSelecionado = ListProjetos.FirstOrDefault(p => p.IdProjeto == projetoId);

            if (projetoSelecionado == null)
            {
                Console.WriteLine("Projeto não encontrado.");
                return;
            }

            trabalhadorSelecionado.AdicionarProjeto(projetoSelecionado);
            Console.WriteLine("Projeto associado ao trabalhador com sucesso.");
        }
        #endregion

        #region Mostrar - Associar Trabalhadores a Projetos
        static void SubMenuExibirProjetosAssociados()
        {
            Console.WriteLine("Exibir Projetos Associados a um Trabalhador:");
            Console.WriteLine("Lista de Trabalhadores:");
            foreach (Trabalhadores trabalhador in ListTrabalhadores)
            {
                Console.WriteLine($"{trabalhador.IdTrabalhador}: {trabalhador.PrimeiroNome} {trabalhador.UltimoNome}");
            }

            Console.Write("Selecione o ID do Trabalhador para ver os projetos associados: ");
            int trabalhadorId = Convert.ToInt32(Console.ReadLine());

            Trabalhadores trabalhadorSelecionado = ListTrabalhadores.FirstOrDefault(t => t.IdTrabalhador == trabalhadorId);

            if (trabalhadorSelecionado == null)
            {
                Console.WriteLine("Trabalhador não encontrado.");
                return;
            }

            trabalhadorSelecionado.MostrarProjetosAssociados();
        }
        #endregion

        #region Remover - Associar Trabalhadores a Projetos
        static void SubMenuRemoverProjeto()
        {
            Console.Clear();
            Console.WriteLine("Remover Projeto da Associação com Trabalhador:");
            Console.WriteLine("Lista de Trabalhadores:");
            foreach (Trabalhadores trabalhador in ListTrabalhadores)
            {
                Console.WriteLine($"{trabalhador.IdTrabalhador}: {trabalhador.PrimeiroNome} {trabalhador.UltimoNome}");
            }

            int trabalhadorId = GetInputUtilizador("Selecione o ID do Trabalhador para remover um projeto da associação: ");

            Trabalhadores trabalhadorSelecionado = ListTrabalhadores.FirstOrDefault(t => t.IdTrabalhador == trabalhadorId);

            if (trabalhadorSelecionado == null)
            {
                Console.WriteLine("Trabalhador não encontrado.");
                return;
            }

            Console.WriteLine("Projetos associados ao Trabalhador:");
            foreach (Projetos projeto in trabalhadorSelecionado.Projetos)
            {
                Console.WriteLine($"{projeto.IdProjeto}: {projeto.NomeProjeto}");
            }

            int projetoId = GetInputUtilizador("Selecione o ID do Projeto para remover da associação com o Trabalhador: ");

            Projetos projetoSelecionado = trabalhadorSelecionado.Projetos.FirstOrDefault(p => p.IdProjeto == projetoId);

            if (projetoSelecionado == null)
            {
                Console.WriteLine("Projeto não encontrado na associação com o Trabalhador.");
                return;
            }

            trabalhadorSelecionado.RemoverProjeto(projetoSelecionado);
        }
        #endregion

        #endregion
        #endregion

        #region Editar

        #region Editar projeto
        static void EditarProjetoPorID(int idProjeto)
        {
            Projetos projeto = ListProjetos.FirstOrDefault(p => p.IdProjeto == idProjeto);

            if (projeto == null)
            {
                Console.WriteLine("Projeto não encontrado.");
                return;
            }

            Console.Write("Novo Nome do Projeto: ");
            string novoNome = Console.ReadLine();

            Console.Write("Nova Descrição do Projeto: ");
            string novaDescricao = Console.ReadLine();

            Console.Write("Nova Data de Fim do Projeto (yyyy-MM-dd): ");
            if (DateTime.TryParse(Console.ReadLine(), out DateTime novaDataFim))
            {

                Console.Write("Novo Estado do Projeto: ");
                EstadoProjeto novoEstado = (EstadoProjeto)Enum.Parse(typeof(EstadoProjeto), Console.ReadLine(), true);

                projeto.EditarProjeto(novoNome, novaDescricao, novaDataFim, novoEstado);
                Console.WriteLine("Projeto editado com sucesso!");
            }
            else
            {
                Console.WriteLine("Data inválida.");
            }
        }
        #endregion

        #region Editar Trabalhador
        static void EditarTrabalhadorPorID(int idTrabalhador)
        {
            Trabalhadores trabalhador = ListTrabalhadores.FirstOrDefault(p => p.IdTrabalhador == idTrabalhador);

            if (trabalhador == null)
            {
                Console.WriteLine("Trabalhador não encontrado.");
                return;
            }

            Console.Write("Novo Email do Trabalhador: ");
            string novoemail = Console.ReadLine();

            Console.Write("Nova Password do Projeto: ");
            string novopassword = Console.ReadLine();


            Console.Write("Novo Região do Trabalhador: ");
            Regioes novoregiao = (Regioes)Enum.Parse(typeof(Regioes), Console.ReadLine(), true);

            trabalhador.EditarTrabalhadores(novoemail, novopassword, novoregiao);
            Console.WriteLine("Projeto editado com sucesso!");

        }
        #endregion

        #endregion

        #region Validações
        static int GetInputUtilizador(string msg)
        {
            int numero;
            bool sucesso = false;

            do
            {
                Console.Write(msg);
                sucesso = int.TryParse(Console.ReadLine(), out numero);

                if (!sucesso)
                {
                    Console.WriteLine("Por favor, insira um número inteiro válido.");
                }

            } while (!sucesso);

            return numero;
        }
        static bool TerNumero(string input)
        {
            return input.Any(char.IsDigit);
        }

        static bool TerLetraM(string input)
        {
            return input.Any(char.IsUpper);
        }

        static bool TerCaracterEspecial(string input)
        {
            string specialCharacters = "!@#$%^&*()-_=+[]{}|;:'\"<>,.?/";
            return input.Any(c => specialCharacters.Contains(c));
        }
        #endregion

        #region Menus

        #region Menu Prinicipal
        static int Menu()
        {
            Console.WriteLine("+---------+------------------------------+");
            Console.WriteLine("| Menu PR |  TASKER MASTER - MANAGEMENT  |");
            Console.WriteLine("|---------+------------------------------|");
            Console.WriteLine("|    1    | Menu Criar                   |");
            Console.WriteLine("|---------+------------------------------|");
            Console.WriteLine("|    2    | Menu Mostrar                 |");
            Console.WriteLine("|---------+------------------------------|");
            Console.WriteLine("|    3    | Menu Associar                |");
            Console.WriteLine("|---------+------------------------------|");
            Console.WriteLine("|    4    | Menu Editar                  |");
            Console.WriteLine("|---------+------------------------------|");
            Console.WriteLine("|    0    | Sair / Terminar o programa   |");
            Console.WriteLine("+---------+------------------------------+");
            int opcao = GetInputUtilizador("Selecione uma opção: ");
            return opcao;
        }
        #endregion

        #region SubMenu Criações (Trabalhadores, Projetos)
        static void SubMenuCriar()
        {

            int criarOpcao = 0;

            do
            {
                Console.WriteLine("+---------+------------------------------+");
                Console.WriteLine("| Menu CR |  TASKER MASTER - MANAGEMENT  |");
                Console.WriteLine("|---------+------------------------------|");
                Console.WriteLine("|    1    | Criar Trabalahdor            |");
                Console.WriteLine("|---------+------------------------------|");
                Console.WriteLine("|    2    | Criar Projeto                |");
                Console.WriteLine("|---------+------------------------------|");
                Console.WriteLine("|     Guilherme F.   &   Daniel S.       |");
                Console.WriteLine("|---------+------------------------------|");
                Console.WriteLine("|    5    | Voltar ao Menu Principal     |");
                Console.WriteLine("+---------+------------------------------+");
                Console.Write("Selecione uma opção válida: ");
                criarOpcao = Convert.ToInt32(Console.ReadLine());

                switch(criarOpcao) 
                {
                    case 1:
                        Console.Clear();
                        int nTrabalhadores = GetInputUtilizador("Quantos Trabalhadores deseja criar: ");

                        for (int i = 0; i < nTrabalhadores; i++)
                        {
                            Trabalhadores trabalhador = CriarTrabalhador();
                            ListTrabalhadores.Add(trabalhador);
                            Console.Clear();                           
                            Console.WriteLine("Trabalhador criado com sucesso!");
                        }
                        break;
                    case 2:
                        Console.Clear();
                        int nProjetos = GetInputUtilizador("Quantos Projetos deseja criar: ");

                        for (int i = 0; i < nProjetos; i++)
                        {
                            Projetos projeto = CriarProjeto();
                            ListProjetos.Add(projeto);
                            Console.Clear();
                            Console.WriteLine("Projeto criado com sucesso!");
                        }
                        break;
                    case 5:
                        Console.Clear();
                        return;

                    default:
                        Console.WriteLine("Opção Inválida!");
                        break;

                }
            } while (criarOpcao != 0);
        }
        #endregion

        #region SubMenu Ler (Trabalhadores, Projetos)
        static void SubMenuLer()
        {
            Trabalhadores trabalhadores = new Trabalhadores();
            Projetos projetos = new Projetos();

            int lerOpcao = 0;
            Console.Clear();
            do
            {
                Console.WriteLine("+---------+------------------------------+");
                Console.WriteLine("| Menu LM |  TASKER MASTER - MANAGEMENT  |");
                Console.WriteLine("|---------+------------------------------|");
                Console.WriteLine("|    1    | Ver Trabalhadores            |");
                Console.WriteLine("|---------+------------------------------|");
                Console.WriteLine("|    2    | Ver Projetos                 |");
                Console.WriteLine("|---------+------------------------------|");
                Console.WriteLine("|     Guilherme F.   &   Daniel S.       |");
                Console.WriteLine("|---------+------------------------------|");
                Console.WriteLine("|    5    | Voltar ao Menu Principal     |");
                Console.WriteLine("+---------+------------------------------+");
                Console.WriteLine("Selecione uma opção válida: ");
                lerOpcao = Convert.ToInt32(Console.ReadLine());

                switch (lerOpcao)
                {
                    case 1:
                        if(ListTrabalhadores.Count != 0)
                        {
                            Console.Clear();
                            Console.WriteLine("Lista de Trabalhadores:");
                            foreach (Trabalhadores trabalhador in ListTrabalhadores)
                            {
                                Console.WriteLine(trabalhador.ToString());
                            }
                        } else
                        {
                            Console.WriteLine("Não existem trabalhadores disponiveis!");
                        }
                        break;
                    case 2:
                        if(ListProjetos.Count != 0)
                        {
                            Console.Clear();
                            Console.WriteLine("Lista de Projetos:");
                            foreach (Projetos projeto in ListProjetos)
                            {
                                Console.WriteLine(projeto.ToString());
                            }                          
                        } else
                        {
                            Console.WriteLine("Não existem projetos disponiveis!");
                        }
                        break;
                    case 5:
                        Console.Clear();
                        return;

                    default: 
                        Console.WriteLine("Opção Inválida!");
                        break;

                }
            } while (lerOpcao != 0);
        }
        #endregion

        #region Associações (Fazer / Mostrar)
        static void SubMenuAssoc()
        {
            Trabalhadores trabalhadores = new Trabalhadores();
            Projetos projetos = new Projetos();

            int lerOpcao = 0;
            Console.Clear();
            do
            {
                Console.WriteLine("+---------+------------------------------+");
                Console.WriteLine("| Menu AC |  TASKER MASTER - MANAGEMENT  |");
                Console.WriteLine("|---------+------------------------------|");
                Console.WriteLine("|    1    | Associar Projetos a Trab.    |");
                Console.WriteLine("|---------+------------------------------|");
                Console.WriteLine("|    2    | Mostrar Trabalhadores do Proj|");
                Console.WriteLine("|---------+------------------------------|");
                Console.WriteLine("|    3    | Remover Associação P - TRB   |");
                Console.WriteLine("|---------+------------------------------|");
                Console.WriteLine("|    5    | Voltar ao Menu Principal     |");
                Console.WriteLine("+---------+------------------------------+");
                Console.WriteLine("Selecione uma opção válida: ");
                lerOpcao = Convert.ToInt32(Console.ReadLine());
                
                switch (lerOpcao)
                {
                    case 1:
                        SubMenuAssociarProjetos();
                        break;
                    case 2:                      
                        SubMenuExibirProjetosAssociados();
                        break;
                    case 3:
                        SubMenuRemoverProjeto();
                        break;
                    case 5:
                        Console.Clear();
                        return;

                    default:
                        Console.WriteLine("Opção Inválida!");
                        break;

                }
            } while (lerOpcao != 0);
        }
        #endregion

        #region Editar (Trabalhadores / projetos)
        static void SubMenuEdit()
        {

            int lerOpcao = 0;
            Console.Clear();
            do
            {
                Console.WriteLine("+---------+------------------------------+");
                Console.WriteLine("| Menu OU |  TASKER MASTER - MANAGEMENT  |");
                Console.WriteLine("|---------+------------------------------|");
                Console.WriteLine("|    1    | Editar Trabalhadores         |");
                Console.WriteLine("|---------+------------------------------|");
                Console.WriteLine("|    2    | Editar Projetos              |");
                Console.WriteLine("|---------+------------------------------|");
                Console.WriteLine("|    3    | Pesquisar Proj. por Estado   |");
                Console.WriteLine("|---------+------------------------------|");
                Console.WriteLine("|    4    | Pesquisar Proj. por Nome     |");
                Console.WriteLine("|---------+------------------------------|");
                Console.WriteLine("|    5    | Voltar ao Menu Principal     |");
                Console.WriteLine("+---------+------------------------------+");
                Console.WriteLine("Selecione uma opção válida: ");
                lerOpcao = Convert.ToInt32(Console.ReadLine());

                switch (lerOpcao)
                {
                    case 1:
                        if (ListTrabalhadores.Count != 0)
                        {
                            Console.Clear();
                            Console.WriteLine("Lista de Trabalhadores:");
                            foreach (Trabalhadores trabalhadores in ListTrabalhadores)
                            {
                                Console.WriteLine(trabalhadores.ToString());
                            }
                        }
                        else
                        {
                            Console.WriteLine("Não existem Trabalhadores disponiveis!");
                        }

                        int idEditarTr = GetInputUtilizador("Digite o Id do Trabalhador que quer alterar: ");
                        EditarTrabalhadorPorID(idEditarTr);
                        break;
                    case 2:
                        if (ListProjetos.Count != 0)
                        {
                            Console.Clear();
                            Console.WriteLine("Lista de Projetos:");
                            foreach (Projetos projeto in ListProjetos)
                            {
                                Console.WriteLine(projeto.ToString());
                            }
                        }
                        else
                        {
                            Console.WriteLine("Não existem projetos disponiveis!");
                        }

                        int idEditar = GetInputUtilizador("Digite o Id do Projeto que quer alterar: ");
                        EditarProjetoPorID(idEditar);
                        break;

                    case 3:
                        Console.WriteLine("Escolha o estado para filtrar projetos:");
                        Console.WriteLine("1 - Por Fazer");
                        Console.WriteLine("2 - A Fazer");
                        Console.WriteLine("3 - Finalizado");
                        int escolhaEstado = GetInputUtilizador("Digite o número do estado: ");

                        EstadoProjeto estadoSelecionado;

                        switch (escolhaEstado)
                        {
                            case 1:
                                estadoSelecionado = EstadoProjeto.PorFazer;
                                break;
                            case 2:
                                estadoSelecionado = EstadoProjeto.AFazer;
                                break;
                            case 3:
                                estadoSelecionado = EstadoProjeto.Finalizado;
                                break;
                            default:
                                Console.WriteLine("Opção inválida. Nenhum filtro será aplicado.");
                                return;
                        }

                        List<Projetos> projetosFiltrados = Projetos.ListarProjetosPorEstado(ListProjetos, (int)estadoSelecionado);

                        foreach (var projeto in projetosFiltrados)
                        {
                            Console.WriteLine($"ID: {projeto.IdProjeto}, Nome: {projeto.NomeProjeto}, Data Criação: {projeto.DataCriacaoProjeto}, Estado: {projeto.Estado}");
                        }
                        break;

                    case 4:
                        Console.WriteLine("Digite a palavra-chave de pesquisa: ");
                        string palavraChave = Console.ReadLine();

                        List<Projetos> projetos = Projetos.PesquisarProjetosPorNome(ListProjetos, palavraChave);

                        foreach (var projeto in projetos)
                        {
                            Console.WriteLine($"ID: {projeto.IdProjeto}, Nome: {projeto.NomeProjeto}, Estado: {projeto.Estado}");
                        }
                        break;


                    case 5:
                        Console.Clear();
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
                        Console.Clear();
                        SubMenuCriar();
                        break;

                    case 2:
                        Console.Clear();
                        SubMenuLer();
                        break;

                    case 3:
                        Console.Clear();
                        SubMenuAssoc();
                        break;

                    case 4:
                        Console.Clear();
                        SubMenuEdit();
                        break;

                    default:
                        if(opcao != 0)
                        {
                            Console.WriteLine($"{opcao}, não é uma seleção válida!");
                        }
                        break;

                } 
            } while (opcao != 0);
        }
    }
}