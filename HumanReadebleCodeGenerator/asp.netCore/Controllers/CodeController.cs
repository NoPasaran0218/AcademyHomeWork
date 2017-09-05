using asp.netCore.BL;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace asp.netCore.Controllers
{
    [Route("api/[controller]")]
    public class CodeController:Controller
    {
        ICodeGenerator _codeGenerator;
        public CodeController(ICodeGenerator codeGenerator)
        {
            _codeGenerator = codeGenerator;
        }
        [HttpGet]
        [Route("GetCode/{id}")]
        public ActionResult GetCode(int id)
        {
            return Ok(_codeGenerator.GetCode(id));
        }

    }
}
