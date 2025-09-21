document.addEventListener("DOMContentLoaded", () => {
  // Datos simulados
  const historialRutas = [
    "Humedal Santa María",
    "Centro Histórico",
    "Parque Natural El Cedral",
  ];

  const logros = [
    { titulo: "Explorador Ecológico", icono: "🌿" },
    { titulo: "Caminante Cultural", icono: "🏛️" },
    { titulo: "Amigo del Planeta", icono: "🌍" },
  ];

  const historialList = document.getElementById("historialRutas");
  historialRutas.forEach(ruta => {
    const li = document.createElement("li");
    li.textContent = ruta;
    historialList.appendChild(li);
  });

  const logrosContainer = document.getElementById("logrosContainer");
  logros.forEach(logro => {
    const div = document.createElement("div");
    div.classList.add("logro-card");
    div.innerHTML = `
      <div class="logro-icon">${logro.icono}</div>
      <div class="logro-titulo">${logro.titulo}</div>
    `;
    logrosContainer.appendChild(div);
  });

  // Botón editar perfil (solo alerta por ahora)
  document.getElementById("editarPerfilBtn").addEventListener("click", () => {
    alert("Funcionalidad de edición aún no implementada.");
  });

  // Menú hamburguesa
  const hamburger = document.getElementById("hamburger");
  const navLinks = document.getElementById("navbar-links");

  hamburger.addEventListener("click", () => {
    navLinks.classList.toggle("active");
  });
});
