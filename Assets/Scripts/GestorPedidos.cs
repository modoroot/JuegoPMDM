using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestorPedidos : MonoBehaviour {

    public event EventHandler OnRecetaInvocada;
    public event EventHandler OnRecetaCompletada;
    public static GestorPedidos Instance { get; private set; }
    [SerializeField] private ListaRecetasSO listaRecetasSO;

    private List<RecetaSO> recetaSOList;
    private float invocarTiempoReceta;
    private float invocarTiempoRecetaMax = 4f;
    private int recetasEsperadasMax = 3;

    private void Awake() {
        Instance = this;
        recetaSOList = new List<RecetaSO>();
    }

    private void Update() {
        invocarTiempoReceta -= Time.deltaTime;
        if (invocarTiempoReceta <= 0f) {
            invocarTiempoReceta = invocarTiempoRecetaMax;

            if (recetaSOList.Count < recetasEsperadasMax) {
                RecetaSO recetaEsperadaSO = listaRecetasSO.recetaSOList[UnityEngine.Random.Range(0, listaRecetasSO.recetaSOList.Count)];
                recetaSOList.Add(recetaEsperadaSO);
                OnRecetaInvocada?.Invoke(this, EventArgs.Empty);

            }
        }
    }

    public void PedidoReceta(PlatoObjetoInteractuable platoObjetoInteractuable) {
        for (int i = 0; i < recetaSOList.Count; i++) {
            RecetaSO recetaEsperadaSO = recetaSOList[i];
            if (recetaEsperadaSO.objetoInteractuableSOList.Count == platoObjetoInteractuable.GetObjetoInteractuableSOList().Count) {
                //Si tiene el mismo número de ingredientes
                bool platoIgualQueReceta = true;
                foreach (ObjetoInteractuableSO recetaObjetoInteractuableSO in recetaEsperadaSO.objetoInteractuableSOList) {
                    //Recorre todos los ingredientes de la receta
                    bool ingredienteEncontrado = false;
                    foreach (ObjetoInteractuableSO platoObjetoInteractuableSO in platoObjetoInteractuable.GetObjetoInteractuableSOList()) {
                        //Recorre todos los ingredientes del plato
                        if (platoObjetoInteractuableSO == recetaObjetoInteractuableSO) {
                            //Los ingredientes del plato y la receta coinciden
                            ingredienteEncontrado = true;
                            break;
                        }
                    }
                    if (!ingredienteEncontrado) {
                        //No se han encontrado los ingredientes de la receta
                    }
                }
                if (platoIgualQueReceta) {
                    //El jugador hizo la receta correcta
                    recetaSOList.RemoveAt(i);
                    OnRecetaCompletada?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
        }
        //No se ha encontrado ninguna receta, por lo que el jugador es malísimo
    }
    public List<RecetaSO> GetRecetaEsperadaSOList() {
        return recetaSOList;
    }
}
