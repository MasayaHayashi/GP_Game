using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailSphereMover : Mover
{
    private void Awake()
    {
        // 各種時間設定
        delayTime = Random.Range(MinDelayTime, MaxDerayTime);
        moveTime = Random.Range(MinMoveTime, MaxMoveTime);

        hash.Clear();

        hash.Add("x", 5.0f);
        hash.Add("time", moveTime);
        hash.Add("easetype", iTween.EaseType.easeInOutBack);
        hash.Add("loopType", iTween.LoopType.pingPong);

        iTween.MoveAdd(gameObject, hash);
    }
}
