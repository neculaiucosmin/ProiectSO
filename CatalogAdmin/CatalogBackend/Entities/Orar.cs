using System;
using System.Collections.Generic;

namespace CatalogBackend.Entities;

public partial class Orar
{
    public long Id { get; set; }

    public string? Grp { get; set; }

    public string? DayOffWeek { get; set; }

    public short? Year { get; set; }

    public string? Hours { get; set; }

    public string? Module { get; set; }

    public string? Class { get; set; }

    public string? Type { get; set; }

    public string? Classroom { get; set; }

    public string? Teacher { get; set; }

    public string? Week { get; set; }
    
}
