// Simulación de navegación
function irA(vista) {
  alert("Ir a: " + vista);
  window.location.href = '../html/' + vista + '.html';
}
const rutasRecomendadas = [
  {
    nombre: "🌳 Humedal Santa María",
    descripcion: "Una caminata entre naturaleza y cultura viva.",
  },
  {
    nombre: "🏛️ Centro Histórico",
    descripcion: "Recorre calles coloniales y descubre la historia local.",
  },
  {
    nombre: "🥘 Ruta Gastronómica La Candelaria",
    descripcion: "Sabores autóctonos y cocina fusión en el corazón cultural.",
  }
];

let indiceActual = 0;
const contenedorCarrusel = document.getElementById("carrusel-container");

function crearTarjetas() {
  rutasRecomendadas.forEach((ruta, index) => {
    const card = document.createElement("div");
    card.classList.add("carrusel-card");
    if (index === 0) card.classList.add("active");
    
    card.innerHTML = `
      <h3>${ruta.nombre}</h3>
      <p>${ruta.descripcion}</p>
      <button onclick="verRuta()">Ver ruta</button>
    `;

    contenedorCarrusel.appendChild(card);
  });
}

function mostrarSiguiente() {
  const tarjetas = document.querySelectorAll(".carrusel-card");
  tarjetas[indiceActual].classList.remove("active");
  tarjetas[indiceActual].classList.add("exit-left");
  
  indiceActual = (indiceActual + 1) % tarjetas.length;

  tarjetas.forEach((t, i) => {
    if (i !== indiceActual) {
      t.classList.remove("active");
      t.classList.remove("exit-left");
    }
  });

  tarjetas[indiceActual].classList.add("active");
}

function mostrarAnterior() {
  const tarjetas = document.querySelectorAll(".carrusel-card");
  tarjetas[indiceActual].classList.remove("active");
  tarjetas[indiceActual].classList.add("exit-left");
  
  indiceActual = (indiceActual - 1 + tarjetas.length) % tarjetas.length;

  tarjetas.forEach((t, i) => {
    if (i !== indiceActual) {
      t.classList.remove("active");
      t.classList.remove("exit-left");
    }
  });

  tarjetas[indiceActual].classList.add("active");
}

document.addEventListener("DOMContentLoaded", () => {
  crearTarjetas();

  setInterval(mostrarSiguiente, 4000);

  // Control manual de botones
  const prevBtn = document.getElementById("prevBtn");
  const nextBtn = document.getElementById("nextBtn");

  if (prevBtn && nextBtn) {
    prevBtn.addEventListener("click", () => {
      mostrarAnterior();
      resetInterval();
    });

    nextBtn.addEventListener("click", () => {
      mostrarSiguiente();
      resetInterval();
    });
  }
});

let intervaloCarrusel = setInterval(mostrarSiguiente, 4000);

function resetInterval() {
  clearInterval(intervaloCarrusel);
  intervaloCarrusel = setInterval(mostrarSiguiente, 4000);
}


function verRuta() {
  alert("Ver detalles de la ruta destacada");
  window.location.href = '../html/explorar.html';
}

// Menú hamburguesa
document.addEventListener("DOMContentLoaded", function () {
  const hamburger = document.getElementById("hamburger");
  const navLinks = document.getElementById("navbar-links");

  hamburger.addEventListener("click", () => {
    navLinks.classList.toggle("active");
  });
});
