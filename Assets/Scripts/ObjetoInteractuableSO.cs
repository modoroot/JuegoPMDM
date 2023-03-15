using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * Clase para crear objetos interactuables en el juego. La clase hereda de ScriptableObject para
 * poder crear objetos desde el editor de Unity y no tener que crearlos en código.
 */
[CreateAssetMenu()]
public class ObjetoInteractuableSO : ScriptableObject
{
    // Prefab del objeto a instanciar
    public Transform prefab;
    public Sprite sprite;
    public string nombreObjeto;

}
