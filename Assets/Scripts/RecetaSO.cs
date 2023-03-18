using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class RecetaSO : ScriptableObject{
    public List<ObjetoInteractuableSO> objetoInteractuableSOList;
    public string nombreReceta;
}
