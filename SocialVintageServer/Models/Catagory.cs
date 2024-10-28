using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SocialVintageServer.Models;

[Table("Catagory")]
public partial class Catagory
{
    [Key]
    public int CatagoryId { get; set; }

    [StringLength(50)]
    public string CatagoryName { get; set; } = null!;

    [InverseProperty("Catagory")]
    public virtual ICollection<Store> Stores { get; set; } = new List<Store>();
}
