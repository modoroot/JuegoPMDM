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
    // Propiedad que devuelve la instancia de la clase Jugador (patrón Singleton)
    public static Jugador Instancia { get; private set; }

    //Código para configurar el patrón Singleton. Se utiliza para que sólo exista una instancia de la clase.
    //En este caso, que sólo se pueda seleccionar un objeto a la vez.
    public event EventHandler<ObjetoSeleccionadoCambiadoEventArgs> ObjetoSeleccionadoCambiado;

    public class ObjetoSeleccionadoCambiadoEventArgs : EventArgs {
        public ContenedorBase objetoSeleccionado;
    }

    // Para que el valor de la velocidad sea editable en el inspector de Unity he utilizado SerializeField
    [SerializeField] private float velocidad = 6f;
    [SerializeField] private GameInput gameInput;
    // Define la capa para el raycast de interacción con objetos
    [SerializeField] private LayerMask layerMaskObjeto;
    // Variable para saber si el jugador se está moviendo
    private bool isWalking;
    // Dirección en la que el jugador interactuó por última vez
    private Vector3 direccionUltimaInteraccion;
    // Objeto que el jugador está seleccionando
    private ContenedorBase objetoSeleccionado;

    private ObjetoInteractuable objInteractuable;
    [SerializeField] private Transform objetoAgarradoPosicionado;

    /**
     * Método que se ejecuta al instanciar la clase. Se utiliza para configurar el patrón Singleton.
     */
    private void Awake() {
        if (Instancia != null) {
            Debug.LogError("Bug. Existe más de una instancia de Jugador");
        }
        // Se asigna la instancia de la clase a la propiedad Instancia
        Instancia = this;

    }
    private void Start() {
        gameInput.OnInteractuarAccion += GameInput_InteractuarAccion;
        gameInput.OnInteractuarAccionAlternativa += GameInput_OnInteractuarAccionAlternativa;
    }

    private void GameInput_OnInteractuarAccionAlternativa(object sender, EventArgs e) {
        //Si hay un objeto seleccionado, se llama a la función de Interactuar de un objeto en concreto
        if (objetoSeleccionado != null) {
            objetoSeleccionado.InteractuarAlternativo(this);
        }
    }

    /**
     * Método que interactúa con el objeto que tiene delante. Tiene en cuenta la posición del jugador
     * y la dirección en la que se está moviendo
     * para así saber si puede interactuar o no.
     */
    private void GameInput_InteractuarAccion(object sender, System.EventArgs e) {
        //Si hay un objeto seleccionado, se llama a la función de Interactuar de un objeto en concreto
        if (objetoSeleccionado != null) {
            objetoSeleccionado.Interactuar(this);
        }


    }

    private void Update() {
        // Maneja el movimiento del jugador
        HandleMovimiento();
        // Maneja la interacción del jugador
        HandleInteraccion();

    }


    /**
     * Devuelve el valor de la variable isWalking
     */
    public bool IsWalking() {
        return isWalking;
    }

    private void HandleInteraccion() {
        // Obtiene el vector de entrada normalizado mediante el método GetMovementVector2Normalizado() de la clase GameInput.
        Vector2 inputVector = gameInput.GetMovementVector2Normalizado();
        /*
         *Crea un vector 3D llamado "movDireccion" que representa la dirección del movimiento del jugador en el plano XZ (plano horizontal).
         *   El componente "x" del vector de entrada se asigna al componente "x" de "movDireccion".
         *   El componente "y" de "movDireccion" se establece en 0 para que el jugador no se mueva verticalmente.
         *   El componente "y" del vector de entrada se asigna al componente "z" de "movDireccion".
         */
        Vector3 movDireccion = new(inputVector.x, 0f, inputVector.y);
        // Actualiza la dirección de la última interacción del jugador si se está moviendo en una dirección
        if (movDireccion != Vector3.zero) {
            direccionUltimaInteraccion = movDireccion;
        }
        // Define la distancia máxima a la que se puede interactuar con un objeto
        float distanciaInteraccion = 2f;
        // Realiza un raycast en la dirección de la última interacción del jugador
        if (Physics.Raycast(transform.position, direccionUltimaInteraccion, out RaycastHit raycastHit, distanciaInteraccion, layerMaskObjeto)) {
            // Si choca con un objeto interactuable, lo interactúa
            if (raycastHit.transform.TryGetComponent(out ContenedorBase contenedorBase)) {
                //Gestiona la selección del objeto. Si hay uno, lo selecciona. Si no, será nulo.
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
        // Obtiene el vector de entrada normalizado mediante el método GetMovementVector2Normalizado() de la clase GameInput.
        Vector2 inputVector = gameInput.GetMovementVector2Normalizado();
        /*
         *Crea un vector 3D llamado "movDireccion" que representa la dirección del movimiento del jugador en el plano XZ (plano horizontal).
         *   El componente "x" del vector de entrada se asigna al componente "x" de "movDireccion".
         *   El componente "y" de "movDireccion" se establece en 0 para que el jugador no se mueva verticalmente.
         *   El componente "y" del vector de entrada se asigna al componente "z" de "movDireccion".
         */
        Vector3 movDireccion = new(inputVector.x, 0f, inputVector.y);

        float distMovimiento = velocidad * Time.deltaTime;
        float radioJugador = .5f;
        float alturaJugador = 4f;
        // Comprueba si el jugador colisiona con algún objeto en la dirección de movimiento usando un CapsuleCast.
        bool moverJugador = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * alturaJugador,
            radioJugador, movDireccion, distMovimiento);


        // No nos podemos mover hacia la dirección deseada
        if (!moverJugador) {
            Vector3 moverEjeX = new Vector3(movDireccion.x, 0, 0).normalized;
            moverJugador = movDireccion.x != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up
                * alturaJugador, radioJugador, moverEjeX, distMovimiento);
            if (moverJugador) {
                //Sólo se mueve el muñeco en el eje X
                movDireccion = moverEjeX;
            } else {
                Vector3 moverEjeZ = new Vector3(0, 0, movDireccion.z).normalized;
                moverJugador = movDireccion.z != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up
                    * alturaJugador, radioJugador, moverEjeZ, distMovimiento);
                if (moverJugador) {
                    movDireccion = moverEjeZ;
                } else {
                    // No se puede mover en ninguna dirección
                }
            }
        }
        // Si el jugador no colisiona con ningún objeto, se mueve en la dirección de movimiento.
        if (moverJugador) {
            // Mueve al jugador en la dirección "movDireccion" multiplicando la velocidad por el tiempo transcurrido.
            transform.position += movDireccion * distMovimiento;
        }


        // Comprueba si el vector de dirección de movimiento no es igual al vector cero (0,0,0). Si es así, significa que el
        // jugador está caminando en una dirección determinada.
        // Si el jugador está caminando, la variable "isWalking" se establece en true. Si no, se asigna a false.
        isWalking = movDireccion != Vector3.zero;

        // Establece la dirección hacia la cual el objeto está mirando (forward) mediante un
        // Slerp entre su dirección actual y la dirección de movimiento deseada.
        // El primer argumento de Slerp es la dirección actual del objeto (forward).
        // El segundo argumento es la dirección de movimiento deseada (movDireccion).
        // El tercer argumento es el tiempo que se tarda en alcanzar la dirección deseada (Time.deltaTime * velocidadRotacion).
        // Slerp hace que el objeto gire de forma fluida hacia la dirección de movimiento, sin cambiar bruscamente de dirección.
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