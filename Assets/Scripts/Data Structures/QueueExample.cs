using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueExample : MonoBehaviour
{

    public GameObject testPrefab;
    public Queue<GameObject> queue = new Queue<GameObject>();
    GameObject tempObject;
    Vector2 lastEnqueuedPosition = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            //Push a new object to the stack
            tempObject = Instantiate(testPrefab, transform);
            if (queue.Count == 0)
            {
                lastEnqueuedPosition = Vector2.zero;
            }
            tempObject.transform.position = new Vector2(lastEnqueuedPosition.x+1, 0);//Calculate testPrefab position
            lastEnqueuedPosition = tempObject.transform.position;
            tempObject.name = "Queued-" + queue.Count;
            tempObject.GetComponent<SpriteRenderer>().color = Random.ColorHSV();

            queue.Enqueue(tempObject);

            Debug.Log("Pushed " + tempObject.name);
        }

        if (Input.GetKeyUp(KeyCode.Q))
        {
            var removedObject = queue.Dequeue();
            Debug.Log("Dequeue from the queue: " + removedObject.name);
            Destroy(removedObject);
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            var topGameObject = queue.Peek();
            topGameObject.GetComponent<SpriteRenderer>().color = Color.red;
            Debug.Log("Object at the start of the queue is: " + topGameObject.name);
        }


    }
}
