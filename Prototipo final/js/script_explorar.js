// Datos simulados de rutas
const rutas = [
  {
    nombre: "Humedal Santa María",
    tipo: "ecologica",
    descripcion: "Explora este ecosistema urbano lleno de vida silvestre.",
  },
  {
    nombre: "Centro Histórico",
    tipo: "cultural",
    descripcion: "Recorre calles coloniales y descubre la historia local.",
  },
  {
    nombre: "Ruta Gastronómica La Candelaria",
    tipo: "gastronomica",
    descripcion: "Sabores autóctonos y cocina fusión en el corazón cultural.",
  },
  {
    nombre: "Parque Natural El Cedral",
    tipo: "ecologica",
    descripcion: "Senderismo, aire puro y biodiversidad en su máxima expresión.",
  },
  {
    nombre: "Museo de la Memoria",
    tipo: "cultural",
    descripcion: "Un viaje por las historias y voces de la comunidad.",
  },
];

function mostrarRutas(filtro = "todas") {
  const container = document.getElementById("rutas-container");
  container.innerHTML = "";

  const filtradas = rutas.filter(ruta => 
    filtro === "todas" || ruta.tipo === filtro
  );

  if (filtradas.length === 0) {
    container.innerHTML = "<p>No hay rutas disponibles para este filtro.</p>";
    return;
  }

  filtradas.forEach(ruta => {
    const div = document.createElement("div");
    div.className = "ruta-item";

    div.innerHTML = `
      <h3>${ruta.nombre}</h3>
      <span class="tag">${capitalizar(ruta.tipo)}</span>
      <p>${ruta.descripcion}</p>
      <button onclick="verRuta('${ruta.nombre}', '${ruta.descripcion}')">Ver más</button>
    `;

    container.appendChild(div);
  });
}

function verRuta(nombre, descripcion) {
  // Actualizar el contenido del modal
  document.getElementById("modal-titulo").textContent = nombre;
  document.getElementById("modal-descripcion").textContent = descripcion;

  // Mostrar el modal
  const modal = document.getElementById("modal");
  modal.style.display = "block";
}

function cerrarModal() {
  const modal = document.getElementById("modal");
  modal.style.display = "none";
}


function filtrarRutas() {
  const tipo = document.getElementById("tipo").value;
  mostrarRutas(tipo);
}

function capitalizar(texto) {
  return texto.charAt(0).toUpperCase() + texto.slice(1);
}

// Menú hamburguesa (ya estaba)
document.addEventListener("DOMContentLoaded", function () {
  
  // Mostrar rutas al cargar
  if (document.getElementById("rutas-container")) {
    mostrarRutas();
  }
});
