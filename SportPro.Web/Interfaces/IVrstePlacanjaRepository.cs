using SportPro.Web.Models.Domains;

namespace SportPro.Web.Interfaces;

public interface IVrstePlacanjaRepository
{
    Task<IEnumerable<VrstePlacanja>> GetAllAsync();
    Task<VrstePlacanja> AddAsync(VrstePlacanja vrstaPlacanja);
    Task<VrstePlacanja>? GetAsync(int? id);
    Task<VrstePlacanja>? UpdateAsync(VrstePlacanja vrstaPlacanja);
    Task<VrstePlacanja>? DeleteAsync(int? id);
}