using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gradius_Core;

namespace Gradius.Models.App
{
    public class AppView
    {
        public string Id { get; set; }
        public string Min { get; set; }
        public string Max { get; set; }
        public FilterOptions FilterOptions { get; set; }
    }
}
