using System;

namespace TodoBlazorApp.Components.Models
{
    public class Todo
    {
        public int id { get; set; }
        public string title { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        public bool isCompleted { get; set; } = false;
        public DateTime dateTime { get; set; } = DateTime.Now;

        public Todo() { }

        public Todo(int id, string title, string description, bool isCompleted, DateTime dateTime)
        {
            this.id = id;
            this.title = title;
            this.description = description;
            this.isCompleted = isCompleted;
            this.dateTime = dateTime;
        }
    }
}
