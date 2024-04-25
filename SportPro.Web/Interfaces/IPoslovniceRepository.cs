using SportPro.Web.Models.Domains;

namespace SportPro.Web.Interfaces;

public interface IPoslovniceRepository
{
    Task<IEnumerable<Poslovnice>> GetAllAsync();
    Task<Poslovnice> AddAsync(Poslovnice poslovnice);
    Task<Poslovnice>? GetAsync(int? id);
    Task<Poslovnice>? UpdateAsync(Poslovnice poslovnice);
    Task<Poslovnice>? DeleteAsync(int? id);
}
