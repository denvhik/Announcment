using Announcment.DataContext;

namespace Announcment.DTO
{
    public class AnnouncmentJobDTO
    {
   
        public string Title { get; set; }
        public string Description { get; set; }

        public DateTime Date_added { get; set; }
        public AnnouncmentJobDTO()
        {
        }

        public AnnouncmentJobDTO(AnnouncmentJob announcmentJob)
        {
           
            Title = announcmentJob.Title;
            Description = announcmentJob.Description;
            Date_added = announcmentJob.Date_added;
        }
    }
}
