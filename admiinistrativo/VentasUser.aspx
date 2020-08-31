<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VentasUser.aspx.cs" Inherits="WebApplication2.admiinistrativo.Gridventas1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">


 <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" />


<script src="https://code.jquery.com/jquery-3.2.1.slim.min.js" ></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.9/umd/popper.min.js" ></script>
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js" ></script>

      <script src="https://kit.fontawesome.com/30fa52b14d.js" ></script>
    <link rel="stylesheet" href="https://unpkg.com/ionicons@4.5.10-0/dist/css/ionicons.min.css" />


    <link href="../Content/Site.css" rel="stylesheet" />
    <title>Listado de ventas Por Usuario</title>
</head>
<body>

   

    <div class="container-fluid"">

        <div class="row">
            
            <div class="card">
  <div class="card-header">
  <h3>Listado de ventas Por Usuario</h3>
  </div>
  <div class="card-body">
    <form id="form1" runat="server" class="form-group">

        <div style="display: none">
            <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
        </div>
       
        <%-- cajero --%>
        <div id="cajero" class="col-md-12">
           
            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" OnPageIndexChanging="GridView1_PageIndexChanging" CellPadding="3" ForeColor="Black" GridLines="Vertical">
                <AlternatingRowStyle BackColor="#CCCCCC" />
                <FooterStyle BackColor="#CCCCCC" />
                <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#808080" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#383838" />
            </asp:GridView>
            
            
            <asp:Button ID="Button2" CssClass="btn btn-success" runat="server" Text="Exportar a Excel" OnClick="Button2_Click" />
            
            
        </div>
        
       <%-- control administrativo--%>
        <div id="administrativo">
            <div class="form-row">


                            <div class="form-group col-md-3" >

                            <asp:Label ID="Label1" runat="server" Text="Cajero"></asp:Label>
                            <asp:DropDownList  class="custom-select" ID="Cajero1" runat="server" DataSourceID="SqlDataSource1" DataTextField="Txt_NomCorto" DataValueField="Int_Idusuario"></asp:DropDownList>

                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SysMasterExchangeConnectionString2 %>" SelectCommand="SELECT [Int_Idusuario], [Txt_NomCorto] FROM [Tb_Usuarios]"></asp:SqlDataSource>

                            </div>

                            <div class="form-group col-md-3">

                            <asp:Label ID="label2" runat="server" Text="Fecha de Inicio"></asp:Label>
                            
                                <asp:TextBox ID="fechainicio" type="date" CssClass="form-control" runat="server"></asp:TextBox>

                              
                            </div>

                            <div class="form-group col-md-3">
                   
                            <asp:Label ID="label3" runat="server" Text="Fecha Final"></asp:Label>
                            
                             <asp:TextBox ID="fechafinal" CssClass="form-control" type="date" runat="server"></asp:TextBox>
                                
                            </div>

                            <div class="form-group col-md-3 pt-4">
                                <asp:Button CssClass="btn btn-success " Type="submit" ID="Buscar" runat="server" Text="Buscar" OnClick="Buscar_Click" />
                            </div>

                        </div>
       
                        <div class="col-md-12">
                            
                             <asp:GridView ID="GridView2" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical">
                                 <AlternatingRowStyle BackColor="#CCCCCC" />
                                 <FooterStyle BackColor="#CCCCCC" />
                                 <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                 <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                 <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                 <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                 <SortedAscendingHeaderStyle BackColor="#808080" />
                                 <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                 <SortedDescendingHeaderStyle BackColor="#383838" />
                             </asp:GridView>
                           
                             <br />
                        </div>
                           <div>
                               <asp:Button ID="Button1" CssClass="btn btn-success " runat="server" Text="Exportar a Excel" OnClick="Button1_Click" />
                          </div>
            
              </div>
                              
    </form>

  </div>
</div>
          
      

    </div>

        </div>

    <%--validacion tipo del usuario--%>
    <script type="text/javascript">

        var areadeusuario = document.getElementById('<%=TextBox3.ClientID%>').value;



            // comprar tipo de usuario
        switch (areadeusuario) {
                //administrativo
                case "1":
                console.log("administrativo");
                document.getElementById("cajero").style.display = "none";
               
                    break;

                // cajero
                case "2":
                    console.log("cajero");
                document.getElementById("administrativo").style.display = "none";
                    break;

                // tipo de usuario no detectado
                default:
                    console.log("no detectado");
                    break;
            }
        

    

    </script>
</body>
</html>
