using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using StringCalculator.Application.Actions;

namespace StringCalculator.Api.Controllers
{
    [Route("api/StringCalculator")]
    [ApiController]
    [Produces("application/json")]
    public class StringCalculator : ControllerBase
    {
        private readonly GetStringCalculator stringCalculator;

        public StringCalculator(GetStringCalculator stringCalculator)
        {
            this.stringCalculator = stringCalculator;
        }

        [HttpGet]
        public ActionResult<string> Get([FromQuery] string input)
        {
            try
            {
                var parsedInput = input.Replace("\\n", "\n");
                return Ok(stringCalculator.Execute(parsedInput));
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500,e);
            }
        }
    }
}
