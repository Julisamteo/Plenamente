document.oncontextmenu = function () {
    return false
}
function right(e) {
    var msg = "Accion bloqueada";
    if (navigator.appName == 'Netscape' && e.which == 3) {
        // alert(msg); //- Si no quieres asustar a tu usuario entonces quita esta linea...
        return false;
    }
    else if (navigator.appName == 'Microsoft Internet Explorer' && event.button == 2) {
        // alert(msg); //- Si no quieres asustar al usuario que utiliza IE,  entonces quita esta linea...
        //- Aunque realmente se lo merezca...
        return false;
    }
    return true;
}
document.onmousedown = right;

window.onload = function () {
    var myInput = document.getElementById('login');
    var myInput1 = document.getElementById('password');
    myInput.onpaste = function (e) {
        e.preventDefault();
        alert("esta acción está prohibida");
    }


    myInput1.onpaste = function (e) {
        e.preventDefault();
        alert("esta acción está prohibida");
    }

    myInput1.oncopy = function (e) {
        e.preventDefault();
        alert("esta acción está prohibida");
    }

}



/*
window.onload = function(e){
//el textbox buscar
otextbox = document.getElementById('buscarDato');
var otable = document.getElementById('tablaVista');
var obody = otable.getElementsByTagName('tbody');
orows = obody[0].getElementsByTagName('tr');
var filamensaje;
var nodotexto;
//evento oninput
otextbox.oninput = function (e) {
    resultado = false;
    ovalor = this.value;
    if (filamensaje && ovalor.length == 0) {
        filamensaje.parentNode.removeChild(filamensaje);
    }
    for (i = 0; i < orows.length; i++) {
        orows[i].style.display = "table-row";
    }
    if (ovalor) {
        for (i = 0; i < orows.length; i++) {
            // firefox no soporta innerText
            if (typeof orows[i].textContent !== "undefined") {
                nodotexto = orows[i].textContent; //para firefox
            } else {
                nodotexto = orows[i].innerText; //para otros navegadores
            }

            if (nodotexto.toLowerCase().indexOf(ovalor.toLowerCase()) != -1) {
                orows[i].style.display = "table-row";
                resultado = true;
            }
            else {
                orows[i].style.display = "none";

            }
        }
        if (!resultado) {
            filamensaje = obody[0].appendChild(document.createElement('tr'));
            celda = document.createElement('td');
            contenido = document.createTextNode("No hay resultados que mostrar!");
            celda.appendChild(contenido);
            filamensaje.appendChild(celda);
        }
    }

}
};
*/

jQuery('input[type=file]').change(function () {
    var filename = jQuery(this).val().split('\\').pop();
    var idname = jQuery(this).attr('id');
    console.log(jQuery(this));
    console.log(filename);
    console.log(idname);
    jQuery('span.' + idname).next().find('span').html(filename);
});