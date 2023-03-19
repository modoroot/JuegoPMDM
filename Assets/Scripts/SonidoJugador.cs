using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonidoJugador : MonoBehaviour {



    private Jugador jugador;
    private float temporizadorPasos;
    private float temporizadorPasosMax = .1f;

    private void Awake() {
        jugador = GetComponent<Jugador>();
    }

    private void Update() {
        temporizadorPasos -= Time.deltaTime;
        if (temporizadorPasos < 0f) {
            temporizadorPasos = temporizadorPasosMax;

            if (jugador.IsWalking()) {
                float volumen = 1f;
                GestorSonido.Instance.SonidoPasos(jugador.transform.position, volumen);
            }
        }
    }
}
