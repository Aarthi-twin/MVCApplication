using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCApplication.Models
{
    public class Customer
    {

        public int id { get; set; }


        [Display(Name ="Your Name")]
        [StringLength(30)]
        [Required(ErrorMessage ="Name cannot be Empty")]
        [Column(TypeName ="varchar")]
        public string CustomerName { get; set; }

        [Display(Name ="Date of Birth")]
        [Required(ErrorMessage ="Date of Birth Field Cannot be Empty")]
        public DateTime? BirthDate { get; set; }

        [Required(ErrorMessage ="Gender is Mandatory")]
        [StringLength(10)]
        [Column(TypeName ="varchar")]
        public string Gender { get; set; }

        [Display(Name="Your city")]
        [Required(ErrorMessage ="City field cannot be Empty")]
        [Column(TypeName ="varchar")]
        [StringLength(20)]
        public string City { get; set; }


        //Reference Table
      
        public MembershipType MembershipType { get; set; }

        //Refence Column
        [Required(ErrorMessage = "Mandatory to fill this field")]
        [Display(Name = "Membership Type")]
        public int? MembershipTypeId { get; set; }

       
    }
}