using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyProject.Controllers
{
    public abstract class ControllerTemplateMethod : Controller
    {
        protected abstract void PrintNotiSuccess();
        protected abstract void PrintNotiError();

        // Template method
        public void PrintInformation()
        {
            PrintNotiSuccess();
            PrintNotiError();
        }
    }
}