const THEME_PRESETS = {
    'default': {
        '--petroleum-blue': '#1E3A8A',
        '--lilac': '#A78BFA',
        '--light-gray': '#E5E7EB',
        '--white': '#FFFFFF',
        '--dark-text': '#1F2937',
        '--light-text': '#6B7280'
    },
    'dark': {
        '--petroleum-blue': '#1E3A8A',
        '--lilac': '#A78BFA',
        '--light-gray': '#15161a',
        '--white': '#e9c6ff',
        '--dark-text': '#6364a9',
        '--light-text': '#6364a9'
    },
    'soft': {
        '--petroleum-blue': '#4A919E',
        '--lilac': '#BCA7E8',
        '--light-gray': '#DEE2E6',
        '--white': '#F8F9FA',
        '--dark-text': '#34495E',
        '--light-text': '#7F8C8D'
    }
};

document.addEventListener('DOMContentLoaded', () => {
    let appData = {
        teachers: [
            { id: 't1', fullName: 'Alice Johnson', email: 'alice@example.com', document: null, subjectIds: ['sub1', 'sub3'] },
            { id: 't2', fullName: 'Bob Williams', email: 'bob@example.com', document: null, subjectIds: ['sub2', 'sub4', 'sub6'] },
            { id: 't3', fullName: 'Carol Davis', email: 'carol@example.com', document: null, subjectIds: ['sub1', 'sub2', 'sub5'] },
            { id: 't4', fullName: 'David Garcia', email: 'david@example.com', document: null, subjectIds: ['sub7', 'sub8'] },
            { id: 't5', fullName: 'Eva Martinez', email: 'eva@example.com', document: null, subjectIds: ['sub5', 'sub6'] },
            { id: 't6', fullName: 'Frank Rodriguez', email: 'frank@example.com', document: null, subjectIds: ['sub1', 'sub4'] }
        ],
        subjects: [
            { id: 'sub1', name: 'Mathematics', code: 'MATH101', workloadHours: 60, seriesIds: ['s1', 's2'], teacherIds: ['t1', 't3', 't6'] },
            { id: 'sub2', name: 'Physics', code: 'PHYS201', workloadHours: 75, seriesIds: ['s2', 's3'], teacherIds: ['t2', 't3'] },
            { id: 'sub3', name: 'Chemistry', code: 'CHEM301', workloadHours: 70, seriesIds: ['s3'], teacherIds: ['t1'] },
            { id: 'sub4', name: 'Biology', code: 'BIO401', workloadHours: 65, seriesIds: ['s1', 's2', 's3'], teacherIds: ['t2', 't6'] },
            { id: 'sub5', name: 'History', code: 'HIST101', workloadHours: 50, seriesIds: ['s1', 's3'], teacherIds: ['t3', 't5'] },
            { id: 'sub6', name: 'Geography', code: 'GEO202', workloadHours: 50, seriesIds: ['s1', 's2'], teacherIds: ['t2', 't5'] },
            { id: 'sub7', name: 'Physical Education', code: 'PE100', workloadHours: 30, seriesIds: ['s1', 's2', 's3'], teacherIds: ['t4'] },
            { id: 'sub8', name: 'Art', code: 'ART101', workloadHours: 40, seriesIds: ['s1', 's2'], teacherIds: ['t4'] }
        ],
        series: [
            { id: 's1', name: '1st Grade', order: 1, subjectIds: ['sub1', 'sub4', 'sub5', 'sub6', 'sub7', 'sub8'] },
            { id: 's2', name: '2nd Grade', order: 2, subjectIds: ['sub1', 'sub2', 'sub4', 'sub6', 'sub7', 'sub8'] },
            { id: 's3', name: '3rd Grade', order: 3, subjectIds: ['sub2', 'sub3', 'sub4', 'sub5', 'sub7'] }
        ],
        classes: [
            { id: 'c1', name: 'Alpha Class', year: 2025, shift: 'Morning', seriesId: 's1', subjectTeacherPairings: [{ subjectId: 'sub1', teacherId: 't1' }, { subjectId: 'sub5', teacherId: 't3' }, { subjectId: 'sub4', teacherId: 't6' }] },
            { id: 'c2', name: 'Beta Class', year: 2025, shift: 'Afternoon', seriesId: 's2', subjectTeacherPairings: [{ subjectId: 'sub2', teacherId: 't2' }, { subjectId: 'sub1', teacherId: 't1' }] },
            { id: 'c3', name: 'Gamma Class', year: 2025, shift: 'Morning', seriesId: 's3', subjectTeacherPairings: [{ subjectId: 'sub3', teacherId: 't1' }, { subjectId: 'sub2', teacherId: 't3' }, { subjectId: 'sub5', teacherId: 't5' }] },
            { id: 'c4', name: 'Delta Class', year: 2025, shift: 'Afternoon', seriesId: 's1', subjectTeacherPairings: [{ subjectId: 'sub6', teacherId: 't5' }, { subjectId: 'sub8', teacherId: 't4' }] }
        ],
        classrooms: [
            { id: 'cr1', name: 'Room 101', capacity: 30, location: 'Main Building', availability: [] },
            { id: 'cr2', name: 'Lab 205', capacity: 20, location: 'Science Wing', availability: [] },
            { id: 'cr3', name: 'Gymnasium', capacity: 50, location: 'Sports Center', availability: [] },
            { id: 'cr4', name: 'Art Studio 301', capacity: 25, location: 'Arts Building', availability: [] }
        ],
        timeSlots: [
            { id: 'ts1', day: 'Monday', startTime: '08:00', endTime: '08:50' },
            { id: 'ts2', day: 'Monday', startTime: '09:00', endTime: '09:50' },
            { id: 'ts3', day: 'Tuesday', startTime: '08:00', endTime: '08:50' },
            { id: 'ts4', day: 'Tuesday', startTime: '10:00', endTime: '10:50' },
            { id: 'ts5', day: 'Wednesday', startTime: '08:00', endTime: '08:50' },
            { id: 'ts6', day: 'Wednesday', startTime: '09:00', endTime: '09:50' },
            { id: 'ts7', day: 'Thursday', startTime: '13:00', endTime: '13:50' },
            { id: 'ts8', day: 'Friday', startTime: '14:00', endTime: '14:50' }
        ],
        holidays: [
            { id: 'h1', date: '2025-01-01', description: "New Year's Day" },
            { id: 'h2', date: '2025-04-21', description: 'Tiradentes Day' },
            { id: 'h3', date: '2025-09-07', description: 'Independence Day' }
        ],
        lessonPlans: [
            { id: 'lp1', createdDate: '2025-05-20', status: 'Approved', version: 1, schedule: {} },
            { id: 'lp2', createdDate: '2025-05-27', status: 'Approved', version: 2, schedule: {} },
            { id: 'lp3', createdDate: '2025-06-01', status: 'Approved', version: 3, schedule: {} },
            { id: 'lp4', createdDate: '2025-06-05', status: 'Approved', version: 4, schedule: {} },
            { id: 'lp5', createdDate: '2025-06-10', status: 'Approved', version: 5, schedule: {} },
            { id: 'lp6', createdDate: '2025-06-11', status: 'Pending', version: 6, schedule: {} },
            { id: 'lp7', createdDate: '2025-06-12', status: 'Pending', version: 7, schedule: {} },
            { id: 'lp8', createdDate: '2025-06-08', status: 'Conflicted', version: 1, schedule: {} }
        ],
        settings: {
            maxVersions: 5,
            startTime: '08:00',
            endTime: '17:00',
            emailTemplate: 'Olá [Professor], sua grade para a semana está pronta para revisão. Acesse pelo link: [Link]',
            enableEmail: true,
            theme: {
                name: 'default',
                colors: {
                    '--petroleum-blue': '#1E3A8A',
                    '--lilac': '#A78BFA',
                    '--light-gray': '#E5E7EB',
                    '--white': '#FFFFFF',
                    '--dark-text': '#1F2937',
                    '--light-text': '#6B7280'
                }
            },
            allowTeacherAvailability: false,
            autoResolveConflicts: true,
            importHolidays: true,
            enableSubstitutions: false,
            enforceCapacity: true,
            notifyOnChange: false,
            enableTeacherReview: true,
            allowWeekend: false,
            maintenanceMode: false,
            suggestTemplate: true
        },
        modules: {
            teachers: true,
            subjects: true,
            series: true,
            classes: true,
            classrooms: true,
            timeSlots: true,
            holidays: true,
            lessonPlans: true,
            teacherReview: true,
            settings: true
        },
        currentUser: {
            name: 'Ana Beatriz',
            email: 'admin@healthtime.com',
            role: 'Administradora',
            memberSince: '2024-01-15',
            profilePictureUrl: null,
            institutions: [
                { name: 'Escola Modelo Padrão', plan: 'Plano de aulas de 2025 ativo.' },
                { name: 'Colégio Avançado Integral', plan: '12 turmas e 45 professores.' }
            ]
        }
    };

    const sidebar = document.getElementById('sidebar');
    const sidebarToggle = document.getElementById('sidebar-toggle');
    const navLinks = document.querySelectorAll('.sidebar-nav a');
    const contentSections = document.querySelectorAll('.content-section');
    const mainContent = document.querySelector('.main-content');
    const toastContainer = document.getElementById('toast-container');
    const teacherModal = document.getElementById('teacher-modal');
    const teacherForm = document.getElementById('teacher-form');
    const addTeacherBtn = document.getElementById('add-teacher-btn');
    const cancelTeacherBtn = document.getElementById('cancel-teacher-btn');
    const teacherIdInput = document.getElementById('teacher-id');
    const teacherFullNameInput = document.getElementById('teacher-full-name');
    const teacherEmailInput = document.getElementById('teacher-email');
    const teacherDocumentUpload = document.getElementById('teacher-document-upload');
    const teacherModalTitle = document.getElementById('teacher-modal-title');
    const teacherSubjectsMultiSelect = document.getElementById('teacher-subjects-multi-select');
    const teacherSelectedSubjectsContainer = document.getElementById('teacher-selected-subjects-container');
    const teacherSubjectSearchInput = document.getElementById('teacher-subject-search-input');
    const teacherAvailableSubjectsList = document.getElementById('teacher-available-subjects-list');
    const subjectModal = document.getElementById('subject-modal');
    const subjectForm = document.getElementById('subject-form');
    const addSubjectBtn = document.getElementById('add-subject-btn');
    const cancelSubjectBtn = document.getElementById('cancel-subject-btn');
    const subjectIdInput = document.getElementById('subject-id');
    const subjectNameInput = document.getElementById('subject-name');
    const subjectCodeInput = document.getElementById('subject-code');
    const subjectWorkloadHoursInput = document.getElementById('subject-workload-hours');
    const subjectModalTitle = document.getElementById('subject-modal-title');
    const subjectSeriesMultiSelect = document.getElementById('subject-series-multi-select');
    const subjectSelectedSeriesContainer = document.getElementById('subject-selected-series-container');
    const subjectSeriesSearchInput = document.getElementById('subject-series-search-input');
    const subjectAvailableSeriesList = document.getElementById('subject-available-series-list');
    const subjectTeachersMultiSelect = document.getElementById('subject-teachers-multi-select');
    const subjectSelectedTeachersContainer = document.getElementById('subject-selected-teachers-container');
    const subjectTeacherSearchInput = document.getElementById('subject-teacher-search-input');
    const subjectAvailableTeachersList = document.getElementById('subject-available-teachers-list');
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
    const userProfileArea = document.querySelector('.sidebar-user-profile');
    const modalHorarios = document.getElementById('modal-horarios');
    const formHorarios = document.getElementById('form-horarios');
    const inputStartTime = document.getElementById('config-start-time');
    const inputEndTime = document.getElementById('config-end-time');
    const modalNotificacoes = document.getElementById('modal-notificacoes');
    const formNotificacoes = document.getElementById('form-notificacoes');
    const inputEmailTemplate = document.getElementById('config-email-template');
    const inputEnableEmail = document.getElementById('inputEnableEmail');
    const modalAparencia = document.getElementById('modal-aparencia');
    const formAparencia = document.getElementById('form-aparencia');
    const customColorPickers = document.querySelectorAll('.color-picker-grid input[type="color"]');
    const themeCards = document.querySelectorAll('.theme-card');
    const modalTabLinks = document.querySelectorAll('.modal-tab-link');
    const modalTabContents = document.querySelectorAll('.modal-tab-content');
    const modalGerais = document.getElementById('modal-gerais');
    const formGerais = document.getElementById('form-gerais');
    const inputMaxVersions = document.getElementById('config-max-versions');
    const inputAllowTeacherAvailability = document.getElementById('config-allow-teacher-availability');
    const inputAutoResolveConflicts = document.getElementById('config-auto-resolve-conflicts');
    const inputImportHolidays = document.getElementById('config-import-holidays');
    const inputEnableSubstitutions = document.getElementById('config-enable-substitutions');
    const inputEnforceCapacity = document.getElementById('config-enforce-capacity');
    const inputNotifyOnChange = document.getElementById('config-notify-on-change');
    const inputEnableTeacherReview = document.getElementById('config-enable-teacher-review');
    const inputAllowWeekend = document.getElementById('config-allow-weekend');
    const inputMaintenanceMode = document.getElementById('config-maintenance-mode');
    const inputSuggestTemplate = document.getElementById('config-suggest-template');
    const modalModulos = document.getElementById('modal-modulos');
    const formModulos = document.getElementById('form-modulos');
    const btnChangePassword = document.getElementById('btn-change-password');
    const changePasswordModal = document.getElementById('modal-change-password');
    const moduleToggles = {
        teachers: document.getElementById('module-teachers'),
        subjects: document.getElementById('module-subjects'),
        series: document.getElementById('module-series'),
        classes: document.getElementById('module-classes'),
        classrooms: document.getElementById('module-classrooms'),
        timeSlots: document.getElementById('module-time-slots'),
        holidays: document.getElementById('module-holidays'),
        lessonPlans: document.getElementById('module-lesson-plans'),
        teacherReview: document.getElementById('module-teacher-review'),
        settings: document.getElementById('module-settings')
    };

    const profilePictureContainer = document.getElementById('profile-picture-container');
    const profilePictureInput = document.getElementById('profile-picture-input');
    const profilePictureImg = document.getElementById('profile-picture-img');
    const imageViewerModal = document.getElementById('modal-image-viewer');
    const imageViewerSrc = document.getElementById('image-viewer-src');
    const btnTriggerUpload = document.getElementById('btn-trigger-upload');
    const btnRemovePhoto = document.getElementById('btn-remove-photo');
    const cropModal = document.getElementById('modal-crop-image');
    const imageToCrop = document.getElementById('image-to-crop');
    const cropAndSaveBtn = document.getElementById('crop-and-save-btn');
    let cropper;
    if (profilePictureContainer) {
        profilePictureContainer.addEventListener('click', () => {
            if (!appData.currentUser.profilePictureUrl) {
                profilePictureInput.click();
            } else {
                imageViewerSrc.src = appData.currentUser.profilePictureUrl;
                openModal(imageViewerModal);
            }
        });
    } if (btnTriggerUpload) {
        btnTriggerUpload.addEventListener('click', () => {
            profilePictureInput.click();
        });
    } if (profilePictureInput) {
        profilePictureInput.addEventListener('change', (e) => {
            closeModal(imageViewerModal);
            const files = e.target.files;
            if (files && files.length > 0) {
                const reader = new FileReader();
                reader.onload = (event) => {
                    imageToCrop.src = event.target.result;
                    openModal(cropModal);
                    if (cropper) cropper.destroy();
                    cropper = new Cropper(imageToCrop, {
                        aspectRatio: 1,
                        viewMode: 1,
                        background: false,
                        autoCropArea: 0.9
                    });
                };
                reader.readAsDataURL(files[0]);
            }
        });
    } if (btnRemovePhoto) {
        btnRemovePhoto.addEventListener('click', () => {
            appData.currentUser.profilePictureUrl = null;
            renderProfile();
            renderSidebarProfile();
            closeModal(imageViewerModal);
            showToast('Foto de perfil removida.', 'info');
        });
    } if (cropAndSaveBtn) {
        cropAndSaveBtn.addEventListener('click', () => {
            if (cropper) {
                const canvas = cropper.getCroppedCanvas({ width: 256, height: 256 });
                const newImageUrl = canvas.toDataURL();
                appData.currentUser.profilePictureUrl = newImageUrl;
                renderProfile();
                renderSidebarProfile();
            }
            closeModal(cropModal);
            showToast('Imagem de perfil atualizada com sucesso!', 'success');
        });
    }

    const modalPlaceholder = document.getElementById('modal-placeholder');
    const placeholderModalTitle = document.getElementById('placeholder-modal-title');
    const placeholderModalText = document.getElementById('placeholder-modal-text');

    const uploader = document.getElementById('teacher-doc-uploader');
    const fileInput = document.getElementById('teacher-document-upload');
    const fileNameDisplay = document.getElementById('teacher-doc-filename');
    const filePreviewBtn = document.getElementById('teacher-doc-preview');

    function renderSidebarProfile() {
        const user = appData.currentUser;
        if (user) {
            document.getElementById('sidebar-user-name').textContent = user.name;
            document.getElementById('sidebar-user-role').textContent = user.role;
            const avatarContainer = document.getElementById('sidebar-user-avatar');
            if (user.profilePictureUrl) {
                avatarContainer.innerHTML = `<img src="${user.profilePictureUrl}" alt="Foto do Perfil">`;
            } else {
                avatarContainer.innerHTML = '<i class="fas fa-user-circle"></i>';
            }
        }
    }
    if (uploader) {
        uploader.addEventListener('click', () => fileInput.click());

        uploader.addEventListener('dragover', (e) => {
            e.preventDefault();
            uploader.classList.add('is-dragover');
        });
        uploader.addEventListener('dragleave', () => {
            uploader.classList.remove('is-dragover');
        });

        uploader.addEventListener('drop', (e) => {
            e.preventDefault();
            uploader.classList.remove('is-dragover');
            const files = e.dataTransfer.files;
            if (files.length > 0) {
                fileInput.files = files;
                handleFile(files[0]);
            }
        });

        fileInput.addEventListener('change', (e) => {
            if (e.target.files.length > 0) {
                handleFile(e.target.files[0]);
            }
        });

        const handleFile = (file) => {
            fileNameDisplay.textContent = file.name;
            uploader.classList.add('is-success');
            const fileURL = URL.createObjectURL(file);
            filePreviewBtn.href = fileURL;
        };
    }


    if (profilePictureInput) {
        profilePictureInput.addEventListener('change', (e) => {
            const files = e.target.files;
            if (files && files.length > 0) {
                const file = files[0];
                profilePictureImg.src = URL.createObjectURL(file);
                showToast('Imagem de perfil selecionada!', 'info');
            }
        });
    }



    function showToast(message, type = 'success') {
        const toast = document.createElement('div');
        toast.className = `toast ${type}`;
        let icon = '';

        if (type === 'success') icon = '<i class="fas fa-check-circle"></i>';
        else if (type === 'error') icon = '<i class="fas fa-times-circle"></i>';
        else if (type === 'warning') icon = '<i class="fas fa-exclamation-triangle"></i>';
        else if (type === 'info') icon = '<i class="fas fa-info-circle"></i>';

        toast.innerHTML = `${icon}<span>${message}</span>`;

        toastContainer.appendChild(toast);
        setTimeout(() => toast.classList.add('show'), 100);
        setTimeout(() => {
            toast.classList.remove('show');
            toast.addEventListener('transitionend', () => toast.remove());
        }, 5000);
    }

    let currentSectionId = 'dashboard';
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
            if (link.dataset.section === sectionId) {
                link.classList.add('active');
            } else {
                link.classList.remove('active');
            }
        });
        if (window.innerWidth < 768) {
            sidebar.classList.remove('active');
        }
        updateCounts();
    }

    const initialHash = window.location.hash.substring(1);
    if (initialHash && document.getElementById(initialHash)) {
        showSection(initialHash);
    } else {
        showSection('dashboard');
    }

    navLinks.forEach(link => {
        link.addEventListener('click', (e) => {
            e.preventDefault();
            const sectionId = e.currentTarget.dataset.section;
            showSection(sectionId);
            if (sectionId === 'teachers') renderTeachers();
            else if (sectionId === 'subjects') renderSubjects();
            else if (sectionId === 'series') renderSeries();
            else if (sectionId === 'classes') renderClasses();
            else if (sectionId === 'classrooms') renderClassrooms();
            else if (sectionId === 'time-slots') renderTimeSlots();
            else if (sectionId === 'holidays') renderHolidays();
            else if (sectionId === 'lesson-plans') renderLessonPlans();
        });
    });

    sidebarToggle.addEventListener('click', () => {
        sidebar.classList.toggle('active');
        mainContent.classList.toggle('expanded');
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
    if (window.innerWidth >= 768) {
        sidebar.classList.add('active');
        mainContent.classList.add('expanded');
    }

    function updateCounts() {
        document.getElementById('count-teachers').textContent = appData.teachers.length;
        document.getElementById('count-subjects').textContent = appData.subjects.length;
        document.getElementById('count-classes').textContent = appData.classes.length;
        document.getElementById('count-classrooms').textContent = appData.classrooms.length;
        document.getElementById('status-pending').textContent = appData.lessonPlans.filter(p => p.status === 'Pending').length;
        document.getElementById('status-approved').textContent = appData.lessonPlans.filter(p => p.status === 'Approved').length;
        document.getElementById('status-conflicted').textContent = appData.lessonPlans.filter(p => p.status === 'Conflicted').length;
    }

    function setupMultiSelect(containerId, selectedContainerId, searchInputId, availableListId, allItems, itemPropertyId, itemPropertyName, selectedIdsArrayRef) {
        const selectedContainer = document.getElementById(selectedContainerId);
        const searchInput = document.getElementById(searchInputId);
        const availableList = document.getElementById(availableListId);
        const parentContainer = document.getElementById(containerId);
        let currentSelectedIds = new Set(selectedIdsArrayRef);
        const renderSelected = () => {
            selectedContainer.innerHTML = '';
            Array.from(currentSelectedIds).forEach(id => {
                const item = allItems.find(i => i[itemPropertyId] === id);
                if (item) {
                    const chip = document.createElement('span');
                    chip.className = 'selected-item-chip';
                    chip.innerHTML = `${item[itemPropertyName]} <button type="button" class="remove-btn" data-id="${id}"><i class="fas fa-times"></i></button>`;
                    selectedContainer.appendChild(chip);
                    chip.querySelector('.remove-btn').addEventListener('click', (e) => {
                        const removeId = e.currentTarget.dataset.id;
                        currentSelectedIds.delete(removeId);
                        selectedIdsArrayRef.splice(selectedIdsArrayRef.indexOf(removeId), 1);
                        renderSelected();
                        renderAvailable(searchInput.value);
                    });
                }
            });
        };
        const renderAvailable = (searchTerm = '') => {
            availableList.innerHTML = '';
            const filteredItems = allItems.filter(item => !currentSelectedIds.has(item[itemPropertyId]) && item[itemPropertyName].toLowerCase().includes(searchTerm.toLowerCase()));
            if (filteredItems.length === 0) {
                availableList.innerHTML = '<div class="available-item text-center">No items available.</div>';
            }
            filteredItems.forEach(item => {
                const listItem = document.createElement('div');
                listItem.className = 'available-item';
                listItem.textContent = item[itemPropertyName];
                listItem.dataset.id = item[itemPropertyId];
                listItem.addEventListener('click', () => {
                    currentSelectedIds.add(item[itemPropertyId]);
                    selectedIdsArrayRef.push(item[itemPropertyId]);
                    searchInput.value = '';
                    renderSelected();
                    renderAvailable('');
                    availableList.classList.remove('active');
                });
                availableList.appendChild(listItem);
            });
            if (filteredItems.length > 0 || searchTerm !== '') {
                availableList.classList.add('active');
            } else {
                availableList.classList.remove('active');
            }
        };
        searchInput.addEventListener('input', () => renderAvailable(searchInput.value));
        searchInput.addEventListener('focus', () => renderAvailable(searchInput.value));
        document.addEventListener('click', (e) => {
            if (!parentContainer.contains(e.target)) {
                availableList.classList.remove('active');
            }
        });
        renderSelected();
        renderAvailable('');
        return {
            getSelectedIds: () => Array.from(currentSelectedIds),
            setSelectedIds: (ids) => {
                currentSelectedIds = new Set(ids);
                renderSelected();
                renderAvailable(searchInput.value);
            },
            clear: () => {
                currentSelectedIds.clear();
                selectedIdsArrayRef.length = 0;
                renderSelected();
                renderAvailable('');
            }
        };
    }

    function openModal(modalElement) {
        modalElement.classList.add('active');
        document.body.style.overflow = 'hidden';
    }

    function closeModal(modalElement) {
        modalElement.classList.remove('active');
        document.body.style.overflow = '';
    }

    let currentTeacherSubjectsMultiSelect = null;
    function renderTeachers(page = 1, searchQuery = '', subjectFilter = '') {
        const teachersList = document.getElementById('teachers-list');
        teachersList.innerHTML = '';
        const teacherFilterSubjectSelect = document.getElementById('teacher-filter-subject');
        teacherFilterSubjectSelect.innerHTML = '<option value="">Filter by Subject</option>';
        appData.subjects.forEach(sub => {
            const option = document.createElement('option');
            option.value = sub.id;
            option.textContent = sub.name;
            teacherFilterSubjectSelect.appendChild(option);
        });
        if (subjectFilter) {
            teacherFilterSubjectSelect.value = subjectFilter;
        }
        let filteredTeachers = appData.teachers.filter(teacher => {
            const matchesSearch = teacher.fullName.toLowerCase().includes(searchQuery.toLowerCase()) || teacher.email.toLowerCase().includes(searchQuery.toLowerCase());
            const matchesSubject = !subjectFilter || teacher.subjectIds.includes(subjectFilter);
            return matchesSearch && matchesSubject;
        });
        const itemsPerPage = 6;
        const totalPages = Math.ceil(filteredTeachers.length / itemsPerPage);
        const startIndex = (page - 1) * itemsPerPage;
        const paginatedTeachers = filteredTeachers.slice(startIndex, startIndex + itemsPerPage);
        if (paginatedTeachers.length === 0) {
            teachersList.innerHTML = '<p class="placeholder-text">No teachers match your search/filter criteria.</p>';
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
                        <button class="btn primary-btn edit-teacher-btn" data-id="${teacher.id}">Edit</button>
                        <button class="btn danger-btn delete-teacher-btn" data-id="${teacher.id}">Delete</button>
                    </div>
                `;
                teachersList.appendChild(card);
            });
        }
        document.querySelectorAll('.edit-teacher-btn').forEach(btn => btn.addEventListener('click', editTeacher));
        document.querySelectorAll('.delete-teacher-btn').forEach(btn => btn.addEventListener('click', deleteTeacher));
        renderPagination('teachers-pagination', totalPages, page, (p) => renderTeachers(p, searchQuery, subjectFilter));
        updateCounts();
    }

    function openTeacherModal(teacher = null) {
        teacherForm.reset();
        teacherIdInput.value = '';
        teacherModalTitle.textContent = 'Add New Teacher';
        let selectedSubjectIds = [];
        if (teacher) {
            teacherModalTitle.textContent = 'Edit Teacher';
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
        showToast('Ação desabilitada no protótipo.', 'info');
    }
    function editTeacher(e) {
        openTeacherModal(appData.teachers.find(t => t.id === e.currentTarget.dataset.id));
    }
    function deleteTeacher(e) {
        showToast('Ação desabilitada no protótipo.', 'info');
    }
    addTeacherBtn.addEventListener('click', () => openTeacherModal());
    cancelTeacherBtn.addEventListener('click', () => closeModal(teacherModal));
    teacherForm.addEventListener('submit', saveTeacher);



    document.getElementById('teacher-search').addEventListener('input', (e) => renderTeachers(1, e.target.value, document.getElementById('teacher-filter-subject').value));
    document.getElementById('teacher-filter-subject').addEventListener('change', (e) => renderTeachers(1, document.getElementById('teacher-search').value, e.target.value));

    let currentSubjectSeriesMultiSelect = null;
    let currentSubjectTeachersMultiSelect = null;

    function renderSubjects(page = 1, searchQuery = '', teacherFilter = '', seriesFilter = '') {
        const subjectsList = document.getElementById('subjects-list');
        subjectsList.innerHTML = '';
        const subjectFilterTeacherSelect = document.getElementById('subject-filter-teacher');
        const subjectFilterSeriesSelect = document.getElementById('subject-filter-series');
        subjectFilterTeacherSelect.innerHTML = '<option value="">Filter by Teacher</option>';
        appData.teachers.forEach(t => {
            const option = document.createElement('option');
            option.value = t.id;
            option.textContent = t.fullName;
            subjectFilterTeacherSelect.appendChild(option);
        });
        if (teacherFilter) subjectFilterTeacherSelect.value = teacherFilter;
        subjectFilterSeriesSelect.innerHTML = '<option value="">Filter by Series</option>';
        appData.series.forEach(s => {
            const option = document.createElement('option');
            option.value = s.id;
            option.textContent = s.name;
            subjectFilterSeriesSelect.appendChild(option);
        });
        if (seriesFilter) subjectFilterSeriesSelect.value = seriesFilter;
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
        if (paginatedSubjects.length === 0) {
            subjectsList.innerHTML = '<p class="placeholder-text">No subjects match your search/filter criteria.</p>';
        } else {
            paginatedSubjects.forEach(subject => {
                const card = document.createElement('div');
                card.className = 'entity-card';
                card.innerHTML = `
                    <h3>${subject.name}</h3>
                    <p>Code: ${subject.code || 'N/A'}</p>
                    <p>Workload: ${subject.workloadHours} hours</p>
                    <div class="info-row">
                        <span>Teachers:</span>
                        <div class="badges-container">
                            ${subject.teacherIds.map(tid => {
                    const teacher = appData.teachers.find(t => t.id === tid);
                    return teacher ? `<span class="badge teacher-badge">${teacher.fullName.split(' ')[0]}</span>` : '';
                }).join('')}
                        </div>
                    </div>
                    <div class="info-row">
                        <span>Series:</span>
                        <div class="badges-container">
                            ${subject.seriesIds.map(sid => {
                    const grade = appData.series.find(s => s.id === sid);
                    return grade ? `<span class="badge series-badge">${grade.name}</span>` : '';
                }).join('')}
                        </div>
                    </div>
                    <div class="actions">
                        <button class="btn primary-btn edit-subject-btn" data-id="${subject.id}">Edit</button>
                        <button class="btn danger-btn delete-subject-btn" data-id="${subject.id}">Delete</button>
                    </div>
                `;
                subjectsList.appendChild(card);
            });
        }
        document.querySelectorAll('.edit-subject-btn').forEach(btn => btn.addEventListener('click', editSubject));
        document.querySelectorAll('.delete-subject-btn').forEach(btn => btn.addEventListener('click', deleteSubject));
        renderPagination('subjects-pagination', totalPages, page, (p) => renderSubjects(p, searchQuery, teacherFilter, seriesFilter));
        updateCounts();
    }
    function openSubjectModal(subject = null) {
        subjectForm.reset();
        subjectIdInput.value = '';
        subjectModalTitle.textContent = 'Add New Subject';
        let selectedSeriesIds = [];
        let selectedTeacherIds = [];
        if (subject) {
            subjectModalTitle.textContent = 'Edit Subject';
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
        showToast('Ação desabilitada no protótipo.', 'info');
    }
    function editSubject(e) {
        openSubjectModal(appData.subjects.find(s => s.id === e.currentTarget.dataset.id));
    }
    function deleteSubject(e) {
        showToast('Ação desabilitada no protótipo.', 'info');
    }
    addSubjectBtn.addEventListener('click', () => openSubjectModal());
    cancelSubjectBtn.addEventListener('click', () => closeModal(subjectModal));
    subjectForm.addEventListener('submit', saveSubject);
    document.getElementById('subject-search').addEventListener('input', (e) => renderSubjects(1, e.target.value, document.getElementById('subject-filter-teacher').value, document.getElementById('subject-filter-series').value));
    document.getElementById('subject-filter-teacher').addEventListener('change', (e) => renderSubjects(1, document.getElementById('subject-search').value, e.target.value, document.getElementById('subject-filter-series').value));
    document.getElementById('subject-filter-series').addEventListener('change', (e) => renderSubjects(1, document.getElementById('subject-search').value, document.getElementById('subject-filter-teacher').value, e.target.value));

    function renderSeries(page = 1, searchQuery = '') {
        const seriesList = document.getElementById('series-list');
        seriesList.innerHTML = '';
        let filteredSeries = appData.series.filter(_series => _series.name.toLowerCase().includes(searchQuery.toLowerCase()));
        let paginatedSeries = filteredSeries;
        if (paginatedSeries.length === 0) {
            seriesList.innerHTML = '<p class="placeholder-text">No series match your search criteria.</p>';
        } else {
            paginatedSeries.forEach(_series => {
                const card = document.createElement('div');
                card.className = 'entity-card';
                card.innerHTML = `
                    <h3>${_series.name}</h3>
                    <p>Order: ${_series.order}</p>
                    <div class="info-row">
                        <span>Related Subjects:</span>
                        <div class="badges-container">
                            ${_series.subjectIds.map(subId => {
                    const subject = appData.subjects.find(s => s.id === subId);
                    return subject ? `<span class="badge subject-badge">${subject.name}</span>` : '';
                }).join('')}
                        </div>
                    </div>
                    <div class="actions">
                        <button class="btn primary-btn" onclick="showToast('Ação desabilitada no protótipo.', 'info')">Edit</button>
                        <button class="btn danger-btn" onclick="showToast('Ação desabilitada no protótipo.', 'info')">Delete</button>
                    </div>
                `;
                seriesList.appendChild(card);
            });
        }
        updateCounts();
    }
    document.getElementById('add-series-btn').addEventListener('click', () => showToast('Modal não implementado neste protótipo.', 'info'));
    document.getElementById('series-search').addEventListener('input', (e) => renderSeries(1, e.target.value));

    function renderClassrooms() {
        const list = document.getElementById('classrooms-list');
        list.innerHTML = '';
        appData.classrooms.forEach(c => {
            const card = document.createElement('div');
            card.className = 'entity-card';
            card.innerHTML = `<h3>${c.name}</h3> <p>Location: ${c.location}</p> <p>Capacity: ${c.capacity}</p> <div class="actions"><button class="btn primary-btn" onclick="showToast('Ação desabilitada no protótipo.', 'info')">Edit</button><button class="btn danger-btn" onclick="showToast('Ação desabilitada no protótipo.', 'info')">Delete</button></div>`;
            list.appendChild(card);
        })
    }
    function renderTimeSlots() {
        const list = document.getElementById('time-slots-body');
        list.innerHTML = '';
        appData.timeSlots.forEach(slot => {
            const row = document.createElement('tr');
            row.innerHTML = `<td>${slot.day}</td><td>${slot.startTime}</td><td>${slot.endTime}</td><td class="actions-cell"><button onclick="showToast('Ação desabilitada no protótipo.', 'info')" class="btn-icon"><i class="fas fa-edit"></i></button><button onclick="showToast('Ação desabilitada no protótipo.', 'info')" class="btn-icon delete"><i class="fas fa-trash-alt"></i></button></td>`;
            list.appendChild(row);
        });
    }
    function renderHolidays() {
        const list = document.getElementById('holidays-list');
        list.innerHTML = '';
        appData.holidays.forEach(holiday => {
            const item = document.createElement('div');
            item.className = 'list-item';
            item.innerHTML = `<div><p class="list-item-title">${new Date(holiday.date).toLocaleDateString()}</p><p class="list-item-subtitle">${holiday.description}</p></div><div class="actions"><button class="btn primary-btn btn-sm" onclick="showToast('Ação desabilitada no protótipo.', 'info')"><i class="fas fa-edit"></i></button><button class="btn danger-btn btn-sm" onclick="showToast('Ação desabilitada no protótipo.', 'info')"><i class="fas fa-trash-alt"></i></button></div>`;
            list.appendChild(item);
        });
    }
    function renderLessonPlans() {
        const list = document.getElementById('lesson-plan-versions-list');
        list.innerHTML = '';
        appData.lessonPlans.forEach(plan => {
            let statusBadgeClass = '';
            if (plan.status === 'Approved') {
                statusBadgeClass = 'status-approved';
            } else if (plan.status === 'Pending') {
                statusBadgeClass = 'status-pending';
            } else if (plan.status === 'Conflicted') {
                statusBadgeClass = 'status-conflicted';
            }
            const item = document.createElement('div');
            item.className = 'list-item';
            item.innerHTML = `
                <div>
                    <p class="list-item-title">Version ${plan.version} - ${new Date(plan.createdDate).toLocaleDateString()}</p>
                    <span class="badge ${statusBadgeClass}">${plan.status}</span>
                </div>
                <div class="actions">
                    <div class="actions"><button class="btn secondary-btn btn-sm">Visualizar</button>
                </div>
            `;
            list.appendChild(item);
        });
    }

    function renderProfile() {
        const user = appData.currentUser;
        document.getElementById('profile-user-name').textContent = user.name;
        document.getElementById('profile-user-email').textContent = user.email;
        document.getElementById('profile-picture-img').src = user.profilePictureUrl || '/imagens/userPlaceholder.png';

        document.getElementById('profile-member-since').textContent = new Date(user.memberSince).toLocaleDateString('pt-BR', { day: '2-digit', month: 'long', year: 'numeric' });
        const roleBadge = document.getElementById('profile-user-role-badge');
        roleBadge.className = 'badge status-approved';
        roleBadge.textContent = user.role;

        const institutionsList = document.getElementById('profile-institutions-list');
        institutionsList.innerHTML = '';
        user.institutions.forEach(inst => {
            const card = document.createElement('a');
            card.href = "#";
            card.className = 'institution-card';
            card.innerHTML = `
            <div class="institution-info">
                <h3><i class="fas fa-school"></i> ${inst.name}</h3>
                <p>${inst.plan}</p>
            </div>
            <span class="btn secondary-btn">Ver Detalhes <i class="fas fa-arrow-right"></i></span>
        `;
            institutionsList.appendChild(card);
        });
    }


    let currentClassSubjectTeacherPairings = [];
    function renderClasses(page = 1, searchQuery = '', seriesFilter = '', shiftFilter = '') {
        const classesList = document.getElementById('classes-list');
        classesList.innerHTML = '';
        const classFilterSeriesSelect = document.getElementById('class-filter-series');
        const classFilterShiftSelect = document.getElementById('class-filter-shift');
        classFilterSeriesSelect.innerHTML = '<option value="">Filter by Series</option>';
        appData.series.forEach(s => {
            const option = document.createElement('option');
            option.value = s.id;
            option.textContent = s.name;
            classFilterSeriesSelect.appendChild(option);
        });
        if (seriesFilter) classFilterSeriesSelect.value = seriesFilter;
        if (shiftFilter) classFilterShiftSelect.value = shiftFilter;
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
        if (paginatedClasses.length === 0) {
            classesList.innerHTML = '<p class="placeholder-text">No classes match your criteria.</p>';
        } else {
            paginatedClasses.forEach(_class => {
                const classSeries = appData.series.find(s => s.id === _class.seriesId)?.name || 'N/A';
                const card = document.createElement('div');
                card.className = 'entity-card';
                card.innerHTML = `
                    <h3>${_class.name} (${_class.year})</h3>
                    <p>Shift: ${_class.shift}</p>
                    <p>Series: ${classSeries}</p>
                    <div class="info-row">
                        <span>Assigned Subjects & Teachers:</span>
                        <ul class="assigned-pairings-list">
                            ${_class.subjectTeacherPairings.map(pairing => {
                    const subject = appData.subjects.find(s => s.id === pairing.subjectId);
                    const teacher = appData.teachers.find(t => t.id === pairing.teacherId);
                    return `<li><span class="badge subject-badge">${subject?.name || 'N/A'}</span> <i class="fas fa-arrow-right"></i> <span class="badge teacher-badge">${teacher?.fullName || 'N/A'}</span></li>`;
                }).join('')}
                        </ul>
                    </div>
                    <div class="actions">
                        <button class="btn primary-btn edit-class-btn" data-id="${_class.id}">Edit</button>
                        <button class="btn danger-btn delete-class-btn" data-id="${_class.id}">Delete</button>
                    </div>
                `;
                classesList.appendChild(card);
            });
        }
        document.querySelectorAll('.edit-class-btn').forEach(btn => btn.addEventListener('click', editClass));
        document.querySelectorAll('.delete-class-btn').forEach(btn => btn.addEventListener('click', deleteClass));
        renderPagination('classes-pagination', totalPages, page, (p) => renderClasses(p, searchQuery, seriesFilter, shiftFilter));
        updateCounts();
    }
    function renderClassPairingGrid() {
        classPairingGridBody.innerHTML = '';
        currentClassSubjectTeacherPairings.forEach((pairing, index) => {
            const row = document.createElement('tr');
            row.innerHTML = `
                <td>
                    <select data-type="subject" data-index="${index}" class="subject-select">
                        <option value="">Select Subject</option>
                        ${appData.subjects.map(s => `<option value="${s.id}" ${pairing.subjectId === s.id ? 'selected' : ''}>${s.name}</option>`).join('')}
                    </select>
                </td>
                <td class="actions-cell">
                    <select data-type="teacher" data-index="${index}" class="teacher-select">
                        <option value="">Select Teacher</option>
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
    addSubjectToClassPairingBtn.addEventListener('click', () => {
        showToast('Ação desabilitada no protótipo.', 'info');
    });
    function openClassModal(_class = null) {
        classForm.reset();
        classModalTitle.textContent = 'Add New Class';
        currentClassSubjectTeacherPairings = [];
        classSeriesSelect.innerHTML = '<option value="">Select Series</option>';
        appData.series.forEach(s => {
            const option = document.createElement('option');
            option.value = s.id;
            option.textContent = s.name;
            classSeriesSelect.appendChild(option);
        });
        if (_class) {
            classModalTitle.textContent = 'Edit Class';
            classNameInput.value = _class.name;
            classYearInput.value = _class.year;
            classShiftSelect.value = _class.shift;
            classSeriesSelect.value = _class.seriesId;
            currentClassSubjectTeacherPairings = JSON.parse(JSON.stringify(_class.subjectTeacherPairings));
        }
        renderClassPairingGrid();
        openModal(classModal);
    }
    function saveClass(e) {
        e.preventDefault();
        showToast('Ação desabilitada no protótipo.', 'info');
    }
    function editClass(e) {
        openClassModal(appData.classes.find(c => c.id === e.currentTarget.dataset.id));
    }
    function deleteClass(e) {
        showToast('Ação desabilitada no protótipo.', 'info');
    }
    addClassBtn.addEventListener('click', () => openClassModal());
    cancelClassBtn.addEventListener('click', () => closeModal(classModal));
    classForm.addEventListener('submit', saveClass);
    document.getElementById('class-search').addEventListener('input', (e) => renderClasses(1, e.target.value, document.getElementById('class-filter-series').value, document.getElementById('class-filter-shift').value));
    document.getElementById('class-filter-series').addEventListener('change', (e) => renderClasses(1, document.getElementById('class-search').value, e.target.value, document.getElementById('class-filter-shift').value));
    document.getElementById('class-filter-shift').addEventListener('change', (e) => renderClasses(1, document.getElementById('class-search').value, document.getElementById('class-filter-series').value, e.target.value));

    function renderPagination(containerId, totalPages, currentPage, renderFunction) {
        const paginationContainer = document.getElementById(containerId);
        paginationContainer.innerHTML = '';
        if (totalPages <= 1) return;
        const createButton = (text, pageNum, isActive = false, isDisabled = false) => {
            const button = document.createElement('button');
            button.textContent = text;
            button.className = `pagination-btn ${isActive ? 'active' : ''}`;
            if (isDisabled) button.disabled = true;
            else button.addEventListener('click', () => renderFunction(pageNum));
            return button;
        };
        paginationContainer.appendChild(createButton('Previous', currentPage - 1, false, currentPage === 1));
        for (let i = 1; i <= totalPages; i++) {
            paginationContainer.appendChild(createButton(i, i, i === currentPage));
        }
        paginationContainer.appendChild(createButton('Next', currentPage + 1, false, currentPage === totalPages));
    }

    function openModalHorarios() {
        inputStartTime.value = appData.settings.startTime;
        inputEndTime.value = appData.settings.endTime;
        openModal(modalHorarios);
    }

    function saveSettingsHorarios(e) {
        e.preventDefault();
        appData.settings.startTime = inputStartTime.value;
        appData.settings.endTime = inputEndTime.value;
        showToast('Horários padrão salvos com sucesso!');
        closeModal(modalHorarios);
    }

    function openModalNotificacoes() {
        inputEmailTemplate.value = appData.settings.emailTemplate;
        inputEnableEmail.checked = appData.settings.enableEmail;
        openModal(modalNotificacoes);
    }

    function openModalSenha() {

        openModal(changePasswordModal);
    }

    function saveSettingsNotificacoes(e) {
        e.preventDefault();
        appData.settings.emailTemplate = inputEmailTemplate.value;
        appData.settings.enableEmail = inputEnableEmail.checked;
        showToast('Configurações de notificação salvas com sucesso!');
        closeModal(modalNotificacoes);
    }

    function applyTheme(themeColors) {
        for (const [variable, color] of Object.entries(themeColors)) {
            document.documentElement.style.setProperty(variable, color);
        }
    }

    function openModalAparencia() {
        for (const [variable, color] of Object.entries(appData.settings.theme.colors)) {
            const picker = document.querySelector(`[data-variable="${variable}"]`);
            if (picker) {
                picker.value = color;
            }
        }
        themeCards.forEach(card => {
            card.classList.remove('active-theme');
            if (card.dataset.themeName === appData.settings.theme.name) {
                card.classList.add('active-theme');
            }
        });
        showTab('temas');
        openModal(modalAparencia);
    }

    function saveSettingsAparencia(e) {
        e.preventDefault();
        showToast('Aparência salva com sucesso!');
        closeModal(modalAparencia);
    }

    function showTab(tabId) {
        modalTabContents.forEach(content => content.classList.remove('active'));
        modalTabLinks.forEach(link => link.classList.remove('active'));
        document.getElementById(`tab-${tabId}`).classList.add('active');
        document.querySelector(`.modal-tab-link[data-tab="${tabId}"]`).classList.add('active');
    }

    function updateSidebarVisibility() {
        for (const moduleName in appData.modules) {
            const navLink = document.querySelector(`.sidebar-nav a[data-section="${moduleName}"]`);
            if (navLink) {
                if (appData.modules[moduleName]) {
                    navLink.parentElement.style.display = '';
                } else {
                    navLink.parentElement.style.display = 'none';
                }
            }
        }
    }

    function openModalModulos() {
        for (const moduleName in moduleToggles) {
            if (moduleToggles[moduleName]) {
                moduleToggles[moduleName].checked = appData.modules[moduleName];
            }
        }
        openModal(modalModulos);
    }

    function saveSettingsModulos(e) {
        e.preventDefault();
        for (const moduleName in moduleToggles) {
            if (moduleToggles[moduleName]) {
                appData.modules[moduleName] = moduleToggles[moduleName].checked;
            }
        }
        updateSidebarVisibility();
        showToast('Visibilidade dos módulos atualizada!');
        closeModal(modalModulos);
    }

    function openModalGerais() {
        inputMaxVersions.value = appData.settings.maxVersions;
        inputAllowTeacherAvailability.checked = appData.settings.allowTeacherAvailability;
        inputAutoResolveConflicts.checked = appData.settings.autoResolveConflicts;
        inputImportHolidays.checked = appData.settings.importHolidays;
        inputEnableSubstitutions.checked = appData.settings.enableSubstitutions;
        inputEnforceCapacity.checked = appData.settings.enforceCapacity;
        inputNotifyOnChange.checked = appData.settings.notifyOnChange;
        inputEnableTeacherReview.checked = appData.settings.enableTeacherReview;
        inputAllowWeekend.checked = appData.settings.allowWeekend;
        inputMaintenanceMode.checked = appData.settings.maintenanceMode;
        inputSuggestTemplate.checked = appData.settings.suggestTemplate;
        openModal(modalGerais);
    }

    function saveSettingsGerais(e) {
        e.preventDefault();
        appData.settings.maxVersions = parseInt(inputMaxVersions.value, 10);
        appData.settings.allowTeacherAvailability = inputAllowTeacherAvailability.checked;
        appData.settings.autoResolveConflicts = inputAutoResolveConflicts.checked;
        appData.settings.importHolidays = inputImportHolidays.checked;
        appData.settings.enableSubstitutions = inputEnableSubstitutions.checked;
        appData.settings.enforceCapacity = inputEnforceCapacity.checked;
        appData.settings.notifyOnChange = inputNotifyOnChange.checked;
        appData.settings.enableTeacherReview = inputEnableTeacherReview.checked;
        appData.settings.allowWeekend = inputAllowWeekend.checked;
        appData.settings.maintenanceMode = inputMaintenanceMode.checked;
        appData.settings.suggestTemplate = inputSuggestTemplate.checked;
        showToast('Configurações gerais salvas com sucesso!');
        closeModal(modalGerais);
    }

    function openPlaceholderModal(title, text) {
        placeholderModalTitle.textContent = title;
        placeholderModalText.textContent = text;
        openModal(modalPlaceholder);
    }

    document.getElementById('btn-config-horarios').addEventListener('click', openModalHorarios);
    formHorarios.addEventListener('submit', saveSettingsHorarios);

    document.getElementById('btn-config-notificacoes').addEventListener('click', openModalNotificacoes);
    formNotificacoes.addEventListener('submit', saveSettingsNotificacoes);
    document.getElementById('btn-config-aparencia').addEventListener('click', openModalAparencia);
    formAparencia.addEventListener('submit', saveSettingsAparencia);
    document.getElementById('btn-config-gerais').addEventListener('click', openModalGerais);
    formGerais.addEventListener('submit', saveSettingsGerais);
    document.getElementById('btn-config-modulos').addEventListener('click', openModalModulos);
    formModulos.addEventListener('submit', saveSettingsModulos);
    document.getElementById('btn-config-funcoes').addEventListener('click', () => openPlaceholderModal('Funções e Permissões', 'Área para gerenciar perfis (Ex: Administrador, Coordenador) e definir quais seções cada um pode acessar.'));
    document.getElementById('btn-config-integracoes').addEventListener('click', () => openPlaceholderModal('Integrações', 'Área para configurar conexões com outras plataformas (Ex: Google Agenda, Slack).'));
    document.getElementById('btn-config-backup').addEventListener('click', () => openPlaceholderModal('Backup e Exportação', 'Opções para gerar e baixar backups dos dados da instituição.'));
    document.getElementById('btn-config-regiao').addEventListener('click', () => openPlaceholderModal('Formato de Datas e Região', 'Configurações de fuso horário, idioma e formato de exibição de datas.'));
    document.getElementById('btn-config-seguranca').addEventListener('click', () => openPlaceholderModal('Segurança', 'Definição de políticas de senha, tempo de sessão e autenticação de dois fatores (2FA).'));
    document.getElementById('btn-config-api').addEventListener('click', () => openPlaceholderModal('API e Webhooks', 'Área para gerar e gerenciar chaves de API para integrações personalizadas.'));
    document.getElementById('btn-config-termos').addEventListener('click', () => openPlaceholderModal('Termos e Privacidade', 'Campos para inserir os links para as páginas de Termos de Uso e Política de Privacidade.'));

    userProfileArea.addEventListener('click', () => {
        showSection('profile');
        renderProfile();
    });

 

    document.getElementById('form-change-password').addEventListener('submit', (e) => {
        e.preventDefault();
        showToast('Alteração de senha desabilitada no protótipo.', 'info');
        closeModal(changePasswordModal);
    });

    document.querySelectorAll('.btn-modal-cancel').forEach(button => {
        button.addEventListener('click', () => {
            const modal = button.closest('.modal-overlay');
            if (modal) {
                closeModal(modal);
            }
        });
    });



    modalTabLinks.forEach(link => {
        link.addEventListener('click', (e) => {
            e.preventDefault();
            showTab(e.target.dataset.tab);
        });
    });

    themeCards.forEach(card => {
        card.addEventListener('click', () => {
            const themeName = card.dataset.themeName;
            const themeColors = THEME_PRESETS[themeName];

            applyTheme(themeColors);

            appData.settings.theme.name = themeName;
            appData.settings.theme.colors = { ...themeColors };

            openModalAparencia();
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

    updateCounts();
    renderTeachers();
    renderSubjects();
    renderSeries();
    renderClasses();
    renderClassrooms();
    renderTimeSlots();
    renderHolidays();
    renderLessonPlans();
    renderSidebarProfile();
    document.querySelectorAll('.quick-access-btn').forEach(button => {
        button.addEventListener('click', (e) => {
            const sectionId = e.target.closest('.quick-access-btn').dataset.section;
            if (sectionId && sectionId !== currentSectionId) {
                showSection(sectionId);
            }
        });
    });

    document.querySelectorAll('.modal-overlay').forEach(modal => {
        modal.addEventListener('click', (e) => {
            if (e.target === modal) {
                closeModal(modal);
            }
        });
    });

    applyTheme(appData.settings.theme.colors);
    updateSidebarVisibility();
});