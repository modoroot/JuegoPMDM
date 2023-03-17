using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContenedorObjetosVisual : MonoBehaviour
{

    private const string ABRIR_CERRAR = "OpenClose";
    [SerializeField] private ContenedorObjetos contenedorObjetos;
    private Animator animator;
    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        contenedorObjetos.JugadorAgarraObjeto += ContenedorObjetos_JugadorAgarraObjeto;
    }

    private void ContenedorObjetos_JugadorAgarraObjeto(object sender, EventArgs e) {
        animator.SetTrigger(ABRIR_CERRAR);
    }
}
