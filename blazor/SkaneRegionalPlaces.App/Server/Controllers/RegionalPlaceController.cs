using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SkaneRegionalPlaces.App.Server.Services;
using SkaneRegionalPlaces.App.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SkaneRegionalPlaces.App.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegionalPlaceController : ControllerBase
    {
   
        public RegionalPlaceRepositoryService _repoService;
        private readonly ISendEmailService _sendEmailService;

        private readonly ILogger<RegionalPlaceController> _logger;

        public RegionalPlaceController(ILogger<RegionalPlaceController> logger, RegionalPlaceRepositoryService repoService, ISendEmailService sendEmailService)
        {
            _logger = logger;
            _repoService = repoService;
            _sendEmailService = sendEmailService;
        }
        [Authorize]
        [HttpGet]
        [Route("/places")]
        public async Task<IEnumerable<RegionalPlace>> GetAsync()
        {
            await Task.Delay(4000);
            return _repoService.GetRegionalPlaces().ToArray();
        }

        [HttpPost]
        [Consumes("application/json")]
        [Route("/email")]
        public async Task<IActionResult> SendEmail([FromBody]Contact contact)
        {          
            var emailSent = await _sendEmailService.SendEmail(contact);
            if (emailSent)
                return new ObjectResult("Email sent") { StatusCode = 200 };
            else
                return new ObjectResult("Email failed to be sent") { StatusCode = 500 };
        }
    }
}
