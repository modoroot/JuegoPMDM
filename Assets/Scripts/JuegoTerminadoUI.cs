using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class JuegoTerminadoUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI numeroRecetasExitosasText;

    private void Start() {
        GestorJuego.Instance.OnEstadoCambiado += GestorJuego_OnEstadoCambiado;
        Ocultar();
    }

    private void GestorJuego_OnEstadoCambiado(object sender, System.EventArgs e) {
        if (GestorJuego.Instance.IsJuegoTerminado()) {
            Mostrar();
            numeroRecetasExitosasText.text = GestorPedidos.Instance.GetCantidadRecetasExitosas().ToString();
        } else {
            Ocultar();
        }
    }

    private void Update() {
        
    }

    private void Ocultar() {
        gameObject.SetActive(false);
    }
    private void Mostrar() {
        gameObject.SetActive(true);
    }
}
