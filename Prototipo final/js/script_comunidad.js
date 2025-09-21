document.addEventListener("DOMContentLoaded", () => {
  const form = document.getElementById("formPost");
  const textarea = document.getElementById("nuevoPost");
  const postsContainer = document.getElementById("postsContainer");

  // Posts iniciales simulados
  const posts = [
    { autor: "María", contenido: "¡Acabo de plantar 10 árboles en el parque local! 🌱🌳" },
    { autor: "Carlos", contenido: "¿Alguien quiere hacer una caminata ecológica este sábado?" },
    { autor: "Laura", contenido: "Les comparto este video sobre la fauna local." }
  ];

  function renderPosts() {
    postsContainer.innerHTML = "";
    posts.forEach((post) => {
      const div = document.createElement("div");
      div.classList.add("post");
      div.innerHTML = `
        <div class="post-header">${post.autor}</div>
        <div class="post-content">${post.contenido}</div>
      `;
      postsContainer.appendChild(div);
    });
  }

  form.addEventListener("submit", (e) => {
    e.preventDefault();
    const texto = textarea.value.trim();
    if (texto.length === 0) return alert("Por favor escribe algo para publicar.");

    // Simulamos que el autor es "Usuario Actual"
    posts.unshift({ autor: "Usuario Actual", contenido: texto });
    renderPosts();
    textarea.value = "";
  });

  renderPosts();

  // Menú hamburguesa (reutiliza el mismo código del index)
  const hamburger = document.getElementById("hamburger");
  const navLinks = document.getElementById("navbar-links");

  hamburger.addEventListener("click", () => {
    navLinks.classList.toggle("active");
  });
});
