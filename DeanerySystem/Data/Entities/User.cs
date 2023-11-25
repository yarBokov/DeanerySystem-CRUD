using System;
using System.Collections.Generic;

namespace DeanerySystem;

public partial class User
{
    public int Id { get; set; }

    public int? PersonId { get; set; }

    public string? HashedPassword { get; set; }

    public int? KeyId { get; set; }

    public virtual Key? Key { get; set; }

    public virtual Person? Person { get; set; }
}
