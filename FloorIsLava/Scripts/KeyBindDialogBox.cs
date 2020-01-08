using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class KeyBindDialogBox : MonoBehaviour
{
    InputManager inputManager;
    public GameObject keyItemPrefab;
    public GameObject keyList;
    string buttonToRebind = null;
    Dictionary<string, Text> buttonToLabel;
    // Start is called before the first frame update
    void Start()
    {
        inputManager = GameObject.FindObjectOfType<InputManager>();
        //Create one "Key List Item" per button in item manager
        string[] buttonNames = inputManager.GetButtonNames();
        buttonToLabel = new Dictionary<string, Text>();
        foreach (string bn in buttonNames)
        {
            GameObject go = (GameObject)Instantiate(keyItemPrefab);
            go.transform.SetParent(keyList.transform);
            go.transform.localScale = Vector3.one;
            Text buttonNameText = go.transform.Find("Button Name").GetComponent<Text>();
            buttonNameText.text = bn;
            Text keyNameText = go.transform.Find("Button/Key Name").GetComponent<Text>();
            keyNameText.text = inputManager.GetKeyNameForButton(bn);
            buttonToLabel[bn] = keyNameText;
            Button keyBindButton = go.transform.Find("Button").GetComponent<Button>();
            keyBindButton.onClick.AddListener(() => { StartRebindFor(bn); });

        }
    }
    public void Test(int i) { }
    // Update is called once per frame
    void Update()
    {
        if (buttonToRebind != null)
        {
            if (Input.anyKeyDown) // detects if any key pressed down this frame
            {
                // which key was pressed down
                // loop through all possible keys and see if it was pressed down
                //Array kcs = Enum.GetValues(typeof(KeyCode)); // another way of writing lines 49-51 is to write:
                // foreach (KeyCode kc in Enum.GetValues( typeof (KeyCode) ))
                foreach (KeyCode kc in Enum.GetValues(typeof(KeyCode)))
                {
                    // is this key down?
                    if (Input.GetKeyDown(kc))
                    {
                        // Yes!
                        inputManager.SetButtonForKey(buttonToRebind, kc);
                        buttonToLabel[buttonToRebind].text = kc.ToString();
                        buttonToRebind = null;
                        break;
                    }
                }
            }
        }
    }
    void StartRebindFor(string buttonName)
    {
        buttonToRebind = buttonName;
        Debug.Log("StartRebindFor: " + buttonName);
    }
}
