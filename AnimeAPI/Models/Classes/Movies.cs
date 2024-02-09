using System.ComponentModel.DataAnnotations;

namespace API.Models.Classes
{
    public class Movies
    {
        public Movies()
        {
            Actors = new List<Actors>(); // Inicialize a lista de atores no construtor
        }
        [Key]
        public int IdMovie { get; set; }
        public string Tittle { get; set; }
        public string ReleaseAt { get; set; }
        public string Review { get; set; }
        public int Rating { get; set; }
        public List<Actors> Actors { get; set; }



    }

}

