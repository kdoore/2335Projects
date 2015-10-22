using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/// <summary>
/// Begin state. 
/// This class is the start state for the application.
///	It corresponds to the Start scene.  It is managed by the
/// StateManager class which determines when it is the activeState
/// </summary>
public class BeginState : IStateBase {

	private StateManager manager;  //reference to the stateManager 
	private bool initialized=false;
	
	private GameObject mainPanel, storyPanel1,dataPanel;   //Panels
	//private GameObject startTitle,dataText;
	private Text title,data;
	private Button dataButton, startButton;
	private CanvasGroup cg, cg1,cgDP;
	private string newline = System.Environment.NewLine;
		
	
	//this is the constructor, called from either StateManager or another State
	public BeginState( StateManager managerReference){
		   this.manager = managerReference;
		  Debug.Log ("BeginState Constructor"); 
	}
	
	/// <summary>
	///after the scene has been initialized, then we can setup our object references
	///to GameObjects in the scene
	/// </summary>
	void initializeObjectRefs(){ 
		//Panels
		//find the MainPanel Canvas Group
		mainPanel=GameObject.Find ("MainPanel");
		cg=mainPanel.GetComponent<CanvasGroup>();
		
		storyPanel1=GameObject.Find ("StoryPanel1");
		cg1=storyPanel1.GetComponent<CanvasGroup>();
		
		dataPanel=GameObject.Find ("DataPanel");
		cgDP=dataPanel.GetComponent<CanvasGroup>();
		//Text
		//find Start scene Title text so we can modify it
		
		title=GameObject.Find ("StartTitle").GetComponent<Text>(); //this will allow updating of the text
		data=GameObject.Find ("DataText").GetComponent<Text>();
		//Buttons
		//find the MenuButton GameObject and 
		//add the function 'doMenuThings' to the list of listeners
		//so when button1 is clicked, doMenuThings() is executed
		dataButton=GameObject.Find("DataButton").GetComponent<Button>();
		dataButton.onClick.AddListener(doMenuThings);
		
		//find StartButton GameObject - add clickEvent Listener
		startButton=GameObject.Find ("StartButton").GetComponent<Button>();
		startButton.onClick.AddListener(showStoryPanel1);		
	}
	
	/// <summary>
	/// Update the States.  This gets called in StateManager Update() method.
	///  	It is called every frame.  We can listen for input events etc.
	/// First we must check to see that the scene has been initialized
	/// then we only want to initialize the GameObject References 1 time
	/// so we use the initialized flag to insure this is only executed one time.
	/// </summary>
	public void StateUpdate(){
		if(initialized==false && GameObject.Find ("MainPanel")!=null){
			initializeObjectRefs();
			initialized=true;
		}
		
		if(Input.GetKeyUp (KeyCode.Space)){
			manager.somePlayerData++;
			Application.LoadLevel ("Scene1");
		 	manager.SwitchState(new State1(manager));
		}
	}
	
	/// <summary>
	/// Shows it.  This is called once per frame in the stateManager OnGUI() method.
	/// We can create Legacy GUI elements and here is where we will want to change the 
	/// values of display elements
	/// </summary>
	public void ShowIt(){
		//Debug.Log ("show it");
		if(initialized==true){  //make sure the scene has been initialized
			title.text="Welcome" + newline + "Do you want to go on a Wyoming Winter Adventure?" ;
		}
		if(GUI.Button (new Rect(10,10,180,30), "Let's Go to Scene 1!")){
		    Application.LoadLevel ("Scene1");
			manager.somePlayerData++;
			manager.SwitchState(new State1(manager));
		}
	}
	
	//This function is called when onClick executes for flowerButton:
	public void doMenuThings(){
		Debug.Log ("Menu Things are Happening");
		//hide mainPanel
		if(cgDP.alpha==0){
			cgDP.alpha=1;  //set the mainPanel to be invisible
			cgDP.interactable=false;  //turn off interactivity for the invisible mainPanel
			cgDP.blocksRaycasts=false;
			data.text="Player Data: " + manager.somePlayerData;
			}
		else cgDP.alpha=0;
			
		
	}
	
	/// <summary>
	///Called from main panel button
	///hides and inactivates the main panel 
	///then shows and activates  StoryPanel1
	/// </summary>
	public void showStoryPanel1(){
		Debug.Log ("StartButton was clicked");
		//hide mainPanel
		cg.alpha=0;  //set the mainPanel to be invisible
		cg.interactable=false;  //turn off interactivity for the invisible mainPanel
		cg.blocksRaycasts=false;
		//show storyPanel1
		cg1.alpha=1;
		cg1.interactable=true;
		cg1.blocksRaycasts=true;
	}
	
}
