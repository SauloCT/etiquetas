// Arquivo JavaScript otimizado para funcionalidades de etiquetas
// Melhorias de performance: cache de elementos, busca otimizada, event delegation

// Cache de elementos DOM para evitar buscas repetitivas
const ElementCache = {
    deposito: null,
    produto: null,
    codigoNFe: null,
    form: null,
    listaItens: null,
    datalistProduto: null,
    datalistCodigo: null,
    
    // Inicializa o cache de elementos
    init() {
        this.deposito = document.getElementById("DepositoNome");
        this.produto = document.getElementById("Produto");
        this.codigoNFe = document.getElementById("CodigoNFe");
        this.form = document.getElementById("form");
        this.listaItens = document.getElementById("Lista-de-itens");
        this.datalistProduto = document.getElementById("produto");
        this.datalistCodigo = document.getElementById("codigoProduto");
    },
    
    // Obtém elemento do cache ou busca no DOM
    get(elementId) {
        if (!this[elementId]) {
            this[elementId] = document.getElementById(elementId);
        }
        return this[elementId];
    }
};

// Cache para opções de datalist para evitar buscas repetitivas
const DatalistCache = {
    produtoOptions: new Map(),
    codigoOptions: new Map(),
    
    // Inicializa o cache de opções
    init() {
        this.cacheProdutoOptions();
        this.cacheCodigoOptions();
    },
    
    // Cache das opções de produto
    cacheProdutoOptions() {
        if (ElementCache.datalistProduto) {
            const options = ElementCache.datalistProduto.getElementsByTagName("option");
            for (let i = 0; i < options.length; i++) {
                const option = options[i];
                this.produtoOptions.set(option.value, {
                    nome: option.value,
                    codigo: option.getAttribute("data-codigo"),
                    id: option.getAttribute("data-id")
                });
            }
        }
    },
    
    // Cache das opções de código
    cacheCodigoOptions() {
        if (ElementCache.datalistCodigo) {
            const options = ElementCache.datalistCodigo.getElementsByTagName("option");
            for (let i = 0; i < options.length; i++) {
                const option = options[i];
                this.codigoOptions.set(option.value, {
                    nome: option.getAttribute("data-nome"),
                    codigo: option.value,
                    id: option.getAttribute("data-id")
                });
            }
        }
    },
    
    // Busca produto por nome (otimizada com Map)
    findByProdutoNome(nome) {
        return this.produtoOptions.get(nome);
    },
    
    // Busca produto por código (otimizada com Map)
    findByCodigo(codigo) {
        return this.codigoOptions.get(codigo);
    }
};

// Função otimizada para adicionar item
function AdicionaItemCorrigido() {
    const deposito = ElementCache.deposito?.value || "";
    let produto = ElementCache.produto?.value || "";
    const codigoNFe = ElementCache.codigoNFe?.value || "";
    let produtoId = "";
    let produtoInfo = null;
    
    // Busca otimizada usando cache Map ao invés de loop
    if (codigoNFe !== "") {
        produtoInfo = DatalistCache.findByCodigo(codigoNFe);
        if (produtoInfo) {
            produto = produtoInfo.nome;
            produtoId = produtoInfo.id;
        }
    } else if (produto !== "") {
        produtoInfo = DatalistCache.findByProdutoNome(produto);
        if (produtoInfo) {
            produtoId = produtoInfo.id;
        }
    }
    
    // Validação otimizada
    if (!produto || !produtoId) {
        showWarningMessage("O campo produto ou código do produto deve estar preenchido.");
        resetAddButton();
        return;
    }
    
    // Validação adicional
    if (produto.length < 2) {
        showWarningMessage("Nome do produto deve ter pelo menos 2 caracteres.");
        resetAddButton();
        return;
    }
    
    // Limpa campos usando cache
    ElementCache.deposito.value = "";
    ElementCache.produto.value = "";
    ElementCache.codigoNFe.value = "";
    
    // Verifica se o produto já foi adicionado (otimizado)
    if (ElementCache.get(produtoId)) {
        resetAddButton();
        return;
    }
    
    // Requisição AJAX otimizada
    const url = "/Etiquetas/GetProduto";
    
    // Usar fetch API para melhor performance e controle
    fetch(url, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/x-www-form-urlencoded',
            'X-Requested-With': 'XMLHttpRequest'
        },
        body: new URLSearchParams({
            'deposito': deposito,
            'produto': produtoId
        })
    })
    .then(response => {
        if (!response.ok) {
            throw new Error(`Erro HTTP: ${response.status} - ${response.statusText}`);
        }
        return response.json();
    })
    .then(data => {
        // Verificar se houve erro
        if (data.error) {
            console.error('Erro do servidor:', data.error);
            showErrorMessage("Erro: " + data.error);
            resetAddButton();
            return;
        }
        
        // Validar dados retornados
        if (!data.Id && !data.id) {
            console.error('Dados inválidos retornados:', data);
            showErrorMessage("Dados do produto inválidos. Tente novamente.");
            resetAddButton();
            return;
        }
        
        // Criar linha da tabela de forma otimizada
        createProductRow(data);
        resetAddButton();
        showSuccessMessage("Produto adicionado com sucesso!");
    })
    .catch(error => {
        console.error('Erro na requisição AJAX:', error);
        
        // Diferentes tipos de erro
        if (error.name === 'TypeError') {
            showErrorMessage("Erro de conexão. Verifique sua internet e tente novamente.");
        } else if (error.message.includes('HTTP error')) {
            showErrorMessage("Erro do servidor. Tente novamente em alguns instantes.");
        } else {
            showErrorMessage("Erro inesperado. Por favor, tente novamente.");
        }
        
        resetAddButton();
    });
}

