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
                if (jugador.GetObjetoInteractuable().TryGetPlato(out PlatoObjetoInteractuable platoObjetoInteractuable)) {
                    if (platoObjetoInteractuable.TryAniadirIngrediente(GetObjetoInteractuable().GetObjetoInteractuableSO())) {
                        GetObjetoInteractuable().DestroySelf();
                    }

                } else {
                    // El jugador no lleva un plato, sino un ingrediente
                    if (GetObjetoInteractuable().TryGetPlato(out platoObjetoInteractuable)) {
                        // Hay un plato en la encimera
                        if (platoObjetoInteractuable.TryAniadirIngrediente(jugador.GetObjetoInteractuable().GetObjetoInteractuableSO())) {
                            jugador.GetObjetoInteractuable().DestroySelf();
                        }
                    }
                }
            } else {
                GetObjetoInteractuable().SetObjetoInteractuablePadre(jugador);
            }
        }
    }

}
