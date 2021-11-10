using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QA.Api.Controllers
{
    [Route("/api/qa")]
    [Controller]
    public class QAController: ControllerBase
    {
        public QAController(IHttpContextAccessor httpContextAccessor)
        {

        }

    }
}
