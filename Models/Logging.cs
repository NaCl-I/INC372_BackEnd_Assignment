using System;
using System.Collections.Generic;

namespace WebAPI.Models;

public partial class Logging
{
    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Status { get; set; } = null!;
}

