using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClearContador : MonoBehaviour {
    [SerializeField] private ObjetoInteractuableSO objetoInteractuable;
    [SerializeField] private Transform puntoSuperiorObjeto;
    [SerializeField] private ClearContador testContador;
    [SerializeField] private bool testBool;
    private ObjetoInteractuable objInteractuable;


    private void Update() {
        if (testBool && Input.GetKeyDown(KeyCode.T)) {
            if (objInteractuable != null) {
                objInteractuable.SetClearContador(testContador);
            }
        }
    }

    public void Interactuar() {
        //Condicional para la antiduplicación de objetos (ingredientes)
        if (objInteractuable == null) {
            Transform ingredienteTransform = Instantiate(objetoInteractuable.prefab, puntoSuperiorObjeto);
            ingredienteTransform.GetComponent<ObjetoInteractuable>().SetClearContador(this);
            
        } else {
            //Esta condición se dará cuando ya exista un objeto encima del objeto
            Debug.Log(objInteractuable.GetClearContador());
        }
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
