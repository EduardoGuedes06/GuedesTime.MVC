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