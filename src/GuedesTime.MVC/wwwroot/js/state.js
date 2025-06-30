export const THEME_PRESETS = {
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

export let appData = {
    teachers: [
        { id: 't1', fullName: 'Alice Johnson', email: 'alice@example.com', document: null, subjectIds: ['sub1', 'sub3'] },
        { id: 't2', fullName: 'Bob Williams', email: 'bob@example.com', document: null, subjectIds: ['sub2', 'sub4', 'sub6'] },
        { id: 't3', fullName: 'Carol Davis', email: 'carol@example.com', document: null, subjectIds: ['sub1', 'sub2', 'sub5'] },
        { id: 't4', fullName: 'David Garcia', email: 'david@example.com', document: null, subjectIds: ['sub7', 'sub8'] },
        { id: 't5', fullName: 'Eva Martinez', email: 'eva@example.com', document: null, subjectIds: ['sub5', 'sub6'] },
        { id: 't6', fullName: 'Frank Rodriguez', email: 'frank@example.com', document: null, subjectIds: ['sub1', 'sub4'] }
    ],
    subjects: [
        { id: 'sub1', name: 'Matemática', code: 'MAT101', workloadHours: 60, seriesIds: ['s1', 's2'], teacherIds: ['t1', 't3', 't6'] },
        { id: 'sub2', name: 'Física', code: 'FIS201', workloadHours: 75, seriesIds: ['s2', 's3'], teacherIds: ['t2', 't3'] },
        { id: 'sub3', name: 'Química', code: 'QUI301', workloadHours: 70, seriesIds: ['s3'], teacherIds: ['t1'] },
        { id: 'sub4', name: 'Biologia', code: 'BIO401', workloadHours: 65, seriesIds: ['s1', 's2', 's3'], teacherIds: ['t2', 't6'] },
        { id: 'sub5', name: 'História', code: 'HIS101', workloadHours: 50, seriesIds: ['s1', 's3'], teacherIds: ['t3', 't5'] },
        { id: 'sub6', name: 'Geografia', code: 'GEO202', workloadHours: 50, seriesIds: ['s1', 's2'], teacherIds: ['t2', 't5'] },
        { id: 'sub7', name: 'Educação Física', code: 'EDF100', workloadHours: 30, seriesIds: ['s1', 's2', 's3'], teacherIds: ['t4'] },
        { id: 'sub8', name: 'Artes', code: 'ART101', workloadHours: 40, seriesIds: ['s1', 's2'], teacherIds: ['t4'] }
    ],
    series: [
        { id: 's1', name: '1º Ano', order: 1, subjectIds: ['sub1', 'sub4', 'sub5', 'sub6', 'sub7', 'sub8'] },
        { id: 's2', name: '2º Ano', order: 2, subjectIds: ['sub1', 'sub2', 'sub4', 'sub6', 'sub7', 'sub8'] },
        { id: 's3', name: '3º Ano', order: 3, subjectIds: ['sub2', 'sub3', 'sub4', 'sub5', 'sub7'] }
    ],
    classes: [
        { id: 'c1', name: 'Turma Alfa', year: 2025, shift: 'Manhã', seriesId: 's1', subjectTeacherPairings: [{ subjectId: 'sub1', teacherId: 't1' }, { subjectId: 'sub5', teacherId: 't3' }, { subjectId: 'sub4', teacherId: 't6' }] },
        { id: 'c2', name: 'Turma Beta', year: 2025, shift: 'Tarde', seriesId: 's2', subjectTeacherPairings: [{ subjectId: 'sub2', teacherId: 't2' }, { subjectId: 'sub1', teacherId: 't1' }] },
        { id: 'c3', name: 'Turma Gama', year: 2025, shift: 'Manhã', seriesId: 's3', subjectTeacherPairings: [{ subjectId: 'sub3', teacherId: 't1' }, { subjectId: 'sub2', teacherId: 't3' }, { subjectId: 'sub5', teacherId: 't5' }] },
        { id: 'c4', name: 'Turma Delta', year: 2025, shift: 'Tarde', seriesId: 's1', subjectTeacherPairings: [{ subjectId: 'sub6', teacherId: 't5' }, { subjectId: 'sub8', teacherId: 't4' }] }
    ],
    classrooms: [
        { id: 'cr1', name: 'Sala 101', capacity: 30, location: 'Prédio Principal', availability: [] },
        { id: 'cr2', name: 'Laboratório 205', capacity: 20, location: 'Ala de Ciências', availability: [] },
        { id: 'cr3', name: 'Ginásio', capacity: 50, location: 'Centro Esportivo', availability: [] },
        { id: 'cr4', name: 'Ateliê de Artes 301', capacity: 25, location: 'Prédio de Artes', availability: [] }
    ],
    timeSlots: [
        { id: 'ts1', day: 'Segunda-feira', startTime: '08:00', endTime: '08:50' },
        { id: 'ts2', day: 'Segunda-feira', startTime: '09:00', endTime: '09:50' },
        { id: 'ts3', day: 'Terça-feira', startTime: '08:00', endTime: '08:50' },
        { id: 'ts4', day: 'Terça-feira', startTime: '10:00', endTime: '10:50' },
        { id: 'ts5', day: 'Quarta-feira', startTime: '08:00', endTime: '08:50' },
        { id: 'ts6', day: 'Quarta-feira', startTime: '09:00', endTime: '09:50' },
        { id: 'ts7', day: 'Quinta-feira', startTime: '13:00', endTime: '13:50' },
        { id: 'ts8', day: 'Sexta-feira', startTime: '14:00', endTime: '14:50' }
    ],
    holidays: [
        { id: 'h1', date: '2025-01-01', description: 'Ano Novo' },
        { id: 'h2', date: '2025-04-21', description: 'Dia de Tiradentes' },
        { id: 'h3', date: '2025-09-07', description: 'Dia da Independência' }
    ],
    lessonPlans: [
        { id: 'lp1', createdDate: '2025-05-20', status: 'Aprovado', version: 1, schedule: {} },
        { id: 'lp2', createdDate: '2025-05-27', status: 'Aprovado', version: 2, schedule: {} },
        { id: 'lp3', createdDate: '2025-06-01', status: 'Aprovado', version: 3, schedule: {} },
        { id: 'lp4', createdDate: '2025-06-05', status: 'Aprovado', version: 4, schedule: {} },
        { id: 'lp5', createdDate: '2025-06-10', status: 'Aprovado', version: 5, schedule: {} },
        { id: 'lp6', createdDate: '2025-06-11', status: 'Pendente', version: 6, schedule: {} },
        { id: 'lp7', createdDate: '2025-06-12', status: 'Pendente', version: 7, schedule: {} },
        { id: 'lp8', createdDate: '2025-06-08', status: 'Em Conflito', version: 1, schedule: {} }
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
