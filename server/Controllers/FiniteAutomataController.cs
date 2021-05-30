using logic.FiniteAutomataService;
using logic.FiniteAutomataService.DTO;
using logic.FiniteAutomataService.Extensions;
using logic.FiniteAutomataService.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.Controllers
{
    [ApiController]
    [Route("api/finiteautomata")]
    public class FiniteAutomataController : Controller
    {
        private IFiniteAutomataService _service;
        public FiniteAutomataController(IFiniteAutomataService service)
        {
            this._service = service;
        }

        [HttpPost]
        [Route("evaluate")]
        public ActionResult<FiniteAutomataStructureDto> EvaluateInstructions([FromBody] InstructionsInput input)
        {
            try
            {
                return Ok(_service.EvaluateFromInstructions(input));
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
        
        [HttpPost]
        [Route("words")]
        public ActionResult<TestsEvaluationResultDTO> CheckWords([FromBody] TestsInput input)
        {
            try
            {
                return Ok(_service.EvaluateTestCases(input));
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
