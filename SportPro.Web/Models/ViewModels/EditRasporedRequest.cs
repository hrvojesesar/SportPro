﻿using System.ComponentModel.DataAnnotations;

namespace SportPro.Web.Models.ViewModels;

public class EditRasporedRequest
{
    [Key]
    public int IDRaspored { get; set; }
    public string Zaposlenik { get; set; }
    public string Posao { get; set; }
    public string? Napomena { get; set; }
    public DateTime PocetakRada { get; set; }
    public DateTime KrajRada { get; set; }
    public double BrojSati { get; set; }
}