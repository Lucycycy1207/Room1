using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallastarMovement : MonoBehaviour
{
    [SerializeField]
    public GameObject FallastarHead;
    [SerializeField]
    public GameObject FallastarLArm;
    [SerializeField]
    public GameObject FallastarRArm;

    public int LArmRotation = -20;
    public int RArmRotation = 20;
    public int HeadRotation =0;
    public float AnimationTimer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame

    private void FallastarAnimation()
    {
        FallastarLArm.transform.Rotate(0,0,LArmRotation * Time.deltaTime/2);
        FallastarRArm.transform.Rotate(0, 0, RArmRotation * Time.deltaTime/2);
        FallastarHead.transform.Rotate(0, 0, HeadRotation * Time.deltaTime);
        if (AnimationTimer >= 1f)
        {
            LArmRotation = 20; RArmRotation = -20; HeadRotation = 20;
        }
        if (AnimationTimer >= 2f )
        {
            LArmRotation = -20; RArmRotation = 20; HeadRotation = -20;
            AnimationTimer = 0;
        }
    }
    void Update()
    {
        FallastarAnimation();
        AnimationTimer = AnimationTimer + Time.deltaTime/2;
       
    }
}
