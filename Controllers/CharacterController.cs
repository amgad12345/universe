using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using universe.Models;
using universe.ViewModels;

namespace universe.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class CharacterController : ControllerBase
  {

    [HttpGet("getallCharacters")]
    public ActionResult GetAllCharacters()
    {
      // return a list of all Characters ordered by fullname
      var db = new DatabaseContext();
      return Ok(db.Characters.OrderBy(character => character.CharacterName));
    }




    [HttpGet("getchar/{id}")]
    public ActionResult GetOneCharacter(int id)
    {
      var db = new DatabaseContext();
      var character = db.Characters.Include(i => i.Universes).FirstOrDefault(ch => ch.Id == id);
      if (character == null)
      {
        return NotFound();
      }
      else
      {
        // create our json object
        var rv = new CharacterDetails
        {
          Id = character.Id,
          CharacterName = character.CharacterName,
          Quote = character.Quote,
          SuperPower = character.SuperPower,
          WearsCape = character.WearsCape,
          Universes = character.Universes.Select(un => new CreatedUniverse
          {
            UniverseName = un.UniverseName,
            UniverseEnemies = un.UniverseEnemies,
            UniverseLocation = un.UniverseLocation,
            CharacterId = un.CharacterId,
            Id = un.Id
          }).ToList()
        };
        return Ok(rv);
      }
    }

    [HttpPost]
    public ActionResult CreateCharacter(Character character)
    {
      var db = new DatabaseContext();
      character.Id = 0;
      db.Characters.Add(character);
      db.SaveChanges();
      return Ok(character);
    }

    [HttpPut("{id}")]
    public ActionResult UpdateCharacter(Character character)
    {
      var db = new DatabaseContext();
      var prevCharacter = db.Characters.FirstOrDefault(ch => ch.Id == character.Id);
      if (prevCharacter == null)
      {
        return NotFound();
      }
      else
      {
        prevCharacter.CharacterName = character.CharacterName;
        prevCharacter.Quote = character.Quote;
        prevCharacter.SuperPower = character.SuperPower;
        prevCharacter.WearsCape = character.WearsCape;
        db.SaveChanges();
        return Ok(prevCharacter);
      }
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteCharacter(int id)
    {
      var db = new DatabaseContext();
      var character = db.Characters.FirstOrDefault(ch => ch.Id == id);
      if (character == null)
      {
        return NotFound();
      }
      else
      {
        db.Characters.Remove(character);
        db.SaveChanges();
        return Ok();
      }
    }

  }
}