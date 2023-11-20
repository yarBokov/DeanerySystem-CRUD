﻿using System;
using System.Collections.Generic;

namespace DeanerySystem.Data.Entities;

public partial class Person
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? SecondName { get; set; }

    public string? PatherName { get; set; }

    public int? GroupId { get; set; }

    public char? Type { get; set; }

    public virtual Group? Group { get; set; }

    public virtual ICollection<Mark> MarkStudents { get; set; } = new List<Mark>();//оценки, полученные студентом; если препод - пустая коллекция

    public virtual ICollection<Mark> MarkTeachers { get; set; } = new List<Mark>(); //оценки, выставленные преподавателем; если студент - пустая коллекция

    public string getFullName()
    {
        return $"{SecondName} {FirstName} {PatherName}";
    }
}
