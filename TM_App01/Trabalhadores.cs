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