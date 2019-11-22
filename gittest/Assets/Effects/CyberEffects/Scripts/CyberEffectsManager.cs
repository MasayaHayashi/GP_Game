using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Author : MasayaHayashi
// 周りのサイバーエフェクト管理

public class CyberEffectsManager : MonoBehaviour
{
    [SerializeField, Header("エフェクト")]
    private List<GameObject> cyberEffects;

    private List<TrailRenderer> trails = new List<TrailRenderer>();
    private Color startColor;

    private Color myChangeColor;

    // Start is called before the first frame update
    void Start()
    {
        if(cyberEffects.Count <= 0)
        {
            Debug.LogError("cyberEffects Material is Not Set");
        }

        foreach(GameObject cyberEffect in cyberEffects)
        {
            trails.Add(cyberEffect.GetComponent<TrailRenderer>());
        }

        startColor = cyberEffects[0].GetComponent<TrailRenderer>().startColor;
    }

    // Update is called once per frame
    void Update()
    {
        changeColor(myChangeColor);

        if (Input.GetKey(KeyCode.Alpha5))
            myChangeColor.r -= 0.1f;

        if (Input.GetKey(KeyCode.Alpha6))
            myChangeColor.r += 0.1f;
    }

    public void changeColor(Color setColor)
    {
        foreach(TrailRenderer trailRenderer in trails)
        {
            trailRenderer.material.SetColor("_EmissionColor", setColor);
        }
    }

    public void ReturnToColor()
    {
        foreach (TrailRenderer trailRenderer in trails)
        {
            trailRenderer.material.SetColor("_EmissionColor", startColor);
        }
    }

   
}
