﻿namespace Message.Models;

public class CreatingMessageModel
{
    public Guid From { set; get; }
    public Guid To { set; get; }
    public bool IsRead { set; get; }
    public string Content { set; get; }
}