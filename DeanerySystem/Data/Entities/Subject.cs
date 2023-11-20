using System;
using System.Collections.Generic;

namespace DeanerySystem.Data.Entities;

public partial class Subject
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Mark> Marks { get; set; } = new List<Mark>();

    public double getAverageMark() =>
        getAverageMarkInList(Marks);

    public double getAverageMarkByGroup(int groupId) =>
        getAverageMarkInList(Marks.Where(m => m.Student.GroupId == groupId));

    private double getAverageMarkInList(IEnumerable<Mark> marks)
    {
        int? markSum = 0;
        foreach (var mark in marks)
        {
            markSum += mark.Value;
        }
        return (double)markSum / marks.Count();
    }
}
