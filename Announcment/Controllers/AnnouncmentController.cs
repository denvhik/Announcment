using Announcment.DataContext;
using Announcment.DTO;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Announcment.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnnouncmentController : ControllerBase
    {
        private readonly AnnouncmentContext _dbContext;
        public AnnouncmentController(AnnouncmentContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("id-range")]
        public async Task<IActionResult> GetIdRange()
        {
            var minId = await _dbContext.AnnouncmentJob.MinAsync(a => a.Id);
            var maxId = await _dbContext.AnnouncmentJob.MaxAsync(a => a.Id);

            return Ok(new { minId, maxId });
        }

        [HttpGet("announcements")]
        public async Task<IActionResult> GetAnnouncement()
        {
            
            var AnnouncmentJobs = await _dbContext.AnnouncmentJob.ToListAsync();
            

            if (AnnouncmentJobs is null) return NotFound("Оголошення під таким номером не знайдене");

            
            return Ok(AnnouncmentJobs);
        }

        [HttpGet("announcements/{id}")]
        public async Task<IActionResult> GetAllAnnouncements( int id)
        {

            var AnnouncmentJob= await _dbContext.AnnouncmentJob.FindAsync(id);

            if (AnnouncmentJob is null) return NotFound("Оголошення під таким номером не знайдене");
            var AnnouncmnetJobDTO = new AnnouncmentJobDTO(AnnouncmentJob);
            return Ok(AnnouncmnetJobDTO);
        }

        [HttpPost("announcements")]
        public async Task<IActionResult> Post([FromBody] AnnouncmentJobDTO announcmentJobDTO)
        {
            if (announcmentJobDTO is null)
            {
                return BadRequest("404 - ERROR");
            }

            var announcment = new AnnouncmentJob
            {
                Title = announcmentJobDTO.Title,
                Description = announcmentJobDTO.Description,
                Date_added = DateTime.Now
            };

            _dbContext.Add(announcment);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }


        [HttpDelete("announcements/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var announcements = await _dbContext.AnnouncmentJob.FindAsync(id);

            if (announcements is null ) return BadRequest(NotFound(" announcment such id not exist"));

            _dbContext.Remove(announcements);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("announcements/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AnnouncmentJobDTO announcementDTO)
        {
            var announcements = await _dbContext.AnnouncmentJob.FindAsync(id);
            

            if (announcements == null)
                return BadRequest(NotFound());

            announcements.Title = announcementDTO.Title;
            announcements.Description = announcementDTO.Description;
            announcements.Date_added = DateTime.Now;



            _dbContext.Update(announcements);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("announcements/{id}/similar")]
        public IEnumerable<AnnouncmentJob> GetAllSimilarAnnouncements(int id)
        {                      
            var announcmentJob = _dbContext.AnnouncmentJob.Find(id);
            List<AnnouncmentJob> similar = new List<AnnouncmentJob>();

            if (announcmentJob is null) return Enumerable.Empty<AnnouncmentJob>();

            HashSet<string> stopWords = new HashSet<string> { "the", "of", "on", "a", "an","you","we","i","he","she","it","they" };
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
}