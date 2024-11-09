using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MyNhaTro.Data;

[Table("customer", Schema = "cus")]
public partial class Customer
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("customer_code")]
    [StringLength(20)]
    [Unicode(false)]
    public string CustomerCode { get; set; } = null!;

    [Column("first_name")]
    [StringLength(100)]
    public string? FirstName { get; set; }

    [Column("last_name")]
    [StringLength(20)]
    public string? LastName { get; set; }

    [DataType(DataType.Date)]
    [Column("day_of_birth")]
    public DateOnly? DayOfBirth { get; set; }

    [Column("identify_number")]
    [StringLength(20)]
    [Unicode(false)]
    public string IdentifyNumber { get; set; } = null!;

    [DataType(DataType.Date)]
    [Column("ngaycap")]
    public DateOnly? Ngaycap { get; set; }

    [Column("noicap")]
    [StringLength(50)]
    public string? Noicap { get; set; }

    [Column("phone")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Phone { get; set; }

    [Column("mobile_phone")]
    [StringLength(20)]
    [Unicode(false)]
    public string? MobilePhone { get; set; }

    [Column("permanent_address")]
    [StringLength(200)]
    public string? PermanentAddress { get; set; }

    [Column("job_name")]
    [StringLength(50)]
    public string? JobName { get; set; }

    [Column("work_place")]
    [StringLength(100)]
    public string? WorkPlace { get; set; }

    [DataType(DataType.Date)]
    [Column("date_join")]
    public DateOnly? DateJoin { get; set; }

    [Column("description")]
    [StringLength(200)]
    public string? Description { get; set; }

    [Column("create_date", TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }
}
