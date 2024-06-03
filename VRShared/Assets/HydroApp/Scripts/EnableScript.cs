using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableScript : MonoBehaviour
{
    public GameObject[] guides;
    // Start is called before the first frame update
    void Start()
    {
        if (do_something_with_dropdown.instance.selectValue == false)
        {
            for (int i = 0; i < guides.Length; i++)
            {
                guides[i].SetActive(false);
            }
        }
        else
        {
            for (int i = 0; i < guides.Length; i++)
            {
                guides[i].SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
