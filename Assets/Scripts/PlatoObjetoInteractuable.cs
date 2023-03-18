using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatoObjetoInteractuable : ObjetoInteractuable {

    public event EventHandler<OnIngredienteAniadidoEventArgs> OnIngredienteAniadido;
    public class OnIngredienteAniadidoEventArgs : EventArgs {
        public ObjetoInteractuableSO objetoInteractuableSO;
    }

    [SerializeField] private List<ObjetoInteractuableSO> objetosInteractuablesValidos;

    private List<ObjetoInteractuableSO> objetoInteractuableSOList;
    private void Awake() {
        objetoInteractuableSOList = new List<ObjetoInteractuableSO>();
    }
    public bool TryAniadirIngrediente(ObjetoInteractuableSO objetoInteractuableSO) {
        if (!objetosInteractuablesValidos.Contains(objetoInteractuableSO)) {
            //Ingrediente que no se puede colocar en un plato (carne cruda, tomate entero, etc.)
            return false;
        }

        if (objetoInteractuableSOList.Contains(objetoInteractuableSO)) {
            return false;
        } else {
            objetoInteractuableSOList.Add(objetoInteractuableSO);
            OnIngredienteAniadido?.Invoke(this, new OnIngredienteAniadidoEventArgs { 
                objetoInteractuableSO = objetoInteractuableSO
            });
            return true;
        }
        
    }

    public List<ObjetoInteractuableSO> GetObjetoInteractuableSOList() { 
        return objetoInteractuableSOList;
    }
}
