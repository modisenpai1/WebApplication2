﻿using WebApplication2.Domain.Models;

namespace WebApplication2.Services
{
    public interface IEventService
    {
        public void AddEvent(Event evnt);
        public Event GetEvent(int id);
        public IEnumerable<Event> GetAll();
        public IEnumerable<Event> Search(string name,City city);
        public void Update(Event evnt);
        public void Delete(Event evnt);
    }
}