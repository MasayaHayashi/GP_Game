using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activeParticle : MonoBehaviour
{
    [SerializeField, Header("生成までの時間")]
    private float spawnTime;

    private List<GameObject> childs             = new List<GameObject>();

    private void Awake()
    {
        StartCoroutine("active");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator active()
    {

        for (int childIndex = 0; childIndex < transform.childCount; childIndex++)
        {
            childs.Add(transform.GetChild(childIndex).gameObject);
        }

        yield return new WaitForSeconds(spawnTime);

        foreach(GameObject child in childs)
        {
            child.SetActive(true);
        }

    }
}
