using System;
using System.Net.Http;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using CatalogAdmin.Entities;

namespace CatalogAdmin.Components;

/// <summary>
///     Interaction logic for CustomRow.xaml
/// </summary>
public partial class CustomRow : UserControl
{
    private readonly Orar _orar;

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
        SetIsEnable(false);
    }

    private void SetIsEnable(bool set)
    {
        Group.IsEnabled = set;
        Class.IsEnabled = set;
        Hours.IsEnabled = set;
        Teacher.IsEnabled = set;
        Classroom.IsEnabled = set;
        Hours.IsEnabled = set;
        Module.IsEnabled = set;
        DayOfWeek.IsEnabled = set;
        Year.IsEnabled = set;
    }

    private async void Delete_OnClick(object sender, RoutedEventArgs e)
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
            var responseMessage = await client.DeleteAsync($"https://localhost:44346/orar/v1/{_orar.Id}");
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
            throw;
        }
    }

    private void Edit_OnClick(object sender, RoutedEventArgs e)
    {
        SetIsEnable(true);
    }
}