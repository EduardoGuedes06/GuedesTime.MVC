import { appData } from '../state.js';
import { showToast, openModal, closeModal } from '../ui.js';

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
const btnChangePassword = document.getElementById('btn-change-password');
const changePasswordModal = document.getElementById('modal-change-password');
const formChangePassword = document.getElementById('form-change-password');

let cropper;

export function renderSidebarProfile() {
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

export function renderProfile() {
    if (!document.getElementById('profile')) return;

    const user = appData.currentUser;
    document.getElementById('profile-user-name').textContent = user.name;
    document.getElementById('profile-user-email').textContent = user.email;
    profilePictureImg.src = user.profilePictureUrl || '/imagens/userPlaceholder.png';

    const memberSinceDate = new Date(user.memberSince);
    document.getElementById('profile-member-since').textContent = memberSinceDate.toLocaleDateString('pt-BR', {
        day: '2-digit',
        month: 'long',
        year: 'numeric'
    });

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

export function initProfile() {
    if (!document.getElementById('profile')) return;

    if (profilePictureContainer) {
        profilePictureContainer.addEventListener('click', () => {
            if (!appData.currentUser.profilePictureUrl) {
                profilePictureInput.click();
            } else {
                imageViewerSrc.src = appData.currentUser.profilePictureUrl;
                openModal(imageViewerModal);
            }
        });
    }
    if (btnTriggerUpload) {
        btnTriggerUpload.addEventListener('click', () => {
            profilePictureInput.click();
        });
    }
    if (profilePictureInput) {
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
    }
    if (btnRemovePhoto) {
        btnRemovePhoto.addEventListener('click', () => {
            appData.currentUser.profilePictureUrl = null;
            renderProfile();
            renderSidebarProfile();
            closeModal(imageViewerModal);
            showToast('Foto de perfil removida.', 'info');
        });
    }
    if (cropAndSaveBtn) {
        cropAndSaveBtn.addEventListener('click', () => {
            if (cropper) {
                const canvas = cropper.getCroppedCanvas({ width: 256, height: 256 });
                appData.currentUser.profilePictureUrl = canvas.toDataURL();
                renderProfile();
                renderSidebarProfile();
            }
            closeModal(cropModal);
            showToast('Imagem de perfil atualizada com sucesso!', 'success');
        });
    }
    if (btnChangePassword) {
        btnChangePassword.addEventListener('click', () => openModal(changePasswordModal));
    }
    if (formChangePassword) {
        formChangePassword.addEventListener('submit', (e) => {
            e.preventDefault();
            showToast('Alteração de senha desabilitada no protótipo.', 'info');
            closeModal(changePasswordModal);
        });
    }
}