﻿using UnityEngine;
using System.Collections;

public class AllCheats : MonoBehaviour {
	
	public bool developerMode;
	[HideInInspector] public bool nitroAllowed,jumpAllowed;
	bool checkPassword,showDeveloperMenu,isMandeepMapAvailable,isMustafaMapAvailable;
	string passwordTry="Enter Password";
	
	
	string checkStatus(bool YESorNO){
		if(YESorNO){
			return " is Enabled";
		}
		else{
			return " is Disabled";
		}
	}
	
	string checkMapUnlocked(bool YESorNO){
		if(YESorNO){
			return " is Unlocked";
		}
		else{
			return " is Locked";
		}
	}
	
	void UnlockMandeepMap(){
		if(!isMandeepMapAvailable){
			GameObject lockedDoor = GameObject.Find ("ClosedDoorMandeep");
			Vector3 pos = lockedDoor.transform.position;
			Quaternion rot = lockedDoor.transform.rotation;
			DestroyObject (lockedDoor);
			Instantiate (Resources.Load ("Doors/OpenDoorMandeep"), pos, rot);
			isMandeepMapAvailable=true;
		}
	}
	
	void UnlockMustafaMap(){
		if(!isMustafaMapAvailable){
			GameObject lockedDoor = GameObject.Find ("ClosedDoorMustafa");
			Vector3 pos = lockedDoor.transform.position;
			Quaternion rot = lockedDoor.transform.rotation;
			DestroyObject (lockedDoor);
			Instantiate (Resources.Load ("Doors/OpenDoorMustafa"), pos, rot);
			isMustafaMapAvailable=true;
		}
	}
	
	void isPasswordRight(){
		if(passwordTry=="GURIorMSK") developerMode=!developerMode;
		if(passwordTry=="kangaroo") jumpAllowed=!jumpAllowed;
		if(passwordTry=="rapid") nitroAllowed=!nitroAllowed;
		if(passwordTry=="mandeepmap") UnlockMandeepMap();
		if(passwordTry=="mustafamap") UnlockMustafaMap();
		//if(passwordTry=="CheatCode") var=!var;
		//if(passwordTry=="CheatCode") var=!var;
		//if(passwordTry=="CheatCode") var=!var;
		
		passwordTry="Enter Password";
		checkPassword = false;
		Time.timeScale = 1;
	}
	
	void Update(){
		if(Input.GetKeyDown(KeyCode.O)&&developerMode){
			showDeveloperMenu=!showDeveloperMenu;
			if(Time.timeScale!=0) Time.timeScale=0;
			else Time.timeScale=1;
		}
		if(Input.GetKeyUp(KeyCode.KeypadMultiply)&&!showDeveloperMenu){
			checkPassword=true;
			Time.timeScale=0;
		}
		if(Input.GetKeyDown(KeyCode.Escape)){
			checkPassword=false;
			showDeveloperMenu=false;
			Time.timeScale=1;
		}
	}
	
	void OnGUI(){
		if (showDeveloperMenu) {
			GUI.Box(new Rect((Screen.width-300)/2,(Screen.height-400)/2,300,400),"Developer Options");
			if(GUI.Button(new Rect((Screen.width-200)/2,(Screen.height-400)/2+40,200,35),"Jump"+checkStatus(jumpAllowed))) jumpAllowed=!jumpAllowed;
			if(GUI.Button(new Rect((Screen.width-200)/2,(Screen.height-400)/2+80,200,35),"Nitro"+checkStatus(nitroAllowed))) nitroAllowed=!nitroAllowed;
			if(GUI.Button(new Rect((Screen.width-200)/2,(Screen.height-400)/2+120,200,35),"Mandeep's map"+checkMapUnlocked(isMandeepMapAvailable))) UnlockMandeepMap();
			if(GUI.Button(new Rect((Screen.width-200)/2,(Screen.height-400)/2+160,200,35),"Mustafa's map"+checkMapUnlocked(isMustafaMapAvailable))) UnlockMustafaMap();
			//if(GUI.Button(new Rect((Screen.width-200)/2,(Screen.height-400)/2+200,200,35),"Var"+checkStatus(var))) var=!var;
			//if(GUI.Button(new Rect((Screen.width-200)/2,(Screen.height-400)/2+240,200,35),"Var"+checkStatus(var))) var=!var;
			//if(GUI.Button(new Rect((Screen.width-200)/2,(Screen.height-400)/2+280,200,35),"Var"+checkStatus(var))) var=!var;
			
		}
		else if(checkPassword){
			GUIStyle myStyle = new GUIStyle(GUI.skin.textField);
			myStyle.alignment = TextAnchor.MiddleCenter;
			passwordTry=GUI.TextField(new Rect((Screen.width-200)/2, (Screen.height-20)/2, 200, 20), passwordTry, myStyle);
			if (Event.current.isKey && Event.current.keyCode == KeyCode.Return || GUI.Button (new Rect ((Screen.width+250)/2,(Screen.height-20)/2,50,20), "Enter")) isPasswordRight();
		}
	}
	
}