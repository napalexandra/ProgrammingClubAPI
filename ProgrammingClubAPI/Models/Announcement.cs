using System.ComponentModel.DataAnnotations;

namespace ProgrammingClubAPI.Models
{
    public class Announcement
    {
        [Key]
        public Guid? IdAnnouncement { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public string? Title { get; set; }
        public string? Text { get; set; }
        public DateTime? EventDate { get; set; }
        public string? Tags { get; set; }
    }
}
