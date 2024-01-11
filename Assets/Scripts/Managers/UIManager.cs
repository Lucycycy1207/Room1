using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreTxt;
    [SerializeField] private TMP_Text highScoreTxt;
    [SerializeField] private TMP_Text healthTxt;
    [SerializeField] private GameObject levelTxt;

    [Header("PowerUp hint")]
    [SerializeField] private GameObject WASD;
    [SerializeField] private GameObject LeftKey;
    [SerializeField] private GameObject GunPowerUpTxt;
    [SerializeField] private GameObject NukeTxt;
    [SerializeField] private GameObject BlockTxt;
    

    private void Start()
    {
        GunPowerUpTxt.SetActive(false);
        NukeTxt.SetActive(false);
        BlockTxt.SetActive(false);
    }
    public void UpdateLevel()
    {
        EnabledUI(levelTxt, Color.red);
        int currLevel = GameManager.GetInstance().GetLevel();
        if (currLevel == 1)
        {
            UpdateBasicControlHint();
        }
        levelTxt.GetComponent<TMP_Text>().SetText("LEVEL " + currLevel.ToString("00"));
        DisableUI(levelTxt, Color.red, 1);
    }

    void DisableUI(GameObject UItext, Color OriColor, float pauseTime)
    {
        
        if (UItext != null)
        {
            Color UIcolor = UItext.GetComponent<TextMeshProUGUI>().color;
           
            StartCoroutine(LerpColorText(UItext, 2, OriColor, pauseTime));
            
            // Disable the UI element
            //levelTxt.SetActive(false);
        }
    }
    IEnumerator LerpColorText(GameObject UItext, float durationTime, Color OriColor, float pauseTime)
    {
        yield return new WaitForSeconds(pauseTime);

        Color LerpedColor;

        float elapsedTime = 0f;

        while (elapsedTime < durationTime)
        {
            LerpedColor = Color.Lerp(OriColor, Color.clear, elapsedTime/durationTime);
            UItext.GetComponent<TextMeshProUGUI>().color = LerpedColor;

            elapsedTime += Time.deltaTime;
            yield return null;//yield control to other scripts
        }
        UItext.GetComponent<TextMeshProUGUI>().color = Color.clear;
    }
    IEnumerator LerpColorImage(GameObject UItext, float durationTime, Color OriColor, float pauseTime)
    {
        yield return new WaitForSeconds(pauseTime);

        Color LerpedColor;

        float elapsedTime = 0f;

        while (elapsedTime < durationTime)
        {
            LerpedColor = Color.Lerp(OriColor, Color.clear, elapsedTime / durationTime);
            UItext.GetComponent<Image>().color = LerpedColor;

            elapsedTime += Time.deltaTime;
            yield return null;//yield control to other scripts
        }
        UItext.GetComponent<Image>().color = Color.clear;
    }

    void EnabledUI(GameObject UI, Color oriColor)
    {
        if (UI != null)
        {
            //reset Color
            UI.GetComponent<TextMeshProUGUI>().color = oriColor;
            // Disable the UI element
            UI.SetActive(true);
            //Debug.Log("set level txt active");
            
        }
    }

    public void UpdateBasicControlHint()
    {
        WASD.SetActive(true);
        LeftKey.SetActive(true);
        //DisableUI(WASD, Color.white, 3);
        Color UIcolor = WASD.GetComponent<Image>().color;
        StartCoroutine(LerpColorImage(WASD, 2, Color.white, 1));

        DisableUI(LeftKey, LeftKey.GetComponent<TextMeshProUGUI>().color, 1);
    }

    public void UpdatePowerUpHint()
    {
        int currLevel = GameManager.GetInstance().GetLevel();
        switch (currLevel)
        {
            case 1:
                GunPowerUpTxt.SetActive(true);
                DisableUI(GunPowerUpTxt, Color.white, 3);
                break;
            case 2:
                NukeTxt.SetActive(true);
                DisableUI(NukeTxt, Color.white, 3);
                break;
            case 3:
                BlockTxt.SetActive(true);
                DisableUI(BlockTxt, Color.white, 3);
                break;
        }
            
    }


    public void UpdateScore()
    {
        scoreTxt.SetText("Score: " + GameManager.GetInstance().scoreManager.GetScore().ToString("00"));
        highScoreTxt.SetText("HighScore: " + GameManager.GetInstance().scoreManager.GetHighScore().ToString("00"));
        
    }

    public void UpdateHealth()
    {
        float tempHealth = GameManager.GetInstance().GetPlayerHealth();
        //Debug.Log("Curr health: " + tempHealth);
        healthTxt.SetText("health: " + tempHealth.ToString("00"));
    }


}
