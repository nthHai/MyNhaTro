using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MyNhaTroShared.DTOs
{
    public class CustomerDto
    {
        public int Id { get; set; }

        public string CustomerCode { get; set; } = null!;

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public DateOnly? DayOfBirth { get; set; }

        public string IdentifyNumber { get; set; } = null!;

        public DateOnly? Ngaycap { get; set; }

        public string? Noicap { get; set; }

        public string? Phone { get; set; }

        public string? MobilePhone { get; set; }

        public string? PermanentAddress { get; set; }

        public string? JobName { get; set; }

        public string? WorkPlace { get; set; }

        public DateOnly? DateJoin { get; set; }

        public string? Description { get; set; }

        public DateTime? CreateDate { get; set; }
    }
}
