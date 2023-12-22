using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListExample : MonoBehaviour
{
    public GameObject testPrefab;
    public List<GameObject> listOfGameObjects = new List<GameObject>();
    public int position = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            int randomNumber = Random.Range(0, listOfGameObjects.Count);
            listOfGameObjects[randomNumber].SetActive(false);
            //listOfGameObjects.RemoveAt(randomNumber);
            //capacity is the memory usage(start from 4, then double), count is elment inside the list
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            GenerateObjectFromList(position);
        }
    }

    public void GenerateObjectFromList(int position)
    {
        GameObject tempObject;
        tempObject = Instantiate(testPrefab, transform);
        tempObject.transform.position = new Vector2(position, 0);

        listOfGameObjects.Add(tempObject);
        this.position++;
        Debug.Log($"the count of the list is {listOfGameObjects.Count} and Capacity is {listOfGameObjects.Capacity}");
    }
}
