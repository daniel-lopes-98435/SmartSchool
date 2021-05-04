using System;

namespace SmartSchool.WebAPI.V1.Dtos
{

    /// <summary>
    /// Este é o DTO para Alunos
    /// </summary>
    /// <returns></returns>         

    public class AlunoDto
    {
        public int Id { get; set; }   
        public int Matricula { get; set; }     
        public string Nome { get; set; }

        public string Sobrenome { get; set; }
        public string Telefone { get; set; }
        public int Idade { get; set; }
        public DateTime DataNasc { get; set; }
        public DateTime DataIni { get; set; } = DateTime.Now;
        public bool Ativo { get; set; } = true;

    }
}