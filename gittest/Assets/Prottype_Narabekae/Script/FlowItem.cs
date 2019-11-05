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
    Vector3 moveLaneVelocity;
    Lane laneClass;

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
        laneName = "";
        changeVelocity = true;
        moveLaneVelocity = Vector3.zero;
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

        work.x = moveLaneVelocity.x * LANE_SPEED;
        work.z = moveLaneVelocity.z * LANE_SPEED;

        selfRigidBody.velocity = work;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (liftFlag)   //持ち上げ中はレーン処理をしない
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
