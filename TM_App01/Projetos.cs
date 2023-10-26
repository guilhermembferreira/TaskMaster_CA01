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
        public enum EstadoProjeto
        {
            PorFazer,
            AFazer,
            Finalizado
        }
        public int IdProjeto { get; set; }
        public string NomeProjeto { get; set; }
        public string DescricaoProjeto { get; set; }
        public DateTime DataCriacaoProjeto { get; set; }
        public DateTime DataFimProjeto { get; set; }
        public EstadoProjeto Estado { get; set; }
        #endregion

        #region Construtores
        public Projetos() 
        { 
            IdProjeto = 0;
            NomeProjeto = "projeto";
            DescricaoProjeto = "descrição";
            DataCriacaoProjeto = DateTime.Now;
            DataFimProjeto = DateTime.Now;
            Estado = EstadoProjeto.PorFazer;
        }

        public Projetos(int idProjeto, string nomeProjeto, string descricaoProjeto, DateTime dataCriacaoProjeto, DateTime dataFimProjeto, EstadoProjeto estado)
        {
            IdProjeto = idProjeto;
            NomeProjeto = nomeProjeto;
            DescricaoProjeto = descricaoProjeto;
            DataCriacaoProjeto = dataCriacaoProjeto;
            DataFimProjeto = dataFimProjeto;
            Estado = estado;
        }
        #endregion

        #region ToString
        public override string ToString()
        {
            return $"Projeto [{IdProjeto}]: Nome -> {NomeProjeto}, Descrição -> {DescricaoProjeto}, " +
                $"Data Criação -> {DataCriacaoProjeto}, Data do Fim do Projeto -> {DataFimProjeto}, " +
                $"Estado do Projeto -> {Estado}.";
        }
        #endregion
    }
}
