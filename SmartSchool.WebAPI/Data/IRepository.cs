using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Data
{
    public interface IRepository
    {
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class; 
        void Delete<T>(T entity) where T : class;
        bool SaveChanges();

        //Alunos
        Aluno[] GetAllAluno(bool includeProfessor = false);
        Aluno[] GetAlunosByDisciplinaId(int disciplinaId, bool includeProfessor = false);
        Aluno GetAlunoById(int alunoId, bool includeProfessor = false);

        //Professores
        Professor[] GetAllProfessores(bool includeAlunos = false);
        Professor[] GetProfessorByDisciplinaId(int disciplinaId,bool includeAlunos = false);
        Professor GetProfessorById(int professorId,bool includeAlunos = false);

    }
}