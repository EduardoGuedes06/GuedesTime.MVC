function initGlobalScripts() {
    import('./theme.js').then(themeModule => themeModule.initTheme());

    import('./modules/profile.js').then(profileModule => {
        if (document.getElementById('sidebar-user-name')) {
            profileModule.renderSidebarProfile();
        }
    });

    import('./utils.js').then(utilsModule => {
        utilsModule.initBuscaCep();
        utilsModule.initValidacaoCnpj();
    });

    const sidebar = document.getElementById('sidebar');
    const sidebarToggle = document.getElementById('sidebar-toggle');
    const mainContent = document.querySelector('.main-content');

    function handleSidebarDisplay() {
        if (!sidebar || !mainContent) return;
        if (window.innerWidth >= 768) {
            sidebar.classList.add('active');
            mainContent.classList.add('expanded');
        } else {
            sidebar.classList.remove('active');
            mainContent.classList.remove('expanded');
        }
    }

    if (sidebarToggle) {
        sidebarToggle.addEventListener('click', () => {
            sidebar.classList.toggle('active');
            mainContent.classList.toggle('expanded');
        });
    }

    window.addEventListener('resize', handleSidebarDisplay);
    handleSidebarDisplay();
}

function loadPageSpecificScripts() {
    const pageModules = {
        'dashboard': { path: './modules/dashboard.js', init: 'initDashboard' },
        'teachers': { path: './modules/teachers.js', init: 'initTeachers' },
        'subjects': { path: './modules/subjects.js', init: 'initSubjects' },
        'classes': { path: './modules/classes.js', init: 'initClasses' },
        'series': { path: './modules/series.js', init: 'initSeries' },
        'classrooms': { path: './modules/classrooms.js', init: 'initClassrooms' },
        'time-slots': { path: './modules/time-slots.js', init: 'initTimeSlots' },
        'holidays': { path: './modules/holidays.js', init: 'initHolidays' },
        'lesson-plans': { path: './modules/lesson-plans.js', init: 'initLessonPlans' },
        'profile': { path: './modules/profile.js', init: 'initProfile' },
        'configuracoes': { path: './modules/settings.js', init: 'initSettings' }
    };

    for (const id in pageModules) {
        if (document.getElementById(id)) {
            const moduleInfo = pageModules[id];
            console.log(`Página ${id} detectada. Carregando ${moduleInfo.path}...`);
            import(moduleInfo.path).then(module => {
                module[moduleInfo.init]();
            }).catch(err => console.error(`Falha ao carregar o módulo para ${id}:`, err));
        }
    }
}

document.addEventListener('DOMContentLoaded', () => {
    initGlobalScripts();
    loadPageSpecificScripts();

    import('./ui.js').then(uiModule => {
        const successMessage = document.getElementById('tempdata-success-message')?.value;
        const errorMessage = document.getElementById('tempdata-error-message')?.value;
        const infoMessage = document.getElementById('tempdata-info-message')?.value;
        const warningMessage = document.getElementById('tempdata-warning-message')?.value;

        if (successMessage) uiModule.showToast(successMessage, 'success');
        if (errorMessage) uiModule.showToast(errorMessage, 'error');
        if (infoMessage) uiModule.showToast(infoMessage, 'info');
        if (warningMessage) uiModule.showToast(warningMessage, 'warning');
    });
});