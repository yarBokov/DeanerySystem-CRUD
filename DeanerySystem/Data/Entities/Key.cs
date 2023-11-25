using System;
using System.Collections.Generic;

namespace DeanerySystem.Data.Entities;

public partial class Key
{
    public string? Key1 { get; set; }

	public Key(string key)
	{
		Key1 = key;
	}
}
