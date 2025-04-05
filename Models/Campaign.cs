using System;
using System.ComponentModel.DataAnnotations;
using API_FEB.Attributes;
namespace API_FEB.Models
{
    public class Campaign
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campaign name is required.")]
        [StringLength(100, ErrorMessage = "Name can't be longer than 100 characters.")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "Description can't be longer than 500 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Start date is required.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End date is required.")]
        [DateGreaterThan(nameof(StartDate), ErrorMessage = "End date must be after start date.")]
        public DateTime EndDate { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
