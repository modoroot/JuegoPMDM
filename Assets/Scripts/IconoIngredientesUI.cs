using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconoIngredientesUI : MonoBehaviour
{
    [SerializeField] private PlatoObjetoInteractuable platoObjetoInteractuable;
    [SerializeField] private Transform iconoTemplate;

    private void Awake() {
        iconoTemplate.gameObject.SetActive(false);
    }

    private void Start() {
        platoObjetoInteractuable.OnIngredienteAniadido += PlatoObjetoInteractuable_OnIngredienteAniadido;
    }

    private void PlatoObjetoInteractuable_OnIngredienteAniadido(object sender, PlatoObjetoInteractuable.OnIngredienteAniadidoEventArgs e) {
        ActualizarVisual();
    }

    private void ActualizarVisual() {
        foreach (Transform child in transform) {
            if(child == iconoTemplate) continue;
            Destroy(child.gameObject);
        }
        foreach (ObjetoInteractuableSO objetoInteractuableSO in platoObjetoInteractuable.GetObjetoInteractuableSOList()) {
            Transform iconoTransform = Instantiate(iconoTemplate, transform);
            iconoTransform.gameObject.SetActive(true);
            iconoTransform.GetComponent<IconoPlatosUI>().SetObjetoInteractuableSO(objetoInteractuableSO);
        }
    }
}
