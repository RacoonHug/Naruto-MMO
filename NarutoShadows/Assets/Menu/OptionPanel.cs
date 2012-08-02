using UnityEngine;
using System.Collections;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
	
public class OptionPanel : MonoBehaviour {
	
	private int _keyToChange;
    private int[] _keyIndex;
	private bool _isLisingForKey;
    private string _error;
	public Texture _panelBackground, _buttonBackground;
    public TextAsset _textAsset;
    private Rect[] _buttonRec;
	
	void Start () 
	{
        SetUpRect();
        _keyToChange = -1;
        _error = "";
        _keyIndex = new int[8];
		this.enabled = false;

        for (int i = 0; i < 8; i++)
        {
            if (!PlayerPrefs.HasKey("KeyInfo" + i))
            {
                PlayerPrefs.SetInt("KeyInfo" + i, 97 + i);
            }
            _keyIndex[i] = PlayerPrefs.GetInt("KeyInfo" + i);
        }
        if (!PlayerPrefs.HasKey("MusicVolume"))
        {
            PlayerPrefs.SetFloat("MusicVolume", 0.9f);
        }
        if (!PlayerPrefs.HasKey("VoiceVolume"))
        {
            PlayerPrefs.SetFloat("VoiceVolume", 0.8f);
        }
        if (!PlayerPrefs.HasKey("FXVolume"))
        {
            PlayerPrefs.SetFloat("FXVolume", 0.7f);
        }
	}

	void OnGUI() 
	{
		GUI.depth = 1;
		int widthPanel = (Screen.width/2) - 192;
		int heightPanel = (Screen.height/2) - 256;
		
		GUI.DrawTexture(new Rect(widthPanel, heightPanel, 384, 512), _panelBackground, ScaleMode.StretchToFill, true, 1.0F);

        PlayerPrefs.SetFloat("MusicVolume", GUI.HorizontalSlider(new Rect(widthPanel + 100, heightPanel + 105, 200, 20), PlayerPrefs.GetFloat("MusicVolume"), 0, 1));
        PlayerPrefs.SetFloat("VoiceVolume", GUI.HorizontalSlider(new Rect(widthPanel + 100, heightPanel + 135, 200, 20), PlayerPrefs.GetFloat("VoiceVolume"), 0, 1));
        PlayerPrefs.SetFloat("FXVolume", GUI.HorizontalSlider(new Rect(widthPanel + 100, heightPanel + 165, 200, 20), PlayerPrefs.GetFloat("FXVolume"), 0, 1));

        KeyButtonsControl();

        if (_keyToChange > -1)
        {
            foreach (KeyCode key in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(key))
                {
                    int keyNumber = (int)key;
                    bool pass = true;
                    foreach (int i in _keyIndex)
                    {
                        if (i.Equals(keyNumber))
                        {
                            _error = "Key already in use!";
                            pass = false;
                        }
                    }

                    if (pass)
                    {
                        _keyIndex[_keyToChange] = keyNumber;
                        PlayerPrefs.SetInt("KeyInfo" + _keyToChange, _keyIndex[_keyToChange]);
                        _keyToChange = -1;
                        _error = "";
                    }
                }
            }
        }

        if (!_error.Equals(""))
        {
            GUI.color = Color.red;
            GUI.Label(new Rect(widthPanel + 75, heightPanel + 450, 256, 48), _error);
            GUI.color = Color.white;
        }

		// save button.
        if (GUI.Button(new Rect(widthPanel + 270, heightPanel + 445, 128, 48), _buttonBackground, "label"))
            PlayerPrefs.Save();
	}

    private void KeyButtonsControl()
    {
        for (int i = 0; i < 8; i++ )
        {
            Color oldColor = GUI.backgroundColor;

            if (i.Equals(_keyToChange))
                GUI.backgroundColor = Color.blue;

            if (GUI.Button(_buttonRec[i], ((KeyCode)_keyIndex[i]).ToString()))
            {
                _keyToChange = i;
                _error = "Press a replacement key.";
            }

            GUI.backgroundColor = oldColor;
        }
    }

    private void SetUpRect()
    {
        int widthPanel = (Screen.width / 2) - 192;
        int heightPanel = (Screen.height / 2) - 256;

        _buttonRec = new Rect[8];

        _buttonRec[0] = new Rect(widthPanel + 100, heightPanel + 250, 30, 20);
        _buttonRec[1] = new Rect(widthPanel + 100, heightPanel + 290, 30, 20);
        _buttonRec[2] = new Rect(widthPanel + 100, heightPanel + 335, 70, 20);
        _buttonRec[3] = new Rect(widthPanel + 100, heightPanel + 390, 70, 20);
        _buttonRec[4] = new Rect(widthPanel + 205, heightPanel + 250, 30, 20);
        _buttonRec[5] = new Rect(widthPanel + 310, heightPanel + 250, 30, 20);
        _buttonRec[6] = new Rect(widthPanel + 260, heightPanel + 310, 30, 20);
        _buttonRec[7] = new Rect(widthPanel + 260, heightPanel + 200, 30, 20);
    }
}

#region Usefull
/*

[ExecuteInEditMode]
[Serializable]
public static class RyXmlTools
{
    public static XmlDocument loadXml(TextAsset xmlFile)
    {
        MemoryStream assetStream = new MemoryStream(xmlFile.bytes);
        XmlReader reader = XmlReader.Create(assetStream);
        XmlDocument xmlDoc = new XmlDocument();
        try
        {
            xmlDoc.Load(reader);
        }
        catch (Exception ex)
        {
            Debug.Log("Error loading " + xmlFile.name + ":\n" + ex);
        }
        finally
        {
            Debug.Log(xmlFile.name + " loaded");
        }

        return xmlDoc;
    }

    public static void WriteXml(TextAsset textAsset, XmlDocument doc)
    {
        MonoBehaviour.print(doc);
        MemoryStream assetStream = new MemoryStream(textAsset.bytes);
        XmlWriter write = XmlWriter.Create(assetStream);
            
        try
        {
            doc.Save(write);
        }
        catch (Exception ex)
        {
            Debug.Log("Error saving " + ex);
        }
        finally
        {
            Debug.Log(textAsset.name + "Saved");
        }
    }
}
*/
#endregion