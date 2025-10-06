using System;
using System.Collections.Generic;

namespace Egg.Scheduler.Models;

public partial class UserMaster
{
    public int UserId { get; set; }

    public int RoleId { get; set; }

    public string UserName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string ConfirmPassword { get; set; } = null!;

    public string Image { get; set; } = null!;

    public bool Active { get; set; }

    public DateTime? AddedDate { get; set; }

    public DateTime? ModifyDate { get; set; }

    public string? HashedPassword { get; set; }
}
