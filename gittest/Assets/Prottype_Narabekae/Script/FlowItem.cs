using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowItem : MonoBehaviour
{
    LaneControl laneControlClass;
    eItemType itemType;
    Rigidbody selfRigidBody;
    Transform selfTrans;

    string laneName;
    bool changeVelocity;
    public Vector3 moveLaneVelocity;
    Lane laneClass;

    public static float LANE_SPEED = 1.0f;
    public static bool laneSpeedUpFlag = false;
    bool liftFlag;
    public bool goalFlag;

    public enum eItemType
    {
        pink,
        white,
        blue,
        red,
        green,
        black,

        disturb,

        MAX,
    }

    // Start is called before the first frame update
    void Start()
    {
        selfRigidBody = GetComponent<Rigidbody>();
        selfTrans = transform;
        laneName = "";
        changeVelocity = true;
        moveLaneVelocity = Vector3.zero;
        liftFlag = false;
        goalFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (goalFlag)
            return;

        //**debug
        if (DebugCanvas.debugCanvas)
        {
            LANE_SPEED = DebugCanvas.laneSpeedSlider.value;
            if (laneSpeedUpFlag)
                LANE_SPEED *= 5.0f;
        }

        LaneFlow();

        //下に落ちたら削除
        //if (selfTrans.position.y <= -3.0f)
        //    Destroy(gameObject);
    }

    void LaneFlow()
    {
        if (liftFlag)
            return;

        Vector3 work = Vector3.zero;

        if (LaneControl.BugFreezeFlag())
        {
            selfRigidBody.velocity = work;
            return;
        }

        work.y = selfRigidBody.velocity.y;

        work.x = moveLaneVelocity.x * LANE_SPEED;
        work.z = moveLaneVelocity.z * LANE_SPEED;

        selfRigidBody.velocity = work;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (liftFlag || goalFlag)   //持ち上げ中はレーン処理をしない
            return;

        string layerName = LayerMask.LayerToName(collision.gameObject.layer);
        if (layerName != "Lane")
            return;

        if (collision.gameObject.name != laneName) {
            laneName = collision.gameObject.name;
            laneClass = collision.gameObject.GetComponent<Lane>();
            if (changeVelocity)
            {
                moveLaneVelocity = laneClass.laneVelocity;
                changeVelocity = false;
                return;
            }
            changeVelocity = true;
        }

        if (!changeVelocity)
            return;

        Vector3 work = selfTrans.position;
        if ((moveLaneVelocity.x > 0.0f && laneClass.pos.x <= selfTrans.position.x) ||
            (moveLaneVelocity.x < 0.0f && laneClass.pos.x >= selfTrans.position.x))
        {
            work.x = laneClass.pos.x;
            moveLaneVelocity = laneClass.laneVelocity;
            changeVelocity = false;
            return;
        }
        if ((moveLaneVelocity.z > 0.0f && laneClass.pos.z <= selfTrans.position.z) ||
            (moveLaneVelocity.z < 0.0f && laneClass.pos.z >= selfTrans.position.z))
        {
            work.z = laneClass.pos.z;
            moveLaneVelocity = laneClass.laneVelocity;
            changeVelocity = false;
            return;
        }
    }

    public void Lift()
    {
        liftFlag = true;
    }

    public void Put(Vector3 pos)
    {
        liftFlag = false;
        changeVelocity = true;
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
