using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Diagnostics;

public class HPLevelController : MonoBehaviour
{
    public TMP_Text hpText;
    public GameObject hpImage;
    private float hpImageWidth;
    private Vector2 HPStartImagePosition;
    private float HPStartLevel;
    private float HPLevel;
    private Unit playerUnitComponent;

    void SetImageWidth()
    {
        float percentage = HPLevel / HPStartLevel;
        float newSize = percentage * this.hpImageWidth ;
        hpImage.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, newSize);
        float currentWidth = hpImage.GetComponent<RectTransform>().sizeDelta.x *
                             hpImage.GetComponent<RectTransform>().localScale.x;
        UnityEngine.Debug.Log("currentWidth = " + currentWidth);
        float offset = this.hpImageWidth - currentWidth;
        UnityEngine.Debug.Log("this.HPStartImagePosition.x = " + this.HPStartImagePosition.x);
        hpImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(this.HPStartImagePosition.x - offset/2, 0);
        UnityEngine.Debug.Log("newSize = " + newSize);
        UnityEngine.Debug.Log("anchoredPosition = " + hpImage.GetComponent<RectTransform>().anchoredPosition);
        UnityEngine.Debug.Log("offset = " + offset);
    }
    void Start()
    {
        this.playerUnitComponent = GameObject.Find("Player").GetComponent<Unit>();
        this.HPStartLevel = playerUnitComponent.Health;
        this.hpImageWidth = 0;
    }
    void Update()
    {
        this.HPLevel = playerUnitComponent.Health;
        this.hpText.text = HPLevel.ToString() + "/" + HPStartLevel.ToString();
        if(hpImageWidth == 0)
        {
            this.hpImageWidth = hpImage.GetComponent<RectTransform>().sizeDelta.x * hpImage.GetComponent<RectTransform>().localScale.x;
            this.HPStartImagePosition = hpImage.GetComponent<RectTransform>().anchoredPosition;
        }

        SetImageWidth();
    }
}
