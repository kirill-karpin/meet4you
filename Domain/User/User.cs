﻿using Entities.Abstractions;

namespace User;

public class User : BaseEntity
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public string SecondName { get; set; }
}