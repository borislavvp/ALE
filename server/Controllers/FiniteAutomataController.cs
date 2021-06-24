using logic.FiniteAutomataService;
using logic.FiniteAutomataService.DTO;
using logic.FiniteAutomataService.Extensions;
using logic.FiniteAutomataService.Interfaces;
using logic.FiniteAutomataService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
        IConfiguration configuration;
        private IFiniteAutomataService _service;
        public FiniteAutomataController(IConfiguration configuration, IFiniteAutomataService service)
        {
            this._service = service;
            this.configuration = configuration;
        }

        [HttpPost]
        [Route("evaluate")]
        public ActionResult<FiniteAutomataEvaluationDTO> EvaluateInstructions([FromBody] InstructionsInput input)
        {
            try
            {
                return Ok(_service.EvaluateFromInstructions(configuration,input));
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
        
        [HttpPost]
        [Route("tests")]
        public ActionResult<TestsEvaluationResult> CheckTests([FromBody] TestsInput input)
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

        [HttpPost]
        [Route("check")]
        public ActionResult<TestsEvaluationResult> CheckWord(string word)
        {
            try
            {
                return Ok(_service.CheckWord(word));
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
