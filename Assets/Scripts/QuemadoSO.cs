using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class QuemadoSO : ScriptableObject {
    public ObjetoInteractuableSO input;
    public ObjetoInteractuableSO output;
    public float tiempoQuemadoMaximo;
}
