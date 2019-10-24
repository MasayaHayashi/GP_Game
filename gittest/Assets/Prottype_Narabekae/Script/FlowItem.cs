using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowItem : MonoBehaviour
{
    LaneControl laneControlClass;
    eItemType itemType;
    Rigidbody selfRigidBody;
    Transform selfTrans;
    int laneNumber;

    public static float LANE_SPEED = 1.0f;
    bool liftFlag;

    public enum eItemType
    {
        pink,
        white,
        blue,
        red,
        green,
        black,

        MAX,
    }

    // Start is called before the first frame update
    void Start()
    {
        selfRigidBody = GetComponent<Rigidbody>();
        selfTrans = transform;
        laneNumber = 0;
        liftFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        //**debug
        if (DebugCanvas.debugCanvas)
            LANE_SPEED = DebugCanvas.laneSpeedSlider.value;

        LaneFlow();

        //下に落ちたら削除
        if (selfTrans.position.y <= -3.0f)
            Destroy(gameObject);
    }

    void LaneFlow()
    {
        if (liftFlag)
            return;

        Vector3 work = Vector3.zero;
        work.y = selfRigidBody.velocity.y;
        switch (laneNumber)
        {
            case 0:
                if(laneControlClass.Lanes[1].transform.position.x >= transform.position.x)
                {
                    laneNumber++;
                    break;
                }
                work.x = -LANE_SPEED;
                break;
            case 1:
                if (laneControlClass.Lanes[2].transform.position.z <= transform.position.z)
                {
                    laneNumber++;
                    break;
                }
                work.z = LANE_SPEED;
                break;
            case 2:
                work.x = LANE_SPEED;
                break;
        }

        selfRigidBody.velocity = work;
    }

    public void Lift()
    {
        liftFlag = true;
    }

    public void Put(Vector3 pos, int lane)
    {
        liftFlag = false;
        laneNumber = lane;
        transform.position = pos;
    }

    public void LiftPosUpdate(Vector3 pos)
    {
        pos.y += 2.0f;
        transform.position = pos;
    }

    public void FirstSet(LaneControl lane, eItemType type, Material mat)
    {
        laneControlClass = lane;
        itemType = type;
        GetComponent<MeshRenderer>().material = mat;
    }

    public eItemType GetItemType() { return itemType; }
}
