using System;
using System.Collections.Generic;
using System.Text;

namespace InfiniDemo.Models.ResponseModels
{
    public class UserMobileResult
    {
        public bool Success { get; set; }
        public UserMobileData Data { get; set; }

        public class UserMobileData
        {
            public string UserId { get; set; }
            public string CurrentUserId { get; set; }
            public int CardId { get; set; }
            public string UserName { get; set; }
            public string FullName { get; set; }
            public string EMail { get; set; }
            public string PositionDescription { get; set; }
            
        }

    }
}
