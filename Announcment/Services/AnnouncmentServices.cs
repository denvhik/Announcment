using Announcment.DataContext;
using Announcment.DTO;
using Announcment.Services;
using Microsoft.EntityFrameworkCore;

public class AnnouncmentService : IAnnouncment
{
    private readonly AnnouncmentContext _dbContext;

    public AnnouncmentService(AnnouncmentContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<(int, int)> GetIdRange()
    {
        var minId = await _dbContext.AnnouncmentJob.MinAsync(a => a.Id);
        var maxId = await _dbContext.AnnouncmentJob.MaxAsync(a => a.Id);

        return (minId, maxId);
    }

    public async Task<List<AnnouncmentJob>> GetAnnouncement()
    {
        return await _dbContext.AnnouncmentJob.ToListAsync();

    }

    public async Task<AnnouncmentJobDTO> GetAllAnnouncements(int id)
    {
        var AnnouncmentJob = await _dbContext.AnnouncmentJob.FindAsync(id);
        if (AnnouncmentJob is null) return null;

        var AnnouncmnetJobDTO = new AnnouncmentJobDTO(AnnouncmentJob);
        return AnnouncmnetJobDTO;
    }

    public async Task<bool> Post(AnnouncmentJobDTO announcmentJobDTO)
    {
        if (announcmentJobDTO is null)
        {
            return false;
        }

        var announcment = new AnnouncmentJob
        {
            Title = announcmentJobDTO.Title,
            Description = announcmentJobDTO.Description,
            Date_added = DateTime.Now
        };

        _dbContext.Add(announcment);
        await _dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<bool> Delete(int id)
    {
        var announcements = await _dbContext.AnnouncmentJob.FindAsync(id);
        if (announcements is null) return false;

        _dbContext.Remove(announcements);
        await _dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<bool> Update(int id, AnnouncmentJobDTO announcementDTO)
    {
        var announcements = await _dbContext.AnnouncmentJob.FindAsync(id);
        if (announcements == null) return false;

        announcements.Title = announcementDTO.Title;
        announcements.Description = announcementDTO.Description;
        announcements.Date_added = DateTime.Now;

        _dbContext.Update(announcements);
        await _dbContext.SaveChangesAsync();

        return true;
    }

    public IEnumerable<AnnouncmentJob> GetAllSimilarAnnouncements(int id)
    {
        var announcmentJob = _dbContext.AnnouncmentJob.Find(id);
        List<AnnouncmentJob> similar = new List<AnnouncmentJob>();

        if (announcmentJob is null) return Enumerable.Empty<AnnouncmentJob>();

        HashSet<string> stopWords = new HashSet<string> { "the", "of", "on", "a", "an", "you", "we", "i", "he", "she", "it", "they" };
        HashSet<string> SetTittle = new HashSet<string>(announcmentJob.Title.Split(',').SelectMany(s => s.Split(' ')).Select(s => s.Trim()).Where(s => !stopWords.Contains(s)));
        HashSet<string> SetDescription = new HashSet<string>(announcmentJob.Description.Split(',').SelectMany(s => s.Split(' ')).Select(s => s.Trim()).Where(s => !stopWords.Contains(s)));
        HashSet<string> mergedSet = new HashSet<string>(SetTittle, StringComparer.OrdinalIgnoreCase);
        mergedSet.UnionWith(SetDescription);

        foreach (var announcment in _dbContext.AnnouncmentJob)
        {
            HashSet<string> TargetTitle = new HashSet<string>(announcment.Title.Split(',').SelectMany(s => s.Split(' ')).Select(s => s.Trim()).Where(s => !stopWords.Contains(s)));
            HashSet<string> TargetDescription = new HashSet<string>(announcment.Description.Split(',').SelectMany(s => s.Split(' ')).Select(s => s.Trim()).Where(s => !stopWords.Contains(s)));
            HashSet<string> mergedSet2 = new HashSet<string>(TargetTitle, StringComparer.OrdinalIgnoreCase);
            mergedSet2.UnionWith(TargetDescription);
            int CommonWordsCount = mergedSet.Intersect(mergedSet2, StringComparer.OrdinalIgnoreCase).Count();

            if (CommonWordsCount >= 1 && announcment.Id != id)
            {
                similar.Add(new AnnouncmentJob
                {
                    Id = announcment.Id,
                    Title = announcment.Title,
                    Description = announcment.Description,
                });
            }
        }
        similar = similar.OrderByDescending(s => s.CommonWordsCount).ToList();

        return similar.Take(3);
    }

}

