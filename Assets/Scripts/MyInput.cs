using UnityEngine;
using System.Collections;

public class MyInput : MonoBehaviour {

	public class MyTouch  {
		TouchPhase mPhase;
		Vector2 mDeltaPosition;
		Vector2 mPosition;
		float mDeltaTime;

		public Vector2 deltaPosition
		{
			get
			{
				return this.mDeltaPosition;
			}
		}


		public float deltaTime
		{
			get
			{
				return this.mDeltaTime;
			}
		}

		public int fingerId
		{
			get
			{
				throw new System.NotImplementedException ();
			}
		}


		public TouchPhase phase
		{
			get
			{
				return this.mPhase;
			}
		}

		public Vector2 position
		{
			get
			{
				return this.mPosition;
			}
		}

		public Vector2 rawPosition
		{
			get
			{
				throw new System.NotImplementedException ();
			}
		}


		public int tapCount
		{
			get
			{
				throw new System.NotImplementedException ();
			}
		}

		public MyTouch(TouchPhase phase,Vector2 deltaPos, Vector2 currentPos,float deltaTime){
			mPhase = phase;
			mDeltaPosition = deltaPos;
			mPosition = currentPos;
			mDeltaTime = deltaTime;
		}
	}
	private static MyInput instance = null;

	Vector2 NullVector = new Vector2 (-1000, -1000);

	Vector2 lastPosition;
	Vector2 currentPosition;
	float deltaTime = 0.0f;

	
	void Start () {
		lastPosition = NullVector;
		currentPosition = NullVector;
		MyInput.instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		if (!isTouchbased()) {
			deltaTime = Time.deltaTime;
			if(Input.GetMouseButtonDown(0)){
				lastPosition = NullVector;
				currentPosition = Input.mousePosition;
			}
			else if(Input.GetMouseButton(0))
			{
				lastPosition = currentPosition;
				currentPosition = Input.mousePosition;
			}
			else if(Input.GetMouseButtonUp(0))
			{
				lastPosition = NullVector;
				currentPosition = NullVector;
			}
		}
	}

	static bool isTouchbased ()
	{
//		return true;
		return Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer;
	}

	public static MyTouch GetTouch(){
		if (isTouchbased ()) {
			return Input.touchCount > 0 ? wrapWithMyTouch(Input.GetTouch (0)) : null;
		} else {
			return instance.getSimulatedTouch();
		}
	}

	static MyTouch wrapWithMyTouch (Touch touch)
	{
		return new MyTouch (touch.phase,touch.deltaPosition,touch.position,touch.deltaTime);
	}

	MyTouch getSimulatedTouch ()
	{
		TouchPhase phase = TouchPhase.Stationary;
		Vector2 deltaPos = NullVector;
		if (lastPosition == NullVector && currentPosition != NullVector) 
		{
			phase = TouchPhase.Began;
		}
		else if(currentPosition == NullVector)
		{
			phase = TouchPhase.Ended;
		}
		else if(lastPosition != NullVector && currentPosition != NullVector)
		{
			deltaPos = currentPosition - lastPosition;
			if(deltaPos.magnitude > Vector2.kEpsilon)
			{
				phase = TouchPhase.Moved;
			}
		}
		return new MyTouch (phase,deltaPos,currentPosition,deltaTime);
	}

}
