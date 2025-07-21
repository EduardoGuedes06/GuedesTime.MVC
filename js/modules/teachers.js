import { appData } from '../state.js';
import { showToast, openModal, closeModal, renderPagination, setupMultiSelect } from '../ui.js';

const teacherModal = document.getElementById('teacher-modal');
const teacherForm = document.getElementById('teacher-form');
const addTeacherBtn = document.getElementById('add-teacher-btn');
const cancelTeacherBtn = document.getElementById('cancel-teacher-btn');
const teacherIdInput = document.getElementById('teacher-id');
const teacherFullNameInput = document.getElementById('teacher-full-name');
const teacherEmailInput = document.getElementById('teacher-email');
const teacherDocumentUpload = document.getElementById('teacher-document-upload');
const teacherModalTitle = document.getElementById('teacher-modal-title');
const teachersList = document.getElementById('teachers-list');
const teacherSearchInput = document.getElementById('teacher-search');
const teacherFilterSubjectSelect = document.getElementById('teacher-filter-subject');

let currentTeacherSubjectsMultiSelect = null;

function editTeacher(e) {
    const teacherId = e.currentTarget.dataset.id;
    const teacher = appData.teachers.find(t => t.id === teacherId);
    openTeacherModal(teacher);
}

function deleteTeacher(e) {
    showToast('Ação de exclusão desabilitada no protótipo.', 'info');
}

export function renderTeachers(page = 1, searchQuery = '', subjectFilter = '') {
    if (!teachersList) return;

    let filteredTeachers = appData.teachers.filter(teacher => {
        const matchesSearch = teacher.fullName.toLowerCase().includes(searchQuery.toLowerCase()) || teacher.email.toLowerCase().includes(searchQuery.toLowerCase());
        const matchesSubject = !subjectFilter || teacher.subjectIds.includes(subjectFilter);
        return matchesSearch && matchesSubject;
    });

    const itemsPerPage = 6;
    const totalPages = Math.ceil(filteredTeachers.length / itemsPerPage);
    const startIndex = (page - 1) * itemsPerPage;
    const paginatedTeachers = filteredTeachers.slice(startIndex, startIndex + itemsPerPage);

    teachersList.innerHTML = '';
    if (paginatedTeachers.length === 0) {
        teachersList.innerHTML = '<p class="placeholder-text">Nenhum professor encontrado com os critérios de busca/filtro.</p>';
    } else {
        paginatedTeachers.forEach(teacher => {
            const card = document.createElement('div');
            card.className = 'entity-card';
            card.innerHTML = `
                <h3>${teacher.fullName}</h3>
                <p>Email: ${teacher.email}</p>
                <div class="badges-container">
                    ${teacher.subjectIds.map(subId => {
                const subject = appData.subjects.find(s => s.id === subId);
                return subject ? `<span class="badge teacher-badge">${subject.name}</span>` : '';
            }).join('')}
                </div>
                <div class="actions">
                    <button class="btn primary-btn edit-teacher-btn" data-id="${teacher.id}">Editar</button>
                    <button class="btn danger-btn delete-teacher-btn" data-id="${teacher.id}">Excluir</button>
                </div>
            `;
            teachersList.appendChild(card);
        });
    }

    document.querySelectorAll('.edit-teacher-btn').forEach(btn => btn.addEventListener('click', editTeacher));
    document.querySelectorAll('.delete-teacher-btn').forEach(btn => btn.addEventListener('click', deleteTeacher));
    renderPagination('teachers-pagination', totalPages, page, (p) => renderTeachers(p, searchQuery, subjectFilter));
}


function openTeacherModal(teacher = null) {
    teacherForm.reset();
    teacherIdInput.value = '';
    teacherModalTitle.textContent = 'Adicionar Novo Professor';
    let selectedSubjectIds = [];

    if (teacher) {
        teacherModalTitle.textContent = 'Editar Professor';
        teacherIdInput.value = teacher.id;
        teacherFullNameInput.value = teacher.fullName;
        teacherEmailInput.value = teacher.email;
        selectedSubjectIds = [...teacher.subjectIds];
    } else {
        selectedSubjectIds = [];
    }

    currentTeacherSubjectsMultiSelect = setupMultiSelect(
        'teacher-subjects-multi-select',
        'teacher-selected-subjects-container',
        'teacher-subject-search-input',
        'teacher-available-subjects-list',
        appData.subjects, 'id', 'name', selectedSubjectIds
    );
    currentTeacherSubjectsMultiSelect.setSelectedIds(selectedSubjectIds);
    openModal(teacherModal);
}

function saveTeacher(e) {
    e.preventDefault();
    showToast('Ação de salvar desabilitada no protótipo.', 'info');
}

function setupTeacherFilters() {
    teacherFilterSubjectSelect.innerHTML = '<option value="">Filtrar por Disciplina</option>';
    appData.subjects.forEach(sub => {
        const option = document.createElement('option');
        option.value = sub.id;
        option.textContent = sub.name;
        teacherFilterSubjectSelect.appendChild(option);
    });
}

export function initTeachers() {
    if (document.getElementById('teachers')) {
        addTeacherBtn.addEventListener('click', () => openTeacherModal());
        cancelTeacherBtn.addEventListener('click', () => closeModal(teacherModal));
        teacherForm.addEventListener('submit', saveTeacher);

        teacherSearchInput.addEventListener('input', (e) => renderTeachers(1, e.target.value, teacherFilterSubjectSelect.value));
        teacherFilterSubjectSelect.addEventListener('change', (e) => renderTeachers(1, teacherSearchInput.value, e.target.value));
        
        setupTeacherFilters();
        renderTeachers();
    }
}