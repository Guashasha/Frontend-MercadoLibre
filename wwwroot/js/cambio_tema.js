document.addEventListener('DOMContentLoaded', (event) => {
    const htmlElement = document.documentElement;
    const switchElement = document.getElementById('darkModeSwitch');
    const modoLuz = document.querySelector('.modo-luz');

    // Establece como el modo light como el predeterminado
    const currentTheme = localStorage.getItem('bsTheme') || 'light';
    htmlElement.setAttribute('data-bs-theme', currentTheme);
    switchElement.checked = currentTheme === 'dark';

    switchElement.addEventListener('change', function () {
        if (this.checked) {
            htmlElement.setAttribute('data-bs-theme', 'dark');
            localStorage.setItem('bsTheme', 'dark');
            modoLuz.classList.remove('bi-brightness-high');
            modoLuz.classList.add('bi-moon-stars');
        } else {
            htmlElement.setAttribute('data-bs-theme', 'light');
            localStorage.setItem('bsTheme', 'light');
            modoLuz.classList.remove('bi-moon-stars');
            modoLuz.classList.add('bi-brightness-high');
        }
    });
});
