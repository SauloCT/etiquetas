// Arquivo JavaScript para funcionalidades de etiquetas
// Criado para resolver problemas de cache e JSON parsing

function AdicionaItemCorrigido() {
    var deposito = document.getElementById("DepositoNome").value;
    var produto = document.getElementById("Produto").value;
    var codigoNFe = document.getElementById("CodigoNFe").value;
    var produtoId = "";
    
    // Se o código do produto foi preenchido, use-o para buscar o produto
    if (codigoNFe !== "") {
        // Procura o produto pelo código
        var datalist = document.getElementById("codigoProduto");
        var options = datalist.getElementsByTagName("option");
        for (var i = 0; i < options.length; i++) {
            if (options[i].value === codigoNFe) {
                produto = options[i].getAttribute("data-nome");
                produtoId = options[i].getAttribute("data-id");
                break;
            }
        }
    } else if (produto !== "") {
        // Procura o produto pelo nome
        var datalist = document.getElementById("produto");
        var options = datalist.getElementsByTagName("option");
        for (var i = 0; i < options.length; i++) {
            if (options[i].value === produto) {
                codigoNFe = options[i].getAttribute("data-codigo");
                produtoId = options[i].getAttribute("data-id");
                break;
            }
        }
    }
    
    if (produto === "" || produtoId === "") {
        window.alert("O campo produto ou código do produto deve estar preenchido.");
        resetAddButton();
        return;
    }
    
    
    document.getElementById("DepositoNome").value = "";
    document.getElementById("Produto").value = "";
    document.getElementById("CodigoNFe").value = "";
    
    if(!document.getElementById(produtoId)){
        var url = "/Etiquetas/GetProduto";
        
        // Usar jQuery com configuração explícita para JSON
        $.ajax({
            url: url,
            type: 'POST',
            data: { "deposito": deposito, "produto": produtoId },
            dataType: 'json', // Força o jQuery a tratar como JSON
            success: function(data) {
                // Verificar se houve erro
                if (data.error) {
                    alert("Erro: " + data.error);
                    resetAddButton();
                    return;
                }
                
                // data já é um objeto JavaScript, não precisa de parse
                var produto = data;
                var row = document.getElementById("Lista-de-itens").insertRow(-1);
                row.id = produto.id; // Usar 'id' minúsculo conforme retornado pelo servidor

                var cell1 = row.insertCell(0);  // Código
                var cell2 = row.insertCell(1);  // Produto
                var cell3 = row.insertCell(2);  // Quantidade
                var cell4 = row.insertCell(3);  // Preço
                var cell5 = row.insertCell(4);  // Lote
                var cell6 = row.insertCell(5);  // Nº Série
                var cell7 = row.insertCell(6);  // Unidade
                var cell8 = row.insertCell(7);  // Marca
                var cell9 = row.insertCell(8);  // Ação

                cell1.innerHTML = produto.codigoNFe || produto.codigo || '';
                cell2.innerHTML = produto.nome || '';
                cell3.innerHTML = '<input type="number" id="' + produto.id + 'Quantidade" min="1" step="1" value="1" class="form-control form-control-sm" onchange="AtualizaQuantidadeItem(\'' + produto.id + '\')">';
                cell4.innerHTML = parseFloat(produto.precoVenda || 0).toFixed(2);
                cell5.innerHTML = '<input type="text" id="' + produto.id + 'Lote" class="form-control form-control-sm" onchange="AtualizaLoteItem(\'' + produto.id + '\')">';
                cell6.innerHTML = '<input type="text" id="' + produto.id + 'NumeroSerie" class="form-control form-control-sm" onchange="AtualizaNumeroSerieItem(\'' + produto.id + '\')">';
                cell7.innerHTML = produto.unidade || 'UN';
                cell8.innerHTML = produto.marca || '';
                cell9.innerHTML = '<button type="button" class="btn btn-danger btn-sm" onclick="RemoveItem(\'' + produto.id + '\')"><i class="fas fa-trash"></i></button>';
                
                // Adiciona input hidden para o form com os valores iniciais
                document.getElementById("form").innerHTML += '<input type="hidden" id="' + produto.id + 'form" name="itens" value="' + produto.id + ',1,,">'; // Inicializa com id, quantidade=1, lote vazio e numeroSerie vazio
                
                // Reset button state after successful addition
                resetAddButton();
            },
            error: function(xhr, status, error) {
                alert("Erro ao adicionar o produto. Por favor, tente novamente.");
                resetAddButton();
            }
        });
    } else {
        resetAddButton();
    }
}

// Substituir a função original quando o documento estiver pronto
$(document).ready(function() {
    // Sobrescrever a função global
    window.AdicionaItem = AdicionaItemCorrigido;
}); 