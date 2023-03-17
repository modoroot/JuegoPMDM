using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetoInteractuable : MonoBehaviour {
    [SerializeField] private ObjetoInteractuableSO objetoInteractuableSO;
    private ClearContador clearContador;

    /**
     * Método para obtener el objeto interactuable
     */
    public ObjetoInteractuableSO GetObjetoInteractuableSO() {
        return objetoInteractuableSO;
    }

    /**
     * Cuando el objeto interactuable se cambia de objeto padre, también cambia su posición
     * a partir de la siguiente función
     */
    public void SetClearContador(ClearContador clearContador) {
        if (this.clearContador != null) {
            this.clearContador.ClearObjInteractuable();
        }
        this.clearContador = clearContador;

        if (clearContador.objInteractuableActivo()) {
            Debug.LogError("Ya tiene un ingrediente el objeto padre / encimera");
        }
        clearContador.SetObjInteractuable(this);

        transform.parent = clearContador.GetObjetoInteractuableConTransform();
        transform.localPosition = Vector3.zero;
    }



    public ClearContador GetClearContador() {
        return clearContador;
    }

}
