import { appData } from '../state.js';
import { showToast, openModal, closeModal, renderPagination } from '../ui.js';

const classModal = document.getElementById('class-modal');
const classForm = document.getElementById('class-form');
const addClassBtn = document.getElementById('add-class-btn');
const cancelClassBtn = document.getElementById('cancel-class-btn');
const classIdInput = document.getElementById('class-id');
const classNameInput = document.getElementById('class-name');
const classYearInput = document.getElementById('class-year');
const classShiftSelect = document.getElementById('class-shift');
const classSeriesSelect = document.getElementById('class-series');
const classPairingGridBody = document.getElementById('class-pairing-grid-body');
const addSubjectToClassPairingBtn = document.getElementById('add-subject-to-class-pairing');
const classModalTitle = document.getElementById('class-modal-title');
const classesList = document.getElementById('classes-list');
const classSearchInput = document.getElementById('class-search');
const classFilterSeriesSelect = document.getElementById('class-filter-series');
const classFilterShiftSelect = document.getElementById('class-filter-shift');

let currentClassSubjectTeacherPairings = [];

function editClass(e) {
    const classId = e.currentTarget.dataset.id;
    const classToEdit = appData.classes.find(c => c.id === classId);
    openClassModal(classToEdit);
}

function deleteClass(e) {
    showToast('Ação de exclusão desabilitada no protótipo.', 'info');
}

function renderClassPairingGrid() {
    classPairingGridBody.innerHTML = '';
    currentClassSubjectTeacherPairings.forEach((pairing, index) => {
        const row = document.createElement('tr');
        row.innerHTML = `
            <td>
                <select data-type="subject" data-index="${index}" class="subject-select">
                    <option value="">Selecione a Disciplina</option>
                    ${appData.subjects.map(s => `<option value="${s.id}" ${pairing.subjectId === s.id ? 'selected' : ''}>${s.name}</option>`).join('')}
                </select>
            </td>
            <td class="actions-cell">
                <select data-type="teacher" data-index="${index}" class="teacher-select">
                    <option value="">Selecione o Professor</option>
                    ${appData.teachers.map(t => `<option value="${t.id}" ${pairing.teacherId === t.id ? 'selected' : ''}>${t.fullName}</option>`).join('')}
                </select>
                <button type="button" data-index="${index}" class="btn-icon delete remove-pairing-row"><i class="fas fa-trash-alt"></i></button>
            </td>
        `;
        classPairingGridBody.appendChild(row);
    });
    document.querySelectorAll('.remove-pairing-row').forEach(btn => {
        btn.addEventListener('click', () => showToast('Ação desabilitada no protótipo.', 'info'));
    });
}

function openClassModal(_class = null) {
    classForm.reset();
    classModalTitle.textContent = 'Adicionar Nova Turma';
    currentClassSubjectTeacherPairings = [];

    classSeriesSelect.innerHTML = '<option value="">Selecione a Série</option>';
    appData.series.forEach(s => {
        const option = document.createElement('option');
        option.value = s.id;
        option.textContent = s.name;
        classSeriesSelect.appendChild(option);
    });

    if (_class) {
        classModalTitle.textContent = 'Editar Turma';
        classIdInput.value = _class.id;
        classNameInput.value = _class.name;
        classYearInput.value = _class.year;
        classShiftSelect.value = _class.shift;
        classSeriesSelect.value = _class.seriesId;
        currentClassSubjectTeacherPairings = JSON.parse(JSON.stringify(_class.subjectTeacherPairings));
    }

    renderClassPairingGrid();
    openModal(classModal);
}

export function renderClasses(page = 1, searchQuery = '', seriesFilter = '', shiftFilter = '') {
    if (!classesList) return;

    let filteredClasses = appData.classes.filter(_class => {
        const matchesSearch = _class.name.toLowerCase().includes(searchQuery.toLowerCase());
        const matchesSeries = !seriesFilter || _class.seriesId === seriesFilter;
        const matchesShift = !shiftFilter || _class.shift === shiftFilter;
        return matchesSearch && matchesSeries && matchesShift;
    });

    const itemsPerPage = 6;
    const totalPages = Math.ceil(filteredClasses.length / itemsPerPage);
    const startIndex = (page - 1) * itemsPerPage;
    const paginatedClasses = filteredClasses.slice(startIndex, startIndex + itemsPerPage);

    classesList.innerHTML = '';
    if (paginatedClasses.length === 0) {
        classesList.innerHTML = '<p class="placeholder-text">Nenhuma turma encontrada com os critérios especificados.</p>';
    } else {
        paginatedClasses.forEach(_class => {
            const classSeries = appData.series.find(s => s.id === _class.seriesId)?.name || 'N/A';
            const card = document.createElement('div');
            card.className = 'entity-card';
            card.innerHTML = `
                <h3>${_class.name} (${_class.year})</h3>
                <p>Turno: ${_class.shift}</p>
                <p>Série: ${classSeries}</p>
                <div class="info-row">
                    <span>Disciplinas e Professores:</span>
                    <ul class="assigned-pairings-list">
                        ${_class.subjectTeacherPairings.map(pairing => {
                const subject = appData.subjects.find(s => s.id === pairing.subjectId);
                const teacher = appData.teachers.find(t => t.id === pairing.teacherId);
                return `<li><span class="badge subject-badge">${subject?.name || 'N/A'}</span> <i class="fas fa-arrow-right"></i> <span class="badge teacher-badge">${teacher?.fullName || 'N/A'}</span></li>`;
            }).join('')}
                    </ul>
                </div>
                <div class="actions">
                    <button class="btn primary-btn edit-class-btn" data-id="${_class.id}">Editar</button>
                    <button class="btn danger-btn delete-class-btn" data-id="${_class.id}">Excluir</button>
                </div>
            `;
            classesList.appendChild(card);
        });
    }

    document.querySelectorAll('.edit-class-btn').forEach(btn => btn.addEventListener('click', editClass));
    document.querySelectorAll('.delete-class-btn').forEach(btn => btn.addEventListener('click', deleteClass));
    renderPagination('classes-pagination', totalPages, page, (p) => renderClasses(p, searchQuery, seriesFilter, shiftFilter));
}

function saveClass(e) {
    e.preventDefault();
    showToast('Ação de salvar desabilitada no protótipo.', 'info');
}

function setupClassFilters() {
    classFilterSeriesSelect.innerHTML = '<option value="">Filtrar por Série</option>';
    appData.series.forEach(s => {
        const option = document.createElement('option');
        option.value = s.id;
        option.textContent = s.name;
        classFilterSeriesSelect.appendChild(option);
    });
}

export function initClasses() {
    if (document.getElementById('classes')) {
        addClassBtn.addEventListener('click', () => openClassModal());
        cancelClassBtn.addEventListener('click', () => closeModal(classModal));
        classForm.addEventListener('submit', saveClass);

        addSubjectToClassPairingBtn.addEventListener('click', () => {
            showToast('Ação desabilitada no protótipo.', 'info');
        });

        classSearchInput.addEventListener('input', (e) => renderClasses(1, e.target.value, classFilterSeriesSelect.value, classFilterShiftSelect.value));
        classFilterSeriesSelect.addEventListener('change', (e) => renderClasses(1, classSearchInput.value, e.target.value, classFilterShiftSelect.value));
        classFilterShiftSelect.addEventListener('change', (e) => renderClasses(1, classSearchInput.value, classFilterSeriesSelect.value, e.target.value));

        setupClassFilters();
        renderClasses();
    }
}