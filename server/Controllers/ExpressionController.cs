using logic.ExpressionService;
using logic.ExpressionService.Common.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.Controllers
{
    [ApiController]
    [Route("api/expression")]
    public class ExpressionController : Controller
    {
        [HttpPost]
        [Route("evaluate")]
        public ActionResult<ExpressionStructureDto> EvaluateExpression(string expression)
        {
            try
            {
                return Ok(ExpressionService.EvaluateExpression(expression));
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
        [HttpGet]
        public ActionResult<string> Get()
        {
            return "FONTYS ALE1 - Logical Expression Service";
        }
    }
}
