using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using OrarAdmin.Components;
using OrarAdmin.Entities;
using MessageBox = System.Windows.MessageBox;

namespace OrarAdmin;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private List<Orar>? _orars;

    public MainWindow()
    {
        InitializeComponent();
    }

    private async Task Request(string grp, string? year = null)
    {
        try
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback =
                    HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };

            // Create a new HttpClient with the configured handler.
            var client = new HttpClient(handler);
            HttpResponseMessage responseMessage;
            if (year is null)
                responseMessage = await client.GetAsync($"https://localhost:7069/orar/v1/{grp}");
            else
                responseMessage = await client.GetAsync($"https://localhost:7069/orar/v1/{grp}/{year}");

            if (responseMessage.IsSuccessStatusCode)
            {
                var dayOrder = new List<string>
                {
                    "Luni", "Marti", "Miercuri", "Joi", "Vineri", "Sambata", "Duminica"
                };
                var responseBody = await responseMessage.Content.ReadAsStringAsync();
                _orars = JsonConvert.DeserializeObject<List<Orar>>(responseBody) ??
                         throw new InvalidOperationException();
                if (!_orars.IsNullOrEmpty())
                {
                    var sortedOrars = _orars.OrderBy(day => dayOrder.IndexOf(day.DayOffWeek ?? throw new InvalidOperationException())).ToList();
                    _orars = sortedOrars;
                    sortedOrars = PdfService.OrderByModul(_orars);
                    foreach (var orar in sortedOrars) MyItemsControl.Children.Add(new CustomRow(orar));
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }


    private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(Search.Text) && Regex.IsMatch(Search.Text, @"^[0-9]+[A-Z]?$"))
        {
            MyItemsControl.Children.Clear();
            if (Year.SelectedIndex != -1)
                await Request(Search.Text, Year.Text);
            else
                await Request(Search.Text);
        }
        else
        {
            MessageBox.Show("Grupa introdusa nu este corecta.\nEx:10212 ");
        }
    }

    private void Add_OnClickClick(object sender, RoutedEventArgs e)
    {
        MyItemsControl.Children.Add(new CustomRow());
    }

    private void CreatePdf_OnClick(object sender, RoutedEventArgs e)
    {
        if (_orars.IsNullOrEmpty())
        {
            MessageBox.Show("Niciun orar gasit. Incercati o noua cautare!");
            return;
        }

        var folder = new FolderBrowserDialog();
        folder.RootFolder = Environment.SpecialFolder.Desktop;
        folder.ShowNewFolderButton = true;
        folder.ShowDialog();
        
        
        string s = folder.SelectedPath;
        PdfService pdfService = new PdfService(_orars, Search.Text);
        pdfService.CreatePdf(s);
        
    }


}