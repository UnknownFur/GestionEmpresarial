document.addEventListener("DOMContentLoaded", () => {
  // Datos simulados
  const ecoTokens = 2450;

  const retosActivos = [
    "Camina 5 km esta semana 🌿",
    "Participa en la limpieza del parque 🧹",
    "Siembra un árbol virtual 🌱",
  ];

  const retosCompletados = [
    "Visitaste el Humedal Santa María ✅",
    "Completaste la ruta cultural centro histórico ✅",
  ];

  const insignias = [
    { titulo: "Explorador Ecológico", icono: "🌿" },
    { titulo: "Caminante Cultural", icono: "🏛️" },
    { titulo: "Amigo del Planeta", icono: "🌍" },
  ];

  // Mostrar tokens
  document.querySelector(".tokens-count").textContent = `🌱 ${ecoTokens}`;

  // Datos de juegos
const juegos = [
  { titulo: "Sembrar Árbol Virtual", icono: "🌱", descripcion: "Ayuda a reforestar virtualmente." },
  { titulo: "Trivia Ecológica", icono: "❓", descripcion: "Pon a prueba tus conocimientos ambientales." },
  { titulo: "Puzzle Cultural", icono: "🧩", descripcion: "Arma piezas de lugares históricos." },
];

// Mostrar juegos
const juegosContainer = document.getElementById("juegosContainer");

juegos.forEach(juego => {
  const div = document.createElement("div");
  div.classList.add("juego-card");
  div.title = juego.descripcion;
  div.innerHTML = `
    <div class="juego-icon">${juego.icono}</div>
    <div class="juego-titulo">${juego.titulo}</div>
  `;

  div.addEventListener("click", () => {
    alert(`Entrando al juego: ${juego.titulo}\n\n${juego.descripcion}\n\n(En construcción...)`);
  });

  juegosContainer.appendChild(div);
});


  // Listar retos activos
  const activosList = document.getElementById("retosActivosList");
  retosActivos.forEach(reto => {
    const li = document.createElement("li");
    li.textContent = reto;
    activosList.appendChild(li);
  });

  // Listar retos completados
  const completadosList = document.getElementById("retosCompletadosList");
  retosCompletados.forEach(reto => {
    const li = document.createElement("li");
    li.textContent = reto;
    completadosList.appendChild(li);
  });

  // Mostrar insignias
  const insigniasContainer = document.getElementById("insigniasContainer");
  insignias.forEach(insignia => {
    const div = document.createElement("div");
    div.classList.add("logro-card");
    div.innerHTML = `
      <div class="logro-icon">${insignia.icono}</div>
      <div class="logro-titulo">${insignia.titulo}</div>
    `;
    insigniasContainer.appendChild(div);
  });

  // Botón reclamar recompensas
  document.getElementById("reclamarBtn").addEventListener("click", () => {
    alert("¡Recompensas reclamadas! Gracias por contribuir al turismo sostenible 🌍");
  });

  // Menú hamburguesa
  const hamburger = document.getElementById("hamburger");
  const navLinks = document.getElementById("navbar-links");

  hamburger.addEventListener("click", () => {
    navLinks.classList.toggle("active");
  });
});
