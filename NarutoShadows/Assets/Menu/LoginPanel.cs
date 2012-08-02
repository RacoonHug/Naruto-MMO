using UnityEngine;
using MySql.Data;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;

public class LoginPanel : MonoBehaviour 
{
	private bool _isLogedIn;
	public Texture _panelBackground, _buttonBackground;
	private string _userName, _password, _error, _conectionInfo;
	private Shinobi _player = null;
	private MySql.Data.MySqlClient.MySqlConnection _connection = null;
	private MySqlCommand _cmd = null;
	private MySqlDataReader _rdr = null;
	
	void Start()
	{
		_isLogedIn = false;
		_userName = "";
		_password = "";
		_error = "";
		this.enabled = false;
		_conectionInfo = @"server=instance22562.db.xeround.com;uid=Shinobi;password=naruto;database=ns";
	}
	
	void OnGUI() 
	{
		if(_isLogedIn)
		{
			GUI.Label(new Rect(500, 300 , 500, 300), "Nickname: " + _player.NickName
				+ "\n Age: " + _player.AGE + "\n Xp: " + _player.XP + "\n Cash: " + _player.Cash);
		}
		else
		{
			GUI.depth = 0;
			if (!_panelBackground || !_buttonBackground) {
	            Debug.LogError("Please assign a texture on the inspector");
	            return;
	        }
	
			int widthPanel = (Screen.width/2) - 192;
			int heightPanel = (Screen.height/2) - 64;
			
			// Make background box
			GUI.DrawTexture(new Rect(widthPanel, heightPanel, 384, 128), _panelBackground, ScaleMode.StretchToFill, true, 1.0F);
			
			//labels
			GUI.color = Color.black;
			GUI.Label(new Rect(widthPanel + 60, heightPanel + 25 , 100, 25), "Username");
			GUI.Label(new Rect(widthPanel + 60, heightPanel + 55 , 100, 25), "Password");
			GUI.color = Color.white;
			
			//texbox
			_userName = GUI.TextField(new Rect(widthPanel + 140, heightPanel + 25 , 210, 25), _userName);
            _password = GUI.PasswordField(new Rect(widthPanel + 140, heightPanel + 55, 210, 25), _password, '*');
			
			// login button.
			if(GUI.Button(new Rect(widthPanel + 270, heightPanel + 75, 128, 48),_buttonBackground, "label")) {
				Login(_userName, _password);
			
			}
			if(!_error.Equals(""))
			{
				GUI.color = Color.red;
				GUI.Label(new Rect(widthPanel + 60, heightPanel + 85, 200, 25), _error);
				GUI.color = Color.white;
			}
		}
	}
	
	private void Login(string username, string password)
	{
		string command = "SELECT * FROM shinobi WHERE shinobi_name=" + username + " AND shinobi_password=" + password;
		if(username.Equals(""))
		{
			_error = "Fill in your username";
		}
		else if (password.Equals(""))
		{
			_error = "Fill in your password";
		}
		else
		{
            Application.LoadLevel(1);
            //uncomment when we have a server !!!!!
            //try
            //{
            //    _connection = new MySqlConnection(_conectionInfo);
            //    _connection.Open();

            //    _cmd = new MySqlCommand(command, _connection);
				
            //    _rdr = _cmd.ExecuteReader();
					
            //    while (_rdr.Read()) 
            //    {
            //        //_error = "Bad Username/password combo";
            //        _player = new Shinobi(_rdr.GetInt32(1), _rdr.GetString(4), _rdr.GetInt32(6), _rdr.GetInt32(7), _rdr.GetInt32(8));
            //    }
            //    _isLogedIn = true;

            //    _connection.Close();
            //}
            //catch (Exception ex)
            //{
            //    Debug.Log(ex.ToString());
            //    _error = "Can't connect to Naruto Shadows.";
            //}
		}
	}
}

public class Shinobi
{
	private readonly int _id;
	private readonly string _nickname;
	private int _age, _xp, _cash;
	
	public Shinobi(int id, string nickname, int age, int xp, int cash)
	{
		_id= id;
		_nickname = nickname;
		_age = age;
		_xp = xp;
		_cash = cash;
	}
	
	public string NickName
	{
		get{return _nickname;}
	}
	public int Cash
	{
		get{return _cash;}
		set{ _cash = value; }
	}
	public int XP
	{
		get{return _xp;}
		set{ _xp = value;}
	}
	public int AGE
	{
		get{return _age;}
		set{ _age = value; }
	}
    
}
