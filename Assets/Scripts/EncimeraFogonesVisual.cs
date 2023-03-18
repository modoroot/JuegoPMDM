using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncimeraFogonesVisual : MonoBehaviour
{
    [SerializeField] private GameObject fogonGameObject;
    [SerializeField] private GameObject particulasGameObject;
    [SerializeField] private EncimeraFogones encimeraFogones;

    private void Start() {
        encimeraFogones.OnEstadoCambiado += EncimeraFogones_OnEstadoCambiado;
    }

    private void EncimeraFogones_OnEstadoCambiado(object sender, EncimeraFogones.OnEstadoCambiadoEventArgs e) {
        bool mostrarVisual = e.estado == EncimeraFogones.Estado.Friendose || e.estado == EncimeraFogones.Estado.Frito;
        fogonGameObject.SetActive(mostrarVisual);
        particulasGameObject.SetActive(mostrarVisual);
    }
}
