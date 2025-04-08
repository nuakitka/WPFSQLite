using System;
using System.Collections.Generic;

namespace WPFSQLite;

public partial class CategoryUser
{
    public int Id { get; set; }

    public int CategoryId { get; set; }

    public int UserId { get; set; }

    public double Cash { get; set; }

    public DateTime? Date { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
