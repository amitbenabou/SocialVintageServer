using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SocialVintageServer.Models;

[Table("Review")]
public partial class Review
{
    [Key]
    public int ReviewId { get; set; }

    public int StoreId { get; set; }

    public int UserId { get; set; }

    [Column("ranking")]
    [StringLength(10)]
    public string Ranking { get; set; } = null!;

    [StringLength(70)]
    public string Info { get; set; } = null!;

    [ForeignKey("StoreId")]
    [InverseProperty("Reviews")]
    public virtual Store Store { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("Reviews")]
    public virtual User User { get; set; } = null!;
}
