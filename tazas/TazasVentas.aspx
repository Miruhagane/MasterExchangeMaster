<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TazasVentas.aspx.cs" Inherits="WebApplication2.tazas.TazasVentas" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="../Content/checkboxstyles.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" />

<script src="https://code.jquery.com/jquery-3.2.1.slim.min.js" ></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.9/umd/popper.min.js" ></script>
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js" ></script>

      <script src="https://kit.fontawesome.com/30fa52b14d.js" ></script>
    <link rel="stylesheet" href="https://unpkg.com/ionicons@4.5.10-0/dist/css/ionicons.min.css" />

    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@9"></script>
    <link href="../Content/Site.css" rel="stylesheet" />
    <title>Control Tazas Ventas</title>
</head>
<body>
    <form id="form1" runat="server" class="form-row">
       
          <div style="display: none">
            <asp:TextBox ID="IdUser" runat="server"></asp:TextBox> 
              <asp:TextBox ID="carga" runat="server"></asp:TextBox>
        </div>
        <div class="col-sm-12 row">

            <div class="col-sm-1" >
                  <h5 style="padding-left: 18px">Plazas:</h5>
                
            </div>

            <div class="col-sm-3 row">
                <div class="select">
                    <select class="" id="Plazas">
                        <option value="0">Selecciona una Plaza </option>
                        <option value="1">Cancun</option>
                        <option value="2">Playa Del Carmen</option>
                        <option value="3">Cabos</option>
                        <option value="4">Tulum</option>
                        <option value="5">CDMX</option>
                        <option value="6">Leon</option>
                    </select>
                </div>
                
            </div>

            <div class="col-sm-2">

            </div>

            <div class="col-sm-2">
                <%--<h5>Sucursales:</h5>--%>
            </div>

            <div class="col-sm-3 row">

                    <div class="">
                      <%--  <asp:DropDownList ID="sucursalselect" CssClass="" runat="server" DataSourceID="sucursales" DataTextField="Txt_Sucursal" DataValueField="Lng_IdSucursal"></asp:DropDownList>
                        <asp:SqlDataSource ID="sucursales" runat="server" ConnectionString="<%$ ConnectionStrings:SysMasterExchangeConnectionString2 %>" SelectCommand="SELECT [Txt_Sucursal], [Lng_IdSucursal] FROM [Tb_Sucursal]"></asp:SqlDataSource>--%>

                    </div>
            </div>

            <div class="col-sm-1">
                <div style="display: none">
                    <asp:Button ID="Buscarsucursal" Text="Buscar" CssClass=" btn-sm btn btn-secondary" runat="server" OnClick="Buscarsucursal_Click" />
                </div>
            </div>

            

        </div>

        <div class="col-sm-12 row pl-5">

            <div class="col-sm-6 form-group row ">

                <div class="card gradientebody shadow-lg rounded" style="width: 100%">

                    <div class="card-body" style="width: 1000px">
  
                        <div class="" id="listcancun" style="display: none">
                            <asp:CheckBoxList ID="CheckBoxcancun" runat="server" DataSourceID="suc_cancun" DataTextField="Txt_Sucursal" DataValueField="Lng_IdSucursal"></asp:CheckBoxList>
                            <asp:SqlDataSource ID="suc_cancun" runat="server" ConnectionString="<%$ ConnectionStrings:SysMasterExchangeConnectionString2 %>" SelectCommand="SELECT [Lng_IdSucursal], [Txt_Sucursal], [Int_IdPlaza] FROM [Tb_Sucursal] WHERE ([Int_IdPlaza] = @Int_IdPlaza)">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="1" Name="Int_IdPlaza" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </div>

                        <div class="" id="listaplaya" style="display: none">
                            <asp:CheckBoxList ID="CheckBoxcarmen" runat="server" DataSourceID="suc_playa" DataTextField="Txt_Sucursal" DataValueField="Lng_IdSucursal"></asp:CheckBoxList>
                            <asp:SqlDataSource ID="suc_playa" runat="server" ConnectionString="<%$ ConnectionStrings:SysMasterExchangeConnectionString2 %>" SelectCommand="SELECT [Lng_IdSucursal], [Int_IdPlaza], [Txt_Sucursal] FROM [Tb_Sucursal] WHERE ([Int_IdPlaza] = @Int_IdPlaza)">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="2" Name="Int_IdPlaza" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </div>

                        <div class="" id="listacabos" style="display: none">
                            <asp:CheckBoxList ID="CheckBoxcabos" runat="server" DataSourceID="suc_cabos" DataTextField="Txt_Sucursal" DataValueField="Lng_IdSucursal"></asp:CheckBoxList>
                            <asp:SqlDataSource ID="suc_cabos" runat="server" ConnectionString="<%$ ConnectionStrings:SysMasterExchangeConnectionString2 %>" SelectCommand="SELECT [Lng_IdSucursal], [Int_IdPlaza], [Txt_Sucursal] FROM [Tb_Sucursal] WHERE ([Int_IdPlaza] = @Int_IdPlaza)">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="3" Name="Int_IdPlaza" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </div>

                        <div class="" id="listatulum" style="display: none">
                            <asp:CheckBoxList ID="CheckBoxtulum" runat="server" DataSourceID="suc_tulum" DataTextField="Txt_Sucursal" DataValueField="Lng_IdSucursal"></asp:CheckBoxList>
                            <asp:SqlDataSource ID="suc_tulum" runat="server" ConnectionString="<%$ ConnectionStrings:SysMasterExchangeConnectionString2 %>" SelectCommand="SELECT [Lng_IdSucursal], [Int_IdPlaza], [Txt_Sucursal] FROM [Tb_Sucursal] WHERE ([Int_IdPlaza] = @Int_IdPlaza)">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="4" Name="Int_IdPlaza" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </div>

                        <div class="" id="listacdmx" style="display: none">
                            <asp:CheckBoxList ID="CheckBoxcdmx" runat="server" DataSourceID="suc_cdmx" DataTextField="Txt_Sucursal" DataValueField="Lng_IdSucursal"></asp:CheckBoxList>
                            <asp:SqlDataSource ID="suc_cdmx" runat="server" ConnectionString="<%$ ConnectionStrings:SysMasterExchangeConnectionString2 %>" SelectCommand="SELECT [Lng_IdSucursal], [Int_IdPlaza], [Txt_Sucursal] FROM [Tb_Sucursal] WHERE ([Int_IdPlaza] = @Int_IdPlaza)">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="5" Name="Int_IdPlaza" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </div>

                        <div class="" id="listaleon" style="display: none">
                            <asp:CheckBoxList ID="CheckBoxleon" runat="server" DataSourceID="suc_leon" DataTextField="Txt_Sucursal" DataValueField="Lng_IdSucursal"></asp:CheckBoxList>
                            <asp:SqlDataSource ID="suc_leon" runat="server" ConnectionString="<%$ ConnectionStrings:SysMasterExchangeConnectionString2 %>" SelectCommand="SELECT [Lng_IdSucursal], [Int_IdPlaza], [Txt_Sucursal] FROM [Tb_Sucursal] WHERE ([Int_IdPlaza] = @Int_IdPlaza)">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="6" Name="Int_IdPlaza" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </div>

                    </div>

                   
                </div>

            </div>

              <div class="col-sm-6 row">
                  <div class="card gradientebody shadow-lg rounded" style="width: 100%">
                      <div class="card-body ml-1" style="width: 500px">
                       
                          <asp:GridView ID="Tb_taxaVentas" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" OnSelectedIndexChanged="Page_Load" OnSorted="Page_Load" AutoGenerateColumns="False" DataKeyNames="Lng_IdTaxa,Int_IdMoneda" Height="250px">
                              <AlternatingRowStyle BackColor="#CCCCCC" Width="500px" />
                              <Columns>
                                  <asp:TemplateField>
                                      <ItemTemplate>
                                          <asp:Image ID="Image" src="" runat="server" />
                                      </ItemTemplate>
                                  </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Valor" SortExpression="Valor">
                                      <ItemTemplate>
                                          <asp:TextBox ID="TextValor" runat="server" Text='<%#Bind("Valor") %>'></asp:TextBox>
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
                      <div class="card-footer">

                          <button type="button" id="btnguardar" class="btn btn-sm btn-secondary" onclick="verificaicon()">Guardar</button>

                          <div style="display: none">
                              <asp:Button ID="Guardar" CssClass="btn btn-secondary btn-sm" runat="server" Text="Guardar" OnClick="Guardar_Click" />
                          </div>
                      </div>
                  </div>
       
            </div>
            
        
            
        </div>
  


    </form>
