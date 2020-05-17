using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace InfiniDemo.Models.ResponseModels
{
    public class CustomerListResult : BindableBase
    {
        public bool Success { get; set; }
        public List<CustomerData> Data { get; set; }
        public int TotalRowCount { get; set; }

        public class CustomerData
        {
            public int CardId { get; set; }
            public int CardTypeId { get; set; }
            public string CardTypeDescr { get; set; }
            public string CardName { get; set; }
            public bool CardIsMale { get; set; }
            public string CardTitleCode { get; set; }
            public string CardProfessionCode { get; set; }
            public bool CardIsInList { get; set; }
            public string CardMobilePhone { get; set; }
            public string CardStatus { get; set; }
            public string CardIdLocationId { get; set; }
            public int LocationId { get; set; }
            public string LocationName { get; set; }
            public string LocationPhone { get; set; }
            public string LocationBrickDescr { get; set; }
            public bool IsPharmacy { get; set; }
            public bool IsInMyList { get; set; }
            public string PropertyFrequency { get; set; }
            public string PropertySegment { get; set; }
            public int ThisMonthPlanCount { get; set; }
            public int ThisMonthRealizedPlanCount { get; set; }
            public int YtdPlanCount { get; set; }
            public int YtdRealizedPlanCount { get; set; }
            public double RealizedCountAvg { get; set; }

            public string IconString
            {
                get
                {
                    string iconSource = string.Empty;

                    if (Device.RuntimePlatform == Device.UWP)
                        iconSource = iconSource + "Assets/";
                    if (IsPharmacy)
                        iconSource = iconSource + "pharmacy.png";
                    else
                        iconSource = iconSource + "hospital.png";

                    return iconSource;
                }
            }

        }
    }
}
