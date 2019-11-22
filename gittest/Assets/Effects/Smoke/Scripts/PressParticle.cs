using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Author :MasayaHayashi   
// プレス演出

public class PressParticle : MonoBehaviour
{
    private Hashtable hash = new Hashtable();

    private const float StopTime = 2.0f;
    private List<GameObject>     childGameObjects   = new List<GameObject>();
    private List<ParticleSystem> childParticles = new List<ParticleSystem>();


    // Start is called before the first frame update
    void Start()
    {
        for(int childIndex = 0; childIndex < transform.childCount;childIndex++)
        {
            childGameObjects.Add(transform.GetChild(childIndex).gameObject);
        }

        foreach(GameObject gameObject in childGameObjects)
        {
            childParticles.Add(gameObject.GetComponent<ParticleSystem>());
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Awake()
    {
    }

    IEnumerator stop()
    {
        yield return new WaitForSeconds(StopTime);

        foreach (ParticleSystem particle in childParticles)
        {
            particle.Stop();
            
        }
        
       // GetComponentInChildren<FogParticle>()
       //
    }


    void CompleteHandler()
    {

    }

    public void start()
    {
        foreach(GameObject particleObject in childGameObjects)
        {
            particleObject.SetActive(true);
        }

    }
}
