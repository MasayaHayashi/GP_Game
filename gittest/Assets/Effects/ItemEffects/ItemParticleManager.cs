using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemParticleManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> particles;

    public Dictionary<string,int> names;

    // Start is called before the first frame update
    void Start()
    {
        int nameIndex = 0;
        foreach(GameObject particle in particles)
        {
            names.Add(particle.name,nameIndex);
            nameIndex ++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject find(string name)
    {
        return particles[names[name]];
    }

    public GameObject find(int index)
    {
        return particles[index];
    }
    
}
