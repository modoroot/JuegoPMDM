using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconoPlatosUI : MonoBehaviour
{
    [SerializeField] private Image image;
    public void SetObjetoInteractuableSO(ObjetoInteractuableSO objetoInteractuableSO) {
        image.sprite = objetoInteractuableSO.sprite;
    }

}
