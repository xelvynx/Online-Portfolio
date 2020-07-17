using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    public Dictionary<string, KeyCode> buttonKeys;
    public static InputManager im;
    // Start is called before the first frame update
    void Start()
    {
        im = this;
        buttonKeys = new Dictionary<string, KeyCode>();
        buttonKeys.Add("Slot1", KeyCode.Alpha1);
        buttonKeys.Add("Slot2", KeyCode.Alpha2);
        buttonKeys.Add("Slot3", KeyCode.Alpha3);
        buttonKeys.Add("Slot4", KeyCode.Alpha4);
        buttonKeys.Add("Slot5", KeyCode.Alpha5);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public bool GetButtonDown(string buttonName)
    {
        if (buttonKeys.ContainsKey(buttonName) == false)
        {
            Debug.LogError("InputManager:GetButtonDown -- No Button Used");
            return false;
        }
        return Input.GetKeyDown(buttonKeys[buttonName]);
    }
}
