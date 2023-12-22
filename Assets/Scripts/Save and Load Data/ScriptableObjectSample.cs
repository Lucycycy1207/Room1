using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//store data independent of scene

[CreateAssetMenu(fileName = "ScriptableObjectTest", menuName = "Lucy/Create Scriptable Object", order = 1)]
public class ScriptableObjectSample : ScriptableObject
{
    public string objectName;
    public int score;
    public Vector2 startPostion;

}
