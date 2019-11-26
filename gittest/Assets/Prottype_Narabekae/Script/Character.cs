using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] GameObject handObj;
    [SerializeField] LaneControl laneControlClass;
    [SerializeField] SoundManager soundClass;
    Rigidbody selfRigidBody;
    Transform selfTrans;

    FlowItem liftItem = null;
    Lane selectLane = null;

    float moveSpeed = 5.0f;

    bool playerInputActiveFlag = true;
    public void SetInputActive(bool flag) { playerInputActiveFlag = flag; }

    // Start is called before the first frame update
    void Start()
    {
        selfRigidBody = GetComponent<Rigidbody>();
        selfTrans = transform;
    }

    // Update is called once per frame
    void Update()
    {
        //**debug
        if (DebugCanvas.debugCanvas)
            moveSpeed = DebugCanvas.characterSpeedSlider.value;

        if(playerInputActiveFlag)
            PlayerMove();

        //レーンとの当たり判定
        CheckLaneCollid();

        //持ち上げ中
        if (liftItem)
        {
            //ポジションの更新
            liftItem.LiftPosUpdate(transform.position);
        }
    }

    void PlayerMove()
    {
        Vector3 work = Vector3.zero;
        work.y = selfRigidBody.velocity.y;
        Quaternion rot = transform.rotation;
        if (Input.GetAxis("Vertical") >= 0.2f)
        {
            work.z = moveSpeed;
            rot = Quaternion.Euler(0.0f, 180.0f, 0.0f);

        }
        else if (Input.GetAxis("Vertical") <= -0.2f)
        {
            work.z = -moveSpeed;
            rot = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        }
        if (/*Input.GetKey(KeyCode.RightArrow)*/ Input.GetAxis("Horizontal") >= 0.2f)
        {
            work.x = moveSpeed;
            rot = Quaternion.Euler(0.0f, -90.0f, 0.0f);
        }
        else if (Input.GetAxis("Horizontal") <= -0.2f)
        {
            work.x = -moveSpeed;
            rot = Quaternion.Euler(0.0f, 90.0f, 0.0f);
        }

        transform.rotation = rot;
        selfRigidBody.velocity = work;
    }

    void CheckLaneCollid()
    {
        RaycastHit hit;
        float distance = 1.0f;      //レイを飛ばす距離

        //デバッグ表示
        Debug.DrawRay(selfTrans.position, -selfTrans.forward * distance, Color.blue);

        //前にレイを飛ばしてレーンの有無を確認
        if (Physics.Raycast(selfTrans.position, -selfTrans.forward,
            out hit, distance, LayerMask.GetMask(new string[] { "Lane" })))
        {
            selectLane = hit.collider.gameObject.GetComponent<Lane>();
            selectLane.UpperHighlight();
        }
        else
        {
            if(selectLane != null)
                selectLane.UpperClear();
            selectLane = null;
        }
    }

    public bool ItemLiftUp(Collider item)
    {
        if (!playerInputActiveFlag || !item.GetComponent<FlowItem>().finStartEffect)
            return false;

        if (!liftItem)
        {
            liftItem = item.gameObject.GetComponent<FlowItem>();
            liftItem.Lift();
        }
        else
        {
            //--- 元々持ってたアイテムを置く ---
            liftItem.Put(item.transform.position, item.gameObject.GetComponent<FlowItem>().moveLaneVelocity);

            //--- 持ち替えでアイテムを持つ ---
            liftItem = item.gameObject.GetComponent<FlowItem>();
            liftItem.Lift();
        }
        soundClass.PlayOneShot((int)SoundManager.eGameSE.takeItem);     //SEの再生
        return true;
    }

    public bool ItemPut(Collider table)
    {
        if (!liftItem || !playerInputActiveFlag)
            return false;

        //--- レイとレーンが接触しているなら、レーン中央にアイテムを置く ---
        CheckLaneCollid();
        if(selectLane != null)
        {
            liftItem.Put(selectLane.gameObject.transform.position, selectLane.laneVelocity);
            liftItem = null;
            soundClass.PlayOneShot((int)SoundManager.eGameSE.putItem);     //SEの再生
            return true;
        }

        //どのテーブルに置くのか
        Vector3 workPos = handObj.transform.position;
        workPos.y = liftItem.transform.position.y;
        Lane lanec = table.gameObject.GetComponent<Lane>();

        if (lanec.laneVelocity.x != 0.0f)
            workPos.z = table.gameObject.transform.position.z;
        else if (lanec.laneVelocity.z != 0.0f)
            workPos.x = table.gameObject.transform.position.x;
        else
            Debug.Log("Error");

        liftItem.Put(workPos, lanec.laneVelocity);
        liftItem = null;
        soundClass.PlayOneShot((int)SoundManager.eGameSE.putItem);     //SEの再生
        return true;
    }

    public bool ItemDust()
    {
        if (!liftItem || !playerInputActiveFlag)
            return false;

        soundClass.PlayOneShot((int)SoundManager.eGameSE.inDust);       //SEの再生
        liftItem.InDustBox() ;
        liftItem = null;
        return true;
    }

}
