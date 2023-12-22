using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DictionaryExample : MonoBehaviour
{
    [SerializeField] private TMP_Text txtTriangle, txtSquare, txtCircle;
    Dictionary<string, int> dictionary = new Dictionary<string, int>();
 
    // Start is called before the first frame update
    void Start()
    {
        dictionary.Add("Triangle", 0);
        dictionary.Add("Square", 0);
        dictionary.Add("Circle", 0);
    }

    // Update is called once per frame
    void Update()
    {

        //Add Triangles
        if (Input.GetKeyUp(KeyCode.Q))
        {
            if (dictionary.ContainsKey("Triangle"))
            {
                dictionary["Triangle"]++;
                txtTriangle.text = dictionary["Triangle"].ToString();
            }
        }

        //Add Squares
        if (Input.GetKeyUp(KeyCode.W))
        {
            if (dictionary.ContainsKey("Square"))
            {
                dictionary["Square"]++;
                txtSquare.text = dictionary["Square"].ToString();
            }
        }

        //Add Circles
        if (Input.GetKeyUp(KeyCode.E))
        {
            if (dictionary.ContainsKey("Circle"))
            {
                dictionary["Circle"]++;
                txtCircle.text = dictionary["Circle"].ToString();
            }
        }


    }
}
