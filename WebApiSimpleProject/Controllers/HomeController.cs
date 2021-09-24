using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


// traq da smenq imeto na NameSpace kato ida v drugiq proekt

namespace WebApiSimpleProject.Controllers

{

    public class HomeController : Controller
    {

        public string FirstAction()
        {
            return "from our first action controller";
        }

    }

}