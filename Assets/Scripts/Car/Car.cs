using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    private string model;
    private int year;
    private float totalGasUsage;
    private float totalMiles;
    private CarType carType = CarType.GasCar;
    public virtual float GasPerMile()
    {
        return totalGasUsage / totalMiles;
    }

    public void SetCarType(CarType _carType)
    {
        carType = _carType;
    }

}
