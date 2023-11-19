﻿using DeanerySystem.Data.Entities;
using DeanerySystem.Models;

namespace DeanerySystem.Services
{
    public interface IMarkService
    {
        void CheckEntries(Mark mark);
        Task<MethodResult> DeleteMarkAsync(int markId);
        Task<IEnumerable<Mark>> GetMarksAsync();
        Task<MethodResult> SaveMarkAsync(Mark mark);
    }
}