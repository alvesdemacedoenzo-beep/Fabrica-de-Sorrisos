// Ao carregar a página, atualiza o número vermelho no carrinho
document.addEventListener("DOMContentLoaded", function () {
    atualizarBadgeCarrinho();
});

async function atualizarBadgeCarrinho() {
    try {
        const response = await fetch("/api/carrinho/resumo");
        if (response.ok) {
            const data = await response.json();
            // Procura todos os elementos com a classe .badge-carrinho e atualiza o número
            document.querySelectorAll(".badge-carrinho").forEach(el => {
                el.innerText = data.totalItens;
                // Se tiver itens, remove a classe 'd-none' (se quiser esconder quando for 0)
                if (data.totalItens > 0) el.classList.remove("d-none");
            });
        }
    } catch (error) {
        console.error("Erro ao atualizar carrinho:", error);
    }
}

async function adicionarAoCarrinho(btn, brinquedoId) {
    // 1. Efeito visual "Carregando..."
    const htmlOriginal = btn.innerHTML;
    btn.innerHTML = '<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>';
    btn.disabled = true;

    try {
        // 2. Chama a API
        const response = await fetch(`/api/carrinho/adicionar/${brinquedoId}`, {
            method: "POST",
            headers: { "Content-Type": "application/json" }
        });

        // Se der erro 401 (Não autorizado), manda pro login
        if (response.status === 401) {
            window.location.href = "/Account/Login";
            return;
        }

        const data = await response.json();

        if (data.success) {
            // 3. Sucesso! Atualiza o ícone lá em cima
            document.querySelectorAll(".badge-carrinho").forEach(el => el.innerText = data.totalItens);

            // 4. Feedback visual no botão (Verde)
            btn.classList.remove("btn-primary");
            btn.classList.add("btn-success");
            btn.innerHTML = '<i class="bi bi-check-lg"></i> Adicionado!';

            // Volta ao normal depois de 1.5 segundos
            setTimeout(() => {
                btn.classList.remove("btn-success");
                btn.classList.add("btn-primary");
                btn.innerHTML = htmlOriginal;
                btn.disabled = false;
            }, 1500);
        } else {
            // Erro de negócio (sem estoque, etc)
            alert(data.message);
            btn.innerHTML = htmlOriginal;
            btn.disabled = false;
        }

    } catch (error) {
        console.error("Erro:", error);
        alert("Ocorreu um erro ao conectar com o servidor.");
        btn.innerHTML = htmlOriginal;
        btn.disabled = false;
    }
}