using System;
using System.Collections.Generic;

namespace WPFSQLite;

public partial class Category
{
    public int Id { get; set; }

    public string NameCategory { get; set; } = null!;

    public virtual ICollection<CategoryUser> CategoryUsers { get; set; } = new List<CategoryUser>();
}
