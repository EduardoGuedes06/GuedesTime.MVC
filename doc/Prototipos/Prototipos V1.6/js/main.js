import { appData } from './state.js';
import { showToast, openModal, closeModal } from './ui.js';
import { initTheme, applyTheme } from './theme.js';
import { initInstituicao, renderInstituicao } from './modules/instituicao.js';
import { initTeachers, renderTeachers } from './modules/teachers.js';
import { initSubjects, renderSubjects } from './modules/subjects.js';
import { initClasses, renderClasses } from './modules/classes.js';
import { initSeries, renderSeries } from './modules/series.js';
import { initClassrooms, renderClassrooms } from './modules/classrooms.js';
import { initTimeSlots, renderTimeSlots } from './modules/timeSlots.js';
import { initHolidays, renderHolidays } from './modules/holidays.js';
import { initLessonPlans, renderLessonPlans } from './modules/lessonPlans.js';
import { initProfile, renderProfile, renderSidebarProfile } from './modules/profile.js';
import { initSettings, updateSidebarVisibility } from './modules/settings.js';

document.addEventListener('DOMContentLoaded', () => {
    const sidebar = document.getElementById('sidebar');
    const sidebarToggle = document.getElementById('sidebar-toggle');
    const navLinks = document.querySelectorAll('.sidebar-nav a');
    const contentSections = document.querySelectorAll('.content-section');
    const mainContent = document.querySelector('.main-content');
    const userProfileArea = document.querySelector('.sidebar-user-profile');

    let currentSectionId = 'dashboard';

    function updateCounts() {


    }

    function showSection(sectionId) {
        contentSections.forEach(section => {
            section.classList.add('hidden');
            section.classList.remove('active');
        });
        const targetSection = document.getElementById(sectionId);
        if (targetSection) {
            targetSection.classList.remove('hidden');
            targetSection.classList.add('active');
            currentSectionId = sectionId;
        }

        navLinks.forEach(link => {
            link.classList.toggle('active', link.dataset.section === sectionId);
        });

        switch (sectionId) {
            case 'instituicao': renderInstituicao(); break;
            case 'teachers': renderTeachers(); break;
            case 'subjects': renderSubjects(); break;
            case 'series': renderSeries(); break;
            case 'classes': renderClasses(); break;
            case 'classrooms': renderClassrooms(); break;
            case 'time-slots': renderTimeSlots(); break;
            case 'holidays': renderHolidays(); break;
            case 'lesson-plans': renderLessonPlans(); break;
            case 'profile': renderProfile(); break;
        }

        if (window.innerWidth < 768) {
            sidebar.classList.remove('active');
        }
        updateCounts();
    }

    sidebarToggle.addEventListener('click', () => {
        sidebar.classList.toggle('active');
        mainContent.classList.toggle('expanded');
    });

    navLinks.forEach(link => {
        link.addEventListener('click', (e) => {
            e.preventDefault();
            const sectionId = e.currentTarget.dataset.section;
            showSection(sectionId);
            window.location.hash = sectionId;
        });
    });

    document.querySelectorAll('.quick-access-btn').forEach(button => {
        button.addEventListener('click', (e) => {
            const sectionId = e.currentTarget.dataset.section;
            if (sectionId && sectionId !== currentSectionId) {
                showSection(sectionId);
            }
        });
    });

    userProfileArea.addEventListener('click', () => {
        showSection('profile');
    });

    // ==========================================================
    // NOVO CÓDIGO INTELIGENTE (Controle do Pop-up de Confirmação)
    // ==========================================================
    document.addEventListener('click', function (e) {
        // Delegação de eventos para os botões de desativar
        if (e.target.matches('.btn-deactivate')) {
            const container = e.target.closest('.inline-confirm-container');
            if (!container) return;

            const popup = container.querySelector('.inline-confirm-popup');

            // --- LÓGICA DE DETECÇÃO DE ESPAÇO ---
            // Primeiro, removemos as classes de direção para recalcular
            popup.classList.remove('popup-left', 'popup-right', 'popup-top', 'popup-bottom');

            const triggerRect = e.target.getBoundingClientRect();
            const popupWidth = popup.offsetWidth; // Mede a largura real do pop-up

            // Verifica se há espaço à esquerda. Se não houver, muda para a direita.
            if (triggerRect.left < popupWidth + 20) { // +20 de margem de segurança
                popup.classList.add('popup-right'); // Força para a direita
            } else {
                popup.classList.add('popup-left'); // Usa a posição padrão (esquerda)
            }
            // --- FIM DA LÓGICA DE DETECÇÃO ---

            // Fecha outros pop-ups abertos antes de abrir o novo
            document.querySelectorAll('.inline-confirm-popup.active').forEach(p => {
                if (p !== popup) p.classList.remove('active');
            });

            // Mostra ou esconde o pop-up atual
            popup.classList.toggle('active');
        }
        // Se clicou em "Não" ou "Cancelar"
        else if (e.target.matches('.btn-confirm-no')) {
            e.target.closest('.inline-confirm-popup').classList.remove('active');
        }
        // Se clicou em "Sim, desativar"
        else if (e.target.matches('.btn-confirm-yes')) {
            const popup = e.target.closest('.inline-confirm-popup');
            const card = popup.closest('.entity-card');

            // Simula a desativação
            if (card) {
                card.style.opacity = '0.5';
                card.style.pointerEvents = 'none';
            }

            popup.classList.remove('active');
        }
        // Se clicou fora de qualquer pop-up container, fecha todos
        else if (!e.target.closest('.inline-confirm-container')) {
            document.querySelectorAll('.inline-confirm-popup.active').forEach(popup => {
                popup.classList.remove('active');
            });
        }
    });

    document.querySelectorAll('.modal-overlay').forEach(modal => {
        modal.addEventListener('click', (e) => {
            if (e.target === modal) {
                closeModal(modal);
            }
        });
    });

    window.addEventListener('resize', () => {
        if (window.innerWidth >= 768) {
            sidebar.classList.add('active');
            mainContent.classList.add('expanded');
        } else {
            sidebar.classList.remove('active');
            mainContent.classList.remove('expanded');
        }
    });

    initTheme();
    initTeachers();
    initSubjects();
    initClasses();
    initSeries();
    initClassrooms();
    initTimeSlots();
    initHolidays();
    initLessonPlans();
    initProfile();
    initSettings();

    if (window.innerWidth >= 768) {
        sidebar.classList.add('active');
        mainContent.classList.add('expanded');
    }

    const initialHash = window.location.hash.substring(1);
    if (initialHash && document.getElementById(initialHash)) {
        showSection(initialHash);
    } else {
        showSection('dashboard');
    }

    renderSidebarProfile();
    updateCounts();
});