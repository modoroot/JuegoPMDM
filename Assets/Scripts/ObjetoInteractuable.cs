using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetoInteractuable : MonoBehaviour {
    [SerializeField] private ObjetoInteractuableSO objetoInteractuableSO;
    private IObjetoInteractuablePadre objetoInteractuablePadre;

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
    public void SetObjetoInteractuablePadre(IObjetoInteractuablePadre objetoInteractuablePadre) {
        if (this.objetoInteractuablePadre != null) {
            this.objetoInteractuablePadre.ClearObjInteractuable();
        }
        this.objetoInteractuablePadre = objetoInteractuablePadre;

        if (objetoInteractuablePadre.objInteractuableActivo()) {
            Debug.LogError("Ya tiene un ingrediente el objeto padre / encimera");
        }
        objetoInteractuablePadre.SetObjInteractuable(this);

        transform.parent = objetoInteractuablePadre.GetObjetoInteractuableConTransform();
        transform.localPosition = Vector3.zero;
    }



    public IObjetoInteractuablePadre GetObjetoInteractuablePadre() {
        return objetoInteractuablePadre;
    }
    /**
     * Destruye el objeto actual de la encimera
     */
    public void DestroySelf() {
        objetoInteractuablePadre.ClearObjInteractuable();
        Destroy(gameObject);
    }

    public static ObjetoInteractuable InvocarObjetoInteractuable(ObjetoInteractuableSO objetoInteractuableSO,
        IObjetoInteractuablePadre objetoInteractuablePadre) {
        Transform ingredienteTransform = Instantiate(objetoInteractuableSO.prefab);
        ObjetoInteractuable objetoInteractuable = ingredienteTransform.GetComponent<ObjetoInteractuable>();
        objetoInteractuable.SetObjetoInteractuablePadre(objetoInteractuablePadre);
        return objetoInteractuable;
    }

}
