using System;
using System.Net.Http;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using CatalogAdmin.Entities;
using ModernWpf.Controls;
using Newtonsoft.Json;

namespace CatalogAdmin.Components;

/// <summary>
///     Interaction logic for CustomRow.xaml
/// </summary>
public partial class CustomRow : UserControl
{
    private readonly Orar? _orar;
    private bool _isNewWindow = true;
    public CustomRow()
    {
        InitializeComponent();
    }

    public CustomRow(Orar orar)
    {
        _orar = orar;
        InitializeComponent();
        
        Group.Text = orar.Grp;
        Class.Text = orar.Class;
        Hours.Text = orar.Hours;
        Teacher.Text = orar.Teacher;
        Classroom.Text = orar.Classroom;
        Hours.Text = orar.Hours;
        Module.Text = orar.Module;
        DayOfWeek.Text = orar.DayOffWeek;
        Year.Text = orar.Year.ToString();
        Type.Text = orar.Type;
        Week.Text = orar.Week;
        
        _isNewWindow = false;
        
        SetIsEnable(false);
    }

    private void SetIsEnable(bool set)
    {
        Group.IsEnabled = set;
        Class.IsEnabled = set;
        Hours.IsEnabled = set;
        Teacher.IsEnabled = set;
        Classroom.IsEnabled = set;
        Module.IsEnabled = set;
        DayOfWeek.IsEnabled = set;
        Year.IsEnabled = set;
        Type.IsEnabled = set;
        Week.IsEnabled = set;
    }

    private async void Delete_OnClick(object sender, RoutedEventArgs e)
    {
        if (_isNewWindow)
        {
            if (this.Parent is SimpleStackPanel parent) parent.Children.Remove(this);
            return;
        }
        try
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback =
                    HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };

            // Create a new HttpClient with the configured handler.
            var client = new HttpClient(handler);
            var responseMessage = await client.DeleteAsync($"https://localhost:7069/orar/v1/{_orar.Id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                Delete.BorderBrush = new SolidColorBrush(Colors.LawnGreen);
                Thread.Sleep(500);
                BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#1976D2"));
            }
            else
            {
                Delete.BorderBrush = new SolidColorBrush(Colors.LawnGreen);
                Thread.Sleep(500);
                BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#1976D2"));
            }
        }
        catch (HttpRequestException)
        {
            MessageBox.Show("Conexiunea la server a esuat!\nVa rugam incercati din nou");
            throw;
        }
    }

    private void Edit_OnClick(object sender, RoutedEventArgs e)
    {
        SetIsEnable(true);
    }

    private async void Save_OnClick(object sender, RoutedEventArgs e)
    {
        Orar orar = new Orar()
        {
            Grp = Group.Text,
            DayOffWeek = DayOfWeek.Text,
            Year = Convert.ToInt16(Year.Text),
            Hours = Hours.Text,
            Module = Module.Text,
            Class = Class.Text,
            Teacher = Teacher.Text,
            Classroom = Classroom.Text,
            Type = Type.Text,
            Week = Week.Text
        };

        var handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        };

        var client = new HttpClient(handler);
        
        var orarJson = JsonConvert.SerializeObject(orar);
        var content = new StringContent(orarJson, System.Text.Encoding.UTF8, "application/json");
        var responseMessage = await client.PostAsync("https://localhost:7069/orar/v1", content);
        if (responseMessage.IsSuccessStatusCode)
        {
            SetIsEnable(false);
        }
    }
}