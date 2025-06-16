using System;
using System.Collections.Generic;

namespace Arna_Project_Track.Models;

public partial class EmployeeRole
{
    public int RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; }
}
