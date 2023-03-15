using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * Clase que gestiona la entrada de datos del jugador usando el paquete de Unity Input System.
 *
 */
public class GameInput : MonoBehaviour {

    public event EventHandler OnInteractuarAccion;
    //Atributo que obtiene y gestiona la entrada de datos del jugador.
    private JugadorInputActions playerInputActions;

    /**
     * Habilita la entrada de datos del jugador 
     * 
     */
    private void Awake() {
        //Se activan los eventos de movimiento
        playerInputActions = new();
        playerInputActions.Jugador.Enable();
        //Se asigna una función al evento. No se utilizan los paréntesis porque no es necesario llamar a la función,
        //sino que se le pasa la referencia a la función.
        playerInputActions.Jugador.Interactuar.performed += Interactuar_performed;
    }
    /**
     * Método que interactúa con el objeto que se encuentra delante del jugador presionando la tecla "E" 
     * o el botón inferior derecho del mando.
     * Los botones han sido configurados a partir del paquete Unity Input System.
     */
    private void Interactuar_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        //If simplificado. Sólo se invoca si el evento no es nulo.
        OnInteractuarAccion?.Invoke(this, EventArgs.Empty);
    }

    /**
     * Devuelve un vector 2D que representa la dirección de movimiento del jugador.
     * 
     */
    public Vector2 GetMovementVector2Normalizado() {
        // Vector de entrada que se inicializa a (0,0).
        Vector2 inputVector = playerInputActions.Jugador.Mover.ReadValue<Vector2>();

        // Normaliza el vector de entrada para que su magnitud sea 1.
        inputVector = inputVector.normalized;
        return inputVector;
    }
}