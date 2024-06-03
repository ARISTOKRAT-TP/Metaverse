using System.Collections;
using System.Collections.Generic;
using Fusion.Addons.ConnectionManagerAddon;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AvatarSelection : MonoBehaviour
{
    public static AvatarSelection instance;
    public string usname;
    [SerializeField] TMP_InputField nameInputField;
     // Start is called before the first frame update
     void Start() {
        if (instance == null) {instance = this;}
     }
     public GameObject[] characters;
     private string[] avatarName = {"TestRigAnn","ArtRig","KateAvatar","ArthurAvatar"};
     public string avatarPath;
     public string sceneName = "HydroArena";

	public int selectedCharacter = 0;

	public void NextCharacter()
	{
		characters[selectedCharacter].SetActive(false);
		selectedCharacter = (selectedCharacter + 1) % characters.Length;
		characters[selectedCharacter].SetActive(true);
        characters[selectedCharacter].transform.rotation = Quaternion.identity;
	}

	public void PreviousCharacter()
    {
        characters[selectedCharacter].SetActive(false);
        selectedCharacter = (selectedCharacter - 1 + characters.Length) % characters.Length;
        characters[selectedCharacter].SetActive(true);
        characters[selectedCharacter].transform.rotation = Quaternion.identity;
    }
    public void SubmitName()
    {
        avatarPath = avatarName[selectedCharacter];
        // PlayerPrefs.SetInt("selectedCharacter", selectedCharacter);
        usname = nameInputField.text == "" ? "NewUser" : nameInputField.text;
        SceneManager.LoadScene(sceneName);
    }
}
