using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * Clase que se utiliza para todo lo relacionado con el jugador: movimiento, interacciones
 * con objetos, etc.
 * 
 */
public class Jugador : MonoBehaviour, IObjetoInteractuablePadre {
    // Propiedad que devuelve la instancia de la clase Jugador (patr�n Singleton)
    public static Jugador Instancia { get; private set; }

    //C�digo para configurar el patr�n Singleton. Se utiliza para que s�lo exista una instancia de la clase.
    //En este caso, que s�lo se pueda seleccionar un objeto a la vez.
    public event EventHandler<ObjetoSeleccionadoCambiadoEventArgs> ObjetoSeleccionadoCambiado;

    public class ObjetoSeleccionadoCambiadoEventArgs : EventArgs {
        public ContenedorBase objetoSeleccionado;
    }

    // Para que el valor de la velocidad sea editable en el inspector de Unity he utilizado SerializeField
    [SerializeField] private float velocidad = 6f;
    [SerializeField] private GameInput gameInput;
    // Define la capa para el raycast de interacci�n con objetos
    [SerializeField] private LayerMask layerMaskObjeto;
    // Variable para saber si el jugador se est� moviendo
    private bool isWalking;
    // Direcci�n en la que el jugador interactu� por �ltima vez
    private Vector3 direccionUltimaInteraccion;
    // Objeto que el jugador est� seleccionando
    private ContenedorBase objetoSeleccionado;

    private ObjetoInteractuable objInteractuable;
    [SerializeField] private Transform objetoAgarradoPosicionado;

    /**
     * M�todo que se ejecuta al instanciar la clase. Se utiliza para configurar el patr�n Singleton.
     */
    private void Awake() {
        if (Instancia != null) {
            Debug.LogError("Bug. Existe m�s de una instancia de Jugador");
        }
        // Se asigna la instancia de la clase a la propiedad Instancia
        Instancia = this;

    }
    private void Start() {
        gameInput.OnInteractuarAccion += GameInput_InteractuarAccion;
        gameInput.OnInteractuarAccionAlternativa += GameInput_OnInteractuarAccionAlternativa;
    }

    private void GameInput_OnInteractuarAccionAlternativa(object sender, EventArgs e) {
        //Si hay un objeto seleccionado, se llama a la funci�n de Interactuar de un objeto en concreto
        if (objetoSeleccionado != null) {
            objetoSeleccionado.InteractuarAlternativo(this);
        }
    }

    /**
     * M�todo que interact�a con el objeto que tiene delante. Tiene en cuenta la posici�n del jugador
     * y la direcci�n en la que se est� moviendo
     * para as� saber si puede interactuar o no.
     */
    private void GameInput_InteractuarAccion(object sender, System.EventArgs e) {
        //Si hay un objeto seleccionado, se llama a la funci�n de Interactuar de un objeto en concreto
        if (objetoSeleccionado != null) {
            objetoSeleccionado.Interactuar(this);
        }


    }

    private void Update() {
        // Maneja el movimiento del jugador
        HandleMovimiento();
        // Maneja la interacci�n del jugador
        HandleInteraccion();

    }


    /**
     * Devuelve el valor de la variable isWalking
     */
    public bool IsWalking() {
        return isWalking;
    }

    private void HandleInteraccion() {
        // Obtiene el vector de entrada normalizado mediante el m�todo GetMovementVector2Normalizado() de la clase GameInput.
        Vector2 inputVector = gameInput.GetMovementVector2Normalizado();
        /*
         *Crea un vector 3D llamado "movDireccion" que representa la direcci�n del movimiento del jugador en el plano XZ (plano horizontal).
         *   El componente "x" del vector de entrada se asigna al componente "x" de "movDireccion".
         *   El componente "y" de "movDireccion" se establece en 0 para que el jugador no se mueva verticalmente.
         *   El componente "y" del vector de entrada se asigna al componente "z" de "movDireccion".
         */
        Vector3 movDireccion = new(inputVector.x, 0f, inputVector.y);
        // Actualiza la direcci�n de la �ltima interacci�n del jugador si se est� moviendo en una direcci�n
        if (movDireccion != Vector3.zero) {
            direccionUltimaInteraccion = movDireccion;
        }
        // Define la distancia m�xima a la que se puede interactuar con un objeto
        float distanciaInteraccion = 2f;
        // Realiza un raycast en la direcci�n de la �ltima interacci�n del jugador
        if (Physics.Raycast(transform.position, direccionUltimaInteraccion, out RaycastHit raycastHit, distanciaInteraccion, layerMaskObjeto)) {
            // Si choca con un objeto interactuable, lo interact�a
            if (raycastHit.transform.TryGetComponent(out ContenedorBase contenedorBase)) {
                //Gestiona la selecci�n del objeto. Si hay uno, lo selecciona. Si no, ser� nulo.
                if (contenedorBase != objetoSeleccionado) {
                    SetObjetoSeleccionado(contenedorBase);
                }
            } else {
                SetObjetoSeleccionado(null);
            }
        } else {
            SetObjetoSeleccionado(null);
        }
    }

