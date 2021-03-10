using MeterReadingsUpload.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MeterReadingsUpload.Api
{
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public ApiController(IAccountService accountService)
        {
            _accountService = accountService;
        }


        [HttpPost]
        [Route("meter-reading-uploads")]
        public async Task<IActionResult> UploadReadings(IFormFile file)
        {
            try
            {
                var reader = new StreamReader(file.OpenReadStream());
                await _accountService.AddMeterReadings(reader);
                return Accepted();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
