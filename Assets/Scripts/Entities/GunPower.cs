using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class GunPower : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Transform playerTransform;
   

    [SerializeField] private Image GunPowerImg;
    private bool coolingDown = true;
    [SerializeField] private float useTime = 10f;

    
    // Start is called before the first frame update
    void Start()
    {
        
        this.transform.position = cam.WorldToScreenPoint(playerTransform.position);
        
    }

// Update is called once per frame
    void Update()
    {
        if (playerTransform == null)
        {
            gameObject.SetActive(false);
            return;
        }
        this.transform.position = cam.WorldToScreenPoint(playerTransform.position);
        //Reduce fill amount over useTime seconds
        GunPowerImg.fillAmount -= 1.0f / useTime * Time.deltaTime;
    }

    public float GetUseTime()
    {
        return useTime;
    }

    public void ResetGunPowerImg()
    {
        GunPowerImg.fillAmount = 1.0f;
    }

}
