function compras_cantidad() {


    var valoress = document.getElementById("valor").value;
    var cantidades = document.getElementById("cantidad").value;


    if (cantidad.value == "") {

    }

    else {
        let t_pagar = cantidades * valoress;

        document.getElementById("total_pago").value = t_pagar;
        document.getElementById("mtotal").value = t_pagar.toFixed(2);

    }

    var moneda = document.getElementById("padre");
    var a = moneda.options[moneda.selectedIndex].text;
    var monto = document.getElementById("cantidad").value;
    var cambio = document.getElementById("valor").value;
    var total = document.getElementById("total_pago").value;
    var totalr = new Intl.NumberFormat().format(total);

    document.getElementById("monedacotizacion").innerHTML = a;
    document.getElementById("montocotizacion").innerHTML = monto;
    document.getElementById("cambiocotizacion").innerHTML = cambio;
    document.getElementById("pagocotizacion").innerHTML = totalr;
   
    var f = new Date();
    document.getElementById("fechajs").innerHTML = f.getDate() + "/" + (f.getMonth() + 1) + "/" + f.getFullYear();

}

function crt() {

    var usddiv = document.getElementById("usd").value;
    var cantidades = document.getElementById("cantidad").value;
    var usdmax = usddiv * 1000;

    let tuser = document.getElementById("idtipodeusuario").value;
    var totalp = document.getElementById("total_pago").value;
    var t = document.getElementById("cantidad").value;


    if (tuser == "2")
    {
        console.log("asdasd_" + tuser);   
        if (totalp >= usdmax) {


            console.log("asjbda_:" + tuser);
            document.getElementById("btn1").style.display = "";
            document.getElementById("modal1").classList.add("invisible");


            Swal.fire({
                title: 'Espera',
                text: 'El Total A Pagar Es Mayor A 1000 USD, Por Favor Verifica O Registra Al CLiente',
                icon: 'warning',
                confirmButtonColor: '#3085d6',
                confirmButtonText: 'Entendido'

            }).then((result) => {
                if (result.value) {
                    $('#modalshow').modal('show');
                }
            })

        }
        else if (totalp == "" || totalp == "0" || t == "" || t == "0") {


            Swal.fire({
                title: "Espera",
                text: "Tienes Que Llenar El Formulario",
                icon: 'error'
            });

        }

        else if (cantidades >= 100000) {
            Swal.fire({
                title: 'Error',
                text: 'la cantidad no puede superar el Millon',
                icon: 'warning',
                confirmButtonColor: '#3085d6',
                confirmButtonText: 'Entendido'

            }).then((result) => {
                if (result.value) {
                    document.getElementById("cantidad").value = "";

                }
            })

        }

        else {

            console.log("asjbda_:" + tuser);
            Swal.fire({
                title: "Verificacion Completa",
                text: "¿Quieres Registrar o Verificar Al Cliente?",
                icon: 'question',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'SI',
                cancelButtonText: 'no'

            }).then((result) => {

                if (result.value) {
                    $('#modalshow').modal('show');
                }
            })
            document.getElementById("btn1").style.display = "";
            document.getElementById("modal1").classList.add("invisible");

        }
    }
    else
    {
        console.log("asdw_:" + tuser);
        document.getElementById("btn1").style.display = "";
        document.getElementById("modal1").classList.add("invisible");
    }

}




