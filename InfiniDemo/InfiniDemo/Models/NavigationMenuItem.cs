using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace InfiniDemo.Models
{
    public class NavigationMenuItem : BindableBase
    {
        public string Title { get; set; }
        public string Icon { get; set; }
        public string PageName { get; set; }
    }
}
