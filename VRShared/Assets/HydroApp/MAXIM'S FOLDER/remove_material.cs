using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class remove_material : MonoBehaviour
{
    MeshRenderer meshRenderer;
    public Material remove_mat;

    void Start() {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void remove_glow_outline() {
        meshRenderer.material = remove_mat;
    }
}
