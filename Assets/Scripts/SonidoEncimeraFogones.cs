using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonidoEncimeraFogones : MonoBehaviour
{
    [SerializeField] private EncimeraFogones encimeraFogones;
    private AudioSource audioSource;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }


    private void Start() {
        encimeraFogones.OnEstadoCambiado += EncimeraFogones_OnEstadoCambiado;
    }

    private void EncimeraFogones_OnEstadoCambiado(object sender, EncimeraFogones.OnEstadoCambiadoEventArgs e) {
        bool reproducirSonido = e.estado == EncimeraFogones.Estado.Friendose || e.estado == EncimeraFogones.Estado.Frito;
        if (reproducirSonido) {
            audioSource.Play();
        } else { 
            audioSource.Pause();
        }
    }
}
