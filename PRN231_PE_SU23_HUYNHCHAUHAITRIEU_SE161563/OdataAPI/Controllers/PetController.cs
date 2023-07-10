using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using OdataData.Model;
using OdataService.Repository;

namespace OdataAPI.Controllers
{
    [ApiController]
    [Route(Helpers.SettingVersionAPI.ApiVersion)]
    public class PetController : ControllerBase
    {
        private readonly IRepository<Pet> _repository;

        public PetController(IRepository<Pet> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [EnableQuery]
        public IActionResult Get()
        {
            var entities = _repository.GetAll();
            return Ok(entities);
        }

        [HttpGet("{id}")]
        [EnableQuery]
        public IActionResult Get(int id)
        {
            var entity = _repository.GetById(id);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Pet entity)
        {
            _repository.Add(entity);
            return CreatedAtAction(nameof(Get), new { id = entity.PetId }, entity);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Pet entity)
        {
            var existingEntity = _repository.GetById(id);
            if (existingEntity == null)
            {
                return NotFound();
            }

            // Cập nhật thuộc tính của existingEntity từ entity

            _repository.Update(existingEntity);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var entity = _repository.GetById(id);
            if (entity == null)
            {
                return NotFound();
            }

            _repository.Delete(entity);
            return NoContent();
        }
    }
}