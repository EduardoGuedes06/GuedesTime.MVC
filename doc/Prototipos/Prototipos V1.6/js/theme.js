import { appData, THEME_PRESETS } from './state.js';

const themeCards = document.querySelectorAll('.theme-card');
const customColorPickers = document.querySelectorAll('.color-picker-grid input[type="color"]');

export function applyTheme(themeColors) {
    for (const [variable, color] of Object.entries(themeColors)) {
        document.documentElement.style.setProperty(variable, color);
    }
}

export function initTheme() {
    themeCards.forEach(card => {
        card.addEventListener('click', () => {
            const themeName = card.dataset.themeName;
            const themeColors = THEME_PRESETS[themeName];

            applyTheme(themeColors);

            appData.settings.theme.name = themeName;
            appData.settings.theme.colors = { ...themeColors };

            themeCards.forEach(c => c.classList.remove('active-theme'));
            card.classList.add('active-theme');

            for (const [variable, color] of Object.entries(themeColors)) {
                const picker = document.querySelector(`[data-variable="${variable}"]`);
                if (picker) {
                    picker.value = color;
                }
            }
        });
    });

    customColorPickers.forEach(picker => {
        picker.addEventListener('input', () => {
            const variable = picker.dataset.variable;
            const color = picker.value;

            document.documentElement.style.setProperty(variable, color);

            appData.settings.theme.name = 'custom';
            appData.settings.theme.colors[variable] = color;

            themeCards.forEach(card => card.classList.remove('active-theme'));
        });
    });

    applyTheme(appData.settings.theme.colors);
}