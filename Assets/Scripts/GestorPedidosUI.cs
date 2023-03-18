using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestorPedidosUI : MonoBehaviour {
    [SerializeField] private Transform contenedor;
    [SerializeField] private Transform recetaTemplate;

    private void Awake() {
        recetaTemplate.gameObject.SetActive(false);
    }

    private void Start() {
        GestorPedidos.Instance.OnRecetaInvocada += GestorPedidos_OnRecetaInvocada;
        GestorPedidos.Instance.OnRecetaCompletada += GestorPedidos_OnRecetaCompletada;
        UpdateVisual();
    }

    private void GestorPedidos_OnRecetaCompletada(object sender, System.EventArgs e) {
        UpdateVisual();
    }

    private void GestorPedidos_OnRecetaInvocada(object sender, System.EventArgs e) {
        UpdateVisual();
    }

    private void UpdateVisual() {
        foreach (Transform child in contenedor) {
            if (child == recetaTemplate) continue;
            Destroy(child.gameObject);

        }
        foreach (RecetaSO recetaSO in GestorPedidos.Instance.GetRecetaEsperadaSOList()) { 
           Transform recetaTransform = Instantiate(recetaTemplate, contenedor);
            recetaTransform.gameObject.SetActive(true);
            recetaTransform.GetComponent<GestorPedidosSingleUI>().SetRecetaSO(recetaSO);
        }
    }
}
