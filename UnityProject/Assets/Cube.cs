using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine.UI;
using UnityEngine;

#if UNITY_IOS || UNITY_TVOS
public class NativeAPI {
    [DllImport("__Internal")]
    public static extern void sendMessageToMobileApp(string message);
}
#endif

public class Cube : MonoBehaviour
{
    public Text text;    
    void appendToText(string line) { text.text += line + "\n"; }

    void Update()
    {
        transform.Rotate(0, Time.deltaTime*10, 0);
		
		if (Application.platform == RuntimePlatform.Android)
            if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
    }

    string lastStringColor = "";
    void ChangeColor(string newColor)
    {
        appendToText( "Changing Color to " + newColor );

        lastStringColor = newColor;
    
        if (newColor == "red") GetComponent<Renderer>().material.color = Color.red;
        else if (newColor == "blue") GetComponent<Renderer>().material.color = Color.blue;
        else if (newColor == "yellow") GetComponent<Renderer>().material.color = Color.yellow;
        else GetComponent<Renderer>().material.color = Color.black;
    }


    public void ButtonPressed()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
        using (AndroidJavaClass jc = new AndroidJavaClass("com.azesmwayreactnativeunity.ReactNativeUnityViewManager"))
        {
            jc.CallStatic("sendMessageToMobileApp", lastStringColor);
        }
        }
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
    #if UNITY_IOS && !UNITY_EDITOR
        NativeAPI.sendMessageToMobileApp(lastStringColor);
    #endif
        }
    }

    void OnGUI()
    {
        GUIStyle style = new GUIStyle("button");
        style.fontSize = 45;
        if (GUI.Button(new Rect(10, 10, 200, 100), "Red", style)) ChangeColor("red");
        if (GUI.Button(new Rect(10, 110, 200, 100), "Blue", style)) ChangeColor("blue");
        if (GUI.Button(new Rect(10, 300, 600, 100), "Show Main With Color", style)) ButtonPressed();

        if (GUI.Button(new Rect(10, 400, 400, 100), "Unload", style)) Application.Unload();
        // if (GUI.Button(new Rect(440, 400, 400, 100), "Send Msg To App", style)) ButtonPressed();
    }
}

