using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basura : ContenedorBase
{

    public static event EventHandler OnObjetoTiradoBasura;

    public override void Interactuar(Jugador jugador) {
        if (jugador.objInteractuableActivo()) { 
            jugador.GetObjetoInteractuable().DestroySelf();

            OnObjetoTiradoBasura?.Invoke(this, EventArgs.Empty);
        }
    }
}
