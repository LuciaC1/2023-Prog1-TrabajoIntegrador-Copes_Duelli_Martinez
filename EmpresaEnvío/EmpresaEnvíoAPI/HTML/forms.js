document.getElementById('formActCliente').addEventListener('submit', function (event) {
    event.preventDefault();
    const stockNuevo = document.getElementById('nuevoStock').value;
    const codigoProducto = document.getElementById('codigoProducto').value;
    let url = new URL(`http://localhost:5024/Producto/${codigoProducto}`);
    fetch(url, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(stockNuevo)
    })
        .then(response => {
            if (!response.ok) {
                throw Error(response.statusText);
            }
            return response.json();
        })
        .then(data => {
            console.log(data);
            showData(data);
        })
        .catch(error => {
            console.log(error);
            showError(error);
        });
});

function showData(data) {
    var respuesta = document.getElementById('formResponse');
    respuesta.innerHTML = `
                    <p>Código = ${data.codProducto}</p>
                    <p>Nombre = ${data.nombreProducto}</p>
                    <p>Marca producto = ${data.marcaProducto}</p>
                    <p>Alto caja = ${data.altoCaja}</p>
                    <p>Ancho caja = ${data.anchoCaja}</p>
                    <p>Profundidad caja = ${data.profundidadCaja}</p>
                    <p>Precio unitario = ${data.precioUnitario}</p>
                    <p>Stock mínimo = ${data.stockMínimo}</p>
                    <p>Stock total = ${data.stockTotal}</p>
                `;
}

function showError(error) {
    var respuesta = document.getElementById('formResponse');
    respuesta.innerHTML = `
        <p> Error!</p>
        <br>
        <p>${error.message}</p>
    `;
}