</body>
</html>
  

<script>

    (function () { obtsucursal(); })();

    function obtsucursal() {
        var valor = document.getElementById('<%= carga.ClientID %>').value;
        console.log(valor);
        if (valor == "1") {
            document.getElementById("Buscarsucursal").click();
        }
        else {

        }

    }


    var selectplazas = document.getElementById("Plazas");
    selectplazas.addEventListener("change", option);

    function option() {
        var opcion = document.getElementById('Plazas').value;
     
        //muestra lista cancun
        if (opcion == "1") {

            document.getElementById("listcancun").style.display = "";
            document.getElementById("listaplaya").style.display = "none";
            document.getElementById("listacabos").style.display = "none";
            document.getElementById("listatulum").style.display = "none";
            document.getElementById("listacdmx").style.display = "none";
            document.getElementById("listaleon").style.display = "none";
        }

        else if (opcion == "2") {


            document.getElementById("listcancun").style.display = "None";
            document.getElementById("listaplaya").style.display = "";
            document.getElementById("listacabos").style.display = "none";
            document.getElementById("listatulum").style.display = "none";
            document.getElementById("listacdmx").style.display = "none";
            document.getElementById("listaleon").style.display = "none";
        }

        else if (opcion == "3") {
            document.getElementById("listcancun").style.display = "None";
            document.getElementById("listaplaya").style.display = "none";
            document.getElementById("listacabos").style.display = "";
            document.getElementById("listatulum").style.display = "none";
            document.getElementById("listacdmx").style.display = "none";
            document.getElementById("listaleon").style.display = "none";
        }

        else if (opcion == "4") {



            document.getElementById("listcancun").style.display = "None";
            document.getElementById("listaplaya").style.display = "none";
            document.getElementById("listacabos").style.display = "none";
            document.getElementById("listatulum").style.display = "";
            document.getElementById("listacdmx").style.display = "none";
            document.getElementById("listaleon").style.display = "none";
        }

        else if (opcion == "5") {

            document.getElementById("listcancun").style.display = "None";
            document.getElementById("listaplaya").style.display = "none";
            document.getElementById("listacabos").style.display = "none";
            document.getElementById("listatulum").style.display = "none";
            document.getElementById("listacdmx").style.display = "";
            document.getElementById("listaleon").style.display = "none";
        }

        else if (opcion == "6") {

            document.getElementById("listcancun").style.display = "None";
            document.getElementById("listaplaya").style.display = "none";
            document.getElementById("listacabos").style.display = "none";
            document.getElementById("listatulum").style.display = "none";
            document.getElementById("listacdmx").style.display = "none";
            document.getElementById("listaleon").style.display = "";
        }


    }



