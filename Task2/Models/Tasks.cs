using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;

namespace Task2.Models
{
    public class Tasks //: TaskBoard
    {
        public int Task_Id { get; set; }

        public string Task_Name { get; set;}

        public string Task_description { get; set;}

        public int Task_DeadLine { get; set; }

        public string Task_Status { get; set;}

        public  int TaskBoard_Id { get; set; }






    }
}