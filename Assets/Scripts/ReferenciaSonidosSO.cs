using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ReferenciaSonidosSO : ScriptableObject {
    public AudioClip[] cortar;
    public AudioClip[] pedidoFallido;
    public AudioClip[] pedidoExitoso;
    public AudioClip[] pasos;
    public AudioClip[] tirarObjeto;
    public AudioClip[] cogerObjeto;
    public AudioClip fogonesSonido;
    public AudioClip[] basura;
    public AudioClip[] advertencia;
}
