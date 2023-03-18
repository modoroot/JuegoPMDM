using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncimeraTrocear : ContenedorBase, IProgreso {

    public event EventHandler<IProgreso.CambioProgresoEventArgs> CambioProgreso;

    public event EventHandler OnCortar;

    [SerializeField] private IngredienteTroceadoSO[] ingredienteTroceadoSOArray;

    private int progresoTroceo;
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
                    progresoTroceo = 0;
                    IngredienteTroceadoSO ingredienteTroceadoSO = GetIngredienteTroceadoSOInput(GetObjetoInteractuable().GetObjetoInteractuableSO());
                    CambioProgreso?.Invoke(this, new IProgreso.CambioProgresoEventArgs { 
                        progresoNormalizado = (float)progresoTroceo / ingredienteTroceadoSO.troceoMaximo 
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
                    }
                }
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
            progresoTroceo++;

            OnCortar?.Invoke(this, EventArgs.Empty);
            IngredienteTroceadoSO ingredienteTroceadoSO = GetIngredienteTroceadoSOInput(GetObjetoInteractuable().GetObjetoInteractuableSO());
            CambioProgreso?.Invoke(this, new IProgreso.CambioProgresoEventArgs {
                progresoNormalizado = (float)progresoTroceo / ingredienteTroceadoSO.troceoMaximo
            });
            if (progresoTroceo >= ingredienteTroceadoSO.troceoMaximo) {
                ObjetoInteractuableSO outputObjetoInteractuableSO = GetOutputPorInput(GetObjetoInteractuable().GetObjetoInteractuableSO());
                GetObjetoInteractuable().DestroySelf();
                ObjetoInteractuable.InvocarObjetoInteractuable(outputObjetoInteractuableSO, this);
            }
        }
    }

    /**
     * 
     */
    private bool IngredienteTroceable(ObjetoInteractuableSO inputObjetoInteractuableSO) {
        IngredienteTroceadoSO ingredienteTroceadoSO = GetIngredienteTroceadoSOInput(inputObjetoInteractuableSO);
        return ingredienteTroceadoSO != null;
    }

    /**
     * 
     */
    private ObjetoInteractuableSO GetOutputPorInput(ObjetoInteractuableSO inputObjetoInteractuableSO) {
        IngredienteTroceadoSO ingredienteTroceadoSO = GetIngredienteTroceadoSOInput(inputObjetoInteractuableSO);
        if (ingredienteTroceadoSO != null) {
            return ingredienteTroceadoSO.output;
        } else {
            return null;
        }
    }
    /**
     * 
     */
    private IngredienteTroceadoSO GetIngredienteTroceadoSOInput(ObjetoInteractuableSO inputObjetoInteractuableSO) {
        foreach (IngredienteTroceadoSO ingredienteTroceadoSO in ingredienteTroceadoSOArray) {
            if (ingredienteTroceadoSO.input == inputObjetoInteractuableSO) {
                return ingredienteTroceadoSO;
            }
        }
        return null;
    }

}
