using System.ComponentModel.DataAnnotations;

namespace NorthwindTraders.Application.DTOs
{
    public class EmployeeDto
    {
        public int EmployeeId { get; set; }

        [Required]
        [StringLength(20)]
        public string LastName { get; set; }

        [Required]
        [StringLength(10)]
        public string FirstName { get; set; }

        [StringLength(30)]
        public string Title { get; set; }

        [StringLength(25)]
        public string TitleOfCourtesy { get; set; }

        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime HireDate { get; set; }

        public string FullName => $"{FirstName} {LastName}";
    }
}
