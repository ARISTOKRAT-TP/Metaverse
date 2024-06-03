using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class add_material : MonoBehaviour
{
    MeshRenderer meshRenderer;
    public Material outline_mat, remove_mat;

    void Start() {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void add_glow_outline() {
        meshRenderer.material = outline_mat;
        StartCoroutine(waiter());
    }

    public void remove_glow_outline() {
        StopAllCoroutines();
        meshRenderer.material = remove_mat;
    }

    IEnumerator waiter() {
        for (int i = 0; i < 3; ++i) {
            yield return new WaitForSeconds(0.5f);
            meshRenderer.material = remove_mat;
            yield return new WaitForSeconds(0.5f);
            meshRenderer.material = outline_mat;
        }
        yield return new WaitForSeconds(0.5f);
        meshRenderer.material = remove_mat;
    }
    


}
