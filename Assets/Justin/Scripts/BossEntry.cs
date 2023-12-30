using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossEntry : MonoBehaviour
{
    [SerializeField]
    public GameObject BossFlashPanel;

    private float FlashTimer;
    private float ClearFlashTimer;

    private Color RedFlash;
    private Color ClearFlash;

    // Start is called before the first frame update
    void Start()
    {
        BossAlert();
    }

    // Update is called once per frame
    public void BossAlert()
    {
        BossFlashPanel.SetActive(true);
        
    }
    void Update()
    {
        FlashTimer = FlashTimer + Time.deltaTime/2;
        ClearFlashTimer = ClearFlashTimer + Time.deltaTime / 2;
        //Debug.Log(FlashTimer);
        BossFlashClear();
   
    }

    void BossFlashClear()
    {
        Color RedFlash = new Color(178, 1, 1, 100);
        Color ClearFlash = new Color(178, 1, 1, 0);
        if (FlashTimer >= 0.0f && FlashTimer <= 1.1f) BossFlashPanel.GetComponent<Image>().color = Color.Lerp(Color.clear, Color.red, FlashTimer);
        if (ClearFlashTimer >= 1.1f && ClearFlashTimer <= 2.1f) FlashFromRed();
    }

    private void FlashFromRed()
    {
        BossFlashPanel.GetComponent<Image>().color = Color.Lerp(Color.red, Color.clear, ClearFlashTimer);
    }
}
