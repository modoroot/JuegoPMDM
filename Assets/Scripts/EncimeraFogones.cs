using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EncimeraTrocear;

public class EncimeraFogones : ContenedorBase, IProgreso {

    [SerializeField] private FreirSO[] freirSOArray;
    [SerializeField] private QuemadoSO[] quemadoSOArray;

    private float tiempoFreir;
    private float tiempoQuemado;

    private FreirSO freirSO;
    private QuemadoSO quemadoSO;
    public event EventHandler<IProgreso.CambioProgresoEventArgs> CambioProgreso;
    public event EventHandler<OnEstadoCambiadoEventArgs> OnEstadoCambiado;
    public class OnEstadoCambiadoEventArgs : EventArgs { 
        public Estado estado;
    }

    public enum Estado {
        Vacio,
        Friendose,
        Frito,
        Quemado,
    }
    private Estado estado;

    private void Start() {
        estado = Estado.Vacio;
    }

    private void Update() {
        switch (estado) {
            case Estado.Vacio:
                break;
            case Estado.Friendose:
                tiempoFreir += Time.deltaTime;
                CambioProgreso?.Invoke(this, new IProgreso.CambioProgresoEventArgs {
                    progresoNormalizado = tiempoFreir / freirSO.tiempoFreirMaximo
                });
                if (tiempoFreir > freirSO.tiempoFreirMaximo) {
                    //Se ha freído
                    GetObjetoInteractuable().DestroySelf();
                    ObjetoInteractuable.InvocarObjetoInteractuable(freirSO.output, this);
                    estado = Estado.Frito;
                    tiempoQuemado = 0f;
                    quemadoSO = GetQuemadoSOInput(GetObjetoInteractuable().GetObjetoInteractuableSO());
                    OnEstadoCambiado?.Invoke(this, new OnEstadoCambiadoEventArgs { 
                        estado = estado
                    });
                }
                break;
            case Estado.Frito:
                tiempoQuemado += Time.deltaTime;
                CambioProgreso?.Invoke(this, new IProgreso.CambioProgresoEventArgs {
                    progresoNormalizado = tiempoQuemado / quemadoSO.tiempoQuemadoMaximo
                });
                if (tiempoQuemado > quemadoSO.tiempoQuemadoMaximo) {
                    //Se ha freído
                    GetObjetoInteractuable().DestroySelf();
                    ObjetoInteractuable.InvocarObjetoInteractuable(quemadoSO.output, this);
                    estado = Estado.Quemado;
                    OnEstadoCambiado?.Invoke(this, new OnEstadoCambiadoEventArgs {
                        estado = estado

                    });
                    CambioProgreso?.Invoke(this, new IProgreso.CambioProgresoEventArgs {
                        progresoNormalizado = 0f
                    });

                }
                break;
            case Estado.Quemado:
                break;
        }
    }

    public override void Interactuar(Jugador jugador) {
        if (!objInteractuableActivo()) {
            //Hueco vacío
            if (jugador.objInteractuableActivo()) {
                //Si no es troceable, el jugador no puede soltarlo en la encimera de trocear
                if (IngredienteTroceable(jugador.GetObjetoInteractuable().GetObjetoInteractuableSO())) {
                    //El jugador lleva un ingrediente que se puede freír
                    jugador.GetObjetoInteractuable().SetObjetoInteractuablePadre(this);

                    freirSO = GetFreirSOInput(GetObjetoInteractuable().GetObjetoInteractuableSO());

                    estado = Estado.Friendose;
                    tiempoFreir = 0f;
                    OnEstadoCambiado?.Invoke(this, new OnEstadoCambiadoEventArgs {
                        estado = estado

                    });
                    CambioProgreso?.Invoke(this, new IProgreso.CambioProgresoEventArgs { 
                        progresoNormalizado = tiempoFreir / freirSO.tiempoFreirMaximo
                    });

                }
            } else {
                //El jugador no lleva nada
            }
        } else {
            if (jugador.objInteractuableActivo()) {
                if (jugador.GetObjetoInteractuable().TryGetPlato(out PlatoObjetoInteractuable platoObjetoInteractuable)) {
                    if (platoObjetoInteractuable.TryAniadirIngrediente(GetObjetoInteractuable().GetObjetoInteractuableSO())) {
                        GetObjetoInteractuable().DestroySelf();
                        estado = Estado.Vacio;
                        OnEstadoCambiado?.Invoke(this, new OnEstadoCambiadoEventArgs {
                            estado = estado
                        });
                        CambioProgreso?.Invoke(this, new IProgreso.CambioProgresoEventArgs {
                            progresoNormalizado = 0f
                        });
                    }

                }

            } else {
                GetObjetoInteractuable().SetObjetoInteractuablePadre(jugador);
                estado = Estado.Vacio;
                OnEstadoCambiado?.Invoke(this, new OnEstadoCambiadoEventArgs {
                    estado = estado
                });
                CambioProgreso?.Invoke(this, new IProgreso.CambioProgresoEventArgs {
                    progresoNormalizado = 0f
                });
            }
        }
    }


    /**
     * 
     */
    private bool IngredienteTroceable(ObjetoInteractuableSO inputObjetoInteractuableSO) {
        FreirSO freirSO = GetFreirSOInput(inputObjetoInteractuableSO);
        return freirSO != null;
    }

    /**
     * 
     */
    private ObjetoInteractuableSO GetOutputPorInput(ObjetoInteractuableSO inputObjetoInteractuableSO) {
        FreirSO freirSO = GetFreirSOInput(inputObjetoInteractuableSO);
        if (freirSO != null) {
            return freirSO.output;
        } else {
            return null;
        }
    }
    /**
     * 
     */
    private FreirSO GetFreirSOInput(ObjetoInteractuableSO inputObjetoInteractuableSO) {
        foreach (FreirSO freirSO in freirSOArray) {
            if (freirSO.input == inputObjetoInteractuableSO) {
                return freirSO;
            }
        }
        return null;
    }
    /**
    * 
    */
    private QuemadoSO GetQuemadoSOInput(ObjetoInteractuableSO inputObjetoInteractuableSO) {
        foreach (QuemadoSO quemadoSO in quemadoSOArray) {
            if (quemadoSO.input == inputObjetoInteractuableSO) {
                return quemadoSO;
            }
        }
        return null;
    }
}
