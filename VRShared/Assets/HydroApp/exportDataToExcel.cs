using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro; 

public class exportDataToExcel : MonoBehaviour
{
    string filename = "";

    [SerializeField]
    Button button;

    [SerializeField]
    TextMeshProUGUI tube11; 

    [SerializeField]
    TextMeshProUGUI tube22;

    [SerializeField]
    TextMeshProUGUI tube33;

    [SerializeField]
    TextMeshProUGUI tube44;

    [SerializeField]
    TextMeshProUGUI tube55;

    // Start is called before the first frame update
    void Start()
    {
        filename = Application.dataPath + "/test.csv";
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WriteCSV()
    {
            TextWriter newTW = new StreamWriter(filename, false);

            newTW.WriteLine("Tube number;1;2;3;4;5");
            newTW.WriteLine("Diametr; 1.77; 1.43; 1.13; 1.54; 2.69");

            newTW.Close();

            newTW = new StreamWriter(filename, true);

            newTW.WriteLine("Hight;" + tube11.text + ";" + tube22.text + ";" +
                      tube33.text + ";" + tube44.text + ";" + tube55.text);
            
            newTW.Close();
    }
}
