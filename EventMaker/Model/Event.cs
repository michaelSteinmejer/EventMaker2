using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Cryptography.Core;
using Windows.UI.Xaml;

namespace EventMaker.Model
{
    class Event
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        [StringLength(50)]
        public string Place { get; set; }

        public DateTime DateTime { get; set; }

        public Event(string name, string description, string place, DateTime dateTime)
        {
            this.Name = name;
            this.Description = description;
            this.Place = place;
            this.DateTime = dateTime;
        }

        //public override string ToString()
        //{
        //    return $"Id: {Id} Navn: {Name} Beskrivelse: {Description}Sted: {Place} Dato:{DateTime}";
        //}
    }
}
