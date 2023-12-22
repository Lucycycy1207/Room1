using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElecCar : Car
{
    private float totalElec;
    private float totalMile;
    private CarType carType = CarType.ElecCar;

    public override float GasPerMile()
    {
        return 0;
    }
    public float ElecPerMile(float totalElec, float totalMile)
    {
        return totalElec / totalMile;
    }
}
