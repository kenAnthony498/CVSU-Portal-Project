<%@ Page Title="" Language="C#" MasterPageFile="~/instructordashboard/instructor_dashboard.Master" AutoEventWireup="true" CodeBehind="studentgrades.aspx.cs" Inherits="ViewingGradesPorjectv1.instructordashboard.studentgrades" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .table-custom {
            position: relative;
            top: 20rem;
            left: 0em;
        }
        .table{
            text-align: center; 
        }
        .table tr {
            vertical-align: middle;
        }
        .input-center-placeholder::placeholder {
            text-align: center;
        }
        .input-center-placeholder {
            text-align: center;
            line-height: inherit; 
        }

        .panel-custom{
            position:absolute;
            top:5rem !important;
            left:30rem !important;
        }

        .img-custom{
            position: absolute;
            top: 5rem;
            left: 65rem;
            width: 10rem; 
            height: 10rem; 
            z-index: 999;
            border: 10px groove #fff;
        }
    </style>    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:Image ID="Image"  CssClass="img-custom" ImageUrl="#" runat="server" visible="false"/>

    <asp:Panel ID="Panel1" CssClass="panel-custom" runat="server">  

        <div class="col-md-12">
            <label>Search Student:</label>
                <div class="form-group">
                    <div class="input-group">
                        <asp:TextBox ID="studentid" CssClass="form-control" runat="server" placeholder="Student ID No."></asp:TextBox>
                        <asp:Button ID="search"  class="btn btn-primary" runat="server" Text="Search" OnClick="search_Click" />
                    </div>
                </div>
            <div class="form-group">
                    <label>Student Name: </label>
                        <asp:TextBox ID="studentname" CssClass="form-control" runat="server" Enabled="false" placeholder="Student Name"></asp:TextBox>  
                </div>
            <div class="form-group">
                    <label>Section: </label>
                        <asp:TextBox ID="studentsection" CssClass="form-control" runat="server" Enabled="false" placeholder="Section"></asp:TextBox>  
                </div>
            </div>  
    </asp:Panel>



    <asp:Table ID="grade" runat="server" CssClass="table table-bordered table-custom" style="background-color:cadetblue;">
        <asp:TableHeaderRow>
            <asp:TableHeaderCell>Subject/Semester</asp:TableHeaderCell>
            <asp:TableHeaderCell>Activities</asp:TableHeaderCell>
            <asp:TableHeaderCell>Quizzes</asp:TableHeaderCell>
            <asp:TableHeaderCell>Mid Term Exam</asp:TableHeaderCell>
            <asp:TableHeaderCell>Project</asp:TableHeaderCell>
            <asp:TableHeaderCell>Final Exam</asp:TableHeaderCell>
            <asp:TableHeaderCell>Final Grade</asp:TableHeaderCell>
        </asp:TableHeaderRow>
        <asp:TableRow>
                <asp:TableCell >                   
                        <asp:DropDownList ID="subject" CssClass="form-control"  runat="server" OnSelectedIndexChanged="subject_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem Text="Subject" Value=""></asp:ListItem>
                            <asp:ListItem Text="ITEC25" Value="it25"></asp:ListItem>
                            <asp:ListItem Text="ITEC50" Value="itec50"></asp:ListItem>
                            <asp:ListItem Text="GNED05" Value="gned05"></asp:ListItem>
                        </asp:DropDownList>
                </asp:TableCell>
                <asp:TableCell>    <asp:TextBox ID="activity" CssClass="form-control input-center-placeholder" placeholder="/100" runat="server" AutoPostBack="true" OnTextChanged="CalculateFinalGrade"></asp:TextBox>     </asp:TableCell>
                <asp:TableCell>    <asp:TextBox ID="quiz" CssClass="form-control input-center-placeholder" placeholder="/100" runat="server" AutoPostBack="true" OnTextChanged="CalculateFinalGrade"></asp:TextBox>     </asp:TableCell>
                <asp:TableCell>    <asp:TextBox ID="midterm_exam" CssClass="form-control input-center-placeholder" placeholder="/100" runat="server" AutoPostBack="true" OnTextChanged="CalculateFinalGrade"></asp:TextBox>     </asp:TableCell>
                <asp:TableCell>    <asp:TextBox ID="project" CssClass="form-control input-center-placeholder" placeholder="/100" runat="server" AutoPostBack="true" OnTextChanged="CalculateFinalGrade"></asp:TextBox>     </asp:TableCell>
                <asp:TableCell>    <asp:TextBox ID="final_exam" CssClass="form-control input-center-placeholder" placeholder="/100" runat="server" AutoPostBack="true" OnTextChanged="CalculateFinalGrade"></asp:TextBox>     </asp:TableCell>
                <asp:TableCell>    <asp:TextBox ID="final_grade" CssClass="form-control input-center-placeholder" placeholder="/5.0" runat="server" AutoPostBack="true" OnTextChanged="CalculateFinalGrade" Enabled="false"></asp:TextBox>     </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>  <asp:RadioButtonList ID="semester" runat="server"  class="input-group" data-toggle="buttons">
                                    <asp:ListItem Text="<b>1st Sem</b>" class="form-control"  Value="1stsem" />
                                    <asp:ListItem Text="<b>2nd Sem</b>" class="form-control"  Value="2ndsem" />
                                  </asp:RadioButtonList>  
                </asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell></asp:TableCell>
                <asp:TableCell>     <asp:Button ID="update" Text="Update to Registrar" class="btn btn-primary" runat="server" OnClick="update_Click" /> </asp:TableCell>
                <asp:TableCell>     <asp:Button ID="save" Text="Send to Registrar" class="btn btn-primary" runat="server" OnClick="save_Click"/>   </asp:TableCell>
            </asp:TableRow>
    </asp:Table>

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [studentacc]"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [request_grades]"></asp:SqlDataSource>
</asp:Content>
