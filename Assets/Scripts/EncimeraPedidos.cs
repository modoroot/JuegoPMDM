using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncimeraPedidos : ContenedorBase
{
    public static EncimeraPedidos Instance { get; private set; }

    private void Awake() {
        Instance = this;
    }

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
