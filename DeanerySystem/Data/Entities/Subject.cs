using System;
using System.Collections.Generic;

namespace DeanerySystem.Data.Entities;

public partial class Subject
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Mark> Marks { get; set; } = new List<Mark>();

    public double getAverageMark()
    {
        int? markSum = 0;
        foreach(var mark in Marks)
        {
            markSum += mark.Value;
        }
        return (double)markSum / Marks.Count();
    }
}
