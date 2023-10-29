using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TM_App01
{
    internal class Trabalhadores
    {
        #region Atributos
        public enum Regioes
        {
            América,
            Asia,
            Oceania,
            Africa,
            UE
        }
        public int IdTrabalhador { get; set; }
        public string PrimeiroNome { get; set; }
        public string UltimoNome { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime DataNascimento { get; set; }
        public Regioes Regiao { get; set; }
        public DateTime DataRegistro { get; set; }

        public List<Projetos> Projetos { get; set; }
        #endregion

        #region Construtores
        // Vazio
        public Trabalhadores()
        {
            IdTrabalhador = 0;
            PrimeiroNome = "primeironome";
            UltimoNome = "ultimonome";
            Email = "email@email.com";
            Password = "password";
            DataNascimento = DateTime.Now;
            Regiao = Regioes.UE;
            DataRegistro = DateTime.Now;

            Projetos = new List<Projetos>();
        }

        public Trabalhadores(int idTrabalhador, string primeiroNome, string ultimoNome, string email, string password,
            DateTime dataNascimento, Regioes regiao, DateTime dataRegistro)
        {
            IdTrabalhador = idTrabalhador;
            PrimeiroNome = primeiroNome;
            UltimoNome = ultimoNome;
            Email = email;
            Password = password;
            DataNascimento = dataNascimento;
            Regiao = regiao;
            DataRegistro = dataRegistro;

            Projetos = new List<Projetos>();
        }
        #endregion

        #region Métodos
        public void AdicionarProjeto(Projetos projeto)
        {
            Projetos.Add(projeto);
        }

        public void RemoverProjeto(Projetos projeto)
        {
            if (Projetos.Contains(projeto))
            {
                Projetos.Remove(projeto);
                Console.WriteLine($"Projeto removido da associação do Trabalhador {IdTrabalhador}.");
            }
            else
            {
                Console.WriteLine($"O Trabalhador {IdTrabalhador} não está associado a esse projeto.");
            }
        }

        public void MostrarProjetosAssociados()
        {
            Console.WriteLine($"Projetos associados ao Trabalhador [{IdTrabalhador}] - {PrimeiroNome} {UltimoNome}:");
            foreach (Projetos projeto in Projetos)
            {
                Console.WriteLine(projeto.ToString());
            }
        }

        public void EditarTrabalhadores(string novoemail, string novopassword, Regioes novoregiao)
        {
            Email = novoemail;
            Password = novopassword;
            Regiao = novoregiao;
        }
        #endregion

        #region ToString
        public override string ToString()
        {
            return $"Cliente [{IdTrabalhador}]: Primeiro Nome -> {PrimeiroNome}, Ultimo Nome -> {UltimoNome}, " +
                $"Email -> {Email}, Password -> {Password}, Data Nascimento -> {DataNascimento}, " +
                $"Nacionalidade -> {Regiao}, Data Registro {DataRegistro}.";
        }
        #endregion
    }
}