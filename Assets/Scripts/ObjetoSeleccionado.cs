using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * Clase usada para el patrón Singleton de la selección de los objetos del juego.
 * Un objeto sólo puede ser seleccionado por un mismo jugador a la vez.
 */
public class ObjetoSeleccionado : MonoBehaviour {

    [SerializeField] private ContenedorBase contenedorBase;
    [SerializeField] private GameObject[] visualGameObjectA;

    private void Start() {
        Jugador.Instancia.ObjetoSeleccionadoCambiado += Jugador_ObjetoSeleccionadoCambiado;
    }
    /**
     * Método que gestiona el evento de cambio de capa adicional del objeto seleccionado.
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
     * Sólo es una capa adicional visual para que el jugador
     * sepa qué objeto está seleccionando en ese momento si pulsa el botón de interacción
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
