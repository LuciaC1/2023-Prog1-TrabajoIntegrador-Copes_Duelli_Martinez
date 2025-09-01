# 2023-Prog1-TrabajoIntegrador-Copes_Duelli_Martinez
# 📦 Sistema de Gestión de Compras y Envíos  

Este proyecto consiste en una **Web API REST** y un conjunto de páginas **HTML** para la gestión de productos, clientes, compras y envíos de una empresa localizada en Rafaela, con cobertura a todo el país.  

---

## 🚀 Funcionalidades principales  

### 🔹 Gestión de Productos  
- Alta de productos (**POST**).  
- Actualización de stock (**PUT `/producto/:id_producto`**).  
- Datos:  
  - Código único (ID autogenerado).  
  - Nombre y marca.  
  - Dimensiones (alto, ancho, profundidad).  
  - Precio unitario.  
  - Stock mínimo y cantidad en stock.  

---

### 🔹 Gestión de Clientes  
- ABM completo: alta, baja lógica, modificación y consulta.  
- Validación de todos los campos obligatorios:  
  - DNI, nombre, apellido, email, teléfono.  
  - Latitud y longitud geográfica.  
  - Fecha de nacimiento.  
- Datos persistidos en archivo JSON.  

---

### 🔹 Gestión de Camionetas  
- Se leen desde archivo (sin ABM).  
- Información por camioneta:  
  - Patente.  
  - Capacidad de carga (cm³).  
  - Distancia máxima de recorrido (km).  

---

### 🔹 Gestión de Compras  
- Alta de compras con validación de stock disponible.  
- Datos registrados:  
  - Código de compra.  
  - Producto y cliente asociados.  
  - Fecha de compra y de entrega solicitada.  
  - Cantidad y estado.  
  - Punto de destino (lat/lon del cliente).  
- Cálculos automáticos:  
  - Monto total con **IVA 21%**.  
  - Descuento del **25%** en compras de más de 4 unidades.  
- Estados de compra:  
  - `OPEN` → compra creada.  
  - `READY_TO_DISPATCH` → lista para despacho.  

---

### 🔹 Gestión de Viajes  
- Creación de viajes (**POST**) recibiendo rango de fechas:  
  - **Fecha desde** ≥ actual.  
  - **Fecha hasta** ≤ 7 días posteriores.  
- Validaciones:  
  - No debe existir otro viaje en el mismo rango.  
  - La distancia al cliente ≤ alcance de la camioneta.  
  - La capacidad de carga de la camioneta no debe superarse.  
- Datos de cada viaje:  
  - Código único.  
  - Camioneta asignada.  
  - Rango de fechas de entrega.  
  - Porcentaje de ocupación de carga.  
  - Listado de compras asignadas.  
- Cambios de estado automáticos:  
  - Compras asignadas → `READY_TO_DISPATCH`.  
  - Compras no asignadas → reprogramadas (+2 semanas).  

---

### 🔹 Frontend (HTML)  
- Página para listar productos con stock bajo.  
- Página para actualizar stock de productos.  
- Formulario para registrar nuevas compras.  
- Página para ejecutar el proceso de asignación de viajes y mostrar resultados.  

---

## ⚙️ Reglas Generales  
- Persistencia en **archivos JSON**.  
- Todas las entidades incluyen:  
  - Fecha de creación.  
  - Fecha de actualización.  
  - Fecha de eliminación (lógica).  
- Se deben implementar al menos **8 tests en la API REST**, incluyendo uno para la **asignación de camiones en viajes**.  

---

Resultados de los test en API REST: https://docs.google.com/document/d/1pfD2hZsBde_WQll1wUDRBDAargFjhsQEkqOBz63nGFk/edit?usp=sharing
