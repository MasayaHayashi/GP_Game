using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressPos : MonoBehaviour
{
    [SerializeField] PressMachine pressMachineClass;
    [SerializeField] RecipeControl itemUiClass;
    public int pressPosNumber;
    public static int setItemNum = 0;
    Transform selfTrans;

    // Start is called before the first frame update
    void Start()
    {
        setItemNum = 0;
        selfTrans = transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (pressPosNumber != setItemNum) 
            return;

        string layerName = LayerMask.LayerToName(other.gameObject.layer);
        if (layerName != "Item")
            return;

        FlowItem classItem = other.gameObject.GetComponent<FlowItem>();
        Vector3 vel = classItem.moveLaneVelocity;
        Vector3 posOther = other.transform.position;

        if ((vel.x > 0.0f && posOther.x >= selfTrans.position.x) ||
            (vel.x < 0.0f && posOther.x <= selfTrans.position.x) ||
            (vel.z > 0.0f && posOther.z >= selfTrans.position.z) ||
            (vel.z < 0.0f && posOther.z <= selfTrans.position.z))
        {
            posOther.x = selfTrans.position.x;
            posOther.z = selfTrans.position.z;
            other.transform.position = posOther;
            classItem.moveLaneVelocity = Vector3.zero;
            classItem.goalFlag = true;
            setItemNum++;
            itemUiClass.ItemGoal(classItem.GetItemType());

            if (setItemNum >= 4)
            {
                pressMachineClass.Press();
                setItemNum = 0;
            }
            return;
        }
    }
}
