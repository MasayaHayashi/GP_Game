using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    [SerializeField] Character characterClass;
    bool colTrigger;

    // Start is called before the first frame update
    void Start()
    {
        colTrigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        colTrigger = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (colTrigger)
                return;

            string layerName = LayerMask.LayerToName(other.gameObject.layer);
            Collider[] collisions = Physics.OverlapBox(transform.position, GetComponent<BoxCollider>().size / 2.0f, transform.rotation);

            //--- ゴールに入っちゃったアイテムにはもう手出しさせない ---
            for (int i = 0; i < collisions.Length; i++)
            {
                if (LayerMask.LayerToName(collisions[i].gameObject.layer) == "PressPos")
                    return;
            }

            //--- アイテムを持ち上げ ---
            if (layerName == "Item")
            {
                colTrigger = characterClass.ItemLiftUp(other);
            }
            //--- アイテムを置く ---
            else if (layerName == "Lane")
            {
                for (int i = 0; i < collisions.Length; i++)
                {
                    if (LayerMask.LayerToName(collisions[i].gameObject.layer) == "Item")
                        return;
                }
                colTrigger = characterClass.ItemPut(other);
            }
            //--- アイテムを捨てる ---
            else if(layerName == "DustBox")
            {
                colTrigger = characterClass.ItemDust();
            }
        }
    }

}
