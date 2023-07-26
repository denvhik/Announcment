using Announcment.DataContext;
using Announcment.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Announcment.Services
{
    public interface IAnnouncment
    {
        Task<(int, int)> GetIdRange();
        Task<List<AnnouncmentJob>> GetAnnouncement();
        Task<AnnouncmentJobDTO> GetAllAnnouncements(int id);
        Task<bool> Post(AnnouncmentJobDTO announcmentJobDTO);
        Task<bool> Delete(int id);
        Task<bool> Update(int id, AnnouncmentJobDTO announcementDTO);
        IEnumerable<AnnouncmentJob> GetAllSimilarAnnouncements(int id);
    }


}

