using Announcment.DataContext;
using Announcment.DTO;
using Announcment.Services;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Announcment.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnnouncmentController : ControllerBase
    {
        private readonly IAnnouncment _IAnnouncment;
        public AnnouncmentController(IAnnouncment iAnnouncment)
        {
            _IAnnouncment = iAnnouncment;
        }

        [HttpGet("id-range")]
        public async Task<IActionResult> GetIdRange()
        {

            try
            {
                // Виклик методу GetIdRange() з сервісу через залежність _announcmentService
                var (minId, maxId) = await _IAnnouncment.GetIdRange();

                return Ok(new { minId, maxId });
            }
            catch (Exception ex)
            {
                // Обробка помилки, якщо щось пішло не так у сервісі
                return BadRequest("Failed to get ID range: " + ex.Message);
            }

        }

        [HttpGet("announcements")]
        public async Task<IActionResult> GetAnnouncement()
        {

            try
            {
                // Виклик методу GetAnnouncements() з сервісу через залежність _announcmentService
                var announcements = await _IAnnouncment.GetAnnouncement();

                return Ok(announcements);
            }
            catch (Exception ex)
            {
                // Обробка помилки, якщо щось пішло не так у сервісі
                return BadRequest("Failed to get announcements: " + ex.Message);
            }
        }

        [HttpGet("announcements/{id}")]
        public async Task<IActionResult> GetAllAnnouncements(int id)
        {

            try
            {
                // Виклик методу GetAnnouncement(id) з сервісу через залежність _announcmentService
                var announcement = await _IAnnouncment.GetAllAnnouncements(id);

                if (announcement == null)
                    return NotFound("Оголошення під таким номером не знайдене");

                return Ok(announcement);
            }
            catch (Exception ex)
            {
                // Обробка помилки, якщо щось пішло не так у сервісі
                return BadRequest("Failed to get announcement: " + ex.Message);
            }
        }

        [HttpPost("announcements")]
        public async Task<IActionResult> Post([FromBody] AnnouncmentJobDTO announcmentJobDTO)
        {
            try
            {
                // Виклик методу PostAnnouncement(announcmentJobDTO) з сервісу через залежність _announcmentService
                await _IAnnouncment.Post(announcmentJobDTO);

                return Ok(announcmentJobDTO);
            }
            catch (Exception ex)
            {
                // Обробка помилки, якщо щось пішло не так у сервісі
                return BadRequest("Failed to add announcement: " + ex.Message);
            }
        }


        [HttpDelete("announcements/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                // Виклик методу DeleteAnnouncement(id) з сервісу через залежність _announcmentService
                await _IAnnouncment.Delete(id);

                return Ok();
            }
            catch (Exception ex)
            {
                // Обробка помилки, якщо щось пішло не так у сервісі
                return BadRequest("Failed to delete announcement: " + ex.Message);
            }
        }

        [HttpPut("announcements/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AnnouncmentJobDTO announcementDTO)
        {
            try
            {
                // Виклик методу UpdateAnnouncement(id, announcementDTO) з сервісу через залежність _announcmentService
                await _IAnnouncment.Update(id, announcementDTO);

                return Ok(id);
            }
            catch (Exception ex)
            {
                // Обробка помилки, якщо щось пішло не так у сервісі
                return BadRequest("Failed to update announcement: " + ex.Message);
            }


        }

        [HttpGet("announcements/{id}/similar")]
        public IEnumerable<AnnouncmentJob> GetAllSimilarAnnouncements(int id)
        {
            var similarAnnouncements = _IAnnouncment.GetAllSimilarAnnouncements(id);
            return similarAnnouncements;

        }
    }
}