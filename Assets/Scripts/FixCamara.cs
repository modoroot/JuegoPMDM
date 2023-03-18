using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixCamara : MonoBehaviour
{
    private enum Mode { 
        LookAt,
        LookAtInverted,

    }

    [SerializeField] private Mode mode;

    /**
     * Esta función se inicia después de Update. Lo uso para que la barra de progreso
     * mire a la cámara después de que los demás objetos ya estén establecidos
     */
    private void LateUpdate() {
        // Orienta correctamente la barra de progreso para que no esté invertida visualmente
        switch (mode) {
            case Mode.LookAt:
                transform.LookAt(Camera.main.transform);
                break;
            case Mode.LookAtInverted:
                Vector3 dirCamera = transform.position - Camera.main.transform.position;
                transform.LookAt(transform.position + dirCamera);
                break;

        }
    }
}
