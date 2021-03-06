using Microsoft.AspNetCore.Mvc;
using System;
using StringCalculator.Application.Actions;

namespace StringCalculator.Api.Controllers
{
    [ApiVersion("2")]
    [Route("api/v{version:apiVersion}/StringCalculator")]
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
                var parsedInput = "";
                if (input != null)
                {
                    parsedInput = input.Replace("\\n", "\n");
                }
                return Ok(stringCalculator.ExecuteV2(parsedInput));
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
