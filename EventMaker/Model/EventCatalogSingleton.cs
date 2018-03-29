using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using EventMaker.Persistency;

namespace EventMaker.Model
{
    class EventCatalogSingleton
    {
        private static EventCatalogSingleton _instance;

        public ObservableCollection<Event> events { get; set; }
        public static EventCatalogSingleton Instance
        {
            get { return _instance ?? (_instance = new EventCatalogSingleton()); }
        }

        private EventCatalogSingleton()
        {
            
            events = new ObservableCollection<Event>();
            events.Clear();
            //Add(new Event("Thomas", "Rejse", "Norge", DateTime.Now));
            LoadEventsAsync();
        }

        public void Add(Event newEvent)
        {
            
            events.Add(newEvent);
           
            PersistencyService.SaveEventsAsJsonAsync(newEvent);
            LoadEventsAsync();
            

        }

        public async void LoadEventsAsync()
        {
            List<Event> eventsFromFile = await PersistencyService.LoadEventsFromJsonAsync();
            if (eventsFromFile!=null)
            {
                foreach (Event e in eventsFromFile)
                {

                    events.Add(e);
                }
            }

        }

        public async void RemoveEvent(Event OldEvent)
        {


            events.Remove(OldEvent);
            PersistencyService.DeleteEventsFromJsonAsync(OldEvent);
        }
        
    }
}
