using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncimeraPlatos : ContenedorBase {

    [SerializeField] private ObjetoInteractuableSO platoObjetoInteractuableSO;
    private float tiempoinvocarPlato;
    private float tiempoMaximoInvocarPlato = 4f;
    private int cantidadPlatos;
    private int cantidadPlatosMax = 3;

    public event EventHandler OnPlatoInvocado;
    public event EventHandler OnPlatoEliminado;

    private void Update() {
        tiempoinvocarPlato += Time.deltaTime;
        if (tiempoinvocarPlato > tiempoMaximoInvocarPlato) {
            tiempoinvocarPlato = 0f;
            if (cantidadPlatos < cantidadPlatosMax) {
                cantidadPlatos++;
                OnPlatoInvocado?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interactuar(Jugador jugador) {
        if (!jugador.objInteractuableActivo()) {
            //El jugador no lleva nada
            if (cantidadPlatos > 0) {
                cantidadPlatos--;
                ObjetoInteractuable.InvocarObjetoInteractuable(platoObjetoInteractuableSO, jugador);
                OnPlatoEliminado?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
