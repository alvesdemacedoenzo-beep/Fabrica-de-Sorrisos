$(document).ready(function () {
    const inputBusca = $("#inputBusca");
    const sugestoesBox = $("#sugestoesBox");
    const formBusca = $("#formBuscaPrincipal");

    // 1. Quando o usuário digita
    inputBusca.on("input", function () {
        let termo = $(this).val();

        if (termo.length < 2) {
            sugestoesBox.hide();
            return;
        }

        $.ajax({
            url: "/Home/BuscarSugestoes",
            data: { termo: termo },
            success: function (dados) {
                sugestoesBox.empty();

                if (dados.length > 0) {
                    dados.forEach(function (item) {
                        // Cria um item clicável
                        let div = `<div class="list-group-item list-group-item-action item-sugestao" style="cursor: pointer;">
                                        <i class="bi bi-search me-2 text-muted"></i> ${item}
                                   </div>`;
                        sugestoesBox.append(div);
                    });
                    sugestoesBox.show();
                } else {
                    sugestoesBox.hide();
                }
            }
        });
    });

    // 2. Quando clica em uma sugestão
    $(document).on("click", ".item-sugestao", function () {
        let texto = $(this).text().trim(); // Pega o texto (ex: "Boneca")
        inputBusca.val(texto);             // Joga na barra de pesquisa
        sugestoesBox.hide();               // Esconde a lista
        formBusca.submit();                // <--- MANDA BUSCAR NA HORA!
    });

    // 3. Clicar fora fecha a caixa
    $(document).on("click", function (e) {
        if (!$(e.target).closest("#inputBusca, #sugestoesBox").length) {
            sugestoesBox.hide();
        }
    });









    // --- LÓGICA PARA OS BOTÕES DE VOLTAR (CARRINHO E FAVORITOS) ---
    $(".btn-voltar-favoritos, .btn-voltar-carrinho").on("click", function (e) {
        e.preventDefault();

        // Captura a URL de onde o usuário veio
        var vindoDe = document.referrer;

        // Verifica se a página anterior causaria um loop ou se não há histórico
        if (vindoDe.includes("Carrinho") || vindoDe.includes("Favoritos") || vindoDe === "") {
            // Se for loop, força o retorno para a Home
            window.location.href = "/";
        } else {
            // Se veio de outra página, volta direto ignorando cliques internos
            window.location.href = vindoDe;
        }
    });

});