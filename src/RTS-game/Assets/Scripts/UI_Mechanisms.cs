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

    public Image dirN;
    public Image dirS;
    public Image dirE;
    public Image dirW;

    public RectTransform compassBarTransform;

    // for future: public rectTransform objectiveMarkrerTransform;
    public RectTransform northMarkrerTransform;
    public RectTransform southMarkrerTransform;

    //public Transform Camera.main.transform = Camera.main.transform; 
    // for future: public Transform objectTransform; 


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
    /*
    float CalcLeftCorner(float centerOfSight, float fieldOfView)
    {
        float leftCornerOfView = centerOfSight - fieldOfView / 2;
        if (leftCornerOfView < 0)
        {
            leftCornerOfView = 360 - leftCornerOfView;
        }
        return leftCornerOfView;
    }

    float CalcRightCorner(float centerOfSight, float fieldOfView)
    {
        float rightCornerOfView = centerOfSight + fieldOfView / 2;
        if (rightCornerOfView >= 360)
        {
            rightCornerOfView = rightCornerOfView - 360;
        }
        return rightCornerOfView;
    }

    bool isSeen(float point, float leftCornerOfView, float rightCornerOfView)
    {
        bool isseen = false;
        if (leftCornerOfView > rightCornerOfView)
        {
            if (leftCornerOfView <= point && point < 360)
            {
                isseen = true;
            }
            else if (0 <= point && point < rightCornerOfView)
            {
                isseen = true;
            }
        }
        else
        {
            if (leftCornerOfView <= point && point < rightCornerOfView)
            {
                isseen = true;
            }
        }
        return isseen;
    }
*/

    void SetNorth()
    {
        Vector3 worldPosition = Vector3.forward * 1000;
        RectTransform markerTransform = northMarkrerTransform;
        Vector3 dirToTarget = worldPosition - Camera.main.transform.position;
        float angle = Vector2.SignedAngle(new Vector2(dirToTarget.x, dirToTarget.z), new Vector2(Camera.main.transform.transform.forward.x, Camera.main.transform.transform.forward.z));
        float compassPositionX = Mathf.Clamp(2 * angle / Camera.main.fieldOfView, -1, 1);
        markerTransform.anchoredPosition = new Vector2(compassBarTransform.rect.width / 2 * compassPositionX, 0);
        Vector2 v = Camera.main.ViewportToScreenPoint(new Vector2(0.5f, 0.5f));
        Debug.Log("markerTransform.anchoredPosition" + markerTransform.anchoredPosition);
    }

    void SetSouth()
    {
        Vector3 worldPosition = Vector3.forward * (-18000);
        RectTransform markerTransform = southMarkrerTransform;
        Vector3 dirToTarget = worldPosition - Camera.main.transform.position;
        float angle = Vector2.SignedAngle(new Vector2(dirToTarget.x, dirToTarget.z), new Vector2(Camera.main.transform.transform.forward.x, Camera.main.transform.transform.forward.z));
        float compassPositionX = Mathf.Clamp(2 * angle / Camera.main.fieldOfView, -1, 1);
        markerTransform.anchoredPosition = new Vector2(compassBarTransform.rect.width / 2 * compassPositionX, 0);
        Vector2 v = Camera.main.ViewportToScreenPoint(new Vector2(0.5f, 0.5f));
        Debug.Log("markerTransform.anchoredPosition" + markerTransform.anchoredPosition);
    }

    void SetMarkerPosition(RectTransform markerTransform, Vector3 worldPosition){
        Vector3 dirToTarget = worldPosition - Camera.main.transform.position;
        float angle = Vector2.SignedAngle(new Vector2(dirToTarget.x, dirToTarget.z), new Vector2(Camera.main.transform.transform.forward.x, Camera.main.transform.transform.forward.z));
        float compassPositionX = Mathf.Clamp(2*angle/Camera.main.fieldOfView, -1, 1);
        markerTransform.anchoredPosition = new Vector2(compassBarTransform.rect.width/2*compassPositionX, 0);
        Vector2 v = Camera.main.ViewportToScreenPoint(new Vector2(0.5f, 0.5f));
        Debug.Log("markerTransform.anchoredPosition" + markerTransform.anchoredPosition);
    }

    void UpdateCompass()
    {
        SetNorth();
        SetSouth();
        
        /*
        SetMarkerPosition(northMarkrerTransform, Vector3.forward*1000);
        SetMarkerPosition(southMarkrerTransform, Vector3.back*1000);

        Debug.Log("north:" + Vector3.forward * 1000 +", south:"+ Vector3.back * 1000);
        /*
        //dirE.rectTransform.anchoredPosition = new Vector2(-40, 0);
        float centerOfSight = Camera.main.transform.localEulerAngles.z;
        float fieldOfView = Camera.main.fieldOfView;
        float leftCornerOfView = CalcLeftCorner(centerOfSight, fieldOfView);
        float rightCornerOfView = CalcRightCorner(centerOfSight, fieldOfView);

        //-- N = 0 --
        if (isSeen(0, leftCornerOfView, rightCornerOfView))
        {
            Debug.Log("North is seen. leftCornerOfView = "+ leftCornerOfView+ ", rightCornerOfView = "+ rightCornerOfView + ", Camera.main.transform.position = " + Camera.main.transform.position);
            dirN.rectTransform.anchoredPosition = new Vector2(0, 0);
        }
        else
        {
            Debug.Log("North is seen. leftCornerOfView = " + leftCornerOfView + ", rightCornerOfView = " + rightCornerOfView + ", Camera.main.transform.position = " + Camera.main.transform.position);
            dirN.rectTransform.anchoredPosition = new Vector2(100, 0);
        }
        //-- E = 90 --
        //-- S = 180 --
        //-- W = 270 --


        float pi = (float)Math.PI;
        if (angleRadians >= 0 && angleRadians < pi)
        {
            float position = 245 + (245 * angleRadians) / pi;
            compassArrow.rectTransform.anchoredPosition = new Vector2(position, 0);
        }
        else if(angleRadians >= pi && angleRadians < 2 * pi)
        {
            float position = 15 + (245 * (angleRadians - pi)) / pi;
            compassArrow.rectTransform.anchoredPosition = new Vector2(position, 0);
        }

        */
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
