using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class StageDatWrite : MonoBehaviour
{
    [SerializeField] string fileName;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            WriteData();
        }
    }

    void WriteData()
    {
        Transform[] gos = gameObject.GetComponentsInChildren<Transform>();
        List<Transform> lanes = new List<Transform>();
        List<Transform> pressPos = new List<Transform>();
        Transform spawnPos = null;
        Transform goalPos = null;
        Transform wall = null;
        Transform pressMachine = null;

        for (int i = 0; i < gos.Length; i++)
        {
            string layerName = LayerMask.LayerToName(gos[i].gameObject.layer);
            switch (layerName)
            {
                case "Lane":
                    lanes.Add(gos[i]);
                    break;
                case "PressPos":
                    pressPos.Add(gos[i]);
                    break;
                case "SpawnPos":
                    spawnPos = gos[i];
                    break;
                case "GoalPoint":
                    goalPos = gos[i];
                    break;
                case "Wall":
                    wall = gos[i];
                    break;
                case "PressMachine":
                    pressMachine = gos[i];
                    break;
            }
        }

        StreamWriter sw = new StreamWriter("Assets/Prottype_Narabekae/TextAssets/" + fileName + ".txt", false); //true=追記 false=上書き
        sw.WriteLine("<LANE>");
        for(int i = 0; i < lanes.Count; i++)
        {
            sw.WriteLine("<pos>");
            sw.WriteLine(lanes[i].position);
            sw.WriteLine("<rot>");
            sw.WriteLine(lanes[i].rotation);
            sw.WriteLine("<scale>");
            sw.WriteLine(lanes[i].localScale);
            sw.WriteLine("<velocity>");
            sw.WriteLine(lanes[i].GetComponent<Lane>().laneVelocity);
        }
        sw.WriteLine("<PRESSPOS>");
        for (int i = 0; i < pressPos.Count; i++)
        {
            sw.WriteLine("<pos>");
            sw.WriteLine(pressPos[i].position);
            sw.WriteLine("<rot>");
            sw.WriteLine(pressPos[i].rotation);
            sw.WriteLine("<scale>");
            sw.WriteLine(pressPos[i].localScale);
        }
        sw.WriteLine("<SPAWNPOS>");
        sw.WriteLine("<pos>");
        sw.WriteLine(spawnPos.position);

        sw.WriteLine("<GOALPOINT>");
        sw.WriteLine("<pos>");
        sw.WriteLine(goalPos.position);
        sw.WriteLine("<rot>");
        sw.WriteLine(goalPos.rotation);
        sw.WriteLine("<scale>");
        sw.WriteLine(goalPos.localScale);

        sw.WriteLine("<WALL>");
        sw.WriteLine("<pos>");
        sw.WriteLine(wall.position);
        sw.WriteLine("<rot>");
        sw.WriteLine(wall.rotation);
        sw.WriteLine("<scale>");
        sw.WriteLine(wall.localScale);

        sw.WriteLine("<PRESSMACHINE>");
        sw.WriteLine("<pos>");
        sw.WriteLine(pressMachine.position);
        sw.WriteLine("<rot>");
        sw.WriteLine(pressMachine.rotation);
        sw.WriteLine("<scale>");
        sw.WriteLine(pressMachine.localScale);

        Debug.Log("ステージデータを書き込みました");

        sw.Flush();
        sw.Close();
    }


}
