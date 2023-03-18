using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basura : ContenedorBase
{
    public override void Interactuar(Jugador jugador) {
        if (jugador.objInteractuableActivo()) { 
            jugador.GetObjetoInteractuable().DestroySelf();
        }
    }
}
