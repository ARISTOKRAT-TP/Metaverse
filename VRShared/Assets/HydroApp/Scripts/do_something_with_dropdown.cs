using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class do_something_with_dropdown : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropdown;
    // public GameObject myObject;
    public static do_something_with_dropdown instance;
    public bool selectValue;
    // public GameObject tmpObject;

    int pick_entry_index = 0;
    string _selected_option = "";
    public void Start()
    {
        if (instance == null) {instance = this;}
        pick_entry_index = dropdown.value;
        // _selected_option = dropdown.options[pick_entry_index].text;
        if (pick_entry_index == 0) {
            selectValue = false;
        } else selectValue = true;
        Debug.Log(pick_entry_index);
        // Debug.Log(GlobalScript.selected_option);
        // tmpObject.SetActive(true);
        // tmpObject.SetActive(false);
        
    }
    public void GetDropdownValue()
    {
        pick_entry_index = dropdown.value;
        // _selected_option = dropdown.options[pick_entry_index].text;

        if (pick_entry_index == 0) {
            selectValue = false;
        } else selectValue = true;
        Debug.Log(pick_entry_index);
        Debug.Log(selectValue);
    }

    // void Awake()
    // {
       
    // }

    // private void OnDestroy()
    // {
    //     if (_selected_option != "")
    //     {
    //         GlobalScript.selected_option = _selected_option;
    //     }
    // }

    // private void OnEnable()
    // {
    //     _selected_option = GlobalScript.selected_option;
    // }
}