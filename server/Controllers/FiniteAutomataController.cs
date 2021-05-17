using logic.FiniteAutomataService;
using logic.FiniteAutomataService.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.Controllers
{
    [ApiController]
    [Route("api/fa/")]
    public class FiniteAutomataController : Controller
    {
        [HttpPost]
        [Route("evaluate")]
        public ActionResult<FiniteAutomataStructureDto> Evaluate(string expression)
        {
            try
            {
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public ActionResult<string> Get()
        {
            return "FONTYS ALE2 - Finite Automata Service";
        }
    }
}
