using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalPoint : MonoBehaviour
{
    [SerializeField] ItemUI itemUiClass;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
       string layerName = LayerMask.LayerToName(other.gameObject.layer);
       if (layerName != "Item")
            return;

        itemUiClass.ItemGoal(other.GetComponent<FlowItem>().GetItemType());
    }
}
