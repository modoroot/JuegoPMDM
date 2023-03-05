using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * Clase "JugadorAnimator" que se utiliza para controlar la animación del jugador 
 * en función de si está caminando o no.
 * @author amna
 * 
 */
public class JugadorAnimator : MonoBehaviour
{
    //Identificador del parámetro de la animación en Unity que representa si el jugador está corriendo o no.
    private const string IS_RUNNING = "IsRunning";
    // Referencia a la clase Jugador para utilizar sus métodos (en este caso solamente IsWalking)
    [SerializeField] private Jugador jugador;
    //Instancia de Animator para controlar la animación del jugador.
    private Animator animator;
    private void Awake() {
        //Obtención de la instancia de Animator.
        animator = GetComponent<Animator>();
    }
    private void Update() {
        //Se establece el valor del parámetro de la animación "IsRunning" en función de si el jugador está caminando o no.
        animator.SetBool(IS_RUNNING, jugador.IsWalking());
    }
}
