using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public Transform target;

    Vector3 offset;
    public float amount = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - target.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 expectedPosition = target.position + offset;

        Vector3 smoothPosition = Vector3.Lerp(transform.position, expectedPosition, 0.1f);//kind of delay camera follow

        transform.position = smoothPosition;
    }
}
