using LibraryV4.Domain;
using LibraryV4.Domain.Interfaces;
using LibraryV4.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LibraryV4.API.Controllers
{
    [Route("api/pinjam")]
    [ApiController]
    public class PinjamController : ControllerBase
    {
        private readonly BookLoanSettings _bookLoanSettings;
        private readonly IPeminjamanService _pinjamService;

        public PinjamController(IOptions<BookLoanSettings> bookLoanSettings, IPeminjamanService peminjamanService)

        {

            _bookLoanSettings = bookLoanSettings.Value;
            _pinjamService = peminjamanService;

        }

        [HttpGet("loan-max")]
        public ActionResult<BookLoanSettings> GetLoanSettings()
        {
            return Ok(_bookLoanSettings.MaxBooksBorrowed);
        }

        [HttpGet("loan-max-day")]
        public ActionResult<BookLoanSettings> MaxdayLoan()
        {
            return Ok(_bookLoanSettings.LoanDurationDays);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Peminjaman>> GetPeminjamanByIdAsync(int id)
        {
            var peminjaman = await _pinjamService.GetPeminjamanByIdAsync(id);
            if (peminjaman == null)
            {
                return NotFound();
            }
            return Ok(peminjaman);
        }

        [HttpPost]
        public async Task<IActionResult> AddPeminjamanAsync([FromBody] Peminjaman peminjaman)
        {
            try
            {
                await _pinjamService.AddPeminjamanAsync(peminjaman);
                return Ok("Peminjaman berhasil dibuat");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePeminjamanAsync(int id)
        {
            var peminjaman = await _pinjamService.GetPeminjamanByIdAsync(id);
            if (peminjaman == null)
            {
                return NotFound();
            }

            await _pinjamService.DeletePeminjamanAsync(id);
            return Ok(peminjaman);
        }
    }
}
