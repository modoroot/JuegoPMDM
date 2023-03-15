using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetoInteractuable : MonoBehaviour
{
    [SerializeField] private ObjetoInteractuableSO objetoInteractuableSO;
    /**
     * Método para obtener el objeto interactuable
     */
    public ObjetoInteractuableSO GetObjetoInteractuableSO() {
        return objetoInteractuableSO;
    }
}
