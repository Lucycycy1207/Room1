using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fallastar : Enemy
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        health = new Health(50, 50, 0);
    }

    // Update is called once per frame
    protected override void Update()
    {
        Debug.Log("Fallastar" + health);
    }
}
