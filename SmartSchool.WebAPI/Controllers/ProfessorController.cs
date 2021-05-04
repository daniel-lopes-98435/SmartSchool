using System.Threading;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.Dtos;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Controllers
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProfessorController : ControllerBase
    {
        private IRepository _repo;
        private readonly IMapper _mapper;
        public ProfessorController(IRepository repo,IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult Get(){
            var professor = _repo.GetAllProfessores(true);
            return Ok(_mapper.Map<IEnumerable<ProfessorDto>>(professor));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id){
            var professor = _repo.GetProfessorById(id,false);

            if(professor ==null) return BadRequest("Professor não encontrado");
            var professorDto = _mapper.Map<ProfessorDto>(professor);

            return Ok(professorDto);
        }


        [HttpPost]
        public IActionResult Post(ProfessorDto model )
        {
            var professor = _mapper.Map<Professor>(model);

            _repo.Add(professor);
            if(_repo.SaveChanges()){

                return Created($"/api/professor/{model.Id}", _mapper.Map<ProfessorDto>(professor));
            }
            return BadRequest("Professor não cadastrado");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, ProfessorDto model)
        {
            var professor = _repo.GetProfessorById(id,false);

            if(professor == null) return BadRequest("Professor não encontrado");

            _mapper.Map(model,professor);
            _repo.Update(professor);

            if(_repo.SaveChanges()){

                return Created($"/api/professor/{model.Id}", _mapper.Map<ProfessorDto>(professor));
            }
            return BadRequest("Professor não atualizado");        
        }
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, ProfessorDto model)
        {
            var professor = _repo.GetProfessorById(id,false);
            if(professor == null) return BadRequest("Professor não encontrado");

            _mapper.Map(model,professor);
            _repo.Update(professor);

            if(_repo.SaveChanges()){

                return Created($"/api/professor/{model.Id}", _mapper.Map<ProfessorDto>(professor));
                
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