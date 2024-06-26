﻿using SportPro.Web.Models.Domains;

namespace SportPro.Web.Interfaces;

public interface IBrendoviRepository
{
    Task<IEnumerable<Brendovi>> GetAllAsync(int pageNumber = 5, int pageSize = 100);
    Task<Brendovi> AddAsync(Brendovi brend);
    Task<Brendovi>? GetAsync(int? id);
    Task<Brendovi>? UpdateAsync(Brendovi brend);
    Task<Brendovi>? DeleteAsync(int? id);
    Task<int> CountAsync();
}
