using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using Task2.Models;

namespace Task2.Controllers
{
    public class TaskBoardController : ApiController
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["taskapi_conn"].ConnectionString);

        TaskBoard taksboard= new TaskBoard();
        TasksController task = new TasksController();

        // GET api/values
        // get all the taksboards 
        public List<TaskBoard> Get()
        {
            List<TaskBoard> firsttask = new List<TaskBoard>();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("GetAllTaskBoard", conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                da.Fill(dt);
               

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TaskBoard tsk = new TaskBoard();
                        tsk.TaskBoard_Id = Convert.ToInt32(dt.Rows[i]["TaskBoard_Id"]);
                        tsk.TaskBoard_Name = dt.Rows[i]["TaskBoard_Name"].ToString();
                        tsk.TaskBoard_Description = dt.Rows[i]["TaskBoard_Description"].ToString();

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
            catch(Exception ex)
            {

            }
            finally
            {

                conn.Close();

            }
            return firsttask;


        }

        // GET api/values/5
         //get specific taskboard from the id
        public TaskBoard Get(int id)
        {
            TaskBoard tsk = new TaskBoard();
            
                SqlDataAdapter da = new SqlDataAdapter("TaskBoardById", conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@TaskBoard_Id", id);
                DataTable dt = new DataTable();
                da.Fill(dt);
               

                if (dt.Rows.Count > 0)
                {

                   
                    tsk.TaskBoard_Name = dt.Rows[0]["TaskBoard_Name"].ToString();
                tsk.TaskBoard_Id = Convert.ToInt32(dt.Rows[0]["TaskBoard_Id"]);
                tsk.TaskBoard_Description = dt.Rows[0]["TaskBoard_Description"].ToString();





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

        // POST api/values
        //add taskboards to the taskboard
        public string Post(TaskBoard taskBoard)
        {
            string msg = "";
            try
            {
                if (taskBoard != null)
                {
                    SqlCommand cmd = new SqlCommand("AddTaskBoard", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TaskBoard_Name", taskBoard.TaskBoard_Name);
                    cmd.Parameters.AddWithValue("@TaskBoard_Description", taskBoard.TaskBoard_Description);
               //     cmd.Parameters.AddWithValue("@TaskBoard_DeadLine", taskBoard.TaskBoard_DeadLine);
                 //   cmd.Parameters.AddWithValue("@TaskBoard_Status", taskBoard.TaskBoard_Status);

                    conn.Open();
                   int i = cmd.ExecuteNonQuery();
                    //conn.Close();

                    if (i > 0)
                    {
                        msg = "taskboard has been added";
                    }
                    else
                    {
                        msg = "error";
                    }
                }
            }
            catch (Exception ex)
            { }
            finally
            {

                conn.Close();
            
            }
            return msg;





        }

        // PUT api/values/5
        //update taskboard using the taskboard_Id
        public string Put(int id, TaskBoard taskboard)
        {
            string msg = "";
            try
            {
            
                if (taskboard != null)
                {
                    SqlCommand cmd = new SqlCommand("UpdateTaskBoard", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TaskBoard_Id", id);
                    cmd.Parameters.AddWithValue("@TaskBoard_Name", taskboard.TaskBoard_Name);
                    cmd.Parameters.AddWithValue("@TaskBoard_Description", taskboard.TaskBoard_Description);
                    

                    conn.Open();
                    int i = cmd.ExecuteNonQuery();
                    conn.Close();

                    if (i > 0)
                    {
                        msg = "TaskBoard has been updated";
                    }
                    else
                    {
                        msg = "error";
                    }
                }
               
            }
            catch(Exception ex)
            {

            }
             return msg;
        }

        // DELETE api/values/5
        //delete taskboard using specific taskboardId
        public string Delete(int id)
        {
            string msg = "";

            SqlCommand cmd = new SqlCommand("DeleteTaskBoard", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TaskBoard_Id", id);


            conn.Open();
            int i = cmd.ExecuteNonQuery();
            conn.Close();

            if (i > 0)
            {
                msg = "Taskboard has been deleted";
            }
            else
            {
                msg = "error";
            }
            return msg;

        }
    }
}
