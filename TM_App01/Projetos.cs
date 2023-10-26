using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TM_App01
{
    internal class Projetos
    {
        #region Atributos
        private int _idCliente { get; set; }
        private string _primeiroNome { get; set; }
        private string _ultimoNome { get; set; }
        private string _email { get; set; }
        private string _password { get; set; }
        private DateTime _dataNascimento { get; set; }
        private string _nacionalidade { get; set; }
        private DateTime _dataRegistro { get; set; }
        #endregion

        #region Contrutores
        public Projetos()
        {
            _idCliente = 0;
            _primeiroNome = "fullname";
            _ultimoNome = "fullname";
            _email = "email";
            _password = "password";
            _dataNascimento = DateTime.Now;
            _nacionalidade = "Português";
            _dataRegistro = DateTime.Now;
        }

        public Projetos(int idCliente, string primeiroNome, string ultimoNome, string email, string password,
            DateTime dataNascimento, string nacionalidade, DateTime dataRegistro)
        {
            _idCliente = idCliente;
            _primeiroNome = primeiroNome;
            _ultimoNome = ultimoNome;
            _email = email;
            _password = password;
            _dataNascimento = dataNascimento;
            _nacionalidade = nacionalidade;
            _dataRegistro = dataRegistro;
        }
        #endregion

        #region Dar Get aos Métodos Privados
        public int GetIdCliente()
        {
            return _idCliente;
        }
        public string GetPrimeiroNome()
        {
            return _primeiroNome;
        }
        public string GetUltimoNome()
        {
            return _ultimoNome;
        }
        public string GetEmail()
        {
            return _email;
        }
        public string GetPassword()
        {
            return _password;
        }
        public DateTime GetDataNascimento()
        {
            return _dataNascimento;
        }
        public string GetNacionalidade()
        {
            return _nacionalidade;
        }
        public DateTime GetDataRegistro()
        {
            return _dataRegistro;
        }
        #endregion

        #region Dar Set aos Métodos Privados
        public void SetEmail(string email)
        {
            _email = email;
        }
        public void SetPassword(string password)
        {
            _password = password;
        }
        #endregion

        #region Método ToString
        public override string ToString()
        {
            return $"Cliente [{_idCliente}]: Primeiro Nome -> {_primeiroNome}, Ultimo Nome -> {_ultimoNome}, " +
                $"Email -> {_email}, Password -> {_password}, Data Nascimento -> {_dataNascimento}, " +
                $"Nacionalidade -> {_nacionalidade}, Data Registro {_dataRegistro}.";
        }
        #endregion      
    }
}