using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

namespace ViewingGradesPorjectv1
{
    public partial class login : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\projectdb.mdf;Integrated Security=True");

        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void Login_Click(object sender, EventArgs e)
        {
            string user = role.SelectedValue;

            if (user == "student")
            {
                conn.Open();
                string userIdInput = userid.Text;

                if (int.TryParse(userIdInput, out int studentId))
                {
                    string checkUserQuery = "SELECT COUNT(*) FROM [dbo].[studentacc] WHERE [student_id] = @id";

                    using (SqlCommand command = new SqlCommand(checkUserQuery, conn))
                    {
                        command.Parameters.AddWithValue("@id", studentId);

                        int userCount = (int)command.ExecuteScalar();

                        if (userCount > 0)
                        {
                            string studentlogin = "SELECT * FROM [studentacc] WHERE [student_id] = @id AND [password] = @pass";
                            SqlCommand com = new SqlCommand(studentlogin, conn);
                            com.Parameters.AddWithValue("@id", studentId);
                            com.Parameters.AddWithValue("@pass", password.Text);

                            SqlDataReader dr = com.ExecuteReader();

                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    Response.Write("<script>alert('Login Successful');</script>");
                                    Session["name"] = dr["full_name"].ToString();
                                    Session["role"] = "student";
                                }
                                Response.Redirect("~/studentdashboard/main.aspx");
                            }
                            else
                            {
                                Response.Write("<script>alert('Invalid password.');</script>");
                            }
                        }
                        else
                        {
                            Response.Write("<script>alert('No student ID found');</script>");
                        }
                    }
                }
                else
                {
                    Response.Write("<script>alert('Invalid student ID. Please enter a valid student ID to log in as a student.');</script>");
                }

                conn.Close();
            }

            else if (user == "registrar")
            {
                    conn.Open();
                    string checkUserQuery = "SELECT COUNT(*) FROM [dbo].[registraracc] WHERE [registrar_name] = @id";

                    using (SqlCommand command = new SqlCommand(checkUserQuery, conn))
                    {
                        command.Parameters.AddWithValue("@id", userid.Text);

                        int userCount = (int)command.ExecuteScalar();

                        if (userCount > 0)
                        {
                            string registrarlogin = "SELECT [registrar_name], [password] FROM [registraracc] WHERE [registrar_name] = @id AND [password] = @pass";
                            SqlCommand com = new SqlCommand(registrarlogin, conn);
                            com.Parameters.AddWithValue("@id", userid.Text);
                            com.Parameters.AddWithValue("@pass", password.Text);

                                SqlDataReader dr = com.ExecuteReader();

                                if (dr.HasRows)
                                {
                                    while (dr.Read())
                                    {
                                        Response.Write("<script>alert('Login Successful');</script>");
                                        Session["name"] = dr["registrar_name"].ToString();
                                        Session["role"] = "registrar";
                                    }
                                    Response.Redirect("~/registrardashboard/main.aspx");
                                }
                                else
                                {
                                    Response.Write("<script>alert('Invalid password.');</script>");
                                }

                        }
                        else
                        {
                            Response.Write("<script>alert('No Registrar name found');</script>");
                        }
                    }
                    conn.Close();
            }
            else if (user == "instructor")
            {
                conn.Open();
                string checkUserQuery = "SELECT COUNT(*) FROM [dbo].[instructoracc] WHERE [instructor_name] = @id";

                using (SqlCommand command = new SqlCommand(checkUserQuery, conn))
                {
                    command.Parameters.AddWithValue("@id", userid.Text);

                    int userCount = (int)command.ExecuteScalar();

                    if (userCount > 0)
                    {
                        string instructorlogin = "SELECT [instructor_name], [password] FROM [instructoracc] WHERE [instructor_name] = @id AND [password] = @pass";
                        SqlCommand com = new SqlCommand(instructorlogin, conn);
                        com.Parameters.AddWithValue("@id", userid.Text);
                        com.Parameters.AddWithValue("@pass", password.Text);

                            SqlDataReader dr = com.ExecuteReader();

                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    Response.Write("<script>alert('Login Successful');</script>");
                                    Session["name"] = dr["instructor_name"].ToString();
                                    Session["role"] = "instructor";
                                }
                                Response.Redirect("~/instructordashboard/main.aspx");
                            }
                            else
                            {
                                Response.Write("<script>alert('Invalid password.');</script>");
                            }

                    }
                    else
                    {
                        Response.Write("<script>alert('No Instructor name found');</script>");
                    }
                }
                conn.Close();      
            }
            else if (user == "")
            {
                Response.Write("<script>alert('Please Select Role!');</script>");
            }
        }
    }
}