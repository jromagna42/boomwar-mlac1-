using UnityEngine;
using System.Collections;
using DPhysics;

/// <summary>
/// This example is similar to the Newton's Balls example except the perfect conservation of momentum is contained within a box.
/// Also, a small pseudo-random force is given at the start to several balls to create some chaos.
/// </summary>
/// 
public class NewtonsBox : MonoBehaviour
{
	public GameObject Ball;
	public GameObject Wall;
	public GameObject BallHolder;
	// This method spawns balls, sets physics variables, and applies an initial force to each ball.
	void Start()
	{
		Starter(10, 10);
	}
	void Starter (int rows, int columns)
	{
		//Initializing the simulation
		Started = true;
		Time.fixedDeltaTime = .1f;
		DPhysicsManager.Restitution = FInt.OneF; //Restitution of 1 results in complete conservation of momentum
		DPhysicsManager.CollisionDamp = FInt.HalfF; //Offset our balls a bit on collisions. This helps keep them seperated.
		DPhysicsManager.Drag = FInt.OneF; //Velocity will be completely conserved
		DPhysicsManager.SleepVelocity = FInt.ZeroF; //Don't want any of our objects sleeping on us in this box of perfect momention conservation.
		
		//The defualt spacing
		FInt Spacing = FInt.Create (3d);
		
		//Instantiating Newton's Balls!
		for (int j = 0; j < rows; j++) {
			Vector2d SpawnVec = new Vector2d ((Spacing * (-columns / 2)), Spacing * (j - rows / 2));
			Body ball;
			for (int i = 0; i < columns; i++) {
				ball = Instantiate (Ball, BallHolder.transform).GetComponent<Body> ();
				ball.Initialize (SpawnVec); //Always remember to initialize
				SpawnVec.x += Spacing; //Increment the spawn position for the next spawned ball

				//Creates and applies an initial pseudo-random force
				Vector2d StartVelocity = new Vector2d(2, 2);

				ball.ApplyVelocity (ref StartVelocity); 
			}
		}


		//Instantiating our walls
		Body WallLeft = Instantiate (Wall).GetComponent<Body> (); 
		WallLeft.Initialize (new Vector2d (Spacing * (-columns / 2 - 2), FInt.ZeroF));
		Body WallRight = Instantiate (Wall).GetComponent<Body> ();
		WallRight.Initialize (new Vector2d (Spacing * (columns / 2 + 2), FInt.ZeroF));
		
		//For the bot and top walls, we need to rotate them to be perpendicular to the other walls to form a box.
		Body WallBot = Instantiate (Wall).GetComponent<Body>();
		WallBot.Initialize (new Vector2d (FInt.ZeroF, Spacing * (-rows / 2 -2)));
		//Make sure you set the rotation after initializing the body.
		WallBot.Rotation = WallBot.Rotation.localright;
		
		Body WallTop = Instantiate (Wall).GetComponent<Body>();
		WallTop.Initialize (new Vector2d (FInt.ZeroF, Spacing * (rows / 2 + 2)));
		WallTop.Rotation = WallTop.Rotation.localright;
	}

	
	void Update ()
	{
		//This call will communicate with Unity's rendering and transform components
		//This is optional and does not affect the simulation
		DPhysicsManager.Visualize ();
	}
	
	int Rows = 15;
	int Columns = 30;
	bool Started = true;
	float camHeight = 60;
	void OnGUI ()
	{
		//GUI stuff :P
		// if (!Started) {
		// 	GUILayout.Label ("Rows: ");
		// 	int tempRows;
		// 	if (int.TryParse (GUILayout.TextField (Rows.ToString ()), out tempRows)) {
		// 		Rows = tempRows;
		// 	}
			
		// 	GUILayout.Label ("Columns: ");
		// 	int tempColumns;
		// 	if (int.TryParse (GUILayout.TextField (Columns.ToString ()), out tempColumns)) {
		// 		Columns = tempColumns;
		// 	}
		// 	if (GUILayout.Button ("Start!")) {
		// 		Starter (Rows, Columns);
		// 	}
		// }
		// else{
		// 	if (GUILayout.Button("Clear"))
		// 	{
		// 		//This dessimilate and destroys all existing physics objects.
		// 		//While dessimilation is optional, it significantly improves performance when there are many objects.
		// 		for (int i = 0; i < DPhysicsManager.SimObjects.Length; i++)
		// 		{
		// 			Body body = DPhysicsManager.SimObjects[i];
		// 			if (body == null) continue;
		// 			DPhysicsManager.Dessimilate(body);
		// 			Destroy(body.gameObject);
		// 		}
		// 		Started = false;
		// 	}
		// }
		//Camera stuff
		GUILayout.Label ("Camera Height");
		camHeight = GUILayout.VerticalSlider(camHeight,120,20);
		Camera.main.transform.position = new Vector3(Camera.main.transform.position.x,camHeight,Camera.main.transform.position.z);
		

	}


	public float simTime = 15f;
	float		timeSinceStart = 0f;
	Component[]   transformTab;
	string		theBigString;
	bool		boucheriedone = false;

	void FixedUpdate()
	{
		if (Started == true)
			timeSinceStart += Time.fixedDeltaTime;
		if (timeSinceStart > simTime  && boucheriedone == false)
		{
			Boucherie();
		}
		else if (boucheriedone == false)
		{
		//Remember to simulate DPhysics with this call
			DPhysicsManager.Simulate ();
		}
	}

	void Boucherie()
	{
		Debug.Log("preboucherie");
		transformTab = BallHolder.GetComponentsInChildren<Transform>();

	//	Debug.Log(transformTab.Cast< Transform >().Select(t => t.position.ToString() + t.rotation.ToString()).Aggregate((n1, n2) => n1 + n2).GetHashCode());
		foreach (Transform t in transformTab)
		{
			theBigString = string.Concat(theBigString, string.Concat(t.position.ToString(), t.rotation.ToString()));
		}
		Debug.Log(theBigString);
		
		Debug.Log(theBigString.GetHashCode());
		
		Debug.Log("postboucherie");
		boucheriedone = true;
	}
}
