document.addEventListener("DOMContentLoaded", function (event) {
    console.info("Loaded")
});

document.getElementById('formRegistroCompra').addEventListener('submit', function (event) {
    event.preventDefault();

    const compra = {
        "codigoCompra": 0,
        "codigoProducto": document.getElementById('codigoProducto').value,
        "dNICliente": document.getElementById('dniCliente').value,
        "fechaCompra": Date.now().toS,
        "cantComprada": document.getElementById('cantidad').value,
        "fechaEntregaSolicitada":  document.getElementById('fechaEsperada').value,
        "estadoCompra": 1,
        "latitudGeografica": 0.0,
        "longitudGeografica": 0.0,
        "montoTotal": 0
    }
    //const host = 'http://localhost:5024';
    const host = 'https://v7m6wdlx-7174.brs.devtunnels.ms'
    fetch(`${host}/Compra`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(compra)
    })
        .then(response => {
            if (response.status === 200) {
                return response.json();
            } else if (response.status === 400) {
                return response.json().then(data => {
                    const errorDetails = data.map(error => error.errorDetail);
                    return errorDetails;
                });
            } else {
                throw new Error(response.status);
            }
        })
        .then(data => {
            if (Array.isArray(data)) {
                showErrorDetails(data);
            } else {
                showData(data);
            }
        })
        .catch(error => {
            console.log(error);
            showError(error);
        });
});

function showData(data) {
    respuesta = document.getElementById('formResponse');
    respuesta.innerHTML = `
                    <br>
                    <h3>Compra realizada</h3>
                    <p>Código de compra: ${data.codigoCompra}</p>
                    <p>Código de producto: ${data.codigoProducto}</p>
                    <p>DNI cliente: ${data.dniCliente}</p>
                    <p>Fecha de compra: ${data.fechaCompra.slice(0,10)}</p>
                    <p>Cantidad comprada: ${data.cantComprada}</p>
                    <p>Fecha entrega solicitada: ${data.fechaEntregaSolicitada.slice(0,10)}</p>
                    <p>Estado de compra: ${Object.keys(estadoEnum)[data.estadoCompra-1]}</p>
                    <p>Latitud geográfica: ${data.latitudGeografica}</p>
                    <p>Longitud geográfica: ${data.longitudGeografica}</p>
                    <p>Monto total: ${data.montoTotal}</p>
                `;
}

function showErrorDetails(data) {
    const errores = data.map(error => `<br><p>${error}</p>`).join('');
    const errorHTML = `<ul>${errores}</ul>`;
    document.getElementById('formResponse').innerHTML = `<br><h3>Error!</h3>` + errorHTML;
}

function showError(error) {
    var respuesta = document.getElementById('formResponse');
    respuesta.innerHTML = `
        <p> Error!</p>
        <br>
        <p>${error.message}</p>
    `;
}

var estadoEnum={
    "OPEN": 1,
    "READY_TO_DISPATCH": 2
}