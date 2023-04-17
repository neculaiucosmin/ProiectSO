using System.Windows.Controls;
using CatalogAdmin.Entities;

namespace CatalogAdmin.Components;

public partial class CustomRow : UserControl
{
    private Orar _orar;
    public CustomRow(Orar orar) 
    {
        InitializeComponent();
        _orar = orar;

    }
}