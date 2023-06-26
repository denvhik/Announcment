
namespace Announcment.DataContext
{
    public class AnnouncmentJob
    {
        public int  Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date_added { get; set; }
        public int CommonWordsCount { get; set; }
    }
}
