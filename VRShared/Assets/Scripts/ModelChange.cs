using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelChange : MonoBehaviour
{
    public GameObject model;

    void Awake()
    {
        model = Resources.Load(AvatarSelection.instance.avatarPath) as GameObject;
        GameObject newPrefabInstance = Instantiate(model);
    }

    void Update()
    {
        
    }
}
