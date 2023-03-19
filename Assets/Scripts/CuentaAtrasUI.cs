using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CuentaAtrasUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI cuentaAtrasTexto;

    private void Start() {
        GestorJuego.Instance.OnEstadoCambiado += GestorJuego_OnEstadoCambiado;
        Ocultar();
    }

    private void GestorJuego_OnEstadoCambiado(object sender, System.EventArgs e) {
        if (GestorJuego.Instance.IsCuentaAtrasActivada()) {
            Mostrar();
        } else {
            Ocultar();
        }
    }

    private void Update() {
        cuentaAtrasTexto.text = Mathf.Ceil(GestorJuego.Instance.GetCuentaAtrasTemporizador()).ToString();
    }

    private void Ocultar() {
        gameObject.SetActive(false);
    }
    private void Mostrar() {
        gameObject.SetActive(true);
    }
}
