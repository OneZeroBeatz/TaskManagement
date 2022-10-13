﻿using TaskManagement.Domain.Models;

namespace TaskManagement.Application.Repositories
{
    public interface ITaskRepository
    {
        Task<List<Domain.Models.Task>> Get(int dailyListId, bool done, DateTime? deadlineLimit);
        Task<int> InsertAsync(Domain.Models.Task task);
        Task<Domain.Models.Task?> Find(int taskId);
        System.Threading.Tasks.Task UpdateAsync(Domain.Models.Task task);

        //System.Threading.Tasks.Task DeleteAsync(int id);
        //Task<bool> Exists(int id, int userId);
    }
}