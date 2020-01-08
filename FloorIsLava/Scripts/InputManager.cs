using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class InputManager : MonoBehaviour
{
    
    public static InputManager im;
    public Dictionary<string, KeyCode> buttonKeys;
    public Dictionary<string, KeyCode> buttonKeys2;

    private void OnEnable()
    {
        AddKeys();
        if (im == null)
        {
            im = this;
        }
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
    public bool GetButtonUp(string buttonName)
    {
        if (buttonKeys.ContainsKey(buttonName) == false)
        {
            Debug.LogError("InputManager:GetButtonDown -- No Button Used");
            return false;
        }
        return Input.GetKeyUp(buttonKeys[buttonName]);
    }
    public bool GetButton(string buttonName)
    {
        if (buttonKeys.ContainsKey(buttonName) == false)
        {
            Debug.LogError("InputManager:GetButtonDown -- No Button Used");
            return false;
        }
        return Input.GetKeyDown(buttonKeys[buttonName]);
    }
    void AddKeys()
    {
        //Consider reading these from a user preferences file
        buttonKeys = new Dictionary<string, KeyCode>();

        buttonKeys["Up"] = KeyCode.W;
        buttonKeys["Down"] = KeyCode.S;
        buttonKeys["Left"] = KeyCode.A;
        buttonKeys["Right"] = KeyCode.D;

        buttonKeys["Ability1"] = KeyCode.Alpha1;
        buttonKeys["Ability2"] = KeyCode.Alpha2;
        buttonKeys["Ability3"] = KeyCode.Alpha3;
        buttonKeys["Ability4"] = KeyCode.Alpha4;

        buttonKeys2 = new Dictionary<string, KeyCode>();

        buttonKeys2["Up"] = KeyCode.I;
        buttonKeys2["Down"] = KeyCode.K;
        buttonKeys2["Left"] = KeyCode.J;
        buttonKeys2["Right"] = KeyCode.L;
        
        buttonKeys2["Ability1"] = KeyCode.U;
        buttonKeys2["Ability2"] = KeyCode.O;
        buttonKeys2["Ability3"] = KeyCode.P;
        buttonKeys2["Ability4"] = KeyCode.LeftCurlyBracket;

  

        //SetButtonForKey("Right", KeyCode.D);
        //SetButtonForKey("Forward", KeyCode.W);
        //SetButtonForKey("Back", KeyCode.S);



    }

    public string[] GetButtonNames()
    {
        return buttonKeys.Keys.ToArray();
    }
    public string GetKeyNameForButton(string buttonName)
    {
        if (buttonKeys.ContainsKey(buttonName) == false)
        {
            Debug.LogError("InputManager:GetButtonDown -- No Button Used");
            return "N/A";
        }
        return buttonKeys[buttonName].ToString();
    }

    public void SetButtonForKey(string buttonName, KeyCode keyCode)
    {
        buttonKeys[buttonName] = keyCode;
    }
    public KeyCode GetP1KeyCode(string buttonName)
    {
        if (buttonKeys.ContainsKey(buttonName) == false)
        {
            Debug.LogError("InputManager:GetP1KeyCode -- None");
            return KeyCode.None;
        }
        return buttonKeys[buttonName];
    }
    public KeyCode GetP2KeyCode(string buttonName) 
    {
        if (buttonKeys2.ContainsKey(buttonName) == false)
        {
            Debug.LogError("InputManager:GetP2KeyCode -- None");
            return KeyCode.None;
        }
        return buttonKeys2[buttonName];
    }
}
