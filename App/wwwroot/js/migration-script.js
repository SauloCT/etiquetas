/**
 * Migration Script for WOW.js Improvements
 * 
 * Este script facilita a migração do wow.min.js antigo para o wow-improved.js
 * Mantém compatibilidade com código existente e adiciona novas funcionalidades
 * 
 * @version 1.0.0
 * @author AI Assistant
 */

(function() {
    'use strict';

    // Configuração de migração
    const MIGRATION_CONFIG = {
        // Configurar logging para produção
        enableLogging: true, // Mudar para false em produção
        logLevel: 'info', // 'error', 'warn', 'info', 'debug'
        
        // Configurações de performance
        debounceTime: 16, // ~60fps
        
        // Configurações de compatibilidade
        autoMigrate: true,
        preserveOldBehavior: false
    };

    /**
     * Função de migração automática
     * Detecta se o WOW antigo está sendo usado e migra automaticamente
     */
    function autoMigrate() {
        // Aguarda o DOM estar pronto
        if (document.readyState === 'loading') {
            document.addEventListener('DOMContentLoaded', autoMigrate);
            return;
        }

        try {
            // Verifica se há elementos com classe 'wow'
            const wowElements = document.querySelectorAll('.wow');
            
            if (wowElements.length > 0) {
                console.log(`[WOW Migration] Encontrados ${wowElements.length} elementos WOW`);
                
                // Configura o logging baseado na configuração
                if (window.WOW && window.WOWLogger) {
                    window.WOWLogger.enabled = MIGRATION_CONFIG.enableLogging;
                    window.WOWLogger.level = MIGRATION_CONFIG.logLevel;
                }
                
                // Inicializa o WOW melhorado se não foi inicializado
                if (window.WOW && !window.wowInstance) {
                    const wowConfig = {
                        boxClass: 'wow',
                        animateClass: 'animated',
                        offset: 0,
                        mobile: true,
                        live: true,
                        callback: function(element) {
                            // Callback personalizado para logging de migração
                            if (MIGRATION_CONFIG.enableLogging) {
                                console.log('[WOW Migration] Elemento animado:', element);
                            }
                        }
                    };

                    window.wowInstance = new window.WOW(wowConfig);
                    window.wowInstance.init();
                    
                    console.log('[WOW Migration] WOW.js melhorado inicializado com sucesso');
                }
            }
        } catch (error) {
            console.error('[WOW Migration] Erro durante migração automática:', error);
        }
    }

    /**
     * Função para verificar compatibilidade
     */
    function checkCompatibility() {
        const issues = [];
        
        // Verifica se o WOW está disponível
        if (!window.WOW) {
            issues.push('WOW.js não está carregado');
        }
        
        // Verifica se há conflitos com versões antigas
        if (window.WOW && !window.WOW.prototype.getDefaults) {
            issues.push('Versão antiga do WOW.js detectada - considere atualizar');
        }
        
        // Verifica suporte a recursos modernos
        if (!window.requestAnimationFrame) {
            issues.push('requestAnimationFrame não suportado - performance pode ser afetada');
        }
        
        if (!window.WeakMap) {
            issues.push('WeakMap não suportado - usando Map como fallback');
        }
        
        if (issues.length > 0) {
            console.warn('[WOW Migration] Problemas de compatibilidade detectados:', issues);
        } else {
            console.log('[WOW Migration] Verificação de compatibilidade passou');
        }
        
        return issues;
    }

    /**
     * Função para configurar logging em produção
     */
    function configureProduction() {
        if (window.WOWLogger) {
            window.WOWLogger.enabled = false;
            window.WOWLogger.level = 'error';
            console.log('[WOW Migration] Configurado para produção - logging desabilitado');
        }
    }

    /**
     * Função para configurar desenvolvimento
     */
    function configureDevelopment() {
        if (window.WOWLogger) {
            window.WOWLogger.enabled = true;
            window.WOWLogger.level = 'debug';
            console.log('[WOW Migration] Configurado para desenvolvimento - logging completo habilitado');
        }
    }

    /**
     * Função para migração manual
     * Permite migração controlada com configurações específicas
     */
    function manualMigrate(config = {}) {
        try {
            const defaultConfig = {
                boxClass: 'wow',
                animateClass: 'animated',
                offset: 0,
                mobile: true,
                live: true
            };

            const finalConfig = Object.assign(defaultConfig, config);
            
            if (window.WOW) {
                const wow = new window.WOW(finalConfig);
                wow.init();
                
                console.log('[WOW Migration] Migração manual concluída com configuração:', finalConfig);
                return wow;
            } else {
                throw new Error('WOW.js não está disponível');
            }
        } catch (error) {
            console.error('[WOW Migration] Erro na migração manual:', error);
            throw error;
        }
    }

    /**
     * Função para rollback (voltar para comportamento antigo)
     */
    function rollback() {
        try {
            if (window.wowInstance) {
                window.wowInstance.stop();
                window.wowInstance = null;
                console.log('[WOW Migration] Rollback executado - WOW parado');
            }
        } catch (error) {
            console.error('[WOW Migration] Erro durante rollback:', error);
        }
    }

    /**
     * Função para obter estatísticas de performance
     */
    function getPerformanceStats() {
        if (window.wowInstance && window.performance) {
            const stats = {
                elementsProcessed: window.wowInstance.boxes ? window.wowInstance.boxes.length : 0,
                isRunning: !!window.wowInstance.boxes,
                performanceEntries: window.performance.getEntriesByType('measure').filter(entry => 
                    entry.name.includes('WOW')
                )
            };
            
            console.log('[WOW Migration] Estatísticas de performance:', stats);
            return stats;
        }
        
        return null;
    }

    // Expor API de migração globalmente
    window.WOWMigration = {
        autoMigrate: autoMigrate,
        manualMigrate: manualMigrate,
        checkCompatibility: checkCompatibility,
        configureProduction: configureProduction,
        configureDevelopment: configureDevelopment,
        rollback: rollback,
        getPerformanceStats: getPerformanceStats,
        config: MIGRATION_CONFIG
    };

    // Executar migração automática se habilitada
    if (MIGRATION_CONFIG.autoMigrate) {
        autoMigrate();
    }

    // Verificar compatibilidade na inicialização
    document.addEventListener('DOMContentLoaded', function() {
        setTimeout(checkCompatibility, 100);
    });

    console.log('[WOW Migration] Script de migração carregado');

})(); 