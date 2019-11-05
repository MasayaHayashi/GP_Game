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


            if (layerName == "Item")
                colTrigger = characterClass.ItemLiftUp(other);
            else if (layerName == "Lane")
            {
                Collider[] collisions = Physics.OverlapBox(transform.position, GetComponent<BoxCollider>().size / 2.0f, transform.rotation);
                for(int i = 0; i < collisions.Length; i++)
                {
                    if (LayerMask.LayerToName(collisions[i].gameObject.layer) == "Item")
                        return;
                }
                colTrigger = characterClass.ItemPut(other);
            }
        }
    }

}
