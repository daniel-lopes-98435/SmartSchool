using System.Linq;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Data
{
    public class Repository : IRepository
    {
        private readonly SmartContext _context;

        public Repository (SmartContext context){
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
            
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
            
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
            
        }
        public bool SaveChanges()
        {
            return (_context.SaveChanges() >0);
        }

        public Aluno[] GetAllAluno(bool inclueProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;
            
            if(inclueProfessor){
                query = query
                    .Include(ad => ad.AlunosDisciplinas)
                    .ThenInclude(d =>d.Disciplina)
                    .ThenInclude(p =>p.Professor);
            }
            
            query = query.AsNoTracking().OrderBy(a =>a.Id);

            return query.ToArray();
        }

        public Aluno[] GetAlunosByDisciplinaId(int disciplinaId, bool includeProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;
            if(includeProfessor){
                query = query
                    .Include(ad =>ad.AlunosDisciplinas)
                    .ThenInclude(d =>d.Disciplina)
                    .ThenInclude(p =>p.Professor);
            }
            query = query
                .AsNoTracking()
                .OrderBy(a =>a.Id)
                .Where(aluno =>aluno.AlunosDisciplinas
                    .Any(ad =>ad.DisciplinaId == disciplinaId));
                
            return query.ToArray();
        }

        public Aluno GetAlunoById(int alunoId, bool includeProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;
            if(includeProfessor)
            {
                query = query
                    .Include(ad => ad.AlunosDisciplinas)
                    .ThenInclude(d =>d.Disciplina)
                    .ThenInclude(p =>p.Professor);
            }

            query = query
                .AsNoTracking()
                .OrderBy(a =>a.Id)
                .Where(aluno =>aluno.Id == alunoId);


            return query.FirstOrDefault();
        }

        public Professor[] GetAllProfessores(bool includeAlunos)
        {
            IQueryable<Professor> query = _context.Professores;

            if(includeAlunos){
                query = query
                .Include(d =>d.Disciplinas)
                .ThenInclude(ad => ad.AlunosDisciplinas)
                .ThenInclude(a => a.Aluno);
            }
            query = query
                .AsNoTracking()
                .OrderBy(p =>p.Id);

            return query.ToArray();
        }

        public Professor[] GetProfessorByDisciplinaId(int disciplinaId, bool includeAlunos)
        {
            IQueryable<Professor> query = _context.Professores;

            if(includeAlunos){
                query = query
                    .Include(d =>d.Disciplinas)
                    .ThenInclude(ad =>ad.AlunosDisciplinas)
                    .ThenInclude(a => a.Aluno);
            }

            query = query
                .AsNoTracking()
                .Where(aluno => aluno.Disciplinas.Any(d =>d.AlunosDisciplinas.Any(ad =>ad.DisciplinaId == disciplinaId)))
                .OrderBy(aluno =>aluno.Id);
            return query.ToArray();
        }

        public Professor GetProfessorById(int professorId, bool includeAlunos)
        {
            IQueryable<Professor> query = _context.Professores;
            
            if(includeAlunos)
            {
                query = query
                    .Include(d =>d.Disciplinas)
                    .ThenInclude(ad =>ad.AlunosDisciplinas)
                    .ThenInclude(a => a.Aluno);
            }

            query = query
                .AsNoTracking()
                .Where(p =>p.Id == professorId)
                .OrderBy(p =>p.Id);

            return query.FirstOrDefault();


        }
    }
}