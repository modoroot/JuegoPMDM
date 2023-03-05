using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * Clase "JugadorAnimator" que se utiliza para controlar la animaci�n del jugador 
 * en funci�n de si est� caminando o no.
 * @author amna
 * 
 */
public class JugadorAnimator : MonoBehaviour
{
    //Identificador del par�metro de la animaci�n en Unity que representa si el jugador est� corriendo o no.
    private const string IS_RUNNING = "IsRunning";
    // Referencia a la clase Jugador para utilizar sus m�todos (en este caso solamente IsWalking)
    [SerializeField] private Jugador jugador;
    //Instancia de Animator para controlar la animaci�n del jugador.
    private Animator animator;
    private void Awake() {
        //Obtenci�n de la instancia de Animator.
        animator = GetComponent<Animator>();
    }
    private void Update() {
        //Se establece el valor del par�metro de la animaci�n "IsRunning" en funci�n de si el jugador est� caminando o no.
        animator.SetBool(IS_RUNNING, jugador.IsWalking());
    }
}
