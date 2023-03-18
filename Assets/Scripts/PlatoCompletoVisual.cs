using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatoCompletoVisual : MonoBehaviour
{

    [Serializable]
    public struct ObjetoInteractuableSO_GameObject {
        public ObjetoInteractuableSO objetoInteractuableSO;
        public GameObject gameObject;
    }

    [SerializeField] private PlatoObjetoInteractuable platoObjetoInteractuable;
    [SerializeField] private List<ObjetoInteractuableSO_GameObject> objetoInteractuableSOGameObjectList;

    private void Start() {
        platoObjetoInteractuable.OnIngredienteAniadido += PlatoObjetoInteractuable_OnIngredienteAniadido;
        foreach (ObjetoInteractuableSO_GameObject objetoInteractuableSOGameObject in objetoInteractuableSOGameObjectList) {
                objetoInteractuableSOGameObject.gameObject.SetActive(false);
        }

    }

    private void PlatoObjetoInteractuable_OnIngredienteAniadido(object sender, PlatoObjetoInteractuable.OnIngredienteAniadidoEventArgs e) {
        foreach (ObjetoInteractuableSO_GameObject objetoInteractuableSOGameObject in objetoInteractuableSOGameObjectList) {
            if (objetoInteractuableSOGameObject.objetoInteractuableSO == e.objetoInteractuableSO) { 
                objetoInteractuableSOGameObject.gameObject.SetActive(true);
            }
        }
        
    }
}
