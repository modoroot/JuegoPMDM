using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraProgresoUI : MonoBehaviour
{
    [SerializeField] private GameObject progresoGameObject;
    [SerializeField] private Image imagenBarra;
    private IProgreso progreso;

    private void Start() {
        progreso = progresoGameObject.GetComponent<IProgreso>();
        if (progreso == null) {
            Debug.LogError("Game Object: "+ progresoGameObject + "no tiene un componente que implemente IProgreso");
        }
        progreso.CambioProgreso += Progreso_CambioProgreso;
        imagenBarra.fillAmount = 0f;

        Ocultar();
    }

    private void Progreso_CambioProgreso(object sender, IProgreso.CambioProgresoEventArgs e) {
        imagenBarra.fillAmount = e.progresoNormalizado;

        if (e.progresoNormalizado == 0f || e.progresoNormalizado == 1f) {
            Ocultar();
        } else { 
            Mostrar();
        }
    }

    private void Mostrar() { 
        gameObject.SetActive(true);

    }

    private void Ocultar() {
        gameObject.SetActive(false);
    }
}
