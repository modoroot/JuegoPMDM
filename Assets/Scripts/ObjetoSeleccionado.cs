using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * Clase usada para el patr�n Singleton de la selecci�n de los objetos del juego.
 * Un objeto s�lo puede ser seleccionado por un mismo jugador a la vez.
 */
public class ObjetoSeleccionado : MonoBehaviour {

    [SerializeField] private ContenedorBase contenedorBase;
    [SerializeField] private GameObject[] visualGameObjectA;

    private void Start() {
        Jugador.Instancia.ObjetoSeleccionadoCambiado += Jugador_ObjetoSeleccionadoCambiado;
    }
    /**
     * M�todo que gestiona el evento de cambio de capa adicional del objeto seleccionado.
     */
    private void Jugador_ObjetoSeleccionadoCambiado(object sender, Jugador.ObjetoSeleccionadoCambiadoEventArgs e) {
        if (e.objetoSeleccionado == contenedorBase) {
            Mostrar();
        } else {
            Ocultar();
        }
    }
    /**
     * Muestra u oculta la capa adicional del objeto seleccionado creada en el prefab del objeto. 
     * S�lo es una capa adicional visual para que el jugador
     * sepa qu� objeto est� seleccionando en ese momento si pulsa el bot�n de interacci�n
     */
    private void Mostrar() {
        foreach (GameObject visualGameObject in visualGameObjectA) {
            visualGameObject.SetActive(true);
        }
    }

    private void Ocultar() {
        foreach (GameObject visualGameObject in visualGameObjectA) {
            visualGameObject.SetActive(false);
        }
    }
}
