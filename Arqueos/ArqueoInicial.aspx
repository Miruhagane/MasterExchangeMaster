<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ArqueoInicial.aspx.cs" Inherits="WebApplication2.Arqueos.ArqueoInicial" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>

    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" />

    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.9/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js"></script>

    <script src="https://kit.fontawesome.com/30fa52b14d.js"></script>
    <link rel="stylesheet" href="https://unpkg.com/ionicons@4.5.10-0/dist/css/ionicons.min.css" />

    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@9"></script>

    <link href="../Content/Site.css" rel="stylesheet" />

    <title></title>
</head>
<body>
   
                <form id="form1" runat="server" class="row">
                                                         <div style="display: none">
                                                             <asp:TextBox ID="idtipo" runat="server"></asp:TextBox>
                                                             <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click"/>
                                                             <asp:TextBox ID="estatus" runat="server"></asp:TextBox>
                                                         </div>
                    
                    <div class="col-sm-12">                                                           u
                                         <div class="col-sm-6">
                                             <asp:GridView ID="Tb_arqueoinicial" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" DataKeyNames="Lng_IdCierre" ForeColor="Black" GridLines="Vertical">
                                                 <AlternatingRowStyle BackColor="#CCCCCC" />
                                                 <Columns>
                                                     <asp:TemplateField HeaderText="ID" InsertVisible="False" SortExpression="Lng_IdCierre">

                                                         <ItemTemplate>
                                                             <asp:TextBox ID="IdCierre" runat="server" CssClass="form-control" ReadOnly="true" Text='<%# Bind("Lng_IdCierre") %>'></asp:TextBox>
                                                         </ItemTemplate>
                                                     </asp:TemplateField>
                                                     <asp:BoundField DataField="Num_Denominacion" HeaderText="Denominacion" SortExpression="Num_Denominacion" />
                                                     <asp:TemplateField HeaderText="Cantidad" SortExpression="Cantidad">
                                                         <ItemTemplate>
                                                             <asp:TextBox ID="Cantidad" runat="server" CssClass="form-control" Text='<%# Bind("Dbl_Cantidad") %>'></asp:TextBox>
                                                         </ItemTemplate>

                                                     </asp:TemplateField>
                                                 </Columns>
                                                 <FooterStyle BackColor="#CCCCCC" />
                                                 <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                                 <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                 <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                                 <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                 <SortedAscendingHeaderStyle BackColor="Gray" />
                                                 <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                 <SortedDescendingHeaderStyle BackColor="#383838" />
                                             </asp:GridView>
                                         </div>
                        <div class="col-sm-6">
                            <asp:Button ID="btnguardar" CssClass="btn btn-secondary" runat="server" Text="Guardar" OnClick="btnguardar_Click" />
                        </div>
                    </div>

                 

                </form>
   
                              <script>
                                  var estatus = document.getElementById('<%=estatus.ClientID%>').value;

                                  if (estatus == 2) {

                                  }
                                  else
                                  {
                                      Swal.fire({
                                          title: 'Recuerda que solo puedes hacer esto 1 Vez',
                                          icon: 'warning',
                                          showCancelButton: true,
                                          confirmButtonColor: '#3085d6',
                                         
                                          confirmButtonText: 'Ok',

                                      }).then((result) => {
                                          if (result.isConfirmed) {
                                              document.getElementById("Button1").click();
                                          }

                                      })
                                  }
                                
                              </script>
</body>
</html>
