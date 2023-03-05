using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * Clase que gestiona la entrada de datos del jugador usando el paquete de Unity Input System.
 *
 */
public class GameInput : MonoBehaviour {
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