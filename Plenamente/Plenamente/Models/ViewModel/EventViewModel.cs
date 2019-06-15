using System;

namespace Plenamente.Models.ViewModel
{
    public class EventViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string BackgroundColor { get; set; }
        public string BorderColor { get; set; }
        public string EventRoute { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string StartDate => Start.ToString("yyyy-MM-dd");
        public string EndDate => End.ToString("yyyy-MM-dd");
    }
}