// Funções para exibir mensagens de erro e sucesso
function showErrorMessage(message) {
    try {
        console.error('Erro:', message);
        
        // Tentar usar toast se disponível, senão usar alert
        if (typeof toastr !== 'undefined') {
            toastr.error(message);
        } else if (typeof Swal !== 'undefined') {
            Swal.fire({
                icon: 'error',
                title: 'Erro',
                text: message,
                timer: 5000
            });
        } else {
            alert(message);
        }
    } catch (error) {
        console.error('Erro ao exibir mensagem:', error);
        alert(message); // Fallback
    }
}

function showSuccessMessage(message) {
    try {
        console.log('Sucesso:', message);
        
        // Tentar usar toast se disponível
        if (typeof toastr !== 'undefined') {
            toastr.success(message);
        } else if (typeof Swal !== 'undefined') {
            Swal.fire({
                icon: 'success',
                title: 'Sucesso',
                text: message,
                timer: 3000,
                showConfirmButton: false
            });
        }
        // Para sucesso, não usar alert por ser menos intrusivo
    } catch (error) {
        console.error('Erro ao exibir mensagem de sucesso:', error);
    }
}

function showWarningMessage(message) {
    try {
        console.warn('Aviso:', message);
        
        if (typeof toastr !== 'undefined') {
            toastr.warning(message);
        } else if (typeof Swal !== 'undefined') {
            Swal.fire({
                icon: 'warning',
                title: 'Aviso',
                text: message,
                timer: 4000
            });
        } else {
            alert(message);
        }
    } catch (error) {
        console.error('Erro ao exibir mensagem de aviso:', error);
        alert(message); // Fallback
    }
}

// Função otimizada para criar linha do produto
function createProductRow(produto) {
    const row = ElementCache.listaItens.insertRow(-1);
    // Usar propriedades com maiúscula conforme retornado pelo servidor
    const produtoId = produto.Id || produto.id;
    row.id = produtoId;
    
    // Array com os dados das células para otimizar a criação
    const cellsData = [
        produto.CodigoNFe || produto.Codigo || produto.codigoNFe || produto.codigo || '', // Código
        produto.Nome || produto.nome || '', // Produto
        `<input type="number" id="${produtoId}Quantidade" min="1" step="1" value="1" class="form-control form-control-sm" onchange="AtualizaQuantidadeItem('${produtoId}')">`, // Quantidade
        parseFloat(produto.PrecoVenda || produto.precoVenda || 0).toFixed(2), // Preço
        `<input type="text" id="${produtoId}Lote" class="form-control form-control-sm" onchange="AtualizaLoteItem('${produtoId}')">`, // Lote
        `<input type="text" id="${produtoId}NumeroSerie" class="form-control form-control-sm" onchange="AtualizaNumeroSerieItem('${produtoId}')">`, // Nº Série
        produto.Unidade || produto.unidade || 'UN', // Unidade
        produto.Marca || produto.marca || '', // Marca
        `<button type="button" class="btn btn-danger btn-sm" onclick="RemoveItem('${produtoId}')"><i class="fas fa-trash"></i></button>` // Ação
    ];
    
    // Criar células de forma otimizada
    cellsData.forEach((data, index) => {
        const cell = row.insertCell(index);
        cell.innerHTML = data;
    });
    
    // Adicionar input hidden otimizado
    const hiddenInput = document.createElement('input');
    hiddenInput.type = 'hidden';
    hiddenInput.id = `${produtoId}form`;
    hiddenInput.name = 'itens';
    hiddenInput.value = `${produtoId},1,,`; // id, quantidade=1, lote vazio, numeroSerie vazio
    
    ElementCache.form.appendChild(hiddenInput);
}

