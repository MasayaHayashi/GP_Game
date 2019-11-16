using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] GameObject handObj;
    [SerializeField] LaneControl laneControlClass;
    Rigidbody selfRigidBody;

    FlowItem liftItem = null;

    float moveSpeed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        selfRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //**debug
        if (DebugCanvas.debugCanvas)
            moveSpeed = DebugCanvas.characterSpeedSlider.value;

        PlayerMove();

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
        if (Input.GetKey(KeyCode.UpArrow))
        {
            work.z = moveSpeed;
            rot = Quaternion.Euler(0.0f, 180.0f, 0.0f);

        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            work.z = -moveSpeed;
            rot = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            work.x = moveSpeed;
            rot = Quaternion.Euler(0.0f, -90.0f, 0.0f);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            work.x = -moveSpeed;
            rot = Quaternion.Euler(0.0f, 90.0f, 0.0f);
        }

        transform.rotation = rot;
        selfRigidBody.velocity = work;
    }

    public bool ItemLiftUp(Collider item)
    {
        if (liftItem)
            return false;

        liftItem = item.gameObject.GetComponent<FlowItem>();
        return true;
    }

    public bool ItemPut(Collider table)
    {
        if (!liftItem)
            return false;

        //どのテーブルに置くのか
        Vector3 workPos = handObj.transform.position;
        Lane lanec = table.gameObject.GetComponent<Lane>();

        if (lanec.laneVelocity.x != 0.0f)
            workPos.z = table.gameObject.transform.position.z;
        else if (lanec.laneVelocity.z != 0.0f)
            workPos.x = table.gameObject.transform.position.x;
        else
            Debug.Log("Error");

        liftItem.Put(workPos);
        liftItem = null;
        return true;
    }

}
