<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="arqueocierre.aspx.cs" Inherits="WebApplication2.Arqueos.arqueocierre" %>

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

    <script src="  https://printjs-4de6.kxcdn.com/print.min.js"></script>

    <link href="../Content/Site.css" rel="stylesheet" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server" class="">
                                   
        <div style="display: none">
            <asp:TextBox ID="idtipo" runat="server"></asp:TextBox>
            <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
            <a class="btn btn-secondary btn-sm" id="findesesion" role="button" href="/home/Logout">Cerrar Sesion</a>
        
            <asp:TextBox ID="estatus"  runat="server"></asp:TextBox>
        </div>
        
        <div class="col-sm-12 pl-5 card-group">

            <div class="card text-white text-center border-light bg-dark" style="max-width: 30rem;">
                <div class="card-header border-light">Arqueo Por Pesos</div>
                <div class="card-body">
                    <asp:GridView ID="Tb_arqueofinal" runat="server" AllowSorting="True" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="0" DataKeyNames="Lng_IdCierre" ForeColor="Black" GridLines="Vertical">
                        <AlternatingRowStyle BackColor="#CCCCCC" />
                        <Columns>
                            <asp:TemplateField HeaderText="ID" InsertVisible="False" SortExpression="Lng_IdCierre" Visible="False">

                                <ItemTemplate>
                                    <asp:TextBox ID="IdCierre" runat="server" CssClass="form-control" ReadOnly="true" Text='<%# Bind("Lng_IdCierre") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Num_Denominacion" HeaderText="Denominacion" SortExpression="Num_Denominacion" DataFormatString="{0:C2}" />
                            <asp:TemplateField HeaderText="Cantidad" SortExpression="Cantidad">
                                <ItemTemplate>
                                    <asp:TextBox ID="Cantidad" runat="server" oninput="sumas()" CssClass="form-control" Text='<%# Bind("Dbl_Cantidad") %>'></asp:TextBox>
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
                    <div>
                        <table class="table table-sm col-sm-12">
                            <thead class="thead-dark">
                                <tr>
                                    <th>Total:</th>
                                    <th></th>
                                    <th></th>
                                    <th></th>
                                    <th></th>
                                    <th></th>
                                    <th></th>
                                    <th></th>
                                    <th></th>
                                    <th>
                                        <span>$</span><span id="valor"></span>
                                    </th>
                                </tr>
                            </thead>

                        </table>
                    </div>
                </div>
                <div class="card-footer border-light">
                    <button id="btn1" type="button" class=" btn btn-info" onclick="adverencia1()">Guardar</button>

                    <button id="btn2" type="button" style="display: none" class=" btn btn-warning" onclick="adverencia2()">Guardar</button>

                    <div id="estatus5" style="display: none">
                        <button type="button" class="btn btn-danger" data-toggle="modal" data-target="#exampleModal">
                            Guardar
                        </button>
                    </div>

                  

                </div>
            </div>
                                   
            
                   <div class="card text-white text-center bg-dark ml-5 border-light" style="max-width: 30rem;">
                       <div class="card-header border-light">Arqueo Por Moneda</div>
                       <div class="card-body">
                           <asp:GridView ID="Tb_arqueoXmoenda" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" DataKeyNames="Codigo" ForeColor="Black" GridLines="Vertical">
                               <AlternatingRowStyle BackColor="#CCCCCC" />
                               <Columns>
                                   <asp:TemplateField HeaderText="Codigo" InsertVisible="False" SortExpression="Codigo" Visible="False">
                                       <ItemTemplate>
                                           <asp:TextBox ID="Codigo" runat="server" CssClass="form-control" ReadOnly="true" Text='<%# Bind("Codigo") %>'></asp:TextBox>
                                       </ItemTemplate>
                                   </asp:TemplateField>
                                   <asp:BoundField DataField="Moneda" HeaderText="Moneda" SortExpression="Moneda" />
                                   <asp:TemplateField HeaderText="Valor" SortExpression="Valor">
                                       <ItemTemplate>
                                           <asp:TextBox ID="Valor" runat="server" oninput="pormoneda()" CssClass="form-control" Text='<%# Bind("Valor") %>'></asp:TextBox>
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


        <div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-sm">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalLabel">Arqueo</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body" id="bodyticket">
                        <table class="table table-sm">
                            <thead>
                           
                            </thead>
                            <tbody>
                                <tr>
                                    <th scope="row"><small>Cajero</small></th>
                                    <td id="Nombre"><small>
                                        <asp:Label ID="cajero" runat="server" ></asp:Label> </small></td>
                                </tr>
                                <tr>
                                    <th scope="row">Fecha</th>
                                    <td>
                                        <asp:Label ID="fecha" runat="server" Text="Label"></asp:Label> </td>
                             
                                </tr>
                             
                                
                            </tbody>
                        </table>

                        <table class="table table-sm">
                            <thead>
                                <tr>
                                    <th scope="col">Denominacion</th>
                                    <th scope="col">Cantidad</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>  
                                  <td>$1,000</td>
                                    <td id="cantidad1"></td>
                                </tr>
                               
                                <tr>
                                    <td>$500</td>
                                    <td id="cantidad2"></td>
                                </tr>

                                <tr>
                                    <td>$200</td>
                                    <td id="cantidad3"></td>
                                </tr>

                                <tr>
                                    <td>$100</td>
                                    <td id="cantidad4"></td>
                                </tr>

                                <tr>
                                    <td>$50</td>
                                    <td id="cantidad5"></td>
                                </tr>

                                <tr>
                                    <td>$20</td>
                                    <td id="cantidad6"></td>
                                </tr>

                                <tr>
                                    <td>$10</td>
                                    <td id="cantidad7"></td>
                                </tr>

                                <tr>
                                    <td>$5</td>
                                    <td id="cantidad8"></td>
                                </tr>

                                <tr>
                                    <td>$2</td>
                                    <td id="cantidad9"></td>
                                </tr>

                                <tr>
                                    <td>$1</td>
                                    <td id="cantidad10"></td>
                                </tr>

                                <tr>
                                    <td>$.50</td>
                                    <td id="cantidad11"></td>
                                </tr>

                              
                            </tbody>
                        </table>


                        <table class="table table-sm">
                            <thead>
                                <tr>
                                    <th scope="col">moneda</th>
                                    <th scope="col">Cantidad</th>
                                    
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>Dolares</td>
                                    <td id="a"></td>
                                </tr>

                                <tr>
                                    <td>Euros</td>
                                    <td id="b"></td>
                                </tr>

                                <tr>
                                    <td>Canadienses</td>
                                    <td id="c"></td>
                                </tr>

                                <tr>
                                    <td>Libras</td>
                                    <td id="d"></td>
                                </tr>

                                <tr>
                                    <td>Suizos</td>
                                    <td id="e"></td>
                                </tr>

                                <tr>
                                    <td>Reales</td>
                                    <td id="f"></td>
                                </tr>

                                
                            </tbody>
                        </table>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>

                         <button type="button" class="btn btn-primary" onclick="printPage()">Guardar</button>

                        <div style="display:none">
                            <asp:Button ID="dt" OnClick="btnguardar_Click" class="btn btn-primary" runat="server" Text="guardar" />
                        </div>
                        
                    </div>
                </div>
            </div>
        </div>
              
    </form>
    <script>
      var estatus = document.getElementById('<%=estatus.ClientID%>').value;



        if (estatus == "") {
            Swal.fire({
                title: 'Espera',
                text: 'Esta seccion solo se puede hacer una vez, una vez terminado el proceso no podras cambiarlo',
                icon: 'warning',
                showCancelButton: false,
                confirmButtonColor: '#3085d6',
                confirmButtonText: 'Ok',

            }).then((result) => {
                if (result.isConfirmed) {
                    document.getElementById("Button1").click();
                }

            })

        }
        else if (estatus == 5)
        {
        

            Swal.fire({
                title: 'Datos Guardados', 
                icon: 'success',
                showCancelButton: false,
                confirmButtonColor: '#3085d6',
                confirmButtonText: 'Ok',

            }).then((result) => {
                if (result.isConfirmed) {
                        document.getElementById("findesesion").click();
                }

            })


        }
       
     



        function adverencia1()
        {
            var a = 3;

            document.getElementById('<%=estatus.ClientID%>').value = a;
            document.getElementById("btn1").style.display = "none";
            document.getElementById("btn2").style.display = "";

            if (estatus == 2) {
                Swal.fire({
                    icon: 'warning',
                    title: 'Espera',
                    text: 'este proceso se hace solo 1 vez, verfica tus datos!'

                })

            }
            else {

            }
        }

        function adverencia2()
        {
            var a = 4;

            document.getElementById('<%=estatus.ClientID%>').value = a;
            document.getElementById("btn1").style.display = "none";
            document.getElementById("btn2").style.display = "none";
            document.getElementById("estatus5").style.display = "";

            if (estatus == 3) {
                Swal.fire({
                    icon: 'warning',
                    title: 'Espera',
                    text: 'este proceso se hace solo 1 vez, verfica tus datos!'

                })

            }
            else
            {

            }
            
        }


                                
    </script>

   <script>
       function sumas()
       {
           var t1 = parseInt(document.getElementById("Tb_arqueofinal_Cantidad_0").value) * 1000;
           var t2 = parseInt(document.getElementById("Tb_arqueofinal_Cantidad_1").value) * 500;
           var t3 = parseInt(document.getElementById("Tb_arqueofinal_Cantidad_2").value) * 200;
           var t4 = parseInt(document.getElementById("Tb_arqueofinal_Cantidad_3").value) * 100;
           var t5 = parseInt(document.getElementById("Tb_arqueofinal_Cantidad_4").value) * 50;
           var t6 = parseInt(document.getElementById("Tb_arqueofinal_Cantidad_5").value) * 20;
           var t7 = parseInt(document.getElementById("Tb_arqueofinal_Cantidad_6").value) * 10;
           var t8 = parseInt(document.getElementById("Tb_arqueofinal_Cantidad_7").value) * 5;
           var t9 = parseInt(document.getElementById("Tb_arqueofinal_Cantidad_8").value) * 2;
           var t10 = parseInt(document.getElementById("Tb_arqueofinal_Cantidad_9").value) * 1;
           var t11 = parseInt(document.getElementById("Tb_arqueofinal_Cantidad_10").value) * 0.50;

          


           document.getElementById("cantidad1").innerHTML = "";
           document.getElementById("cantidad2").innerHTML = "";
           document.getElementById("cantidad3").innerHTML = "";
           document.getElementById("cantidad4").innerHTML = "";
           document.getElementById("cantidad5").innerHTML = "";
           document.getElementById("cantidad6").innerHTML = "";
           document.getElementById("cantidad7").innerHTML = "";
           document.getElementById("cantidad8").innerHTML = "";
           document.getElementById("cantidad9").innerHTML = "";
           document.getElementById("cantidad10").innerHTML = "";
           document.getElementById("cantidad11").innerHTML = "";

           document.getElementById("cantidad1").innerHTML = document.getElementById("Tb_arqueofinal_Cantidad_0").value;
           document.getElementById("cantidad2").innerHTML = document.getElementById("Tb_arqueofinal_Cantidad_1").value;
           document.getElementById("cantidad3").innerHTML = document.getElementById("Tb_arqueofinal_Cantidad_2").value;
           document.getElementById("cantidad4").innerHTML = document.getElementById("Tb_arqueofinal_Cantidad_3").value;
           document.getElementById("cantidad5").innerHTML = document.getElementById("Tb_arqueofinal_Cantidad_4").value;
           document.getElementById("cantidad6").innerHTML = document.getElementById("Tb_arqueofinal_Cantidad_5").value;
           document.getElementById("cantidad7").innerHTML = document.getElementById("Tb_arqueofinal_Cantidad_6").value;
           document.getElementById("cantidad8").innerHTML = document.getElementById("Tb_arqueofinal_Cantidad_7").value;
           document.getElementById("cantidad9").innerHTML = document.getElementById("Tb_arqueofinal_Cantidad_8").value;
           document.getElementById("cantidad10").innerHTML = document.getElementById("Tb_arqueofinal_Cantidad_9").value;
           document.getElementById("cantidad11").innerHTML = document.getElementById("Tb_arqueofinal_Cantidad_10").value;

      



           var resultado = t1 + t2 + t3 + t4 + t5 + t6 + t7 + t8 + t9 + t10 + t11;
           var a = new Intl.NumberFormat().format(resultado);
           document.getElementById("valor").innerHTML = a;
        
        
           

       }
   </script>

    <script>

        function pormoneda()
        {
      

            var a = document.getElementById("Tb_arqueoXmoenda_Valor_0").value;
            var b = document.getElementById("Tb_arqueoXmoenda_Valor_1").value;
            var c = document.getElementById("Tb_arqueoXmoenda_Valor_2").value;
            var d = document.getElementById("Tb_arqueoXmoenda_Valor_3").value;
            var e = document.getElementById("Tb_arqueoXmoenda_Valor_4").value;
            var f = document.getElementById("Tb_arqueoXmoenda_Valor_5").value;

            document.getElementById("a").innerHTML = a;
            document.getElementById("b").innerHTML = b;
            document.getElementById("c").innerHTML = c;
            document.getElementById("d").innerHTML = d;
            document.getElementById("e").innerHTML = e;
            document.getElementById("f").innerHTML = f;

        }
    </script>

    <script>
        function printPage() {

            var div = document.getElementById("bodyticket").innerHTML;
            var contenidoOriginal = document.body.innerHTML;

            document.getElementById("dt").click();

            document.body.innerHTML = div;

            document.body.innerHTML = contenidoOriginal;
            
            window.print();
        }

    </script>

</body>
</html>
