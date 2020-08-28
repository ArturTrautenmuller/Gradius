using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Gradius_Core;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Gradius.Controllers
{
    public class DataManagerController : Controller
    {
        public PartialViewResult Index([FromQuery(Name="Id")] string Id)
        {
            Models.DataManager.DataManagaerView dataManagaerView = new Models.DataManager.DataManagaerView();
            dataManagaerView.Id = Id;

            string text = System.IO.File.ReadAllText(Location.ConfLocation);
            Conf conf = JsonConvert.DeserializeObject<Conf>(text);

            TransformedData main = new TransformedData();
            main.Load(conf.TransformedDataPath + "\\" + Id + "\\main.csv");
            main.Table.RemoveAt(0);

            dataManagaerView.TransformedData = main;

            return PartialView(dataManagaerView);
        }

        public async Task<string> UploadReplaceRawData([FromQuery(Name = "Id")] string Id)
        {
            string text = System.IO.File.ReadAllText(Location.ConfLocation);
            Conf conf = JsonConvert.DeserializeObject<Conf>(text);

            var file = Request.Form.Files[0];
            var filePath = Path.Combine(conf.RawDataPath + "/" + Id, "main.csv");
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return "ok";
        }

        public async Task<string> UploadConcatRawData([FromQuery(Name = "Id")] string Id)
        {
            string text = System.IO.File.ReadAllText(Location.ConfLocation);
            Conf conf = JsonConvert.DeserializeObject<Conf>(text);

            var file = Request.Form.Files[0];
            var filePath = Path.Combine(conf.RawDataPath + "/" + Id, "add.csv");
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            RawData main = new RawData();
            main.Load(conf.RawDataPath + "\\" + Id + "\\main.csv");

            RawData add = new RawData();
            add.Load(conf.RawDataPath + "\\" + Id + "\\add.csv");

            main.Concat(add);

            System.IO.File.Delete(conf.RawDataPath + "\\" + Id + "\\main.csv");
            System.IO.File.Delete(conf.RawDataPath + "\\" + Id + "\\add.csv");

            string content = "";
            foreach(string[] line in main.Table)
            {
                content += string.Join(";", line) + Environment.NewLine;
            }

            System.IO.File.WriteAllText(conf.RawDataPath + "\\" + Id + "\\main.csv", content);

            return "ok";
        }

        public async Task<string> UploadReplaceTransformedData([FromQuery(Name = "Id")] string Id)
        {
            string text = System.IO.File.ReadAllText(Location.ConfLocation);
            Conf conf = JsonConvert.DeserializeObject<Conf>(text);

            var file = Request.Form.Files[0];
            var filePath = Path.Combine(conf.TransformedDataPath + "/" + Id, "main.csv");
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return "ok";
        }

        public async Task<string> UploadConcatTransformedData([FromQuery(Name = "Id")] string Id)
        {
            string text = System.IO.File.ReadAllText(Location.ConfLocation);
            Conf conf = JsonConvert.DeserializeObject<Conf>(text);

            var file = Request.Form.Files[0];
            var filePath = Path.Combine(conf.TransformedDataPath + "/" + Id, "add.csv");
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            TransformedData main = new TransformedData();
            main.Load(conf.TransformedDataPath + "\\" + Id + "\\main.csv");

            TransformedData add = new TransformedData();
            add.Load(conf.TransformedDataPath + "\\" + Id + "\\add.csv");

            main.Concat(add);

            System.IO.File.Delete(conf.TransformedDataPath + "\\" + Id + "\\main.csv");
            System.IO.File.Delete(conf.TransformedDataPath + "\\" + Id + "\\add.csv");

            string content = "";
            foreach (string[] line in main.Table)
            {
                content += string.Join(";", line) + Environment.NewLine;
            }

            System.IO.File.WriteAllText(conf.TransformedDataPath + "\\" + Id + "\\main.csv", content);

            return "ok";
        }

        public string Grouping([FromQuery(Name = "Id")] string Id)
        {


            string text = System.IO.File.ReadAllText(Location.ConfLocation);
            Conf conf = JsonConvert.DeserializeObject<Conf>(text);

            RawData main = new RawData();
            main.Load(conf.RawDataPath + "\\" + Id + "\\main.csv");

            TransformedData transformedData = TransformedData.ConvertRawtoTransformed(main);

            string content = "";
            foreach (string[] line in transformedData.Table)
            {
                content += string.Join(";", line) + Environment.NewLine;
            }

            System.IO.File.WriteAllText(conf.TransformedDataPath + "\\" + Id + "\\main.csv", content);

            return "ok";
        }
    }
}