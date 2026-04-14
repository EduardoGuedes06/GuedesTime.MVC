import { loadingService } from '../services/loadingService.js';

function clamp(n, min, max) {
    return Math.max(min, Math.min(max, n));
}

function getImageFromFile(file) {
    return new Promise((resolve, reject) => {
        const url = URL.createObjectURL(file);
        const img = new Image();
        img.onload = () => {
            URL.revokeObjectURL(url);
            resolve(img);
        };
        img.onerror = () => {
            URL.revokeObjectURL(url);
            reject(new Error('Falha ao carregar imagem.'));
        };
        img.src = url;
    });
}

function drawPreview(state) {
    const { canvas, ctx, img, crop } = state;
    const w = canvas.width;
    const h = canvas.height;

    ctx.clearRect(0, 0, w, h);
    if (!img) {
        ctx.fillStyle = '#f3f4f6';
        ctx.fillRect(0, 0, w, h);
        ctx.fillStyle = '#6b7280';
        ctx.font = '14px sans-serif';
        ctx.fillText('Selecione uma imagem para recortar', 14, 28);
        return;
    }

    // Área de recorte (quadrada) no centro do canvas
    const boxSize = Math.floor(Math.min(w, h) * 0.72);
    const boxX = Math.floor((w - boxSize) / 2);
    const boxY = Math.floor((h - boxSize) / 2);

    // Ajuste do "pan" (crop.x/y) com limites para não deixar área vazia dentro do box
    const scale = crop.scale;
    const iw = img.naturalWidth;
    const ih = img.naturalHeight;
    const scaledW = iw * scale;
    const scaledH = ih * scale;

    const minX = boxX + boxSize - scaledW;
    const maxX = boxX;
    const minY = boxY + boxSize - scaledH;
    const maxY = boxY;
    crop.x = clamp(crop.x, minX, maxX);
    crop.y = clamp(crop.y, minY, maxY);

    // Fundo
    ctx.fillStyle = '#111827';
    ctx.fillRect(0, 0, w, h);

    // Imagem
    ctx.drawImage(img, crop.x, crop.y, scaledW, scaledH);

    // Overlay escuro fora do box
    ctx.save();
    ctx.fillStyle = 'rgba(0,0,0,0.55)';
    ctx.beginPath();
    ctx.rect(0, 0, w, h);
    ctx.rect(boxX, boxY, boxSize, boxSize);
    ctx.fill('evenodd');
    ctx.restore();

    // Borda do box
    ctx.strokeStyle = 'rgba(255,255,255,0.9)';
    ctx.lineWidth = 2;
    ctx.strokeRect(boxX + 1, boxY + 1, boxSize - 2, boxSize - 2);

    // Guias
    ctx.strokeStyle = 'rgba(255,255,255,0.25)';
    ctx.lineWidth = 1;
    const third = boxSize / 3;
    ctx.beginPath();
    ctx.moveTo(boxX + third, boxY);
    ctx.lineTo(boxX + third, boxY + boxSize);
    ctx.moveTo(boxX + 2 * third, boxY);
    ctx.lineTo(boxX + 2 * third, boxY + boxSize);
    ctx.moveTo(boxX, boxY + third);
    ctx.lineTo(boxX + boxSize, boxY + third);
    ctx.moveTo(boxX, boxY + 2 * third);
    ctx.lineTo(boxX + boxSize, boxY + 2 * third);
    ctx.stroke();

    state.box = { x: boxX, y: boxY, size: boxSize };
}

function exportCroppedDataUrl(state, sizePx = 256, mime = 'image/png') {
    const { img, crop, box } = state;
    if (!img || !box) return '';

    const scale = crop.scale;
    const iw = img.naturalWidth;
    const ih = img.naturalHeight;
    const scaledW = iw * scale;
    const scaledH = ih * scale;

    // Converter coords do box (canvas) para coords na imagem original
    const sx = (box.x - crop.x) / scale;
    const sy = (box.y - crop.y) / scale;
    const sSize = box.size / scale;

    const out = document.createElement('canvas');
    out.width = sizePx;
    out.height = sizePx;
    const octx = out.getContext('2d', { alpha: true });
    octx.imageSmoothingEnabled = true;
    octx.imageSmoothingQuality = 'high';
    octx.drawImage(img, sx, sy, sSize, sSize, 0, 0, sizePx, sizePx);

    // Garantir limite de tamanho no backend; PNG costuma ser ok.
    return out.toDataURL(mime, 0.92);
}

