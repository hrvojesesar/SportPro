﻿using SportPro.Web.Models.Domains;

namespace SportPro.Web.Interfaces;

public interface IVelicineRepository
{
    Task<IEnumerable<Velicine>> GetAllAsync(string? searchQuery = null, string? sortBy = null, string? sortDirection = null, int pageNumber = 1, int pageSize = 100);
    Task<Velicine> AddAsync(Velicine velicina);
    Task<Velicine>? GetAsync(int? id);
    Task<Velicine>? UpdateAsync(Velicine velicina);
    Task<Velicine>? DeleteAsync(int? id);
    Task<int> CountAsync();
}
