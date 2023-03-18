using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GestorPedidosSingleUI : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI nombreRecetaTextMesh;
    [SerializeField] private Transform contenedorIcono;
    [SerializeField] private Transform iconoTemplate;

    private void Awake() {
        iconoTemplate.gameObject.SetActive(false);
    }

    public void SetRecetaSO(RecetaSO recetaSO) {
        nombreRecetaTextMesh.text = recetaSO.nombreReceta;
        foreach (Transform child in contenedorIcono) {
            if (child == iconoTemplate) continue;
            Destroy(child.gameObject);
        }
        foreach (ObjetoInteractuableSO objetoInteractuableSO in recetaSO.objetoInteractuableSOList) {
            Transform iconoTransform = Instantiate(iconoTemplate, contenedorIcono);
            iconoTransform.gameObject.SetActive(true);
            iconoTransform.GetComponent<Image>().sprite = objetoInteractuableSO.sprite;
        }
    }
}
