using SportPro.Web.Models.Domains;

namespace SportPro.Web.Interfaces;

public interface IProSportKorisniciRepository
{
    Task<IEnumerable<ProSportKorisnici>> GetAllAsync();
    Task<ProSportKorisnici> AddAsync(ProSportKorisnici proSportKorisnik);
    Task<ProSportKorisnici>? GetAsync(int? id);
    Task<ProSportKorisnici>? UpdateAsync(ProSportKorisnici proSportKorisnik);
    Task<ProSportKorisnici>? DeleteAsync(int? id);
}
