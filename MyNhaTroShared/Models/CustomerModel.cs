//using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MyNhaTro.Models
{
    public class CustomerModel
    {
        //Chỉ hiện thị các cột cần, không sử dụng entyti của API trong Data
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [DisplayName("Mã khách hàng")]
        [Column("customer_code")]
        [StringLength(20)]
        public string CustomerCode { get; set; } = null!;

        [DisplayName("Họ đệm")]
        [Column("first_name")]
        [StringLength(100)]
        public string? FirstName { get; set; }

        [DisplayName("Tên")]
        [Column("last_name")]
        [StringLength(20)]
        public string? LastName { get; set; }

        [DisplayName("Ngày sinh")]
        [DataType(DataType.Date)]
        [Column("day_of_birth")]
        public DateOnly? DayOfBirth { get; set; }

        [DisplayName("Số định danh")]
        [Column("identify_number")]
        [StringLength(20)]
        public string IdentifyNumber { get; set; } = null!;

        [DisplayName("Ngày cấp")]
        [DataType(DataType.Date)]
        [Column("ngaycap")]
        public DateOnly? Ngaycap { get; set; }

        [DisplayName("Nơi cấp")]
        [Column("noicap")]
        [StringLength(50)]
        public string? Noicap { get; set; }

        [DisplayName("Điện thoại")]
        [Column("phone")]
        [StringLength(20)]
        public string? Phone { get; set; }

        [DisplayName("Di động")]
        [Column("mobile_phone")]
        [StringLength(20)]
        public string? MobilePhone { get; set; }

        [DisplayName("Thường trú")]
        [Column("permanent_address")]
        [StringLength(200)]
        public string? PermanentAddress { get; set; }

        [DisplayName("Nghề nghiệp")]
        [Column("job_name")]
        [StringLength(50)]
        public string? JobName { get; set; }

        [DisplayName("Nơi làm việc")]
        [Column("work_place")]
        [StringLength(100)]
        public string? WorkPlace { get; set; }

        [DisplayName("Ngày tham gia")]
        [DataType(DataType.Date)]
        [Column("date_join")]
        public DateOnly? DateJoin { get; set; }

        [DisplayName("Ghi chú")]
        [Column("description")]
        [StringLength(200)]
        public string? Description { get; set; }

        [DisplayName("Ngày tạo")]
        [Column("create_date", TypeName = "datetime")]
        public DateTime? CreateDate { get; set; }
    }
}
