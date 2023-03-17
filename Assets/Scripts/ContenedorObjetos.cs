using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContenedorObjetos : ContenedorBase {
    [SerializeField] private ObjetoInteractuableSO objetoInteractuable;


    public event EventHandler JugadorAgarraObjeto;

    public override void Interactuar(Jugador jugador) {
        //Si el jugador no lleva nada, puede coger un objeto
        if (!jugador.objInteractuableActivo()) {
            ObjetoInteractuable.InvocarObjetoInteractuable(objetoInteractuable, jugador);
            JugadorAgarraObjeto?.Invoke(this, EventArgs.Empty);
        }

            
    }


}
