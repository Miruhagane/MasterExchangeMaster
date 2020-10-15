//  obtencio de valores de ventas.cshtml
function compras_cantidad() {

    var valoress = document.getElementById("valor").value;
    var cantidades = document.getElementById("cantidad").value;
    var valor2 = document.getElementById("valor2").value;
    var valor3 = document.getElementById("entregado").value;



    let multi = cantidades * valoress;



    if (cantidad.value == "") {


    }

    else if (monedaid.value == monid.value) {
        Swal.fire({
            title: "Espera",
            text: "VE A COMPRAR",
            icon: 'error'
        });

        document.getElementById("cambio").value = "";
        document.getElementById("total_pago").value = cantidades;

    }

    else {
        let t_pagar = multi / valor2;
        document.getElementById("total_pago").value = t_pagar.toFixed(4);
        document.getElementById("totalpagar").value = t_pagar.toFixed(2);
        let entregado = t_pagar - valor3;

        
        let total2 = entregado * valor2;

        console.log(total2);
        document.getElementById("cambio").value = total2.toFixed(2);

    }


    var monedapadre = document.getElementById("padre");
    var apadre = monedapadre.options[monedapadre.selectedIndex].text;
    var montopadre = document.getElementById("cantidad").value;
    var cambiopadre = document.getElementById("valor").value;

    var monedamadre = document.getElementById("Madre");
    var amadre = monedamadre.options[monedamadre.selectedIndex].text;
    var cambiomadre = document.getElementById("valor2").value;

    var total = document.getElementById("total_pago").value;


    document.getElementById("monedacotizacion1").innerHTML = apadre;
    document.getElementById("montocotizacion1").innerHTML = montopadre;
    document.getElementById("cambiocotizacion1").innerHTML = cambiopadre;

    document.getElementById("monedacotizacion2").innerHTML = amadre;
    document.getElementById("cambiocotizacion2").innerHTML = cambiomadre;

    document.getElementById("pagocotizacion1").innerHTML = total;

    var f = new Date();
    document.getElementById("fechajs").innerHTML = f.getDate() + "/" + (f.getMonth() + 1) + "/" + f.getFullYear();

   

}



// Operaciones de compra
function crt() {
    var totalp = document.getElementById("total_pago").value;
    var t = document.getElementById("cantidad").value;
    var v = document.getElementById("valor2").value;

    var valoress = document.getElementById("valor").value;
    var cantidades = document.getElementById("cantidad").value;

    var usddiv = document.getElementById("usd").value;
    var usdmax = usddiv * 1000;

    let multi = cantidades * valoress;


    if (multi >= usdmax) {

        document.getElementById("btn1").style.display = "";
        document.getElementById("modal1").classList.add("invisible");


        Swal.fire({
            title: 'Espera',
            text: 'El Total A Pagar Es Mayor A 1000 MXN, Por Favor Verifica O Registra Al CLiente',
            icon: 'warning',
            confirmButtonColor: '#3085d6',
            confirmButtonText: 'Entendido'

        }).then((result) => {
            if (result.value) {
                $('#modalshow').modal('show');
            }
        })

    }
    else if (totalp == "" || totalp == "0" || t == "" || t == "0" || v == "" || v == "0") {


        Swal.fire({
            title: "Espera",
            text: "Tienes Que Llenar El Formulario",
            icon: 'error'
        });

    }
    else if (entregado.value > total_pago.value) {
        Swal.fire({
            title: "Espera",
            text: "El Cantidad Entregada No Puede Ser Mayor Al Total a Pagar",
            icon: 'error'
        });
    }

    else {


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


(function () {
    obtenertipousuario();
})();

//validacion de usuarios
function obtenertipousuario() {
    var tipousuario = document.getElementById("idtipodeusuario").value;

    // comprar tipo de usuario
    switch (tipousuario) {
        //administrativo
        case "1":
            console.log("administrativo");
            break;

        // cajero
        case "2":
            console.log("cajero");
    
            break;

        // tipo de usuario no detectado
        default:
            console.log("no detectado");
            break;
    }
}


//obtener tipo especial
var esp = document.getElementById("Bol_Especial");

esp.addEventListener("change", refresh);


refresh();

function refresh() {
    var especial = Bol_Especial.value;
    console.log(especial);
    if (especial == "false" || especial == "") {

    }
    else {
        console.log("funcion remove readonly corriendo");
        var cambiodesabilitado = document.getElementById("valor");
        cambiodesabilitado.removeAttribute("readonly");
        
        var cambiodesabilitado = document.getElementById("valor2");
        cambiodesabilitado.removeAttribute("readonly");
    }
}