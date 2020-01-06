using System.Collections.Generic;



namespace universe.ViewModels
{
  public class CharacterDetails
  {
    public int Id { get; set; }

    
    public string CharacterName { get; set; }

    
    public string Quote { get; set; }
    public string SuperPower { get; set; }
  
    public bool WearsCape { get; set; } = true;

    public List<CreatedUniverse> Universes { get; set; }
      = new List<CreatedUniverse>();
  }
}