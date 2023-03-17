using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClearContador : ContenedorBase {

    [SerializeField] private ObjetoInteractuableSO objetoInteractuable;

    public override void Interactuar(Jugador jugador) {
        if (!objInteractuableActivo()) {
            //Hueco vacío
            if (jugador.objInteractuableActivo()) {
                jugador.GetObjetoInteractuable().SetObjetoInteractuablePadre(this);
            } else { 
                //El jugador no lleva nada
            }
        } else {
            if (jugador.objInteractuableActivo()) {

            } else {
                GetObjetoInteractuable().SetObjetoInteractuablePadre(jugador);
            }
        }
    }

}