    private void HandleMovimiento() {
        // Obtiene el vector de entrada normalizado mediante el m�todo GetMovementVector2Normalizado() de la clase GameInput.
        Vector2 inputVector = gameInput.GetMovementVector2Normalizado();
        /*
         *Crea un vector 3D llamado "movDireccion" que representa la direcci�n del movimiento del jugador en el plano XZ (plano horizontal).
         *   El componente "x" del vector de entrada se asigna al componente "x" de "movDireccion".
         *   El componente "y" de "movDireccion" se establece en 0 para que el jugador no se mueva verticalmente.
         *   El componente "y" del vector de entrada se asigna al componente "z" de "movDireccion".
         */
        Vector3 movDireccion = new(inputVector.x, 0f, inputVector.y);

        float distMovimiento = velocidad * Time.deltaTime;
        float radioJugador = .5f;
        float alturaJugador = 4f;
        // Comprueba si el jugador colisiona con alg�n objeto en la direcci�n de movimiento usando un CapsuleCast.
        bool moverJugador = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * alturaJugador,
            radioJugador, movDireccion, distMovimiento);


        // No nos podemos mover hacia la direcci�n deseada
        if (!moverJugador) {
            Vector3 moverEjeX = new Vector3(movDireccion.x, 0, 0).normalized;
            moverJugador = movDireccion.x != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up
                * alturaJugador, radioJugador, moverEjeX, distMovimiento);
            if (moverJugador) {
                //S�lo se mueve el mu�eco en el eje X
                movDireccion = moverEjeX;
            } else {
                Vector3 moverEjeZ = new Vector3(0, 0, movDireccion.z).normalized;
                moverJugador = movDireccion.z != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up
                    * alturaJugador, radioJugador, moverEjeZ, distMovimiento);
                if (moverJugador) {
                    movDireccion = moverEjeZ;
                } else {
                    // No se puede mover en ninguna direcci�n
                }
            }
        }
        // Si el jugador no colisiona con ning�n objeto, se mueve en la direcci�n de movimiento.
        if (moverJugador) {
            // Mueve al jugador en la direcci�n "movDireccion" multiplicando la velocidad por el tiempo transcurrido.
            transform.position += movDireccion * distMovimiento;
        }


        // Comprueba si el vector de direcci�n de movimiento no es igual al vector cero (0,0,0). Si es as�, significa que el
        // jugador est� caminando en una direcci�n determinada.
        // Si el jugador est� caminando, la variable "isWalking" se establece en true. Si no, se asigna a false.
        isWalking = movDireccion != Vector3.zero;

        // Establece la direcci�n hacia la cual el objeto est� mirando (forward) mediante un
        // Slerp entre su direcci�n actual y la direcci�n de movimiento deseada.
        // El primer argumento de Slerp es la direcci�n actual del objeto (forward).
        // El segundo argumento es la direcci�n de movimiento deseada (movDireccion).
        // El tercer argumento es el tiempo que se tarda en alcanzar la direcci�n deseada (Time.deltaTime * velocidadRotacion).
        // Slerp hace que el objeto gire de forma fluida hacia la direcci�n de movimiento, sin cambiar bruscamente de direcci�n.
        float velocidadRotacion = 10f;
        transform.forward = Vector3.Slerp(transform.forward, movDireccion, Time.deltaTime * velocidadRotacion);
    }
    private void SetObjetoSeleccionado(ContenedorBase objetoSeleccionado) {
        this.objetoSeleccionado = objetoSeleccionado;
        // Lanza el evento de que el objeto seleccionado ha cambiado
        // el primer campo objetoSeleccionado se refiere a la instancia de la clase que lanza el evento, y el segundo al de la clase ClearObjeto
        ObjetoSeleccionadoCambiado?.Invoke(this, new ObjetoSeleccionadoCambiadoEventArgs { objetoSeleccionado = objetoSeleccionado });
    }

    public Transform GetObjetoInteractuableConTransform() {
        return objetoAgarradoPosicionado;
    }


    public ObjetoInteractuable GetObjetoInteractuable() {
        return objInteractuable;
    }

    public void SetObjInteractuable(ObjetoInteractuable objInteractuable) {
        this.objInteractuable = objInteractuable;
    }

    public void ClearObjInteractuable() {
        objInteractuable = null;
    }

    public bool objInteractuableActivo() {
        return objInteractuable != null;
    }
}