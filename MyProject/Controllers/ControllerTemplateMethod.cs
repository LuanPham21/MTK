using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyProject.Controllers
{
    public abstract class ControllerTemplateMethod : Controller
    {
        protected abstract void PrintRoutes();
        protected abstract void PrintDIs();

        // Template method
        public void PrintInformation()
        {
            PrintRoutes();
            PrintDIs();
        }
    }
}