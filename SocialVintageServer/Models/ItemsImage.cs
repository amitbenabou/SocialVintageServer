using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SocialVintageServer.Models;

public partial class ItemsImage
{
    [Key]
    public int Id { get; set; }

    public int ItemId { get; set; }

    [ForeignKey("ItemId")]
    [InverseProperty("ItemsImages")]
    public virtual Item Item { get; set; } = null!;
}
