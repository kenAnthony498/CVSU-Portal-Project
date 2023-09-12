<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="ViewingGradesPorjectv1.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

 <title>LoginPage</title>
    <link rel="icon" href="img/cvsuicon.png" type="image/x-icon" />

    <link href="styles/login.css" rel="stylesheet" />
    <link href="Bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <script src="Bootstrap/js/bootstrap.min.js"></script>
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.7.0/css/all.css" integrity="sha384-lZN37f5QGtY3VHgisS14W3ExzMWZxybE1SJSEsQp9S+oqd12jhcu+A56Ebc1zFSJ" crossorigin="anonymous"/>

</head>
<body>
    <form id="form1" runat="server">
    <div class="container">
        <div class="row justify-content-center">
            <div class="col-md-2">
                <div class="card card-custom padding-custom">
                    <div class="card-body">
                    <asp:label for="role" runat="server" CssClass="label-custom">Select Role:</asp:label>
                    <asp:DropDownList ID="role" CssClass="label-custom" runat="server">
                        <asp:ListItem Text="..." Value=""></asp:ListItem>
                        <asp:ListItem Text="Student" Value="student"></asp:ListItem>
                        <asp:ListItem Text="Registrar" Value="registrar"></asp:ListItem>
                        <asp:ListItem Text="Instructor" Value="instructor"></asp:ListItem>
                    </asp:DropDownList>
                        <br />
                        <div class="input-group" style="padding-top:15px;">
                            <i class="input-group-text far fa-user-circle" style="padding-top: 10px;"></i>
                            <asp:Textbox id="userid" Cssclass="form-control" placeholder="Student ID / Name" runat="server"/>
                        </div>
                        <br />  
                        <div class="input-group">
                            <span class="input-group-text fas fa-lock" style="padding-top: 10px;"></span>
                            <asp:Textbox id="password" Cssclass="form-control"  placeholder="Password" runat="server" TextMode="Password"/>
                        </div>
                        <br />
                        <div class="input-group">
                            <asp:button id="submit" Text="Login" runat="server" class="btn btn-primary" OnClick="Login_Click" />
                           <a href="#" class="a-custom" id="openModal" >Forgot Password?</a>
                                <!-- The Modal -->
                                <div id="myModal" class="modal">
                                    <div class="modal-content">
                                        <span class="close" id="closeModal">&times;</span>
                                        <h2>Forgot Password?</h2>
                                        <p>Relax and try to remind it :D</p>
                                    </div>
                                </div>
                                 <!-- The Modal -->
                        </div> 
                    </div>
                </div>
            </div>
        </div>
    </div>
      
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [studentacc]"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [registraracc]"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [instructoracc]"></asp:SqlDataSource>
      
    </form>

</body>
    <script>
// Get the modal and close button elements
var modal = document.getElementById("myModal");
var closeModalBtn = document.getElementById("closeModal");
var openModalBtn = document.getElementById("openModal");

// Function to open the modal
function openModal() {
    modal.style.display = "block";
}

// Function to close the modal
function closeModal() {
    modal.style.display = "none";
}

// Event listeners for opening and closing the modal
openModalBtn.addEventListener("click", openModal);
closeModalBtn.addEventListener("click", closeModal);

// Close the modal if the user clicks outside of it
window.addEventListener("click", function (event) {
    if (event.target == modal) {
        closeModal();
    }
});

    </script>
</html>
