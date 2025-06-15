import { appData } from '../state.js';
import { showToast, openModal, closeModal, renderPagination, setupMultiSelect } from '../ui.js';

const subjectModal = document.getElementById('subject-modal');
const subjectForm = document.getElementById('subject-form');
const addSubjectBtn = document.getElementById('add-subject-btn');
const cancelSubjectBtn = document.getElementById('cancel-subject-btn');
const subjectIdInput = document.getElementById('subject-id');
const subjectNameInput = document.getElementById('subject-name');
const subjectCodeInput = document.getElementById('subject-code');
const subjectWorkloadHoursInput = document.getElementById('subject-workload-hours');
const subjectModalTitle = document.getElementById('subject-modal-title');
const subjectsList = document.getElementById('subjects-list');
const subjectSearchInput = document.getElementById('subject-search');
const subjectFilterTeacherSelect = document.getElementById('subject-filter-teacher');
const subjectFilterSeriesSelect = document.getElementById('subject-filter-series');

let currentSubjectSeriesMultiSelect = null;
let currentSubjectTeachersMultiSelect = null;

function editSubject(e) {
    const subjectId = e.currentTarget.dataset.id;
    const subject = appData.subjects.find(s => s.id === subjectId);
    openSubjectModal(subject);
}

function deleteSubject(e) {
    showToast('Ação de exclusão desabilitada no protótipo.', 'info');
}

export function renderSubjects(page = 1, searchQuery = '', teacherFilter = '', seriesFilter = '') {
    if (!subjectsList) return;

    let filteredSubjects = appData.subjects.filter(subject => {
        const matchesSearch = subject.name.toLowerCase().includes(searchQuery.toLowerCase()) || (subject.code && subject.code.toLowerCase().includes(searchQuery.toLowerCase()));
        const matchesTeacher = !teacherFilter || subject.teacherIds.includes(teacherFilter);
        const matchesSeries = !seriesFilter || subject.seriesIds.includes(seriesFilter);
        return matchesSearch && matchesTeacher && matchesSeries;
    });

    const itemsPerPage = 6;
    const totalPages = Math.ceil(filteredSubjects.length / itemsPerPage);
    const startIndex = (page - 1) * itemsPerPage;
    const paginatedSubjects = filteredSubjects.slice(startIndex, startIndex + itemsPerPage);

    subjectsList.innerHTML = '';
    if (paginatedSubjects.length === 0) {
        subjectsList.innerHTML = '<p class="placeholder-text">Nenhuma disciplina encontrada com os critérios de busca/filtro.</p>';
    } else {
        paginatedSubjects.forEach(subject => {
            const card = document.createElement('div');
            card.className = 'entity-card';
            card.innerHTML = `
                <h3>${subject.name}</h3>
                <p>Código: ${subject.code || 'N/A'}</p>
                <p>Carga Horária: ${subject.workloadHours} horas</p>
                <div class="info-row">
                    <span>Professores:</span>
                    <div class="badges-container">
                        ${subject.teacherIds.map(tid => {
                const teacher = appData.teachers.find(t => t.id === tid);
                return teacher ? `<span class="badge teacher-badge">${teacher.fullName.split(' ')[0]}</span>` : '';
            }).join('')}
                    </div>
                </div>
                <div class="info-row">
                    <span>Séries:</span>
                    <div class="badges-container">
                        ${subject.seriesIds.map(sid => {
                const grade = appData.series.find(s => s.id === sid);
                return grade ? `<span class="badge series-badge">${grade.name}</span>` : '';
            }).join('')}
                    </div>
                </div>
                <div class="actions">
                    <button class="btn primary-btn edit-subject-btn" data-id="${subject.id}">Editar</button>
                    <button class="btn danger-btn delete-subject-btn" data-id="${subject.id}">Excluir</button>
                </div>
            `;
            subjectsList.appendChild(card);
        });
    }

    document.querySelectorAll('.edit-subject-btn').forEach(btn => btn.addEventListener('click', editSubject));
    document.querySelectorAll('.delete-subject-btn').forEach(btn => btn.addEventListener('click', deleteSubject));
    renderPagination('subjects-pagination', totalPages, page, (p) => renderSubjects(p, searchQuery, teacherFilter, seriesFilter));
}

function openSubjectModal(subject = null) {
    subjectForm.reset();
    subjectIdInput.value = '';
    subjectModalTitle.textContent = 'Adicionar Nova Disciplina';
    let selectedSeriesIds = [];
    let selectedTeacherIds = [];

    if (subject) {
        subjectModalTitle.textContent = 'Editar Disciplina';
        subjectIdInput.value = subject.id;
        subjectNameInput.value = subject.name;
        subjectCodeInput.value = subject.code || '';
        subjectWorkloadHoursInput.value = subject.workloadHours;
        selectedSeriesIds = [...subject.seriesIds];
        selectedTeacherIds = [...subject.teacherIds];
    }

    currentSubjectSeriesMultiSelect = setupMultiSelect(
        'subject-series-multi-select', 'subject-selected-series-container', 'subject-series-search-input',
        'subject-available-series-list', appData.series, 'id', 'name', selectedSeriesIds
    );
    currentSubjectSeriesMultiSelect.setSelectedIds(selectedSeriesIds);

    currentSubjectTeachersMultiSelect = setupMultiSelect(
        'subject-teachers-multi-select', 'subject-selected-teachers-container', 'subject-teacher-search-input',
        'subject-available-teachers-list', appData.teachers, 'id', 'fullName', selectedTeacherIds
    );
    currentSubjectTeachersMultiSelect.setSelectedIds(selectedTeacherIds);
    openModal(subjectModal);
}

function saveSubject(e) {
    e.preventDefault();
    showToast('Ação de salvar desabilitada no protótipo.', 'info');
}

function setupSubjectFilters() {
    subjectFilterTeacherSelect.innerHTML = '<option value="">Filtrar por Professor</option>';
    appData.teachers.forEach(t => {
        const option = document.createElement('option');
        option.value = t.id;
        option.textContent = t.fullName;
        subjectFilterTeacherSelect.appendChild(option);
    });

    subjectFilterSeriesSelect.innerHTML = '<option value="">Filtrar por Série</option>';
    appData.series.forEach(s => {
        const option = document.createElement('option');
        option.value = s.id;
        option.textContent = s.name;
        subjectFilterSeriesSelect.appendChild(option);
    });
}

export function initSubjects() {
    if (document.getElementById('subjects')) {
        addSubjectBtn.addEventListener('click', () => openSubjectModal());
        cancelSubjectBtn.addEventListener('click', () => closeModal(subjectModal));
        subjectForm.addEventListener('submit', saveSubject);

        subjectSearchInput.addEventListener('input', (e) => renderSubjects(1, e.target.value, subjectFilterTeacherSelect.value, subjectFilterSeriesSelect.value));
        subjectFilterTeacherSelect.addEventListener('change', (e) => renderSubjects(1, subjectSearchInput.value, e.target.value, subjectFilterSeriesSelect.value));
        subjectFilterSeriesSelect.addEventListener('change', (e) => renderSubjects(1, subjectSearchInput.value, subjectFilterTeacherSelect.value, e.target.value));

        setupSubjectFilters();
        renderSubjects();
    }
}