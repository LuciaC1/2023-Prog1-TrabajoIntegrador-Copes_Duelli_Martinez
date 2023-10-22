document.addEventListener("DOMContentLoaded", getProductos);

function getProductos() {
    //const host = 'http://localhost:5024';
    const host = 'https://v7m6wdlx-7174.brs.devtunnels.ms'
    fetch(`${host}/Producto`, {
        method: 'GET',
        headers: {
            'accept': '*/*'
        }
    })
        .then(response => {
            if (response.status === 200) {
                return response.json();
            } else {
                throw new Error(response.status);
            }
        })
        .then(data => {
            showData(data);
        })
        .catch(error => {
            console.log(error);
            showError(error);
        });
}

function showData(data) {
    respuesta = document.getElementById('tablaProductos');
    data.forEach(Producto => {
        var row = document.createElement('tr');
        row.classList.add("table-item");
        row.innerHTML = `
        <td>${Producto.codProducto}</td>
        <td>${Producto.nombreProducto}</td>
        <td>${Producto.marcaProducto}</td>
        <td>${Producto.altoCaja}</td>
        <td>${Producto.anchoCaja}</td>
        <td>${Producto.profundidadCaja}</td>
        <td>${Producto.precioUnitario}</td>
        <td>${Producto.stockMinimo}</td>
        <td>${Producto.stockTotal}</td>
    `
    respuesta.appendChild(row);
    });
}

function showError(error) {
    var respuesta = document.getElementById('formResponse');
    respuesta.innerHTML = `
        <p> Error!</p>
        <br>
        <p>${error.message}</p>
    `;
}