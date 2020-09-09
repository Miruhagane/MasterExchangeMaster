﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TazasVentas.aspx.cs" Inherits="WebApplication2.tazas.TazasVentas" %>

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
    <title>Control Tazas Ventas</title>
</head>
<body>
    <form id="form1" runat="server" class="form-row">
          <div style="display: none">
            <asp:TextBox ID="IdUser" runat="server"></asp:TextBox>    
        </div>

        <div class="col-sm-12 pl-5"> 
              <div class="col-sm-7">
                    <asp:GridView ID="Tb_taxaVentas" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" OnSelectedIndexChanged="Page_Load" OnSorted="Page_Load" AutoGenerateColumns="False" DataSourceID="TaxaVentas" DataKeyNames="Lng_IdTaxa,Int_IdMoneda">
                <AlternatingRowStyle BackColor="#CCCCCC" />
                <Columns>
                    <asp:BoundField DataField="IdTaxa" HeaderText="IdTaxa" SortExpression="IdTaxa" />
                     
                    <asp:TemplateField HeaderText="Valor" SortExpression="Valor">
                             <ItemTemplate>
                                   <asp:TextBox ID="TextValor" runat="server" Text='<%#Bind("Valor") %>'></asp:TextBox>
                             </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="Dia" HeaderText="Dia" SortExpression="Dia" />
                    <asp:BoundField DataField="Moneda" HeaderText="Moneda" SortExpression="Moneda" />
                     <asp:TemplateField HeaderText="Lng_IdTaxa" ShowHeader="False" SortExpression="Lng_IdTaxa" Visible="False">
                        <ItemTemplate>
                            <asp:TextBox ID="TextIdTaxa" runat="server" Text='<%#Bind("Lng_IdTaxa") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="Int_IdMoneda" HeaderText="Int_IdMoneda" ReadOnly="True" SortExpression="Int_IdMoneda" />
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
                 <asp:SqlDataSource ID="TaxaVentas" runat="server" ConnectionString="<%$ ConnectionStrings:SysMasterExchangeConnectionString %>" SelectCommand="SELECT [IdTaxa], [Valor], [Dia], [Moneda], [Lng_IdTaxa], [Int_IdMoneda] FROM [TaxaVenta]"></asp:SqlDataSource>
            </div>
            
            <div class="col-sm-5">
                         <div class="form-check form-check-inline col-sm-12">
                             <span class="form-check-input" type="checkbox"><input id="Cancun" type="checkbox" name="Cancun" onclick="cancun();" /></span>
                             <label class="form-check-label" for="cancun">Cancun</label>
                         </div>

                         <div class="form-check form-check-inline col-sm-12">
                             <span class="form-check-input" type="checkbox"><input id="Playa_Carmen" type="checkbox" name="Playa_Carmen" onclick="carmen();" /></span>
                             <label class="form-check-label" for="Playa_Carmen">Playa Del Carmen</label>
                         </div>

                         <div class="form-check form-check-inline col-sm-12">
                             <span class="form-check-input" type="checkbox"><input id="Cabos" type="checkbox" name="Cabos" onclick="cabos();" /></span>
                             <label class="form-check-label" for="Cabos"> Cabos</label>
                         </div>
                         
                         <div class="form-check form-check-inline col-sm-12">
                             <span class="form-check-input" type="checkbox"><input id="Tulum" type="checkbox" name="Tulum" onclick="tulum();" /></span>
                             <label class="form-check-label" for="Tulum">Tulum</label>
                         </div>
                         
                         <div class="form-check form-check-inline col-sm-12">
                             <span class="form-check-input" type="checkbox"><input id="CDMX" type="checkbox" name="CDMX" onclick="cdmx();" /></span>
                             <label class="form-check-label" for="CDMX">CDMX</label>
                         </div>
                         
                         <div class=" form-check form-check-inline col-sm-12">
                             <span class="form-check-input" type="checkbox"><input id="Leon1" type="checkbox" name="Leon1" onclick="checkleon();" /></span>
                             <label class="form-check-label" for="Leon1"> Leon</label>
                         </div>

            </div>
        </div>

        <div class="col-sm-12 form-group row pl-5 pt-1">
                         
                         
                           <div class="col-sm-2" id="listcancun" style="display:none">
                               <asp:CheckBoxList ID="CheckBoxcancun" runat="server" DataSourceID="suc_cancun" DataTextField="Txt_Sucursal" DataValueField="Lng_IdSucursal"></asp:CheckBoxList>
                               <asp:SqlDataSource ID="suc_cancun" runat="server" ConnectionString="<%$ ConnectionStrings:SysMasterExchangeConnectionString2 %>" SelectCommand="SELECT [Lng_IdSucursal], [Txt_Sucursal], [Int_IdPlaza] FROM [Tb_Sucursal] WHERE ([Int_IdPlaza] = @Int_IdPlaza)">
                                   <SelectParameters>
                                       <asp:Parameter DefaultValue="1" Name="Int_IdPlaza" Type="Int32" />
                                   </SelectParameters>
                               </asp:SqlDataSource>
                           </div>

                           <div class="col-sm-2" id="listaplaya" style="display:none">
                               <asp:CheckBoxList ID="CheckBoxcarmen" runat="server" DataSourceID="suc_playa" DataTextField="Txt_Sucursal" DataValueField="Lng_IdSucursal"></asp:CheckBoxList>
                               <asp:SqlDataSource ID="suc_playa" runat="server" ConnectionString="<%$ ConnectionStrings:SysMasterExchangeConnectionString2 %>" SelectCommand="SELECT [Lng_IdSucursal], [Int_IdPlaza], [Txt_Sucursal] FROM [Tb_Sucursal] WHERE ([Int_IdPlaza] = @Int_IdPlaza)">
                                   <SelectParameters>
                                       <asp:Parameter DefaultValue="2" Name="Int_IdPlaza" Type="Int32" />
                                   </SelectParameters>
                               </asp:SqlDataSource>
                           </div>

                           <div class="col-sm-2" id="listacabos" style="display:none">
                               <asp:CheckBoxList ID="CheckBoxcabos" runat="server" DataSourceID="suc_cabos" DataTextField="Txt_Sucursal" DataValueField="Lng_IdSucursal"></asp:CheckBoxList>
                               <asp:SqlDataSource ID="suc_cabos" runat="server" ConnectionString="<%$ ConnectionStrings:SysMasterExchangeConnectionString2 %>" SelectCommand="SELECT [Lng_IdSucursal], [Int_IdPlaza], [Txt_Sucursal] FROM [Tb_Sucursal] WHERE ([Int_IdPlaza] = @Int_IdPlaza)">
                                   <SelectParameters>
                                       <asp:Parameter DefaultValue="3" Name="Int_IdPlaza" Type="Int32" />
                                   </SelectParameters>
                               </asp:SqlDataSource>
                           </div>

                           <div class="col-sm-2" id="listatulum" style="display:none">
                               <asp:CheckBoxList ID="CheckBoxtulum" runat="server" DataSourceID="suc_tulum" DataTextField="Txt_Sucursal" DataValueField="Lng_IdSucursal"></asp:CheckBoxList>
                               <asp:SqlDataSource ID="suc_tulum" runat="server" ConnectionString="<%$ ConnectionStrings:SysMasterExchangeConnectionString2 %>" SelectCommand="SELECT [Lng_IdSucursal], [Int_IdPlaza], [Txt_Sucursal] FROM [Tb_Sucursal] WHERE ([Int_IdPlaza] = @Int_IdPlaza)">
                                   <SelectParameters>
                                       <asp:Parameter DefaultValue="4" Name="Int_IdPlaza" Type="Int32" />
                                   </SelectParameters>
                               </asp:SqlDataSource>
                           </div>

                           <div class="col-sm-2" id="listacdmx" style="display:none">
                               <asp:CheckBoxList ID="CheckBoxcdmx" runat="server" DataSourceID="suc_cdmx" DataTextField="Txt_Sucursal" DataValueField="Lng_IdSucursal"></asp:CheckBoxList>
                               <asp:SqlDataSource ID="suc_cdmx" runat="server" ConnectionString="<%$ ConnectionStrings:SysMasterExchangeConnectionString2 %>" SelectCommand="SELECT [Lng_IdSucursal], [Int_IdPlaza], [Txt_Sucursal] FROM [Tb_Sucursal] WHERE ([Int_IdPlaza] = @Int_IdPlaza)">
                                   <SelectParameters>
                                       <asp:Parameter DefaultValue="5" Name="Int_IdPlaza" Type="Int32" />
                                   </SelectParameters>
                               </asp:SqlDataSource>
                           </div>

                           <div class="col-sm-2" id="listaleon" style="display:none">
                               <asp:CheckBoxList ID="CheckBoxleon" runat="server" DataSourceID="suc_leon" DataTextField="Txt_Sucursal" DataValueField="Lng_IdSucursal"></asp:CheckBoxList>
                               <asp:SqlDataSource ID="suc_leon" runat="server" ConnectionString="<%$ ConnectionStrings:SysMasterExchangeConnectionString2 %>" SelectCommand="SELECT [Lng_IdSucursal], [Int_IdPlaza], [Txt_Sucursal] FROM [Tb_Sucursal] WHERE ([Int_IdPlaza] = @Int_IdPlaza)">
                                   <SelectParameters>
                                       <asp:Parameter DefaultValue="6" Name="Int_IdPlaza" Type="Int32" />
                                   </SelectParameters>
                               </asp:SqlDataSource>
                           </div>

                    <div class=" col-sm-12">
                        <div class="col-sm-6">
                            <asp:Label ID="valores" runat="server"></asp:Label>
                            <asp:Label ID="idmon" runat="server"></asp:Label>
                        </div>
                        
                        <div class="col-sm-6">
                            <asp:Button ID="Guardar" CssClass="btn btn-info" runat="server" Text="Guardar" OnClick="Guardar_Click" />

                        </div>

                    
                    </div>
                    
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


