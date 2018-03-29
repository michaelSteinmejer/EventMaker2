using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventMaker.Converter;
using EventMaker.Model;
using EventMaker.Persistency;
using EventMaker.ViewModel;

namespace EventMaker.Handler
{
    class EventHandler
    {
        private EventViewModel _eventViewModel;
        

        public EventHandler(EventViewModel eventViewModel)
        {
            _eventViewModel = eventViewModel;
        }

        public void CreateEvent()
        {
           Event newEvent = new Event(_eventViewModel.Name, _eventViewModel.Description,
                _eventViewModel.Place,
                DateTimeConverter.DateTimeOffsetAndTimeSetToDateTime(_eventViewModel.Date, _eventViewModel.Time));
            PersistencyService.SaveEventsAsJsonAsync(newEvent);
            EventCatalogSingleton.Instance.events.Clear();
            EventCatalogSingleton.Instance.LoadEventsAsync();
        }

        public void DeleteEvent()
        {
            _eventViewModel.EventCatalog.RemoveEvent(EventViewModel.SelectedEvent);
           
        }

        public void SetSelectedEvent(Event ev)
        {
           EventViewModel.SelectedEvent = ev;
        }
        
    }
}
