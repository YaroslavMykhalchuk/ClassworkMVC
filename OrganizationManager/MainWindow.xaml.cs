using DemoClients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
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

namespace OrganizationManager
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

        //private void Window_Loaded(object sender, RoutedEventArgs e)
        //{
        //    var r = App.HttpClient.GetAsync("api/organisations");
        //    r.Wait();
        //    var response = r.Result;
        //    if(response.IsSuccessStatusCode)
        //    {
        //        var r1 = response.Content.ReadFromJsonAsync<QueryResult<Organisation>>();
        //        r1.Wait();
        //        dg.ItemsSource = r1.Result?.Items;
        //    }
        //}

        private async Task UpdateGrid()
        {
            var responce = await App.HttpClient.GetAsync("api/organisations");
            if (responce.IsSuccessStatusCode)
            {
                var result = await responce.Content.ReadFromJsonAsync<QueryResult<Organisation>>();
                dg.ItemsSource = result?.Items;
            }
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await UpdateGrid();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var organisation = new Organisation
            {
                Name = "Organization",
                FullName = "Full name organization"
            };
            var responce = await App.HttpClient.PostAsJsonAsync("api/organisations", organisation);
            await UpdateGrid();
        }

        private async void dg_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if(e.EditAction == DataGridEditAction.Commit)
            {
                var editedItem = e.Row.Item as Organisation;
                
                if(editedItem != null)
                {
                    var newItem = new Organisation
                    {
                        Id = editedItem.Id,
                        Name = editedItem.Name,
                        FullName = editedItem.FullName
                    };

                    var response = App.HttpClient.PutAsJsonAsync($"api/organisations/{editedItem.Id}", newItem);
                    if (response.IsCompletedSuccessfully)
                    {
                        await UpdateGrid();
                    }

                }
            }
        }
    }
}
