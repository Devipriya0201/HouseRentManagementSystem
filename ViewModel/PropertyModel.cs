using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HomeRentManagementSystem.ViewModel
{
    public class PropertyModel
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [Required]
        public string Sellername { get; set; }

        [Required]
        public string Houseno { get; set; }

        [Required]
        public string Streetname { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public long Zip { get; set; }
        [Required]
        public string Flattype { get; set; }
        [Required]
        public long Rent { get; set; }
        [Required]
        public long Phone { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Image { get; set; }
        

    }
    
}