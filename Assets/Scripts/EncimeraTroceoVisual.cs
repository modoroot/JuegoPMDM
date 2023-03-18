using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncimeraTroceoVisual : MonoBehaviour
{

    private const string CUT = "Cut";
    [SerializeField] private EncimeraTrocear encimeraTrocear;
    private Animator animator;
    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        encimeraTrocear.OnCortar += EncimeraTrocear_OnCortar;
    }

    private void EncimeraTrocear_OnCortar(object sender, EventArgs e) {
        animator.SetTrigger(CUT);
    }
}
