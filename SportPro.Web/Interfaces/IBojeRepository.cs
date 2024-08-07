﻿using SportPro.Web.Models.Domains;

namespace SportPro.Web.Interfaces;

public interface IBojeRepository
{
    Task<IEnumerable<Boje>> GetAllAsync(string? searchQuery = null, string? sortBy = null, string? sortDirection = null, int pageNumber = 1, int pageSize = 100);
    Task<Boje> AddAsync(Boje boja);
    Task<Boje>? GetAsync(int? id);
    Task<Boje>? UpdateAsync(Boje boja);
    Task<Boje>? DeleteAsync(int? id);
    Task<int> CountAsync();
}
