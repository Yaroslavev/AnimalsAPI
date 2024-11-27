using AutoMapper;
using Core.IServices;
using Core.Models;
using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data.Enteties;

namespace AnimalsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalsController(IMapper mapper, AnimalsDbContext context/*, IFilesService filesService*/) : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var animals = mapper.Map<IEnumerable<AnimalModel>>(context.Animals.Include(x => x.Gender).Include(x => x.AnimalType).ToList());
            return Ok(animals);
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            List<Animal> animals = context.Animals.Include(x => x.Gender).Include(x => x.AnimalType).ToList();
            Animal animal = animals.Find(x => x.Id == id)!;

            if (animal == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<AnimalModel>(animal));
        }

        [HttpPost]
        public IActionResult Create([FromForm] AddAnimalModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Animal animal = mapper.Map<Animal>(model);

            //if (model.Image != null)
            //{
            //    animal.ImageUrl = await filesService.SaveImage(model.Image);
            //}

            context.Animals.Add(animal);
            context.SaveChanges();

            return Ok();
        }

        [HttpPut]
        public IActionResult Edit([FromBody] EditAnimalModel model)
        {
            if (model == null) { return NotFound(); }

            Animal editModel = mapper.Map<Animal>(model);

            context.Animals.Update(editModel);
            context.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var Animal = context.Animals.Find(id);
            if (Animal == null)
            {
                return NotFound();
            }

            //if (Animal.ImageUrl != null)
            //{
            //    await filesService.DeleteImage(Animal.ImageUrl);
            //}

            context.Animals.Remove(Animal);
            context.SaveChanges();

            return Ok();
        }
    }
}
