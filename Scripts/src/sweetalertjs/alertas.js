

function compras_cantidad() {


    var valoress = document.getElementById("valor").value;
    var cantidades = document.getElementById("cantidad").value;


    if (cantidad.value == "") {

        alert("Tienes Que escribir la cantidad a resivir ");

    }

    else {
        let t_pagar = cantidades * valoress;
        document.getElementById("total_pago").value = t_pagar;
    }

}

function crt() {
    var totalp = document.getElementById("total_pago").value;
    var t = document.getElementById("cantidad").value;
    if (totalp >= 1000) {

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
                $('modalshowr').modal('show')
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
                $('modalshow').modal('show')
            }
        })


        document.getElementById("btn1").style.display = "";
        document.getElementById("modal1").classList.add("invisible");


    }

}
