document.addEventListener("DOMContentLoaded", function (event) {
    console.info("Loaded")
});

document.getElementById('formActCliente').addEventListener('submit', function (event) {
    event.preventDefault();
    const stockNuevo = document.getElementById('nuevoStock').value;
    const codigoProducto = document.getElementById('codigoProducto').value;
    //const host = 'http://localhost:5024';
    const host = 'https://v7m6wdlx-7174.brs.devtunnels.ms'
    let url = new URL(`${host}/Producto/${codigoProducto}`);
    fetch(url, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(stockNuevo)
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
                    <h3>Producto actualizado</h3>
                    <p>Código = ${data.codProducto}</p>
                    <p>Nombre = ${data.nombreProducto}</p>
                    <p>Marca producto = ${data.marcaProducto}</p>
                    <p>Alto caja = ${data.altoCaja}</p>
                    <p>Ancho caja = ${data.anchoCaja}</p>
                    <p>Profundidad caja = ${data.profundidadCaja}</p>
                    <p>Precio unitario = ${data.precioUnitario}</p>
                    <p>Stock mínimo = ${data.stockMinimo}</p>
                    <p>Stock total = ${data.stockTotal}</p>
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