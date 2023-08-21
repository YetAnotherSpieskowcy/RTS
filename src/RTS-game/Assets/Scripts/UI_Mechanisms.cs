using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;



// ----- enums -----
public enum Mode
{
    NORMAL,
    COMBAT,
    BUILDING
}

// ----- structuress -----
// public struct Storage
// {
//     public int funds;
//     public int wood;
//     public int stones;
// }

// ----- mechanisms -----
public class UI_Mechanisms : MonoBehaviour
{
    public Mode gameMode = Mode.NORMAL;
    //public Storage storage;
    public TMP_Text textF;
    public TMP_Text textW;
    public TMP_Text textS;

    public TMP_Text date;
    private DateTime startDate;
    private bool isRunning;
    public bool clockTrigger;

    public Image compassArrow;
    public float angleRadians;

    public RectTransform compassBarTransform;

    public RectTransform northMarkrerTransform;
    public RectTransform southMarkrerTransform;
    public RectTransform eastMarkrerTransform;
    public RectTransform westMarkrerTransform;

    
    public RectTransform enemyOneMarkrerTransform;
    public RectTransform enemyTwoMarkrerTransform;
    public Transform enemyOneTransform; 
    public Transform enemyTwoTransform; 

    //DONE clock
    //TODO compass
    //DONE storage
    //TODO characters
    //TODO buildings
    //TODO key interrupts
    //TODO comments
    //TODO possible commands

    // ----- storage -----
    void IncreaseSource(TMP_Text sourceT, int value)
    {
        int newI = int.Parse(sourceT.text) + value;
        sourceT.text = newI.ToString();
    }
    void DectraseSource(TMP_Text sourceT, int value)
    {
        sourceT.text = (int.Parse(sourceT.text) + value).ToString();
    }

    // ----- date -----
        public void StartClock()
    {
        isRunning = true;
        clockTrigger = false;
        StartCoroutine(UpdateClock());
    }

    public void StopClock()
    {
        isRunning = false;
        clockTrigger = false;
        StopCoroutine(UpdateClock());
    }

    private System.Collections.IEnumerator UpdateClock()
    {
        while (isRunning)
        {
            startDate = startDate.Add(new System.TimeSpan(0, 0, 1, 0));
            date.text = startDate.ToString();

            yield return new WaitForSeconds(5f);
        }
    }
    // ----- compass -----
 

    public Transform CameraTransform;
    void SetMarkerPosition(RectTransform markerTransform, Vector3 worldPosition){
        Vector3 dirToTarget = worldPosition - CameraTransform.position;
        float angle = Vector2.SignedAngle(new Vector2(dirToTarget.x, dirToTarget.z), new Vector2(CameraTransform.transform.forward.x, CameraTransform.transform.forward.z));
        float compassPositionX = Mathf.Clamp(2*angle/Camera.main.fieldOfView, -1, 1);
        if (compassPositionX == 1 || compassPositionX == (-1) ){
            markerTransform.anchoredPosition = new Vector2(0, 100);
        }
        else{
            markerTransform.anchoredPosition = new Vector2(compassBarTransform.rect.width/2*compassPositionX, 0);
        }
    }

    void UpdateCompass()
    {
        
        /**/
        SetMarkerPosition(northMarkrerTransform, new Vector3(4041,0,60000));
        SetMarkerPosition(southMarkrerTransform, new Vector3(4041,0,-60000));
        SetMarkerPosition(eastMarkrerTransform, new Vector3(60000,0,2300));
        SetMarkerPosition(westMarkrerTransform, new Vector3(-60000,0,2300));
        SetMarkerPosition(eastMarkrerTransform, new Vector3(60000,0,2300));
        SetMarkerPosition(westMarkrerTransform, new Vector3(-60000,0,2300));

        SetMarkerPosition(enemyOneMarkrerTransform, enemyOneTransform.position);
        SetMarkerPosition(enemyTwoMarkrerTransform, enemyTwoTransform.position);
    }

    public Texture2D inputtexture2D;
    public RawImage rawImage;
    
    Texture2D Resize(Texture2D texture2D, int targetX, int targetY)
    {
        RenderTexture rt = new RenderTexture(targetX, targetY, 24);
        RenderTexture.active = rt;
        Graphics.Blit(texture2D, rt);
        Texture2D result = new Texture2D(targetX, targetY);
        result.ReadPixels(new Rect(0, 0, targetX, targetY), 0, 0);
        result.Apply();
        return result;
    }



    void CreateEnemy()
    {
        Texture2D t = Resources.Load<Texture2D>("Assets/Cainos/Pixel Art Icon Pack - RPG/Texture/Weapon & Tool/Iron Sword.png");

        t = Resize(t, 100, 100);

        Sprite s = Sprite.Create(t, new Rect(0.0f, 0.0f, 100.0f, 100.0f), new Vector2(0.0f, 0.0f));

       // s.transform.localScale = new Vector3(20.0f, 20.0f, 1.0f);


        //Camera.main.transform.localEulerAngles.y
    }
    // ----- UI -----
    void Start()
    {
        //rawImage.texture = Resize(inputtexture2D, 200, 100);


        startDate = new DateTime(1500, 1, 1, 8, 0, 0);
        date.text = startDate.ToString();
        isRunning = false;
        clockTrigger = false;
        angleRadians = 0;
        
        CreateEnemy();
    }

    void Update()
    {
        if (clockTrigger)
        {
            if (isRunning)
            {
                StopClock();
                Debug.Log("Clock Stoped");
            }
            else
            {
                StartClock();
                Debug.Log("Clock Started");
            }
        }

        UpdateCompass();

    }
}
