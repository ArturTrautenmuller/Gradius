using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Gradius_Core;
using Newtonsoft.Json;
using Gradius_DataBaseLayer;
using Microsoft.AspNetCore.Http;

namespace Gradius.Controllers
{
    public class AppController : Controller
    {
        public PartialViewResult Index([FromQuery(Name = "Id")] string Id)
        {
            string text = System.IO.File.ReadAllText(Location.ConfLocation);
            Conf conf = JsonConvert.DeserializeObject<Conf>(text);

            Models.App.AppView appView = new Models.App.AppView();
            appView.Id = Id;
            
            TransformedData transformedData = new TransformedData();
            transformedData.Load(conf.TransformedDataPath+"\\"+Id+"\\main.csv");
            FilterOptions filterOptions = transformedData.GetFilterOptions();
            appView.FilterOptions = filterOptions;
            appView.Max = transformedData.getMaxValor();
            appView.Min = transformedData.getMinValor();

            return PartialView(appView);
        }

        public Models.App.QtdResponse GetQtdResponse([FromBody] Models.App.QtdResponseBody qtdResponseBody)
        {
            Models.App.QtdResponse qtdResponse = new Models.App.QtdResponse();
            Regressor regressor = new Regressor();
            string filterTxt = JsonConvert.SerializeObject(qtdResponseBody.FilterOptions);
            regressor.LoadByFilter(filterTxt);
            string text = System.IO.File.ReadAllText(Location.ConfLocation);
            Conf conf = JsonConvert.DeserializeObject<Conf>(text);


            if (regressor.RegressorId == null || regressor.RegressorId == "")
            {
                //treinar
                string Regressor_Id = Encoder.Base64Encode(Convert.ToString(DateTime.Now));
                string trainerDir = conf.TrainerDir;
                string Trainercmd = "python main.py ";
                Trainercmd += qtdResponseBody.appId;
                Trainercmd += " ";
                Trainercmd += Regressor_Id;
                Trainercmd += " ";
                Trainercmd += filterTxt.Replace("\"", "\\\"");

                Command.runCmd(trainerDir, Trainercmd);

                Regressor insertRegressor = new Regressor();
                insertRegressor.RegressorId = Regressor_Id;
                insertRegressor.AppId = qtdResponseBody.appId;
                insertRegressor.Filter = filterTxt;
                insertRegressor.Insert();
            }

            //consultar
             qtdResponse.Qtd = new List<double>();
            string ConsultDir = conf.PredictorDir;
            string ConsultCmd = "python main.py ";
            ConsultCmd += qtdResponseBody.appId;
            ConsultCmd += " ";
            ConsultCmd += regressor.RegressorId;
            ConsultCmd += " ";
            ConsultCmd += qtdResponseBody.Min;
            ConsultCmd += " ";
            ConsultCmd += qtdResponseBody.Max;
            ConsultCmd += " ";
            ConsultCmd += qtdResponseBody.Step;

            string Image = Command.runCmd(ConsultDir, ConsultCmd);
            Models.App.Previsor previsor = JsonConvert.DeserializeObject<Models.App.Previsor>(Image);
            qtdResponse.Qtd = previsor.Prev;
            
            return qtdResponse;
        }
        public PartialViewResult AddApp()
        {
            return PartialView();
        }

        public string CreateApp([FromQuery(Name = "Nome")] string Nome)
        {
            string userId = HttpContext.Session.GetString("User");
            App app = new App();
            app.UserId = userId;
            app.Nome = Nome;
            app.Insert();

            return "ok";
        }
    }
}