﻿using DeanerySystem.Data.Entities;
using DeanerySystem.Models;

namespace DeanerySystem.Services
{
    public interface IMarkService
    {
        void CheckEntries(Mark mark);
        Task<MethodResult> DeleteMarkAsync(int markId);
        List<AvgMarkTermModel> GetAvgMarkTermModels();
        IEnumerable<int> GetFreeTermsToEdit(Mark mark);
        List<MarkDistrModel> GetMarkDistrs();
        Task<IEnumerable<Mark>> GetMarksAsync();
        Task<MethodResult> SaveMarkAsync(Mark mark);
    }
}