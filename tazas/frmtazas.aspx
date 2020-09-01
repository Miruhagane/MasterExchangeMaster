<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmtazas.aspx.cs" Inherits="WebApplication2.aspx.frmtazas" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>

     <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" />


<script src="https://code.jquery.com/jquery-3.2.1.slim.min.js" ></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.9/umd/popper.min.js" ></script>
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js" ></script>

      <script src="https://kit.fontawesome.com/30fa52b14d.js" ></script>
    <link rel="stylesheet" href="https://unpkg.com/ionicons@4.5.10-0/dist/css/ionicons.min.css" />


    <link href="../Content/Site.css" rel="stylesheet" />

    <title>Control Tazas</title>
</head>
<body>
    <form id="form1" runat="server" class="form-row">
        <div style="display: none"><asp:TextBox ID="IdUser" runat="server"></asp:TextBox></div>

        <div class="col-md-5">
            <asp:GridView ID="Tb_taxaCompras" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" OnSelectedIndexChanged="Page_Load" OnSorted="Page_Load" AutoGenerateColumns="False" DataSourceID="taxacompras">
                <AlternatingRowStyle BackColor="#CCCCCC" />
                <Columns>
                    <asp:BoundField DataField="IdTaxa" HeaderText="IdTaxa" SortExpression="IdTaxa" />
                    <asp:TemplateField HeaderText="Valor" SortExpression="Valor">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Valor") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("Valor") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Dia" HeaderText="Dia" SortExpression="Dia" />
                </Columns>
                <FooterStyle BackColor="#CCCCCC" />
                <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#808080" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#383838" />
            </asp:GridView>
            <asp:SqlDataSource ID="taxacompras" runat="server" ConnectionString="<%$ ConnectionStrings:SysMasterExchangeConnectionString %>" SelectCommand="SELECT [IdTaxa], [Valor], [Dia] FROM [TaxaCompra]"></asp:SqlDataSource>
        </div>
            
                     <div class="col-md-3">

                     <div class="col-ms-12">

                     </div>
                     
                         <div class="col-ms-12 pt-2">
                     
                         </div>
                         
                     </div>

                      <div class="col-md-4">
                      </div>


    </form>
</body>
</html>

    <%--validacion tipo del usuario--%>
    <script type="text/javascript">

        var areadeusuario = document.getElementById('<%=IdUser.ClientID%>').value;

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