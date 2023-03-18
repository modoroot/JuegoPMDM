using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContenedorBase : MonoBehaviour, IObjetoInteractuablePadre
{
    [SerializeField] private Transform puntoSuperiorObjeto;
    private ObjetoInteractuable objInteractuable;
    public virtual void Interactuar(Jugador jugador) {
        Debug.LogError("error contenedor base");
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
    }

    public void ClearObjInteractuable() {
        objInteractuable = null;
    }

    public bool objInteractuableActivo() {
        return objInteractuable != null;
    }

}
