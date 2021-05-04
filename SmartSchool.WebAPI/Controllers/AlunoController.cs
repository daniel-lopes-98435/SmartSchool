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

    /// <summary>
    /// Versão 1 do meu controlador de Alunos
    /// </summary>  
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AlunoController : ControllerBase
    {
        private readonly IRepository _repo;
        private readonly IMapper _mapper;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="mapper"></param>        
        public AlunoController(IRepository repo,IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        /// <summary>
        /// Método para listar todos os alunos
        /// </summary>
        /// <returns></returns>         
        [HttpGet]
        public IActionResult Get(){

            var alunos = _repo.GetAllAluno(true);

            return Ok(_mapper.Map<IEnumerable<AlunoDto>>(alunos));

        }

        /// <summary>
        /// Método para listar apenas um alunos
        /// </summary>     
        /// <returns></returns> 
        [HttpGet("{id}")]
        public IActionResult GetById(int id){
            var aluno = _repo.GetAlunoById(id,false);
            if(aluno == null) return BadRequest("Aluno não encontrado");
            var alunoDto = _mapper.Map<AlunoDto>(aluno);
            return Ok(alunoDto);
        }
 

        [HttpPost]
        public IActionResult Post(AlunoDto model){
            var aluno = _mapper.Map<Aluno>(model);

            _repo.Add(aluno);
            if(_repo.SaveChanges())
            {
                return Created($"/api/aluno/{model.Id}",_mapper.Map<AlunoDto>(aluno));    
            }
            
            return BadRequest("Aluno não cadastrado");    
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, AlunoDto model){

            var aluno = _repo.GetAlunoById(id);
            
            if(aluno ==null) return BadRequest("Aluno não encontrado");

            _mapper.Map(model,aluno);
            _repo.Update(aluno);
            if(_repo.SaveChanges())
            {
                return Created($"/api/aluno/{model.Id}",_mapper.Map<AlunoDto>(aluno));    
            }
            
            return BadRequest("Aluno não atualizado");                
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, AlunoDto model){
            var aluno = _repo.GetAlunoById(id);
            if(aluno == null) return BadRequest("Aluno não encontrado");

            _mapper.Map(model,aluno);
            _repo.Update(aluno);
            if(_repo.SaveChanges())
            {
                return Created($"/api/aluno/{model.Id}",_mapper.Map<AlunoDto>(aluno));    
            }
            
            return BadRequest("Aluno não atualizado");    
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id){
            var aluno = _repo.GetAlunoById(id);
            if(aluno == null) return BadRequest("Aluno não encontrado");

            _repo.Delete(aluno);
            if(_repo.SaveChanges())
            {
                return Ok("Aluno excluído");    
            }
            
            return BadRequest("Aluno não excluído");    
        }
    }
}