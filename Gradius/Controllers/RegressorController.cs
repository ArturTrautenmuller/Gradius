using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Gradius_DataBaseLayer;
using Gradius_Core;
using Newtonsoft.Json;

namespace Gradius.Controllers
{
    public class RegressorController : Controller
    {
        public PartialViewResult Index([FromQuery(Name = "Id")] string Id)
        {
            Models.Regressor.RegressorView regressorView = new Models.Regressor.RegressorView();
            regressorView.Id = Id;
            App app = new App();
            app.Id = Id;
            app.LoadRegressors();
            regressorView.App = app;


            return PartialView(regressorView);
        }

        public string TrainRegressor([FromQuery(Name = "Id")] string Id, [FromQuery(Name = "Hash")] string Hash)
        {
            string text = System.IO.File.ReadAllText(Location.ConfLocation);
            Conf conf = JsonConvert.DeserializeObject<Conf>(text);

            Regressor regressor = new Regressor();
            regressor.LoadByHash(Hash);

            string trainerDir = conf.TrainerDir;
            string Trainercmd = "python main.py ";
            Trainercmd += Id;
            Trainercmd += " ";
            Trainercmd += regressor.RegressorId;
            Trainercmd += " ";
            Trainercmd += regressor.Filter.Replace("\"", "\\\"");

            Command.runCmd(trainerDir, Trainercmd);

            return "ok";
        }
    }
}