using System;
using System.Collections.Generic;

namespace Рабочка_beta_1._0.Models;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int Role { get; set; }

    public string Phone { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Avatar { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Job> Jobs { get; } = new List<Job>();

    public virtual ICollection<Message> MessageReceivers { get; } = new List<Message>();

    public virtual ICollection<Message> MessageSenders { get; } = new List<Message>();

    public virtual Role RoleNavigation { get; set; } = null!;
}
