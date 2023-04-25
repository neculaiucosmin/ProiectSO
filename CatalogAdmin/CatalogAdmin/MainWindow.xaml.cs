using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using CatalogAdmin.Components;
using CatalogAdmin.Entities;
using Newtonsoft.Json;

namespace CatalogAdmin;

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
            {
                responseMessage = await client.GetAsync($"https://localhost:7069/orar/v1/{grp}");
            }
            else
            {
                responseMessage = await client.GetAsync($"https://localhost:7069/orar/v1/{grp}/{year}");
            }    

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseBody = await responseMessage.Content.ReadAsStringAsync();
                _orars = JsonConvert.DeserializeObject<List<Orar>>(responseBody) ?? throw new InvalidOperationException();
                foreach (var orar in _orars) MyItemsControl.Children.Add(new CustomRow(orar));
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
        
        if (!string.IsNullOrEmpty(Search.Text)&&Regex.IsMatch(Search.Text,@"^[0-9]+[A-Z]?$"))
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
}