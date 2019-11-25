using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowItem : MonoBehaviour
{
    public Dissolver dissolvClass;
    LaneControl laneControlClass;
    eItemType itemType;
    Rigidbody selfRigidBody;
    Transform selfTrans;

    string laneName;
    bool changeVelocity;
    public Vector3 moveLaneVelocity;
    float lanePutPosY;
    Lane laneClass;

    public static float LANE_SPEED = 1.0f;
    public static bool laneSpeedUpFlag = false;

    bool liftFlag;
    public bool goalFlag;
    public bool finStartEffect;

    public enum eItemType
    {
        blue,
        green,
        orrange,
        red,
        white,
        yellow,
        black,
        //perple,

        disturb,    //perple

        MAX,
    }

    // Start is called before the first frame update
    void Start()
    {
        //dissolvClass.initilizeAttach();
        //dissolvClass.begin();
        finStartEffect = false;
        selfRigidBody = GetComponent<Rigidbody>();
        selfTrans = transform;
        laneName = "";
        changeVelocity = true;
        moveLaneVelocity = Vector3.zero;
        liftFlag = false;
        goalFlag = false;
        lanePutPosY = 0.0f;
        laneControlClass.PlaySe("itemSpawn");
    }

    // Update is called once per frame
    void Update()
    {
        if (goalFlag || !laneControlClass.GetLaneActive())
            return;

        //==== スタート演出 ====
        if (!finStartEffect)
        {
            if (dissolvClass.isComplite)
                finStartEffect = true;
            return;
        }

        //**debug
        if (DebugCanvas.debugCanvas)
        {
            LANE_SPEED = DebugCanvas.laneSpeedSlider.value;
            if (laneSpeedUpFlag)
                LANE_SPEED *= 5.0f;
        }

        //--- レーンとの当たり判定を調べる ---
        CheckLane();

        //--- レーンにそって流れる ---
        LaneFlow();

        //下に落ちたら削除
        //if (selfTrans.position.y <= -3.0f)
        //    Destroy(gameObject);
    }

    void LaneFlow()
    {
        if (liftFlag)       //持たれているとき
            return;

        Vector3 work = Vector3.zero;
        Vector3 pos = selfTrans.position;

        if (LaneControl.BugFreezeFlag())        //レーンフリーズ中は止まる
        {
            //selfRigidBody.velocity = work;
            return;
        }

        //work.y = selfRigidBody.velocity.y;      //重力はそのまま利用

        //固定値より上にいるなら重力を追加
        if(lanePutPosY < selfTrans.position.y)
        {
            work.y = -2.0f;
        }

        //ナナメ移動ができないように
        if (moveLaneVelocity.x != 0.0f)
        {
            work.x = moveLaneVelocity.x * LANE_SPEED * Time.deltaTime;
        }
        else if (moveLaneVelocity.z != 0.0f)
        {
            work.z = moveLaneVelocity.z * LANE_SPEED * Time.deltaTime;
        }

        //selfRigidBody.velocity = work;
        selfTrans.position += work;

        //固定値よりY下回ったら固定値に戻す
        if(selfTrans.position.y <= lanePutPosY){
            work = selfTrans.position;
            work.y = lanePutPosY;
            selfTrans.position = work;
        }
    }

    void CheckLane()
    {
        if (liftFlag || goalFlag)   //持ち上げ中はレーン処理をしない
            return;

        RaycastHit hit;
        float distance = 5.0f;      //レイを飛ばす距離
        
        //デバッグ表示
        Debug.DrawRay(selfTrans.position, new Vector3(0.0f, -1.0f, 0.0f) * distance, Color.red);

        //下にレイを飛ばしてレーンの有無を確認
        if(Physics.Raycast(selfTrans.position, new Vector3(0.0f, -1.0f, 0.0f),
            out hit, distance, LayerMask.GetMask(new string[] { "Lane" })))
        {
            LaneCollide(hit.collider.gameObject);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (liftFlag || goalFlag)   //持ち上げ中はレーン処理をしない
            return;

        string layerName = LayerMask.LayerToName(collision.gameObject.layer);
        if (layerName != "Lane")
            return;

        LaneCollide(collision.gameObject);
    }

    void LaneCollide(GameObject obj)
    {
        if (obj.name != laneName)
        {
            laneName = obj.gameObject.name;
            laneClass = obj.gameObject.GetComponent<Lane>();
            if (changeVelocity)
            {
                moveLaneVelocity = laneClass.laneVelocity;
                lanePutPosY = laneClass.itemPosY;
                changeVelocity = false;
                Vector3 work2 = selfTrans.position;
                if (moveLaneVelocity.x != 0.0f)
                    work2.z = laneClass.transform.position.z;
                else if (moveLaneVelocity.z != 0.0f)
                    work2.x = laneClass.transform.position.x;
                selfTrans.position = work2;
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

    public void Put(Vector3 pos, Vector3 vel)
    {
        moveLaneVelocity = vel;
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

    // ==== プレスされた ====
    public void Press()
    {
        if(goalFlag)
            Destroy(gameObject);
    }

    //==== ゴミ箱に入れられた =====
    public bool InDustBox()
    {
        Destroy(gameObject);
        return true;
    }
}
