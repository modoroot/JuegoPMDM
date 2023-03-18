using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncimeraPedidos : ContenedorBase
{
    public override void Interactuar(Jugador jugador) {
        if (jugador.objInteractuableActivo()) {
            if (jugador.GetObjetoInteractuable().TryGetPlato(out PlatoObjetoInteractuable platoObjetoInteractuable)) {
                //Sólo acepta platos
                GestorPedidos.Instance.PedidoReceta(platoObjetoInteractuable);
            jugador.GetObjetoInteractuable().DestroySelf();
            }
            
        }
    }
}
