document.addEventListener("DOMContentLoaded", function (event) {
    console.info("Loaded")
});

document.getElementById('formAsignarViaje').addEventListener('submit', function (event) {
    event.preventDefault();
    const fechaDesde = document.getElementById('fechaDesde').value;
    const fechaHasta = document.getElementById('fechaHasta').value;
    //const host = 'http://localhost:5024';
    const host = 'https://v7m6wdlx-7174.brs.devtunnels.ms'
    let url = new URL(`${host}/Viaje/${fechaDesde}&${fechaHasta}`);
    fetch(url, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        }
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
                    <h3>Viaje agendado</h3>
                    <p>Código de viaje: ${data.codigoUnicoViaje}</p>
                    <p>Patente camioneta: ${data.patente}</p>
                    <p>Entregas desde: ${data.fechaEntregasDesde.slice(0,10)}</p>
                    <p>Entregas hasta: ${data.fechaEntregasHasta.slice(0,10)}</p>
                    <p>Porcentaje ocupación: ${Math.floor(data.porcentajeOcupacionCarga * 10)/10}%</p>
                    <p>Código de compras a enviar:</p>
                        ${data.listadoCompras.map(compra => `<p>${compra}</p>`).join('')}
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
