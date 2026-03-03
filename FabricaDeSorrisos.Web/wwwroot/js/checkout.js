// ==========================================
// LÓGICA DE CHECKOUT (CEP, CPF, FRETE)
// ==========================================

function setPag(tipo) {
    document.getElementById('inputPagamento').value = tipo;
}

// --- MÁSCARA CPF ---
function mascaraCPF(i) {
    var v = i.value;
    if (isNaN(v[v.length - 1])) { // impede entrar outro caractere que não seja número
        i.value = v.substring(0, v.length - 1);
        return;
    }
    i.setAttribute("maxlength", "14");
    if (v.length == 3 || v.length == 7) i.value += ".";
    if (v.length == 11) i.value += "-";
}

// --- BUSCA CEP AUTOMÁTICA (VIACEP) ---
async function buscarEnderecoViaCep() {
    let cep = document.getElementById('inputCep').value.replace(/\D/g, ''); // Remove traços

    if (cep.length === 8) {
        // 1. Feedback visual
        document.getElementById('inputLogradouro').value = "...";

        try {
            // 2. Chama API ViaCEP
            const response = await fetch(`https://viacep.com.br/ws/${cep}/json/`);
            const data = await response.json();

            if (!data.erro) {
                // 3. Preenche os campos
                document.getElementById('inputLogradouro').value = data.logradouro;
                document.getElementById('inputBairro').value = data.bairro;
                document.getElementById('inputCidade').value = data.localidade;
                document.getElementById('inputUf').value = data.uf;

                // Foca no número para o usuário digitar
                document.getElementById('inputNumero').focus();

                // 4. Calcula o Frete (Chama sua API interna)
                calcularFreteInterno(cep);
            } else {
                alert("CEP não encontrado.");
                limparFormularioCep();
            }
        } catch (e) {
            console.error(e);
            alert("Erro ao buscar CEP.");
            limparFormularioCep();
        }
    }
}

function limparFormularioCep() {
    document.getElementById('inputLogradouro').value = "";
    document.getElementById('inputBairro').value = "";
    document.getElementById('inputCidade').value = "";
    document.getElementById('inputUf').value = "";
}

async function calcularFreteInterno(cep) {
    // Reaproveitando sua lógica de frete
    const response = await fetch('/Carrinho/CalcularFrete', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(cep)
    });

    if (response.ok) {
        const data = await response.json();

        // Atualiza visualmente o Frete
        document.getElementById('txtFrete').innerText = "R$ " + data.valor.toFixed(2);
        document.getElementById('msgFrete').innerHTML = `<i class='bi bi-truck'></i> Entrega em ${data.prazo} - R$ ${data.valor.toFixed(2)}`;

        // Atualiza o input hidden que vai pro Controller
        document.getElementById('inputValorFrete').value = data.valor;

        // Recupera o subtotal do input hidden que colocamos no HTML
        const subtotal = parseFloat(document.getElementById('subtotalInicial').value);

        // Calcula o novo total
        const total = subtotal + data.valor;
        document.getElementById('txtTotal').innerText = "R$ " + total.toFixed(2);
    }
}