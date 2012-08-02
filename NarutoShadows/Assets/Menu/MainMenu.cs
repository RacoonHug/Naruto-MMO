using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	
	public Texture _menuBackground;
    public AudioSource _backgroundMusic;
	private LoginPanel _loginPanel;
	private OptionPanel _optionPanel;
	private MenuButton[] _menuItems = new MenuButton[]
	{ 
		new MenuButton(0, "Login", 100),
		new MenuButton(1, "Options", 160),
		new MenuButton(2, "Exit", 220),
	};

	void Start () 
	{
		_loginPanel = GetComponent<LoginPanel>();
		_optionPanel = GetComponent<OptionPanel>();
        _backgroundMusic.Play();

        audio.Play();
	}
	
	void OnGUI() 
	{
        _backgroundMusic.volume = PlayerPrefs.GetFloat("MusicVolume");
		GUI.depth = 2;
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), _menuBackground, ScaleMode.StretchToFill, true, 1.0F);
		
		foreach(MenuButton item in _menuItems)
		{
			if(GUI.Button(new Rect(20, item.Top, 200, 50), item.Name)) 
			{
				SelectMenuItem(item.ID);
			}
		}
	}
	
	private void SelectMenuItem(int index)
	{
		switch(index)
		{
			case 0:
				_optionPanel.enabled = false;
				_loginPanel.enabled = !_loginPanel.enabled;
			break;
			case 1:
				_loginPanel.enabled = false;
				_optionPanel.enabled = !_optionPanel.enabled;
			break;
			case 2:
				Application.Quit();
			break;
		}
	}
}

public class MenuButton
{
	private bool _selected;
	private readonly string _name;
	private int _top, _id;
	
	public MenuButton(int id, string name, int top)
	{
		_id = id;
		_selected = false;
		_name = name;
		_top = top;
	}
	
	public int ID
	{
		get{return _id;}
	}
	public string Name
	{
		get{return _name;}
	}
	public int Top
	{
		get{return _top;}
		set{_top = value;}
	}
	public bool Selected
	{
		get{return _selected;}
		set{ _selected = value; }
	}
}
