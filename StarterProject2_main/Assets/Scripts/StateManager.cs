/// <summary>
/// State manager. 
/// Singleton class to manage states and to maintain a reference to the currentState 
/// </summary>
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StateManager : MonoBehaviour {
	
	//For Singleton Pattern, we want to insure that only 1 instance of this class can ever exist
	//This item is not destroyed when chainging Scenes.
	public static StateManager instanceRef; 
	
	private IStateBase activeState;  //reference to activeState
	public int somePlayerData=0;
	//Called before any GameObjects Start() method is called
	//Allows pre-initialization: Called once when a script is loaded
	void Awake(){
		if(instanceRef==null){
			instanceRef=this;
			Debug.Log ("create me");
			DontDestroyOnLoad(gameObject);
		}
		else{
			Debug.Log ("destroy me");
			DestroyImmediate(gameObject);
		}
	}
	//Unity-Hook: Called once when a new scene is initiated
	void Start () {  
		activeState= new BeginState(this);   //pass in a reference to this manager
		Debug.Log ("in stateManager start" + activeState);	
	}
	
	//Unity-Hook: Update is called once per frame
	void Update () {
		if(activeState != null){
			activeState.StateUpdate();
			
		}
		
	}
	//Unity-Hook: Possibly called multiple times per frame
	void OnGUI(){
		if(activeState != null){
			activeState.ShowIt();  //call state object's GUI statements
			//Debug.Log ("onGUI");
		}
	}
	
	//Called from the current state to notify StateManager that a new state is the activeState
	public void SwitchState( IStateBase newState){
		activeState = newState;  //change the reference to the current active state	
	}
}
