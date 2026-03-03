// ==========================================
// 1. RODA AUTOMATICAMENTE AO ABRIR A PÁGINA
// ==========================================
document.addEventListener("DOMContentLoaded", function () {
    pintarFavoritosNaTela();
});

function pintarFavoritosNaTela() {
    // Pergunta para a nossa nova API quais são os IDs favoritados do cliente
    $.get("/api/Favoritos/meus-ids")
        .done(function (idsFavoritados) {

            // Se o servidor retornar uma lista válida
            if (Array.isArray(idsFavoritados) && idsFavoritados.length > 0) {

                // Varre todos os botões de coração da tela atual
                $(".btn-favorito").each(function () {
                    var btn = $(this);
                    var idBrinquedo = btn.data("id"); // Pega o id do HTML (data-id="@item.Id")

                    // Se esse brinquedo estiver na lista de favoritos do banco...
                    if (idsFavoritados.includes(idBrinquedo)) {
                        var icon = btn.find("i");
                        // Pinta o coração de vermelho!
                        icon.removeClass("bi-heart text-secondary").addClass("bi-heart-fill text-danger");
                    }
                });
            }
        })
        .fail(function () {
            // Fica em silêncio. Se não estiver logado, os corações ficam normais (vazios).
        });
}

// ==========================================
// 2. AÇÃO DE CLICAR NO CORAÇÃO (Seu código original melhorado)
// ==========================================
function toggleFavorito(btn, brinquedoId) {
    // Verificação de Login
    var isAuth = $(btn).data('auth');

    if (!isAuth || isAuth.toString().toLowerCase() === "false") {
        window.location.href = "/Account/Login";
        return;
    }

    var icon = $(btn).find("i");
    var isTrash = icon.hasClass("bi-trash") || $(btn).hasClass("btn-remover");

    // Chamada à API para Favoritar/Desfavoritar
    $.post("/api/Favoritos/toggle/" + brinquedoId)
        .done(function (response) {

            if (response.success) {
                // AÇÃO DE REMOVER DA TELA DE "MEUS FAVORITOS" (LIXEIRA)
                if (isTrash) {
                    var card = $("#card-favorito-" + brinquedoId);
                    card.fadeOut(400, function () {
                        $(this).remove();
                        if ($("#lista-favoritos").children(":visible").length === 0) {
                            location.reload(); // Atualiza pra mostrar "Sem favoritos"
                        }
                    });
                }
                // AÇÃO DE PINTAR/DESPINTAR NA HOME, BUSCA E DETALHES
                else {
                    if (response.action === "added") {
                        icon.removeClass("bi-heart text-secondary").addClass("bi-heart-fill text-danger");
                    } else {
                        icon.removeClass("bi-heart-fill text-danger").addClass("bi-heart text-secondary");
                    }
                }
            }
        })
        .fail(function (xhr) {
            console.error("Erro API:", xhr.responseText);
            if (xhr.status === 401) {
                window.location.href = "/Account/Login";
            } else {
                alert("Não foi possível atualizar o favorito.");
            }
        });
}