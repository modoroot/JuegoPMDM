using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncimeraPlatosVisual : MonoBehaviour
{
    [SerializeField] private Transform puntoSuperiorObjeto;
    [SerializeField] private EncimeraPlatos encimeraPlatos;
    [SerializeField] private Transform platoVisualPrefab;

    private List<GameObject> platoVisualGameObjectList;


    private void Awake() {
        platoVisualGameObjectList = new List<GameObject>();
    }
    private void Start() {
        encimeraPlatos.OnPlatoInvocado += EncimeraPlatos_OnPlatoInvocado;
        encimeraPlatos.OnPlatoEliminado += EncimeraPlatos_OnPlatoEliminado;
    }

    private void EncimeraPlatos_OnPlatoEliminado(object sender, System.EventArgs e) {
        GameObject platoGameObject = platoVisualGameObjectList[platoVisualGameObjectList.Count - 1];
        platoVisualGameObjectList.Remove(platoGameObject);
        Destroy(platoGameObject);
    }

    private void EncimeraPlatos_OnPlatoInvocado(object sender, System.EventArgs e) {
       Transform platoVisualTransform = Instantiate(platoVisualPrefab, puntoSuperiorObjeto);
        float platosPosicionY = .1f;
        platoVisualTransform.localPosition = new Vector3 (0, platosPosicionY * platoVisualGameObjectList.Count, 0);
        platoVisualGameObjectList.Add(platoVisualTransform.gameObject);
    }
}
