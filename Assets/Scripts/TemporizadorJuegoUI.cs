using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TemporizadorJuegoUI : MonoBehaviour
{
    [SerializeField] private Image imagenTemporizador;

    private void Update() {
     imagenTemporizador.fillAmount = GestorJuego.Instance.GetTiempoRestanteNormalized();
    }

}
