using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Storage;
using Windows.UI.Popups;
using EventMaker.Model;
using Newtonsoft.Json;

namespace EventMaker.Persistency
{
    class PersistencyService
    {
        private static string JsonFileName = "Events.json";

        public static async void SaveEventsAsJsonAsync(Event events)
        {
            const string ServerUrl = "http://localhost:58420";
            HttpClientHandler handler = new HttpClientHandler();
            handler.UseDefaultCredentials = true;

            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(ServerUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    await client.PostAsJsonAsync("api/Events", events);
                }
                catch (Exception ex)
                {
                    new MessageDialog(ex.Message).ShowAsync();
                }
            }
        }

        public static async Task<List<Event>> LoadEventsFromJsonAsync()
        {
            const string ServerUrl = "http://localhost:58420";
            HttpClientHandler handler = new HttpClientHandler();
            handler.UseDefaultCredentials = true;

            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(ServerUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    var responce = client.GetAsync("api/Events").Result;
                    if (responce.IsSuccessStatusCode)
                    {
                        var eventdata = responce.Content.ReadAsAsync<IEnumerable<Event>>().Result;
                        return eventdata.ToList();
                    }
                    return null;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public static async void DeleteEventsFromJsonAsync(Event events)
        {
            const string ServerUrl = "http://localhost:58420";
            HttpClientHandler handler = new HttpClientHandler();
            handler.UseDefaultCredentials = true;

            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(ServerUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    await client.DeleteAsync("api/Events/" + events.Id);
                }
                catch (Exception ex)
                {
                    new MessageDialog(ex.Message).ShowAsync();
                }
            }
        }



        //private static async void SerializeEventsFileAsync(string EventsJsonString, string fileName)
        //{
        //    StorageFile localFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
        //    await FileIO.WriteTextAsync(localFile, EventsJsonString);
        //}


        //private static async Task<string> DeserializeEventsFileAsync(string fileName)
        //{
        //    try
        //    {
        //        StorageFile localFile = await ApplicationData.Current.LocalFolder.GetFileAsync(fileName);
        //        return await FileIO.ReadTextAsync(localFile);
        //    }
        //    catch (FileNotFoundException ex)
        //    {
        //        MessageDialogHelper.Show("Loading for the first time? - Try Add and Save some Notes before trying to Save for the first time", "File not Found");
        //        return null;
        //    }
        //}


        private class MessageDialogHelper
        {
            public static async void Show(string content, string title)
            {
                MessageDialog messageDialog = new MessageDialog(content, title);
                await messageDialog.ShowAsync();
            }
        }
    }
}
