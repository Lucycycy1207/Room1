using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSONText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Serializing Data to JSON
        SampleData sample = new SampleData();
        sample.name = "Rico";
        sample.score = 20;

        string data = JsonUtility.ToJson(sample);
        Debug.Log("ToJson: "+ data);

        // Deserializing JSON data
        string json = "{\n\t\"name\": \"Rico\", \n\t\"score\":  20\n}";


        SampleData sample2 = JsonUtility.FromJson<SampleData>(json);

        Debug.Log("The sample data is " + sample2.name + " and " + sample2.score);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
