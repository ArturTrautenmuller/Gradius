using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gradius
{
    public static class Location
    {
        public static string ConfLocation = @"C:\Gradius\Conf.json";
        public static string serverUrl = "http://localhost:8080";
    }

    public class Conf
    {

        public string TransformedDataPath { get; set; }
        public string RawDataPath { get; set; }
        public string TrainerDir { get; set; }
        public string PredictorDir { get; set; }



    }
}
