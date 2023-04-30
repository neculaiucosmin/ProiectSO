using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.IdentityModel.Tokens;
using OrarAdmin.Entities;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;

namespace OrarAdmin;
/// <summary>
/// Class made for creating a PDF file 
/// </summary>
public class PdfService
{
    private readonly string _grp;
    private readonly List<Orar> _orars;

    private static readonly List<string> ModuleOrder = new ()
    {
        "M1", "M2", "M3", "M4", "M5", "M6", "M7"
    };
    public PdfService(List<Orar> orars, string grp)
    {
        _orars = orars;
        _grp = grp;
    }
/// <summary>
/// Create a PDF in a specific file path
/// </summary>
/// <param name="path">Take a path string as param. The pdf file will pe save at path + file.pdf</param>
    public void CreatePdf(string path)
    {
        var document = new PdfDocument();
        var page = document.AddPage();

        var gfx = XGraphics.FromPdfPage(page);
        var font = new XFont("Arial", 16, XFontStyle.Regular);

        var textColor = XBrushes.Black;

        var layout = new XRect(20, 20, page.Width, page.Height);

        gfx.DrawString(_grp, new XFont("Arial", 16, XFontStyle.Italic), textColor,
            new XRect(20, 20, page.Width, page.Height), XStringFormats.TopCenter);

        var s = OrderByModul(_orars);
        int index = 0;
        foreach (var orar in s)
        {
            gfx.DrawString(OrarStr(orar), font, textColor, new XRect(20, 100 + 20 * index, page.Width, page.Height),
                    XStringFormats.TopLeft);
            index++;
        }
           
        document.Save(path + @"\file.pdf");
    }
/// <summary>
/// Equivalent of ToString() 
/// </summary>
    private string OrarStr(Orar orar)
    {
        return $"{orar.DayOffWeek} | {orar.Module} | {orar.Hours} | {orar.Class} | {orar.Classroom} | {orar.Teacher}";
    }

    public static List<Orar> OrderByModul(List<Orar> orars)
    {
        
        var matOrar = new List<List<Orar>>
        {
            new(),
            new(),
            new(),
            new(),
            new(),
            new(),
            new()
        };
        for (var i = 0; i < orars.Count(); i++)
            switch (orars[i].DayOffWeek?.ToLower())
            {
                case "luni":
                    matOrar[0].Add(orars[i]);
                    break;
                case "marti":
                    matOrar[1].Add(orars[i]);
                    break;
                case "miercuri":
                    matOrar[2].Add(orars[i]);
                    break;
                case "joi":
                    matOrar[3].Add(orars[i]);
                    break;
                case "vineri":
                    matOrar[4].Add(orars[i]);
                    break;
                case "sambata":
                    matOrar[5].Add(orars[i]);
                    break;
                default:
                    matOrar[6].Add(orars[i]);
                    break;
            }
        
        List<Orar> sortedList = new List<Orar>();
        for (var i = 0; i < 7; i++)
        {
            if (matOrar[i].IsNullOrEmpty()) continue;

            matOrar[i] = matOrar[i].OrderBy(m => ModuleOrder.IndexOf(m.Module ?? throw new InvalidOperationException())).ToList();
            foreach (var orar in matOrar[i])
            {
                sortedList.Add(orar);
            }
        }
        return sortedList;
    }
}