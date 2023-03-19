using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestorSonido : MonoBehaviour {
    public static GestorSonido Instance { get; private set; }
    [SerializeField] private ReferenciaSonidosSO referenciaSonidosSO;


    private void Awake() {
        Instance = this;
    }

    private void Start() {
        GestorPedidos.Instance.OnRecetaCompletada += GestorPedidos_OnRecetaCompletada;
        GestorPedidos.Instance.OnRecetaFallida += GestorPedidos_OnRecetaFallida;
        EncimeraTrocear.OnCortarSonido += EncimeraTrocear_OnCortarSonido;
        Jugador.Instancia.OnCogerObjeto += Instancia_OnCogerObjeto;
        ContenedorBase.OnObjetoColocado += ContenedorBase_OnObjetoColocado;
        Basura.OnObjetoTiradoBasura += Basura_OnObjetoTiradoBasura;
    }

    private void Basura_OnObjetoTiradoBasura(object sender, System.EventArgs e) {
        Basura basura = sender as Basura;
        ReproducirSonido(referenciaSonidosSO.basura, basura.transform.position);
    }

    private void ContenedorBase_OnObjetoColocado(object sender, System.EventArgs e) {
        ContenedorBase contenedorBase = sender as ContenedorBase;
        ReproducirSonido(referenciaSonidosSO.tirarObjeto, contenedorBase.transform.position);
    }

    private void Instancia_OnCogerObjeto(object sender, System.EventArgs e) {
        ReproducirSonido(referenciaSonidosSO.cogerObjeto, Jugador.Instancia.transform.position);
    }

    private void EncimeraTrocear_OnCortarSonido(object sender, System.EventArgs e) {
        EncimeraTrocear encimeraTrocear = sender as EncimeraTrocear;
        ReproducirSonido(referenciaSonidosSO.cortar, encimeraTrocear.transform.position);
    }

    private void GestorPedidos_OnRecetaFallida(object sender, System.EventArgs e) {
        EncimeraPedidos encimeraPedidos = EncimeraPedidos.Instance;
        ReproducirSonido(referenciaSonidosSO.pedidoFallido, encimeraPedidos.transform.position);
    }

    private void GestorPedidos_OnRecetaCompletada(object sender, System.EventArgs e) {
        EncimeraPedidos encimeraPedidos = EncimeraPedidos.Instance;
        ReproducirSonido(referenciaSonidosSO.pedidoExitoso, encimeraPedidos.transform.position);
    }

    private void ReproducirSonido(AudioClip audioClip, Vector3 posicion, float volumen = 1f) {
        AudioSource.PlayClipAtPoint(audioClip, posicion, volumen);
    }
    private void ReproducirSonido(AudioClip[] audioClipArray, Vector3 posicion, float volumen = 1f) {
        ReproducirSonido(audioClipArray[Random.Range(0, audioClipArray.Length)], posicion, volumen);
    }

    public void SonidoPasos(Vector3 posicion, float volumen) {
        ReproducirSonido(referenciaSonidosSO.pasos, posicion, volumen);
    }
}
