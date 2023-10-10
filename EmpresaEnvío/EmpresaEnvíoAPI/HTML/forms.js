document.getElementById('FormCrearReq').addEventListener('submit', function(event){
    event.preventDefault();
    document.getElementById('formResponse').style.display = "none";
    document.getElementById('tablaReqPost').innerHTML = "";

    const titulo = document.getElementById('Titulo').value;
    const descripcion = document.getElementById('Descripcion').value;

    const prioridad = prioridadEnum[document.getElementById('Prioridad').value];
    const fechaVencimiento = document.getElementById('FechaVencimiento').value;

    const Requerimiento = {
        id: 0,
        titulo: titulo,
        descripcion: descripcion,
        prioridad: prioridad,
        fechaCreacion: fechaVencimiento,
        fechaVencimiento: fechaVencimiento,
        estado: 0
    };

    fetch('http://localhost:5232/Requerimiento', {
        method: 'POST',
        headers:{
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(Requerimiento)
    })
    .then(response => response.json())
    .then(data => {
        console.log(data);
        showData(data);
        alert('Procesamiento correcto');
    })
    .catch(error => {
        console.log(error);
        alert('Error al procesar', error);
    });
});

function showData(data) {
    var fila = document.createElement("tr");
                fila.innerHTML = `
                    <td>${data.id}</td>
                    <td>${data.titulo}</td>
                    <td>${data.descripcion}</td>
                    <td>${Object.keys(prioridadEnum)[data.prioridad-1]}</td>
                    <td>${Object.keys(estadoEnum)[data.estado-1]}</td>
                    <td>${data.fechaVencimiento.slice(0,10)}</td>
                    <td>${data.fechaCreacion.slice(0,10)}</td>
                `;
    document.getElementById('tablaReqPost').appendChild(fila);
    document.getElementById('formResponse').style.display = "block";
}

var prioridadEnum = {
    "Mínima": 1,
    "Baja": 2,
    "Media": 3,
    "Alta": 4,
    "Máxima": 5
}

var estadoEnum = {
    "Pendiente": 1,
    "En progreso": 2,
    "Completado": 3
}