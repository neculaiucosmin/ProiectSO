using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CatalogAdmin.Entities;
using Newtonsoft.Json;

namespace CatalogAdmin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async Task Request()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage responseMessage = await client.GetAsync("https://localhost:44346/orar/v1/10212");
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseBody = await responseMessage.Content.ReadAsStringAsync();
                var orar = JsonConvert.DeserializeObject<List<Orar>>(responseBody);
                // Grp.Text = orar[0].Grp;
                
            }
            else
            {
                // Grp.Text = "Something went wrong";
            }

        }
    }
}