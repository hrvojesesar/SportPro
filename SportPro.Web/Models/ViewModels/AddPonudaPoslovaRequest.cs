﻿using System.ComponentModel.DataAnnotations;

namespace SportPro.Web.Models.ViewModels;

public class AddPonudaPoslovaRequest
{
    [Key]
    public int IDPonudaPoslova { get; set; }
    [Required]
    [MaxLength(200, ErrorMessage = "Naziv has to be maximum 200 characters")]
    public string Naziv { get; set; }
    [Required]
    public string OpisPosla { get; set; }
    [Required]
    public int BrojOsoba { get; set; }
    [Required]
    public double BrojSati { get; set; }
    [Required]
    public double CijenaSata { get; set; }
    [MaxLength(200, ErrorMessage = "Potrebna oprema has to be maximum 200 characters")]
    public string? PotrebnaOprema { get; set; }
    [Required]
    public DateTime PocetakRadova { get; set; }
    [Required]
    public DateTime KrajRadova { get; set; }
    [Required]
    [MaxLength(100, ErrorMessage = "Lokacija has to be maximum 100 characters")]
    public string Lokacija { get; set; }
}