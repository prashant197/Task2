using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Http;
using Task2.Models;
using System.Configuration;
using System.Threading.Tasks;

namespace Task2.Controllers
{
    public class TaskBoard_TaskController:ApiController
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["taskapi_conn"].ConnectionString);
        TaskBoard taksboard = new TaskBoard();
        

        //get all task using specific taskboard-Id
        public List<TaskBoardTasks> Get(int id)
        {
            Tasks tsk = new Tasks();
            TaskBoard tskboard = new TaskBoard();
            List<Tasks> alltask = new List<Tasks>();
            SqlDataAdapter da = new SqlDataAdapter("AllTaskFromTaskBoard", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@TaskBoard_Id", id);
            DataSet dataSet = new DataSet();
            da.Fill(dataSet);
            List<TaskBoardTasks> taskBoardTasksList = new List<TaskBoardTasks>();
            if (dataSet.Tables.Count > 1)
            {

                DataTable dataTableTaskBoard = dataSet.Tables[0];
                TaskBoardTasks taskBoardTasks;
                for (int j = 0; j < dataTableTaskBoard.Rows.Count; j++)
                {

                    taskBoardTasks = new TaskBoardTasks();
                    taskBoardTasks.TaskBoard_Id = Convert.ToInt32(dataTableTaskBoard.Rows[j]["TaskBoard_ID"]);
                    taskBoardTasks.TaskBoard_Name = dataTableTaskBoard.Rows[j]["TaskBoard_Name"].ToString();
                    taskBoardTasks.TaskBoard_Description = dataTableTaskBoard.Rows[j]["TaskBoard_Description"].ToString();


                    DataTable dt = dataSet.Tables[1];
                    taskBoardTasks.AllTask = new List<Tasks>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        //  tskboard.TaskBoard_Id = Convert.ToInt32(dt.Rows[i]["TaskBoard_Id"]);
                        //  tskboard.TaskBoard_Name = dt.Rows[i]["TaskBoard_Name"].ToString();
                        //  tskboard.TaskBoard_Description = dt.Rows[i]["TaskBoard_Description"].ToString();
                        tsk = new Tasks();
                        tsk.Task_Id = Convert.ToInt32(dt.Rows[i]["Task_Id"]);
                        tsk.Task_Name = dt.Rows[i]["Task_Name"].ToString();
                        tsk.Task_description = dt.Rows[i]["Task_description"].ToString();
                        tsk.Task_DeadLine = Convert.ToInt32(dt.Rows[i]["Task_DeadLine"]);
                        tsk.Task_Status = dt.Rows[i]["Task_Status"].ToString();
                       

                        taskBoardTasks.AllTask.Add(tsk);



                    }


                    taskBoardTasksList.Add(taskBoardTasks);
                }
            }
            return taskBoardTasksList;

        }

      /*  public TaskBoardTasks GetTaskBoardTasks(int dashboardId)
        {
            TaskBoardTasks taskBoard = new TaskBoardTasks();
            taskBoard.TaskBoard_Name = "Hp";
            taskBoard.TaskBoard_Description = "hp providing laptops";
            taskBoard.AllTask = new List<Task>();
           
            return taskBoard;

        }
      */
    }
}