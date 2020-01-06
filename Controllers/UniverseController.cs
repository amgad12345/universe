using System.Linq;
using Microsoft.AspNetCore.Mvc;
using universe.Models;
using universe.ViewModels;

namespace universe.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class UniverseController : ControllerBase
  {

    [HttpPost]
    public ActionResult Createduniverse(NewuniverseViewModel vm)
    {
      var db = new DatabaseContext();
      var character = db.Characters
        .FirstOrDefault(ch => ch.Id == vm.CharacterId);
      if (character == null)
      {
        return NotFound();
      }
      else
      {
        var world = new Universe
        {
          UniverseName = vm.UniverseName,
          UniverseEnemies = vm.UniverseEnemies,
          CharacterId = vm.CharacterId,
          UniverseLocation = vm.UniverseLocation
        };
        db.Universes.Add(world);
        db.SaveChanges();
        var rv = new CreatedUniverse
        {
          Id = world.Id,
          UniverseName = world.UniverseName,
          UniverseEnemies = world.UniverseEnemies,
          CharacterId = world.CharacterId,
          UniverseLocation = world.UniverseLocation
        };
        return Ok(rv);
      }
    }


   /*  [HttpPost]
     public ActionResult CreateUniverse(Universe universe)
     {
       var db = new DatabaseContext();
       universe.Id = 0;
       db.Universes.Add(universe);
       db.SaveChanges();
       return Ok(universe);
       }

*/

 

 [HttpGet("getuni/{id}")]
    public ActionResult GetOneUniverses()
    {
      // return a list of all Characters ordered by fullname
      var db = new DatabaseContext();
      return Ok(db.Universes.OrderBy(universe => universe.UniverseName));
    }


    [HttpGet("{id}")]
    public ActionResult GetUniverseId(int id)
    {
      var db = new DatabaseContext();
      var uni = db.Universes.FirstOrDefault(Universe => Universe.Id == id);
      if (uni != null)
      {
        var rv = new NewuniverseViewModel
        {
          Id = uni.Id,
          UniverseName = uni.UniverseName,
          UniverseEnemies = uni.UniverseEnemies,
          UniverseLocation = uni.UniverseLocation
        };
        return Ok(rv);
      }
      else
      {
        return NotFound();
      }
    }




 [HttpGet]
    public ActionResult GetAllUniverses()
    {
      // return a list of all Characters ordered by fullname
      var db = new DatabaseContext();
      return Ok(db.Universes.OrderBy(universe => universe.UniverseName));
    }




    [HttpPut("{id}")]
    public ActionResult UpdateUniverse(Universe universe)
    {
      var db = new DatabaseContext();
      var prevUniverse = db.Universes.FirstOrDefault(un => un.Id == universe.Id);
      if (prevUniverse == null)
      {
        return NotFound();
      }
      else
      {
        prevUniverse.UniverseName = universe.UniverseName;
        prevUniverse.UniverseEnemies = universe.UniverseEnemies;
        prevUniverse.UniverseLocation = universe.UniverseLocation;
        prevUniverse.CharacterId = universe.CharacterId;
        db.SaveChanges();
        return Ok(prevUniverse);
      }
    }



    [HttpDelete("{id}")]
    public ActionResult DeleteUniverse(int id)
    {
      var db = new DatabaseContext();
      var universe = db.Universes.FirstOrDefault(un => un.Id == id);
      if (universe == null)
      {
        return NotFound();
      }
      else
      {
        db.Universes.Remove(universe);
        db.SaveChanges();
        return Ok();
      }
    }




  }
}