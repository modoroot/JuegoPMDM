using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObjetoInteractuablePadre {
    public Transform GetObjetoInteractuableConTransform();


    public ObjetoInteractuable GetObjetoInteractuable();

    public void SetObjInteractuable(ObjetoInteractuable objInteractuable);

    public void ClearObjInteractuable();

    public bool objInteractuableActivo();
}