using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 移動用スクリプト
// Author : MasayaHayashi

public class Mover : MonoBehaviour
{
    private bool isMoviong = false;
    private Vector3 moveDirection;

    private Hashtable hash = new Hashtable();

    // Start is called before the first frame update
    void Start()
    {
        initilizeHash();
        move();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void initilizeHash()
    {
        hash.Add("x", 10f);
        hash.Add("loopType", "pingPong");
        hash.Add("pingPong", iTween.EaseType.punch);
        hash.Add("oncomplete", "OncompleteHandler");
        hash.Add("oncompletetarget", gameObject);
    }

    void OnCompleteHandler()
    {
        isMoviong = false;
    }

    private void move()
    {
        iTween.MoveAdd(gameObject, hash);
        hash.Clear();
    }

}
