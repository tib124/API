using System.ComponentModel.DataAnnotations;

namespace API.Models.Classes
{
    public class Actors
    {

        [Key]
        public int IdActor { get; set; }
        public string Name { get; set; }
    }
}