export function initializeUserAvatarUploader(container) {
    const fileInput = container.querySelector('input[type="file"][data-avatar-file]');
    const canvas = container.querySelector('canvas[data-avatar-canvas]');
    const zoom = container.querySelector('input[type="range"][data-avatar-zoom]');
    const hiddenDataUrl = container.querySelector('input[type="hidden"][data-avatar-dataurl]');
    const form = container.closest('form');
    const btnReset = container.querySelector('[data-avatar-reset]');
    const btnSave = container.querySelector('[data-avatar-save]');
    const btnPick = container.querySelector('[data-avatar-pick]');
    const fileNameEl = container.querySelector('[data-avatar-filename]');
    const editor = container.querySelector('[data-avatar-editor]');

    if (!fileInput || !canvas || !zoom || !hiddenDataUrl || !form || !btnSave) {
        console.error('Uploader de avatar: elementos obrigatórios não encontrados.', { fileInput, canvas, zoom, hiddenDataUrl, form, btnSave });
        return;
    }

    const ctx = canvas.getContext('2d', { alpha: true });
    const state = {
        canvas,
        ctx,
        img: null,
        box: null,
        crop: { x: 0, y: 0, scale: 1 },
        drag: { active: false, startX: 0, startY: 0, baseX: 0, baseY: 0 }
    };

    const syncFromZoom = () => {
        if (!state.img) return;
        const z = parseFloat(zoom.value || '1');
        // 1..3
        const scale = clamp(z, 1, 3);
        state.crop.scale = scale;
        drawPreview(state);
    };

    const reset = () => {
        state.img = null;
        state.box = null;
        state.crop = { x: 0, y: 0, scale: 1 };
        state.drag = { active: false, startX: 0, startY: 0, baseX: 0, baseY: 0 };
        zoom.value = '1';
        hiddenDataUrl.value = '';
        if (fileNameEl) fileNameEl.textContent = 'Nenhum arquivo selecionado';
        if (editor) editor.style.display = 'none';
        drawPreview(state);
    };

    const fitImage = () => {
        const { img } = state;
        if (!img) return;

        // define canvas size (fixo, mas responsivo via CSS)
        canvas.width = 420;
        canvas.height = 320;

        // scale mínima para cobrir o box
        const w = canvas.width;
        const h = canvas.height;
        const boxSize = Math.floor(Math.min(w, h) * 0.72);
        const iw = img.naturalWidth;
        const ih = img.naturalHeight;
        const minScale = Math.max(boxSize / iw, boxSize / ih);

        // Usar o range 1..3 como multiplicador sobre a escala mínima
        const z = clamp(parseFloat(zoom.value || '1'), 1, 3);
        state.crop.scale = minScale * z;

        const scaledW = iw * state.crop.scale;
        const scaledH = ih * state.crop.scale;
        state.crop.x = Math.floor((w - scaledW) / 2);
        state.crop.y = Math.floor((h - scaledH) / 2);
        drawPreview(state);
    };

    drawPreview(state);

    if (btnPick) {
        btnPick.addEventListener('click', (ev) => {
            ev.preventDefault();
            fileInput.click();
        });
    }

    fileInput.addEventListener('change', async () => {
        const file = fileInput.files && fileInput.files[0];
        if (!file) {
            reset();
            return;
        }
        if (!file.type.startsWith('image/')) {
            reset();
            return;
        }
        try {
            loadingService.show('Carregando imagem...');
            state.img = await getImageFromFile(file);
            zoom.value = '1';
            hiddenDataUrl.value = '';
            if (fileNameEl) fileNameEl.textContent = file.name;
            if (editor) editor.style.display = '';
            fitImage();
        } catch (e) {
            console.error(e);
            reset();
        } finally {
            loadingService.hide();
        }
    });

    zoom.addEventListener('input', () => {
        // Recalcular scale mantendo o centro visual
        if (!state.img) return;
        // Refit baseado no zoom atual para manter cobertura e centralização razoável
        fitImage();
    });

    const getPointer = (ev) => {
        const rect = canvas.getBoundingClientRect();
        const x = (ev.clientX - rect.left) * (canvas.width / rect.width);
        const y = (ev.clientY - rect.top) * (canvas.height / rect.height);
        return { x, y };
    };

    canvas.addEventListener('pointerdown', (ev) => {
        if (!state.img) return;
        canvas.setPointerCapture(ev.pointerId);
        const p = getPointer(ev);
        state.drag.active = true;
        state.drag.startX = p.x;
        state.drag.startY = p.y;
        state.drag.baseX = state.crop.x;
        state.drag.baseY = state.crop.y;
    });

    canvas.addEventListener('pointermove', (ev) => {
        if (!state.drag.active || !state.img) return;
        const p = getPointer(ev);
        const dx = p.x - state.drag.startX;
        const dy = p.y - state.drag.startY;
        state.crop.x = state.drag.baseX + dx;
        state.crop.y = state.drag.baseY + dy;
        drawPreview(state);
    });

    canvas.addEventListener('pointerup', () => {
        state.drag.active = false;
    });

    canvas.addEventListener('pointercancel', () => {
        state.drag.active = false;
    });

    if (btnReset) {
        btnReset.addEventListener('click', (ev) => {
            ev.preventDefault();
            fileInput.value = '';
            reset();
        });
    }

    btnSave.addEventListener('click', (ev) => {
        ev.preventDefault();
        if (!state.img) return;
        const dataUrl = exportCroppedDataUrl(state, 256, 'image/png');
        hiddenDataUrl.value = dataUrl;
        form.requestSubmit();
    });

    form.addEventListener('submit', () => {
        if (!form.hasAttribute('data-no-loading')) {
            loadingService.show('Processando...');
        }
    });
}

