using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace universe.Models
{
  public class Universe
  {

    public int Id { get; set; }

    public string UniverseName { get; set; }
    public string UniverseEnemies { get; set; }
    public string UniverseLocation { get; set; }
    public int CharacterId { get; set; }

    public Character Character { get; set; }

  }
}