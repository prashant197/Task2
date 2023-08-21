using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.Http;
using Task2.Models;

namespace Task2.Controllers
{
    public class TasksController:ApiController
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["taskapi_conn"].ConnectionString);

         TaskBoard taksboard = new TaskBoard();
        Tasks task = new Tasks();
        //get all api
        //get all the tasks
        public List<Tasks> Get()
        {
            List<Tasks> firsttask = new List<Tasks>();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("GetAllTask", conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                da.Fill(dt);


                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Tasks tsk = new Tasks();
                        tsk.Task_Id = Convert.ToInt32(dt.Rows[i]["Task_Id"]);
                        tsk.Task_Name = dt.Rows[i]["Task_Name"].ToString();
                        tsk.Task_description = dt.Rows[i]["Task_description"].ToString();
                        tsk.Task_DeadLine = Convert.ToInt32(dt.Rows[i]["Task_DeadLine"]);
                        tsk.Task_Status = dt.Rows[i]["Task_Status"].ToString();
                        tsk.TaskBoard_Id = Convert.ToInt32(dt.Rows[i]["TaskBoard_Id"]);

                        firsttask.Add(tsk);



                    }
                    if (firsttask.Count > 0)
                    {
                        return firsttask;
                    }
                    else
                    {
                        return null;
                    }
                }

            }
            catch (Exception ex)
            {

            }
            finally
            {

                conn.Close();

            }
            return firsttask;


        }

        //get task from task_id
         public Tasks Get(int id)
        {

            Tasks tsk = new Tasks();

            SqlDataAdapter da = new SqlDataAdapter("TaskById", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Task_Id", id);
            DataTable dt = new DataTable();
            da.Fill(dt);


            if (dt.Rows.Count > 0)
            {


                tsk.Task_Name = dt.Rows[0]["Task_Name"].ToString();
                tsk.Task_Id = Convert.ToInt32(dt.Rows[0]["Task_Id"]);
                tsk.Task_description = dt.Rows[0]["Task_description"].ToString();
                tsk.Task_DeadLine = Convert.ToInt32(dt.Rows[0]["Task_DeadLine"]);
                tsk.Task_Status = dt.Rows[0]["Task_Status"].ToString();
                tsk.TaskBoard_Id = Convert.ToInt32(dt.Rows[0]["TaskBoard_Id"]);




            }
            if (tsk != null)
            {
                return tsk;
            }
            else
            {
                return null;
            }


            return tsk;
        }

        //post different task in tasks
        public string Post(Tasks task, int id)
        {
            string msg = "";
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("CheckTaskBoardID", conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@TaskBoard_Id", id);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    if (task != null)
                    {
                        SqlCommand cmd = new SqlCommand("AddTask", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Task_Name", task.Task_Name);
                        cmd.Parameters.AddWithValue("@Task_description", task.Task_description);
                        cmd.Parameters.AddWithValue("@Task_DeadLine", task.Task_DeadLine);
                        cmd.Parameters.AddWithValue("@Task_Status", task.Task_Status);
                        cmd.Parameters.AddWithValue("@TaskBoard_Id", task.TaskBoard_Id);

                        conn.Open();
                        int i = cmd.ExecuteNonQuery();
                        //conn.Close();

                        if (i > 0)
                        {
                            msg = "task has been added";
                        }
                        else
                        {
                            msg = "error";
                        }
                    }
                }
                else { return "no taskboard found with this taskboard id"; }
            }
            catch (Exception ex)
            { }
            finally
            {

                conn.Close();

            }
            return msg;

        }
        // update task from task id
        public string Put(int task_Id, Tasks task)
        {
            return "update tasks";

        }

        //delete task from taskID
        public string Delete(int task_Id)
        {
            return "delete task";

        }
    }
}