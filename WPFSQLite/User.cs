using System;
using System.Collections.Generic;

namespace WPFSQLite;

public partial class User
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public double? Salary { get; set; }

    public double? cashPerDay => Salary / 30;

    public virtual ICollection<CategoryUser> CategoryUsers { get; set; } = new List<CategoryUser>();

}
