using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace universe.Models
{
  public class Character
  {
    public int Id { get; set; }


    public string CharacterName { get; set; }


    public string Quote { get; set; }
    public string SuperPower { get; set; }

    public bool WearsCape { get; set; } = true;



    public List<Universe> Universes { get; set; } = new List<Universe>();
  }
}