// Função para resetar estado do botão (compatível com a implementação da view)
function resetAddButton() {
    const btnAdicionar = document.getElementById('btnAdicionar');
    if (btnAdicionar) {
        btnAdicionar.disabled = false;
        const buttonText = btnAdicionar.querySelector('.button-text');
        const loadingDots = btnAdicionar.querySelector('.loading-dots');
        
        if (buttonText) {
            buttonText.style.visibility = 'visible';
        }
        if (loadingDots) {
            loadingDots.classList.remove('show');
        }
    }
}

// Event delegation para melhor performance em elementos dinâmicos
function setupEventDelegation() {
    // Usar event delegation para inputs de quantidade, lote e número de série
    if (ElementCache.listaItens) {
        ElementCache.listaItens.addEventListener('change', function(e) {
            const target = e.target;
            
            if (target.type === 'number' && target.id.endsWith('Quantidade')) {
                const produtoId = target.id.replace('Quantidade', '');
                AtualizaQuantidadeItem(produtoId);
            } else if (target.type === 'text' && target.id.endsWith('Lote')) {
                const produtoId = target.id.replace('Lote', '');
                AtualizaLoteItem(produtoId);
            } else if (target.type === 'text' && target.id.endsWith('NumeroSerie')) {
                const produtoId = target.id.replace('NumeroSerie', '');
                AtualizaNumeroSerieItem(produtoId);
            }
        });
        
        // Event delegation para botões de remoção
        ElementCache.listaItens.addEventListener('click', function(e) {
            const target = e.target;
            const button = target.closest('button[onclick*="RemoveItem"]');
            
            if (button) {
                e.preventDefault();
                const onclick = button.getAttribute('onclick');
                const produtoId = onclick.match(/RemoveItem\('([^']+)'\)/)?.[1];
                if (produtoId) {
                    RemoveItem(produtoId);
                }
            }
        });
    }
}

// Inicialização otimizada quando o documento estiver pronto
$(document).ready(function() {
    // Inicializar caches
    ElementCache.init();
    DatalistCache.init();
    
    // Configurar event delegation
    setupEventDelegation();
    
    // Sobrescrever a função global
    window.AdicionaItem = AdicionaItemCorrigido;
    
    // Otimização: Debounce para campos de busca se existirem
    const searchInputs = document.querySelectorAll('input[list]');
    searchInputs.forEach(input => {
        let timeout;
        input.addEventListener('input', function() {
            clearTimeout(timeout);
            timeout = setTimeout(() => {
                // Atualizar cache se necessário
                if (input.getAttribute('list') === 'produto') {
                    DatalistCache.cacheProdutoOptions();
                } else if (input.getAttribute('list') === 'codigoProduto') {
                    DatalistCache.cacheCodigoOptions();
                }
            }, 300); // Debounce de 300ms
        });
    });
});

// Funções auxiliares que podem ser chamadas externamente (mantidas para compatibilidade)
function AtualizaQuantidadeItem(produtoId) {
    const quantidadeInput = document.getElementById(produtoId + 'Quantidade');
    const formInput = document.getElementById(produtoId + 'form');
    
    if (quantidadeInput && formInput) {
        const valores = formInput.value.split(',');
        valores[1] = quantidadeInput.value;
        formInput.value = valores.join(',');
    }
}

function AtualizaLoteItem(produtoId) {
    const loteInput = document.getElementById(produtoId + 'Lote');
    const formInput = document.getElementById(produtoId + 'form');
    
    if (loteInput && formInput) {
        const valores = formInput.value.split(',');
        valores[2] = loteInput.value;
        formInput.value = valores.join(',');
    }
}

function AtualizaNumeroSerieItem(produtoId) {
    const numeroSerieInput = document.getElementById(produtoId + 'NumeroSerie');
    const formInput = document.getElementById(produtoId + 'form');
    
    if (numeroSerieInput && formInput) {
        const valores = formInput.value.split(',');
        valores[3] = numeroSerieInput.value;
        formInput.value = valores.join(',');
    }
}

function RemoveItem(produtoId) {
    const row = document.getElementById(produtoId);
    const formInput = document.getElementById(produtoId + 'form');
    
    if (row) {
        row.remove();
    }
    
    if (formInput) {
        formInput.remove();
    }
} 