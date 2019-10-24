using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugCanvas : MonoBehaviour
{
    public static bool debugCanvas = false;
    public static Slider characterSpeedSlider;
    public static Slider laneSpeedSlider;
    public static Slider createIntervalSlider;

    [SerializeField] Slider _characterSpeed;
    [SerializeField] Slider _laneSpeed;
    [SerializeField] Slider _createInterval;

    bool on;

    // Start is called before the first frame update
    void Awake()
    {
        on = true;
        debugCanvas = true;
        characterSpeedSlider = _characterSpeed;
        laneSpeedSlider = _laneSpeed;
        createIntervalSlider = _createInterval;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            on ^= true;
            characterSpeedSlider.gameObject.SetActive(on);
            laneSpeedSlider.gameObject.SetActive(on);
            createIntervalSlider.gameObject.SetActive(on);
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            Debug.Log("CharacterSpeed : " + characterSpeedSlider.value + "\n" +
                "LaneSpeed : " + laneSpeedSlider.value + "\n" +
                "CreateInterval : " + createIntervalSlider.value);
        }
    }
}