</script>

<script>

    function verificaicon() {
        const swalWithBootstrapButtons = Swal.mixin({
            customClass: {
                cancelButton: 'btn btn-danger ',
                confirmButton: ' btn btn-success'
                
            },
            buttonsStyling: false
        })

        swalWithBootstrapButtons.fire({
            title: '¿Quieres guardar los cambios?',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonText: 'Si, Guardar!  ',
            cancelButtonText: ' No, cancelar',
            reverseButtons: false
        }).then((result) => {
            if (result.isConfirmed) {
                swalWithBootstrapButtons.fire(
                    'Guardado!',
                    '',
                    'success'
                )

                document.getElementById("Guardar").click();
            } else if (
                /* Read more about handling dismissals below */
                result.dismiss === Swal.DismissReason.cancel
            ) {
                swalWithBootstrapButtons.fire(
                    'Proceso cancelado',
                    '',
                    'error'
                )
            }
        })

    }
</script>

<script>

    setInterval(function () { banderas(); }, 1000);
    function banderas() {


        document.getElementById("Tb_taxaVentas_Image_0").src = "../../Content/baderas/003-estados-unidos.png";
        document.getElementById("Tb_taxaVentas_Image_1").src = "../../Content/baderas/001-union-europea.png";
        document.getElementById("Tb_taxaVentas_Image_2").src = "../../Content/baderas/005-canada.png";
        document.getElementById("Tb_taxaVentas_Image_3").src = "../../Content/baderas/004-reino-unido.png";
        document.getElementById("Tb_taxaVentas_Image_4").src = "../../Content/baderas/006-suiza.png";
        document.getElementById("Tb_taxaVentas_Image_5").src = "../../Content/baderas/002-brasil.png";
        document.getElementById("Tb_taxaVentas_Image_6").src = "../../Content/baderas/007-australia.png";

    }
</script>
