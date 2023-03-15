using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClearContador : MonoBehaviour
{
    [SerializeField] private ObjetoInteractuableSO objetoInteractuable
        ;
    [SerializeField] private Transform puntoSuperiorObjeto;
    public void Interactuar() {
       Transform ingredienteTransform =  Instantiate(objetoInteractuable.prefab, puntoSuperiorObjeto);
        ingredienteTransform.localPosition = Vector3.zero;

        Debug.Log(ingredienteTransform.GetComponent<ObjetoInteractuable>().GetObjetoInteractuableSO().nombreObjeto);
    }
}
