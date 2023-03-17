using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncimeraTrocear : ContenedorBase {
    [SerializeField] private IngredienteTroceadoSO[] ingredienteTroceadoSOArray;
    /**
     * 
     */
    public override void Interactuar(Jugador jugador) {
        if (!objInteractuableActivo()) {
            //Hueco vacío
            if (jugador.objInteractuableActivo()) {
                //Si no es troceable, el jugador no puede soltarlo en la encimera de trocear
                if (IngredienteTroceable(jugador.GetObjetoInteractuable().GetObjetoInteractuableSO())) {
                    //El jugador lleva un ingrediente troceable
                    jugador.GetObjetoInteractuable().SetObjetoInteractuablePadre(this);
                }
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
    /**
     * 
     */
    public override void InteractuarAlternativo(Jugador jugador) {
        //Sólo se troceará el ingrediente si es un objeto troceable y no ha sido cortado ya
        if (objInteractuableActivo() && IngredienteTroceable(GetObjetoInteractuable().GetObjetoInteractuableSO())) {
            ObjetoInteractuableSO outputObjetoInteractuableSO = GetOutputPorInput(GetObjetoInteractuable().GetObjetoInteractuableSO());
            GetObjetoInteractuable().DestroySelf();
            ObjetoInteractuable.InvocarObjetoInteractuable(outputObjetoInteractuableSO, this);
        }
    }

    private bool IngredienteTroceable(ObjetoInteractuableSO inputObjetoInteractuableSO) {
        foreach (IngredienteTroceadoSO ingredienteTroceadoSO in ingredienteTroceadoSOArray) {
            if (ingredienteTroceadoSO.input == inputObjetoInteractuableSO) {
                return true;
            }
        }
        return false;
    }

    /**
     * 
     */
    private ObjetoInteractuableSO GetOutputPorInput(ObjetoInteractuableSO inputObjetoInteractuableSO) {
        foreach (IngredienteTroceadoSO ingredienteTroceadoSO in ingredienteTroceadoSOArray) {
            if (ingredienteTroceadoSO.input == inputObjetoInteractuableSO) {
                return ingredienteTroceadoSO.output;
            }
        }
        return null;
    }
}
