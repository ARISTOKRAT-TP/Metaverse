using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelChange : MonoBehaviour
{
    public GameObject model;

    // Start is called before the first frame update
    void Awake()
    {
        model = Resources.Load(AvatarSelection.instance.avatarPath) as GameObject;
        GameObject newPrefabInstance = Instantiate(model);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
