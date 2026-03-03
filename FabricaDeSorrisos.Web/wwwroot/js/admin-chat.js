const API_URL = "/api/Suporte";
let currentUser = null;
let abaAtual = "pendente";

function mudarAba(status) {
    abaAtual = status;

    const tabPendente = document.getElementById("tab-pendente");
    const tabEncerrado = document.getElementById("tab-encerrado");

    if (status === 'pendente') {
        tabPendente.classList.add('active');
        tabEncerrado.classList.remove('active');
    } else {
        tabPendente.classList.remove('active');
        tabEncerrado.classList.add('active');
    }

    currentUser = null;
    esconderChat();
    carregarUsuarios();
}

// ... (carregarUsuarios mantém igual) ...

async function carregarUsuarios() {
    const lista = document.getElementById("listaUsuarios");
    if (!lista) return;

    try {
        const resp = await fetch(`${API_URL}/conversas?status=${abaAtual}`);
        if (!resp.ok) return;

        const conversas = await resp.json();
        lista.innerHTML = "";

        if (!conversas || conversas.length === 0) {
            lista.innerHTML = `<div class="text-center p-5 text-muted small border-bottom">Nenhum atendimento em "${abaAtual}".</div>`;
            return;
        }

        conversas.forEach(c => {
            const item = document.createElement("button");
            const isActive = currentUser === c.usuarioId;
            let classesBase = "list-group-item list-group-item-action p-3 border-bottom lh-sm";
            if (isActive) item.className = `${classesBase} active bg-primary text-white`;
            else item.className = `${classesBase} bg-white text-dark`;

            const hora = new Date(c.data).toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' });

            item.innerHTML = `
                <div class="d-flex w-100 align-items-center justify-content-between mb-1">
                    <strong class="mb-0 text-truncate" style="max-width: 70%; font-size: 0.95rem;">${c.nome}</strong>
                    <small class="${isActive ? 'text-white-50' : 'text-muted'}" style="font-size: 0.75rem;">${hora}</small>
                </div>
                <div class="col-10 mb-0 small text-truncate ${isActive ? 'text-white-50' : 'text-muted'}">${c.ultimaMensagem}</div>
            `;
            item.onclick = () => abrirChatAdmin(c.usuarioId, c.nome);
            lista.appendChild(item);
        });

    } catch (e) { console.error("Erro JS:", e); }
}

async function abrirChatAdmin(userId, nomeUsuario) {
    currentUser = userId;

    document.getElementById("chatHeader").classList.remove("d-none");
    document.getElementById("adminChatBody").style.display = "block";
    document.getElementById("adminChatInput").classList.remove("d-none");

    if (nomeUsuario) {
        document.getElementById("nomeUsuarioChat").innerText = nomeUsuario;
    }

    // --- LÓGICA DO STATUS E BOTÃO DE ENCERRAR ---
    const statusLabel = document.getElementById("statusAtendimento");
    const btnEncerrar = document.querySelector("button[onclick='encerrarAtendimento()']");

    if (abaAtual === 'encerrado') {
        statusLabel.innerText = "Atendimento Finalizado";
        statusLabel.className = "text-danger fw-bold";
        if (btnEncerrar) btnEncerrar.classList.add("d-none"); // Esconde botão se já encerrou
        document.getElementById("adminChatInput").classList.add("d-none"); // Bloqueia digitação (opcional)
    } else {
        statusLabel.innerText = "Atendimento em andamento";
        statusLabel.className = "text-success fw-bold";
        if (btnEncerrar) btnEncerrar.classList.remove("d-none");
        document.getElementById("adminChatInput").classList.remove("d-none");
    }

    carregarUsuarios();

    try {
        const resp = await fetch(`${API_URL}/historico?targetUserId=${userId}`);
        if (resp.ok) {
            const mensagens = await resp.json();
            renderizarMensagensAdmin(mensagens);
        }
    } catch (e) { console.error(e); }
}

// ... (Resto das funções enviarResposta, encerrarAtendimento, renderizarMensagensAdmin iguais) ...
function renderizarMensagensAdmin(lista) {
    const body = document.getElementById("adminChatBody");
    body.innerHTML = "";
    if (!lista || lista.length === 0) {
        body.innerHTML = '<div class="h-100 d-flex align-items-center justify-content-center text-muted small">Nenhuma mensagem.</div>';
        return;
    }
    lista.forEach(m => {
        const divRow = document.createElement("div");
        divRow.className = m.respondidoPorAdmin ? "d-flex justify-content-end mb-3" : "d-flex justify-content-start mb-3";
        const balao = document.createElement("div");
        balao.className = m.respondidoPorAdmin ? "bg-primary text-white p-2 px-3 rounded-3 shadow-sm" : "bg-white border p-2 px-3 rounded-3 shadow-sm";
        balao.style.maxWidth = "75%";
        balao.innerText = m.texto;
        const hora = document.createElement("div");
        hora.className = "text-end mt-1";
        hora.style.fontSize = "0.6rem";
        hora.style.opacity = "0.7";
        hora.innerText = new Date(m.dataEnvio).toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' });
        balao.appendChild(hora);
        divRow.appendChild(balao);
        body.appendChild(divRow);
    });
    body.scrollTop = body.scrollHeight;
}

async function enviarResposta(e) {
    e.preventDefault();
    const input = document.getElementById("adminMsgInput");
    const texto = input.value.trim();
    if (!texto || !currentUser) return;
    try {
        const resp = await fetch(`${API_URL}/enviar`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ texto: texto, destinatarioId: currentUser })
        });
        if (resp.ok) {
            input.value = "";
            abrirChatAdmin(currentUser);
            carregarUsuarios();
        }
    } catch (e) { console.error("Erro envio:", e); }
}

async function encerrarAtendimento() {
    if (!currentUser) return;
    if (!confirm("Tem certeza que deseja encerrar este atendimento?")) return;
    try {
        const resp = await fetch(`${API_URL}/encerrar`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(currentUser)
        });
        if (resp.ok) {
            esconderChat();
            currentUser = null;
            carregarUsuarios();
        }
    } catch (e) { console.error("Erro encerrar:", e); }
}

function esconderChat() {
    document.getElementById("chatHeader").classList.add("d-none");
    document.getElementById("adminChatBody").style.display = "none";
    document.getElementById("adminChatInput").classList.add("d-none");
}

document.addEventListener("DOMContentLoaded", () => {
    carregarUsuarios();
    setInterval(() => {
        if (currentUser) abrirChatAdmin(currentUser); // Atualiza chat aberto
        else carregarUsuarios(); // Atualiza lista se ninguém selecionado
    }, 5000);
});