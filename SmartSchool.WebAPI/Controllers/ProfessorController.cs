using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Controllers
{
    [ApiController]
    [Route("/api/{controller}")]
    public class ProfessorController : ControllerBase
    {
        private IRepository _repo;
        public ProfessorController(IRepository repo)
        {
            _repo = repo;
        }
        [HttpGet]
        public IActionResult Get(){
            var result = _repo.GetAllProfessores(true);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id){
            var professor = _repo.GetProfessorById(id,false);

            if(professor ==null) return BadRequest("Professor não encontrado");

            return Ok(professor);
        }


        [HttpPost]
        public IActionResult Post(Professor professor )
        {
            _repo.Add(professor);
            if(_repo.SaveChanges()){
                return Ok("Professor cadastrado");
            }
            return BadRequest("Professor não cadastrado");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Professor professor)
        {
            var prof = _repo.GetProfessorById(id,false);
            if(prof == null) return BadRequest("Professor não encontrado");
            if(_repo.SaveChanges()){
                return Ok(professor);
            }
            return BadRequest("Professor não atualizado");        
        }
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Professor professor)
        {
            var prof = _repo.GetProfessorById(id,false);
            if(prof == null) return BadRequest("Professor não encontrado");
            if(_repo.SaveChanges()){
                return Ok(professor);
            }
            return BadRequest("Professor não atualizado");        
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var prof = _repo.GetProfessorById(id);
            if(prof == null) return BadRequest("Professor não encontrado");
            if(_repo.SaveChanges()){
                return Ok("Professor excluído");
            }
            return BadRequest("Professor não excluído");                }
    }
}