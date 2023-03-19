using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestorJuego : MonoBehaviour {

    public static GestorJuego Instance { get; private set; }

    public event EventHandler OnEstadoCambiado;


    private enum Estado {
        Esperando,
        CuentaAtras,
        JuegoEmpezado,
        JuegoTerminado,
    }

    private Estado estado;

    private float esperandoTemporizador = 1f;
    private float cuentaAtrasTemporizador = 3f;
    private float juegoEmpezadoTemporizador;
    private float juegoEmpezadoTemporizadorMax = 30f;

    private void Awake() {
        Instance = this;

        estado = Estado.Esperando;
    }

    private void Update() {
        switch (estado) {
            case Estado.Esperando:
                esperandoTemporizador -= Time.deltaTime;
                if (esperandoTemporizador < 0f) {
                    estado = Estado.CuentaAtras;
                    OnEstadoCambiado?.Invoke(this, EventArgs.Empty);
                }
                break;
            case Estado.CuentaAtras:
                cuentaAtrasTemporizador -= Time.deltaTime;
                if (cuentaAtrasTemporizador < 0f) {
                    estado = Estado.JuegoEmpezado;
                    juegoEmpezadoTemporizador = juegoEmpezadoTemporizadorMax;
                    OnEstadoCambiado?.Invoke(this, EventArgs.Empty);
                }
                break;
            case Estado.JuegoEmpezado:
                juegoEmpezadoTemporizador -= Time.deltaTime;
                if (juegoEmpezadoTemporizador < 0f) {
                    estado = Estado.JuegoTerminado;
                    OnEstadoCambiado?.Invoke(this, EventArgs.Empty);
                }
                break;
            case Estado.JuegoTerminado:
                break;
        }
    }


    public bool IsJuegoEmpezado() {
        return estado == Estado.JuegoEmpezado;
    }


    public bool IsCuentaAtrasActivada() {
        return estado == Estado.CuentaAtras;
    }

    public float GetCuentaAtrasTemporizador() {
        return cuentaAtrasTemporizador;
    }

    public bool IsJuegoTerminado() {
        return estado == Estado.JuegoTerminado;
    }

    public float GetTiempoRestanteNormalized() {
        return 1 - (juegoEmpezadoTemporizador / juegoEmpezadoTemporizadorMax);
    }
}
