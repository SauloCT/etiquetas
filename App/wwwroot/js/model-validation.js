// Validações para formulários de modelo de etiqueta
function validateModelForm(formId) {
    const form = document.getElementById(formId);
    let isValid = true;
    
    // Validar campos obrigatórios
    const requiredFields = form.querySelectorAll('[required]');
    requiredFields.forEach(field => {
        if (!validateModelField(field)) {
            isValid = false;
        }
    });
    
    // Validações específicas
    const nome = document.getElementById('nome');
    if (nome && nome.value.trim().length < 2) {
        setFieldInvalid(nome, 'Nome deve ter pelo menos 2 caracteres.');
        isValid = false;
    }
    
    const larguraPapel = document.getElementById('larguraPapel');
    if (larguraPapel && parseFloat(larguraPapel.value) <= 0) {
        setFieldInvalid(larguraPapel, 'Largura do papel deve ser maior que 0.');
        isValid = false;
    }
    
    const alturaPapel = document.getElementById('alturaPapel');
    if (alturaPapel && parseFloat(alturaPapel.value) <= 0) {
        setFieldInvalid(alturaPapel, 'Altura do papel deve ser maior que 0.');
        isValid = false;
    }
    
    // Validações específicas para dimensões das etiquetas (obrigatórias)
    const larguraEtiqueta = document.getElementById('larguraEtiqueta');
    if (larguraEtiqueta) {
        const larguraEtiquetaValue = parseFloat(larguraEtiqueta.value || '0');
        if (larguraEtiquetaValue <= 0) {
            setFieldInvalid(larguraEtiqueta, 'Largura da etiqueta deve ser maior que 0.');
            isValid = false;
        }
    }
    
    const alturaEtiqueta = document.getElementById('alturaEtiqueta');
    if (alturaEtiqueta) {
        const alturaEtiquetaValue = parseFloat(alturaEtiqueta.value || '0');
        if (alturaEtiquetaValue <= 0) {
            setFieldInvalid(alturaEtiqueta, 'Altura da etiqueta deve ser maior que 0.');
            isValid = false;
        }
    }
    
    // Validações para outros campos numéricos (opcionais, mas não podem ser negativos se preenchidos)
    const espacamentoHorizontal = document.getElementById('espacamentoHorizontal');
    if (espacamentoHorizontal && espacamentoHorizontal.value) {
        const espacamentoHorizontalValue = parseFloat(espacamentoHorizontal.value);
        if (espacamentoHorizontalValue < 0) {
            setFieldInvalid(espacamentoHorizontal, 'Espaçamento horizontal não pode ser negativo.');
            isValid = false;
        }
    }
    
    const espacamentoVertical = document.getElementById('espacamentoVertical');
    if (espacamentoVertical && espacamentoVertical.value) {
        const espacamentoVerticalValue = parseFloat(espacamentoVertical.value);
        if (espacamentoVerticalValue < 0) {
            setFieldInvalid(espacamentoVertical, 'Espaçamento vertical não pode ser negativo.');
            isValid = false;
        }
    }
    
    const margemEsquerda = document.getElementById('margemEsquerda');
    if (margemEsquerda && margemEsquerda.value) {
        const margemEsquerdaValue = parseFloat(margemEsquerda.value);
        if (margemEsquerdaValue < 0) {
            setFieldInvalid(margemEsquerda, 'Margem esquerda não pode ser negativa.');
            isValid = false;
        }
    }
    
    const margemSuperior = document.getElementById('margemSuperior');
    if (margemSuperior && margemSuperior.value) {
        const margemSuperiorValue = parseFloat(margemSuperior.value);
        if (margemSuperiorValue < 0) {
            setFieldInvalid(margemSuperior, 'Margem superior não pode ser negativa.');
            isValid = false;
        }
    }
    
    const alturaBarras = document.getElementById('alturaBarras');
    if (alturaBarras && alturaBarras.value) {
        const alturaBarrasValue = parseFloat(alturaBarras.value);
        if (alturaBarrasValue < 0) {
            setFieldInvalid(alturaBarras, 'Altura do código de barras não pode ser negativa.');
            isValid = false;
        }
    }
    
    return isValid;
}

function validateModelField(field) {
    const value = field.value.trim();
    
    // Verificar se campo obrigatório está preenchido
    if (field.hasAttribute('required') && !value) {
        setFieldInvalid(field, field.getAttribute('data-error') || 'Este campo é obrigatório.');
        return false;
    }
    
    // Validações específicas por tipo
    if (field.type === 'number' && value) {
        const numValue = parseFloat(value);
        const min = parseFloat(field.getAttribute('min'));
        const max = parseFloat(field.getAttribute('max'));
        
        if (isNaN(numValue)) {
            setFieldInvalid(field, 'Valor deve ser um número válido.');
            return false;
        }
        
        // Validações específicas para campos de dimensões de etiquetas
        if (field.id === 'larguraEtiqueta' || field.id === 'alturaEtiqueta') {
            if (numValue <= 0) {
                setFieldInvalid(field, 'Valor deve ser maior que 0.');
                return false;
            }
        }
        
        // Validações para campos que não podem ser negativos
        if (['espacamentoHorizontal', 'espacamentoVertical', 'margemEsquerda', 'margemSuperior', 'alturaBarras'].includes(field.id)) {
            if (numValue < 0) {
                setFieldInvalid(field, 'Valor não pode ser negativo.');
                return false;
            }
        }
        
        if (min !== null && numValue < min) {
            setFieldInvalid(field, `Valor deve ser maior ou igual a ${min}.`);
            return false;
        }
        
        if (max !== null && numValue > max) {
            setFieldInvalid(field, `Valor deve ser menor ou igual a ${max}.`);
            return false;
        }
    }
    
    setFieldValid(field);
    return true;
}

function setFieldInvalid(field, message) {
    field.classList.add('is-invalid');
    field.classList.remove('is-valid');
    
    const feedback = field.parentNode.querySelector('.invalid-feedback');
    if (feedback) {
        feedback.textContent = message;
    }
}

function setFieldValid(field) {
    field.classList.remove('is-invalid');
    field.classList.add('is-valid');
}

// Função para atualizar tamanho do papel
function atualizaTamanhoPapel() {
    var papel = document.getElementById("papel").value;
    var altura = document.getElementById("alturaPapel");
    var largura = document.getElementById("larguraPapel");
    
    switch(papel) {
        case '0':
            altura.value = 29.7;
            altura.readOnly = true;
            largura.value = 21.0;
            largura.readOnly = true;
            break;
        case '1':
            altura.value = 27.9;
            altura.readOnly = true;
            largura.value = 21.6;
            largura.readOnly = true;
            break;
        case '2':
            altura.readOnly = false;
            largura.readOnly = false;
            break;
    }
    
    // Revalidar campos após mudança
    if (altura && largura) {
        validateModelField(altura);
        validateModelField(largura);
    }
} 