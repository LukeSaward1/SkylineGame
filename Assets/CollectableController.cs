using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectableController : MonoBehaviour
{
    [Header("Collectables Stats")]
    public int collectableCount;

    [Header("Collectables Stats")]
    public Image col1;
    public Image col2;
    public Image col3;
    public Image col4;
    public Image col5;
    public Image col6;
    public Image col7;
    public Image col8;
    public Image col9;
    public Image col10;
    public Image col11;
    public Image col12;
    public Image col13;
    public Image col14;
    public Image col15;
    public Image col16;
    public Image col17;
    public Image col18;
    public Image col19;
    public Image col20;
    public Image col21;
    public Image col22;
    public Image col23;
    public Image col24;
    public Image col25;
    public Image col26;
    public Image col27;
    public Image col28;
    public Image col29;
    public Image col30;

    void Update()
    {
        Image col1img = col1.GetComponent<Image>();
        Color c = col1img.color;

        if(collectableCount >= 1)
        {
            c.a = 0.7f;
            col1img.color = c;
        }
    }
}
