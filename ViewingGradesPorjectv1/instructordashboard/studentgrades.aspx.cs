using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

namespace ViewingGradesPorjectv1.instructordashboard
{
    public partial class studentgrades : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\projectdb.mdf;Integrated Security=True");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                activity.TextChanged += new EventHandler(CalculateFinalGrade);
                quiz.TextChanged += new EventHandler(CalculateFinalGrade);
                midterm_exam.TextChanged += new EventHandler(CalculateFinalGrade);
                project.TextChanged += new EventHandler(CalculateFinalGrade);
                final_exam.TextChanged += new EventHandler(CalculateFinalGrade);

            }
        }

        void clear()
        {
            studentid.Text = "";
            studentname.Text = "";
            studentsection.Text = "";
            Image.Visible = false;
            semester.ClearSelection();
            subject.SelectedIndex = 0;
            activity.Text = "";
            quiz.Text = "";
            midterm_exam.Text = "";
            project.Text = "";
            final_exam.Text = "";
            final_grade.Text = "";

        }
        protected void CalculateFinalGrade(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(activity.Text) &&
                !string.IsNullOrEmpty(quiz.Text) &&
                !string.IsNullOrEmpty(midterm_exam.Text) &&
                !string.IsNullOrEmpty(project.Text) &&
                !string.IsNullOrEmpty(final_exam.Text))
            {
                int activityValue, quizValue, midtermExamValue, projectValue, finalExamValue;

                if (int.TryParse(activity.Text, out activityValue) &&
                    int.TryParse(quiz.Text, out quizValue) &&
                    int.TryParse(midterm_exam.Text, out midtermExamValue) &&
                    int.TryParse(project.Text, out projectValue) &&
                    int.TryParse(final_exam.Text, out finalExamValue))
                {
                    double average = (activityValue + quizValue + midtermExamValue + projectValue + finalExamValue) / 5; 

                    if(average > 96.7)
                    {
                        final_grade.Text = "1.00";
                    }else if(average <= 96.6 && average > 93.4){
                        final_grade.Text = "1.25";
                    }
                    else if (average <= 93.30 && average > 90.1)
                    {
                        final_grade.Text = "1.50";
                    }
                    else if (average <= 90.0 && average > 86.7)
                    {
                        final_grade.Text = "1.75";
                    }
                    else if (average <= 86.6 && average > 83.4)
                    {
                        final_grade.Text = "2.00";
                    }
                    else if (average <= 83.3 && average > 80.1)
                    {
                        final_grade.Text = "2.25";
                    }
                    else if (average <= 80.0 && average > 76.7)
                    {
                        final_grade.Text = "2.50";
                    }
                    else if (average <= 76.6 && average > 73.4)
                    {
                        final_grade.Text = "2.75";
                    }
                    else if (average <= 73.3 && average > 70.00)
                    {
                        final_grade.Text = "3.00";
                    }
                    else if (average <= 69.9 && average > 50.0)
                    {
                        final_grade.Text = "4.00";
                    }
                    else if (average <= 50.0)
                    {
                        final_grade.Text = "5.00";
                    }         
                }
                else
                {
                    final_grade.Text = "Invalid input values";
                }
            }
            else
            {
                final_grade.Text = string.Empty;
            }
        }

        protected void search_Click(object sender, EventArgs e)
        {
            conn.Open();

            string search = "SELECT * FROM [studentacc] WHERE [student_id] = @id";
            SqlCommand com = new SqlCommand(search, conn);
            com.Parameters.AddWithValue("@id", studentid.Text);
            using (SqlDataReader reader = com.ExecuteReader())
            {
                if (reader.Read())
                {
                    studentname.Text = reader["full_name"].ToString();
                    studentsection.Text = reader["section"].ToString();

                    string Url = reader["student_img"].ToString();
                    Image.ImageUrl = Url;
                    Image.Visible = true;
                    
                }
                else
                {
                    Response.Write("<script>alert('No student ID found');</script>");
                }
            }
        }

        protected void save_Click(object sender, EventArgs e)
        {
            conn.Open();
            string selectedSubject = subject.SelectedValue;
            if (string.IsNullOrEmpty(studentid.Text) || string.IsNullOrEmpty(selectedSubject) || string.IsNullOrEmpty(activity.Text) || string.IsNullOrEmpty(quiz.Text) || string.IsNullOrEmpty(midterm_exam.Text) || string.IsNullOrEmpty(project.Text) || string.IsNullOrEmpty(final_exam.Text) || string.IsNullOrEmpty(final_grade.Text) || string.IsNullOrEmpty(semester.Text))
            {
                Response.Write("<script>alert('Please fill in all required fields.');</script>");
            }
            else
            {
                string checkSubjectQuery = "SELECT COUNT(*) FROM [request_grades] WHERE [subject] = @subject";
                SqlCommand checkSubjectCommand = new SqlCommand(checkSubjectQuery, conn);
                checkSubjectCommand.Parameters.AddWithValue("@subject", selectedSubject);

                int subjectCount = (int)checkSubjectCommand.ExecuteScalar();

                if (subjectCount > 0)
                {
                    Response.Write("<script>alert('Subject already exists in the database.');</script>");
                }
                else
                {
                    string pending = "True";
                    string save = "INSERT INTO [request_grades] (student_id, subject, activity, quiz, mid_term_exam, project, final_exam, final_grade, sem, isPending ) VALUES (@id, @subject, @activity, @quiz, @midterm, @project, @finalexam, @finalgrade, @sem, @pending)";
                    SqlCommand com = new SqlCommand(save, conn);
                    com.Parameters.AddWithValue("@id", studentid.Text);
                    com.Parameters.AddWithValue("@subject", subject.Text);
                    com.Parameters.AddWithValue("activity", int.Parse(activity.Text));
                    com.Parameters.AddWithValue("@quiz", int.Parse(quiz.Text));
                    com.Parameters.AddWithValue("@midterm", int.Parse(midterm_exam.Text));
                    com.Parameters.AddWithValue("@project", int.Parse(project.Text));
                    com.Parameters.AddWithValue("@finalexam", int.Parse(final_exam.Text));
                    com.Parameters.AddWithValue("@finalgrade", double.Parse(final_grade.Text));
                    com.Parameters.AddWithValue("@sem", semester.Text);
                    com.Parameters.AddWithValue("@pending", pending);

                    com.ExecuteNonQuery();
                    Response.Write("<script>alert('Grade Requested!');</script>");
                    clear();
                }
            }
            conn.Close();
        }

        protected void update_Click(object sender, EventArgs e)
        {
            conn.Open();
            if (string.IsNullOrEmpty(studentid.Text) || string.IsNullOrEmpty(activity.Text) || string.IsNullOrEmpty(quiz.Text) || string.IsNullOrEmpty(midterm_exam.Text) || string.IsNullOrEmpty(project.Text) || string.IsNullOrEmpty(final_exam.Text) || string.IsNullOrEmpty(final_grade.Text) || string.IsNullOrEmpty(semester.Text))
            {
                Response.Write("<script>alert('Please fill in all required fields.');</script>");
            }
            else
            {
                string update = "UPDATE [request_grades] SET [activity] = @activity, [quiz] = @quiz, [mid_term_exam] = @midterm, [project] = @project, [final_exam] = @finalexam, [final_grade]=@finalgrade, [sem] = @sem";
                SqlCommand com = new SqlCommand(update, conn);
                com.Parameters.AddWithValue("@activity", int.Parse(activity.Text));
                com.Parameters.AddWithValue("@quiz", int.Parse(quiz.Text));
                com.Parameters.AddWithValue("@midterm", int.Parse(midterm_exam.Text));
                com.Parameters.AddWithValue("@project", int.Parse(project.Text));
                com.Parameters.AddWithValue("@finalexam", int.Parse(final_exam.Text));
                com.Parameters.AddWithValue("@finalgrade", double.Parse(final_grade.Text));
                com.Parameters.AddWithValue("@sem", semester.Text);

                com.ExecuteNonQuery();
                Response.Write("<script>alert('Grade Request Updated!');</script>");
                clear();
            }
            conn.Close();
        }

        protected void subject_SelectedIndexChanged(object sender, EventArgs e)
        {
            conn.Open();
            string selectedSubject = subject.SelectedValue;

            if (!string.IsNullOrEmpty(selectedSubject))
            {
                string select = "SELECT * FROM [request_grades] WHERE [subject] = @subject AND [student_id] = @id";
                SqlCommand com = new SqlCommand(select, conn);
                com.Parameters.AddWithValue("@subject", selectedSubject);
                com.Parameters.AddWithValue("@id", studentid.Text);

                SqlDataReader dr = com.ExecuteReader();

                if (dr.Read())
                {
                    activity.Text = dr["activity"].ToString();
                    quiz.Text = dr["quiz"].ToString();
                    midterm_exam.Text = dr["mid_term_exam"].ToString();
                    project.Text = dr["project"].ToString();
                    final_exam.Text = dr["final_exam"].ToString();
                    final_grade.Text = dr["final_grade"].ToString();
                    semester.SelectedValue = dr["sem"].ToString();
                }
                else
                {
                    ClearInputFields();
                }
            }
            else
            {
                ClearInputFields();
            }

            conn.Close();
        }

        private void ClearInputFields()
        {
            activity.Text = string.Empty;
            quiz.Text = string.Empty;
            midterm_exam.Text = string.Empty;
            project.Text = string.Empty;
            final_exam.Text = string.Empty;
            final_grade.Text = string.Empty;
            semester.SelectedValue = string.Empty;
            semester.ClearSelection();
        }

    }
}