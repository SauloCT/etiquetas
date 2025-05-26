// Utilitários JavaScript para o sistema de etiquetas
// Funções de validação, sanitização e tratamento de erros

const Util = {
    // Validações
    validation: {
        // Valida se um valor não está vazio
        isNotEmpty(value) {
            return value !== null && value !== undefined && value.toString().trim() !== '';
        },
        
        // Valida se é um número válido
        isValidNumber(value) {
            return !isNaN(value) && isFinite(value) && value > 0;
        },
        
        // Valida se é um ID válido (ObjectId do MongoDB)
        isValidId(id) {
            return /^[0-9a-fA-F]{24}$/.test(id);
        },
        
        // Valida email
        isValidEmail(email) {
            const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
            return emailRegex.test(email);
        },
        
        // Valida se string tem tamanho mínimo
        hasMinLength(value, minLength) {
            return this.isNotEmpty(value) && value.toString().trim().length >= minLength;
        },
        
        // Valida se string tem tamanho máximo
        hasMaxLength(value, maxLength) {
            return value === null || value === undefined || value.toString().length <= maxLength;
        }
    },
    
    // Sanitização
    sanitize: {
        // Remove caracteres perigosos de string
        string(input) {
            if (!input) return '';
            
            return input.toString()
                .trim()
                .replace(/[<>]/g, '') // Remove < e >
                .replace(/javascript:/gi, '') // Remove javascript:
                .replace(/on\w+=/gi, '') // Remove eventos on*=
                .replace(/script/gi, ''); // Remove script
        },
        
        // Sanitiza número
        number(input) {
            const num = parseFloat(input);
            return isNaN(num) ? 0 : num;
        },
        
        // Sanitiza inteiro
        integer(input) {
            const num = parseInt(input);
            return isNaN(num) ? 0 : num;
        }
    },
    
    // Tratamento de erros
    error: {
        // Log de erro com contexto
        log(error, context = '') {
            const timestamp = new Date().toISOString();
            const errorInfo = {
                timestamp,
                context,
                message: error.message || error,
                stack: error.stack,
                userAgent: navigator.userAgent,
                url: window.location.href
            };
            
            console.error('Erro capturado:', errorInfo);
            
            // Enviar para servidor se necessário
            this.sendToServer(errorInfo);
        },
        
        // Envia erro para o servidor
        sendToServer(errorInfo) {
            try {
                // Implementar envio para endpoint de logging se necessário
                // fetch('/api/log-error', { ... })
            } catch (e) {
                console.error('Falha ao enviar erro para servidor:', e);
            }
        },
        
        // Trata erro de requisição AJAX
        handleAjaxError(error, context = '') {
            this.log(error, `AJAX - ${context}`);
            
            if (error.name === 'TypeError') {
                return 'Erro de conexão. Verifique sua internet e tente novamente.';
            } else if (error.message && error.message.includes('HTTP')) {
                return 'Erro do servidor. Tente novamente em alguns instantes.';
            } else if (error.message && error.message.includes('timeout')) {
                return 'Tempo limite excedido. Tente novamente.';
            } else {
                return 'Erro inesperado. Por favor, tente novamente.';
            }
        }
    },
    
    // Utilitários DOM
    dom: {
        // Busca elemento com cache
        getElementById(id) {
            if (!this._elementCache) {
                this._elementCache = new Map();
            }
            
            if (!this._elementCache.has(id)) {
                this._elementCache.set(id, document.getElementById(id));
            }
            
            return this._elementCache.get(id);
        },
        
        // Limpa cache de elementos
        clearElementCache() {
            if (this._elementCache) {
                this._elementCache.clear();
            }
        },
        
        // Adiciona event listener com tratamento de erro
        addEventListenerSafe(element, event, handler) {
            if (!element) return;
            
            element.addEventListener(event, (e) => {
                try {
                    handler(e);
                } catch (error) {
                    Util.error.log(error, `Event: ${event}`);
                }
            });
        }
    },
    
    // Utilitários de formatação
    format: {
        // Formata número para moeda
        currency(value) {
            return new Intl.NumberFormat('pt-BR', {
                style: 'currency',
                currency: 'BRL'
            }).format(value);
        },
        
        // Formata número com decimais
        decimal(value, decimals = 2) {
            return parseFloat(value).toFixed(decimals);
        },
        
        // Formata data
        date(date) {
            return new Intl.DateTimeFormat('pt-BR').format(new Date(date));
        }
    },
    
    // Utilitários de loading
    loading: {
        // Mostra loading
        show(element) {
            if (element) {
                element.disabled = true;
                const originalText = element.textContent;
                element.setAttribute('data-original-text', originalText);
                element.innerHTML = '<i class="fas fa-spinner fa-spin"></i> Carregando...';
            }
        },
        
        // Esconde loading
        hide(element) {
            if (element) {
                element.disabled = false;
                const originalText = element.getAttribute('data-original-text');
                if (originalText) {
                    element.textContent = originalText;
                    element.removeAttribute('data-original-text');
                }
            }
        }
    },
    
    // Debounce para otimizar eventos
    debounce(func, wait) {
        let timeout;
        return function executedFunction(...args) {
            const later = () => {
                clearTimeout(timeout);
                func(...args);
            };
            clearTimeout(timeout);
            timeout = setTimeout(later, wait);
        };
    },
    
    // Throttle para otimizar eventos
    throttle(func, limit) {
        let inThrottle;
        return function() {
            const args = arguments;
            const context = this;
            if (!inThrottle) {
                func.apply(context, args);
                inThrottle = true;
                setTimeout(() => inThrottle = false, limit);
            }
        };
    },

    // Utilitários de validação de formulário
    validation: {
        // Função para focar no primeiro campo com erro
        focusFirstErrorField() {
            try {
                // Procurar pelo primeiro campo com classe 'is-invalid'
                const firstErrorField = document.querySelector('.is-invalid');
                
                if (firstErrorField) {
                    // Scroll suave até o campo
                    firstErrorField.scrollIntoView({
                        behavior: 'smooth',
                        block: 'center',
                        inline: 'nearest'
                    });
                    
                    // Aguardar um pouco para o scroll completar e então focar
                    setTimeout(() => {
                        firstErrorField.focus();
                        
                        // Se for um campo de texto, selecionar o conteúdo
                        if (firstErrorField.type === 'text' || firstErrorField.type === 'number') {
                            firstErrorField.select();
                        }
                        
                        // Adicionar um destaque visual temporário
                        firstErrorField.style.boxShadow = '0 0 10px rgba(220, 53, 69, 0.5)';
                        setTimeout(() => {
                            firstErrorField.style.boxShadow = '';
                        }, 2000);
                        
                    }, 300);
                    
                    return true;
                }
                
                return false;
            } catch (error) {
                console.error('Erro ao focar no primeiro campo com erro:', error);
                return false;
            }
        },

        // Função para validar formulário e focar no primeiro erro
        validateFormAndFocus(formId) {
            try {
                const form = document.getElementById(formId);
                if (!form) {
                    console.error('Formulário não encontrado:', formId);
                    return false;
                }

                // Remover classes de validação anteriores
                const allFields = form.querySelectorAll('.is-invalid, .is-valid');
                allFields.forEach(field => {
                    field.classList.remove('is-invalid', 'is-valid');
                });

                let isValid = true;
                let firstErrorField = null;

                // Validar campos obrigatórios
                const requiredFields = form.querySelectorAll('[required]');
                requiredFields.forEach(field => {
                    if (!this.validateSingleField(field)) {
                        isValid = false;
                        if (!firstErrorField) {
                            firstErrorField = field;
                        }
                    }
                });

                // Se há erros, focar no primeiro campo com erro
                if (!isValid && firstErrorField) {
                    setTimeout(() => {
                        this.focusFirstErrorField();
                    }, 100);
                }

                return isValid;
            } catch (error) {
                console.error('Erro na validação do formulário:', error);
                return false;
            }
        },

        // Função para validar um campo individual
        validateSingleField(field) {
            try {
                const value = field.value.trim();
                
                // Verificar se campo obrigatório está preenchido
                if (field.hasAttribute('required') && !value) {
                    this.setFieldInvalid(field, field.getAttribute('data-error') || 'Este campo é obrigatório.');
                    return false;
                }
                
                // Validações específicas por tipo
                if (field.type === 'number' && value) {
                    const numValue = parseFloat(value);
                    const min = parseFloat(field.getAttribute('min'));
                    const max = parseFloat(field.getAttribute('max'));
                    
                    if (isNaN(numValue)) {
                        this.setFieldInvalid(field, 'Valor deve ser um número válido.');
                        return false;
                    }
                    
                    if (min !== null && !isNaN(min) && numValue < min) {
                        this.setFieldInvalid(field, `Valor deve ser maior ou igual a ${min}.`);
                        return false;
                    }
                    
                    if (max !== null && !isNaN(max) && numValue > max) {
                        this.setFieldInvalid(field, `Valor deve ser menor ou igual a ${max}.`);
                        return false;
                    }
                }
                
                // Validações específicas por ID do campo
                if (field.id === 'nome' && value.length < 2) {
                    this.setFieldInvalid(field, 'Nome deve ter pelo menos 2 caracteres.');
                    return false;
                }
                
                // Validações específicas para campos de etiquetas
                if (['larguraEtiqueta', 'alturaEtiqueta'].includes(field.id)) {
                    const numValue = parseFloat(value || '0');
                    if (numValue <= 0) {
                        this.setFieldInvalid(field, 'Valor deve ser maior que 0.');
                        return false;
                    }
                }
                
                // Validações para campos que não podem ser negativos
                if (['espacamentoHorizontal', 'espacamentoVertical', 'margemEsquerda', 'margemSuperior', 'alturaBarras'].includes(field.id)) {
                    const numValue = parseFloat(value || '0');
                    if (value && numValue < 0) {
                        this.setFieldInvalid(field, 'Valor não pode ser negativo.');
                        return false;
                    }
                }
                
                this.setFieldValid(field);
                return true;
            } catch (error) {
                console.error('Erro na validação do campo:', error);
                return false;
            }
        },

        // Função para marcar campo como inválido
        setFieldInvalid(field, message) {
            try {
                field.classList.add('is-invalid');
                field.classList.remove('is-valid');
                
                const feedback = field.parentNode.querySelector('.invalid-feedback');
                if (feedback) {
                    feedback.textContent = message;
                }
            } catch (error) {
                console.error('Erro ao marcar campo como inválido:', error);
            }
        },

        // Função para marcar campo como válido
        setFieldValid(field) {
            try {
                field.classList.remove('is-invalid');
                field.classList.add('is-valid');
            } catch (error) {
                console.error('Erro ao marcar campo como válido:', error);
            }
        }
    }
};

// Inicialização global de tratamento de erros
window.addEventListener('error', (event) => {
    Util.error.log(event.error, 'Global Error Handler');
});

window.addEventListener('unhandledrejection', (event) => {
    Util.error.log(event.reason, 'Unhandled Promise Rejection');
});

// Exportar para uso global
window.Util = Util; 