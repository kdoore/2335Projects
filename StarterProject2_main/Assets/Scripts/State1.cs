using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class State1 : IStateBase {

	private StateManager manager;
	public Text displayText;
	
	public State1( StateManager managerReference){
		this.manager = managerReference;
		
		Debug.Log ("constructor for State1");
	}
	
	public void StateUpdate(){
		if(Input.GetKeyUp (KeyCode.Space)){
			manager.somePlayerData++;
			Application.LoadLevel ("Start");
			Debug.Log ("leaving Scene1");
			manager.SwitchState(new BeginState(manager));
		}
	}
	
	public void ShowIt(){
		
	}
	
}
