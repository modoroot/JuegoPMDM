using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContenedorBase : MonoBehaviour, IObjetoInteractuablePadre {

    public static event EventHandler OnObjetoColocado;

    [SerializeField] private Transform puntoSuperiorObjeto;
    private ObjetoInteractuable objInteractuable;
    public virtual void Interactuar(Jugador jugador) {
    }
    public virtual void InteractuarAlternativo(Jugador jugador) {
    }


    public Transform GetObjetoInteractuableConTransform() {
        return puntoSuperiorObjeto;
    }


    public ObjetoInteractuable GetObjetoInteractuable() {
        return objInteractuable;
    }

    public void SetObjInteractuable(ObjetoInteractuable objInteractuable) {
        this.objInteractuable = objInteractuable;
        if (objInteractuable != null) {
            OnObjetoColocado?.Invoke(this, EventArgs.Empty);
        }
    }

    public void ClearObjInteractuable() {
        objInteractuable = null;
    }

    public bool objInteractuableActivo() {
        return objInteractuable != null;
    }

}