<script>


    function cancun() {
        var cancun = document.getElementById('Cancun');

        //muestra lista cancun
        if (cancun.checked == true) {

            document.getElementById("listcancun").style.display = "";
        }
        else {

            document.getElementById("listcancun").style.display = "none";
        }

    }

    function carmen() {
        var carmen = document.getElementById('Playa_Carmen');
        //muestra lista playa
        if (carmen.checked == true) {

            document.getElementById("listaplaya").style.display = "";
        }
        else {

            document.getElementById("listaplaya").style.display = "none";
        }
    }

    function cabos() {
        var cabos = document.getElementById('Cabos');
        //muestra lista tulum
        if (cabos.checked == true) {

            document.getElementById("listacabos").style.display = "";
        }
        else {

            document.getElementById("listacabos").style.display = "none";
        }
    }



    function tulum() {
        var tulum = document.getElementById('Tulum');
        //muestra lista cabos
        if (tulum.checked == true) {

            document.getElementById("listatulum").style.display = "";
        }
        else {

            document.getElementById("listatulum").style.display = "none";
        }
    }

    function cdmx() {
        var cdmx = document.getElementById('CDMX');

        //muestra lista cdmx
        if (cdmx.checked == true) {

            document.getElementById("listacdmx").style.display = "";
        }
        else {

            document.getElementById("listacdmx").style.display = "none";
        }
    }

    function checkleon() {

        var leon = document.getElementById('Leon1');

        //muestra lista leon
        if (leon.checked == true) {

            document.getElementById("listaleon").style.display = "";
        }
        else {

            document.getElementById("listaleon").style.display = "none";
        }
    }




</script>