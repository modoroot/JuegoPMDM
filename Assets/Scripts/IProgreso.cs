using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProgreso
{
    public event EventHandler<CambioProgresoEventArgs> CambioProgreso;
    public class CambioProgresoEventArgs : EventArgs {
        public float progresoNormalizado;
    }
}
