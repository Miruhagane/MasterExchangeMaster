<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Historialtazas.aspx.cs" Inherits="WebApplication2.tazas.Historialtazas" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="../Content/checkboxstyles.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" />

    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.9/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js"></script>

    <script src="https://kit.fontawesome.com/30fa52b14d.js"></script>
    <link rel="stylesheet" href="https://unpkg.com/ionicons@4.5.10-0/dist/css/ionicons.min.css" />

    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@9"></script>

    <link href="../Content/Site.css" rel="stylesheet" />
    <title>Tazas Actuales</title>

</head>
<body>
    <form id="form1" runat="server">

       <div class="col-sm-12 row">

           <div class="col-sm-12">
               <div class="select">
                   <asp:DropDownList ID="sucursalselect" runat="server" DataSourceID="sucursales" AutoPostBack="true" DataTextField="Txt_Sucursal" DataValueField="Lng_IdSucursal" OnSelectedIndexChanged="sucursalselect_SelectedIndexChanged" OnTextChanged="sucursalselect_SelectedIndexChanged"></asp:DropDownList>
                   <asp:SqlDataSource ID="sucursales" runat="server" ConnectionString="<%$ ConnectionStrings:SysMasterExchangeConnectionString2 %>" SelectCommand="SELECT [Txt_Sucursal], [Lng_IdSucursal] FROM [Tb_Sucursal]"></asp:SqlDataSource>

               </div>

           </div>

           <div class="col-sm-6">
                 <div class="card shadow-lg">
                     <div class="card-header">
                           <h5>Tasa de Compra - <small><em><span id="sucursalcompra"></span></em></small></h5>
                     </div>
                      <div class="card-body">
                          <asp:GridView CssClass="table-sm tablacss table-hover" ID="Tb_taxaCompras" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="0" ForeColor="Black" GridLines="Vertical" OnSelectedIndexChanged="Page_Load" OnSorted="Page_Load" AutoGenerateColumns="False" DataKeyNames="Lng_IdTaxa,Int_IdMoneda" Height="250px" HorizontalAlign="Left" Width="500px">
                              <AlternatingRowStyle BackColor="#CCCCCC" />
                              <Columns>
                                  <asp:BoundField DataField="IdTaxa" HeaderText="Num." SortExpression="IdTaxa" />
                                  <asp:TemplateField HeaderText="Valor" SortExpression="Valor">
                                      <ItemTemplate>
                                          <asp:TextBox scope="row" ID="TextValor" ReadOnly="true" CssClass="form-control-sm" runat="server" Text='<%#Bind("Valor") %>'></asp:TextBox>
                                      </ItemTemplate>
                                  </asp:TemplateField>
                                  <asp:BoundField DataField="Dia" HeaderText="Dia" SortExpression="Dia" ApplyFormatInEditMode="True" DataFormatString="{0:d}" />
                                  <asp:BoundField DataField="Moneda" HeaderText="Moneda" SortExpression="Moneda" />
                                  <asp:TemplateField HeaderText="Lng_IdTaxa" ShowHeader="False" SortExpression="Lng_IdTaxa" Visible="False">
                                      <ItemTemplate>
                                          <asp:TextBox scope="row" ID="TextIdTaxa" runat="server"   CssClass="form-control-sm" Text='<%#Bind("Lng_IdTaxa") %>'></asp:TextBox>
                                      </ItemTemplate>
                                  </asp:TemplateField>
                              </Columns>
                              <FooterStyle BackColor="#CCCCCC" />
                              <HeaderStyle CssClass="thead-dark" BackColor="Black" Font-Bold="True" ForeColor="White" />
                              <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                              <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                              <SortedAscendingCellStyle BackColor="#F1F1F1" />
                              <SortedAscendingHeaderStyle BackColor="#808080" />
                              <SortedDescendingCellStyle BackColor="#CAC9C9" />
                              <SortedDescendingHeaderStyle BackColor="#383838" />
                          </asp:GridView>
                      </div>
                 </div>   
           </div>

           <div class="col-sm-6">
               <div class="card shadow-lg rounded">
                   <div class="card-header">
                          <h5>Tasa de Venta - <small><em><span id="sucursalventa"></span></em></small></h5>
                   </div>
                  <div class="card-body">
                      <asp:GridView CssClass="table-sm tablacss table-hover" ID="Tb_taxaVentas" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" OnSelectedIndexChanged="Page_Load" OnSorted="Page_Load" AutoGenerateColumns="False" DataKeyNames="Lng_IdTaxa,Int_IdMoneda" Height="250px">
                          <AlternatingRowStyle BackColor="#CCCCCC" Width="500px" />
                          <Columns>
                              <asp:BoundField DataField="IdTaxa" HeaderText="Num." SortExpression="IdTaxa" />
                              <asp:TemplateField HeaderText="Valor" SortExpression="Valor">
                                  <ItemTemplate>
                                      <asp:TextBox ID="TextValor" ReadOnly="true" runat="server" Text='<%#Bind("Valor") %>'></asp:TextBox>
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:BoundField DataField="Dia" HeaderText="Dia" SortExpression="Dia" DataFormatString="{0:d}" />
                              <asp:BoundField DataField="Moneda" HeaderText="Moneda" SortExpression="Moneda" />
                              <asp:TemplateField HeaderText="Lng_IdTaxa" ShowHeader="False" SortExpression="Lng_IdTaxa" Visible="False">
                                  <ItemTemplate>
                                      <asp:TextBox ID="TextIdTaxa" runat="server" Text='<%#Bind("Lng_IdTaxa") %>'></asp:TextBox>
                                  </ItemTemplate>
                              </asp:TemplateField>
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
                  </div>
               </div>
           </div>

       </div>

    </form>
</body>
</html>
<script>

    (function () { obtsuc(); })();

    function obtsuc()
    {
        var selectsucursal = document.getElementById("sucursalselect");
        var option = selectsucursal.options[selectsucursal.selectedIndex].text;

        document.getElementById("sucursalcompra").innerHTML = option;
        document.getElementById("sucursalventa").innerHTML = option;
    }
</script>
