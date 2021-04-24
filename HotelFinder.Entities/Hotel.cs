using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelFinder.Entities
{
    public class Hotel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)] //primary key ve identity 1den başla 1er art
        public int Id { get; set; }
        [StringLength(50)] //nvarchar 50
        [Required]
        public string Name { get; set; }
        [StringLength(50)]
        [Required]
        public string City { get; set; }
    }
}
