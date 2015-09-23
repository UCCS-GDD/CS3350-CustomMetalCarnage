using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour 
{
	public float heightOffset;
	private Vector3 targetPosition;
	private Vector3 prePhysicsPosition;
	//public float maxHeight;
	private Camera myCamera;
	//private GameObject myPlayer;
	private float lookDirectionTargetX;
	private float lookDirectionTargetZ;
	private Vector3 lookDirection;
	private float inputX;
	private float inputY;
	public float lookTiltScaler;
	public float moveHeightScaler;
	private bool oldPosUsed;

	// Use this for initialization
	void Start () 
	{
		oldPosUsed = true;
		myCamera = GetComponent<Camera> ();
		myCamera.nearClipPlane = 5;

		//myPlayer = transform.parent.gameObject;

		//transform.position = new Vector3 (followOffset.x + myCamera.transform.position.x, followOffset.y, followOffset.z + myCamera.transform.position.z);

	}

	void FixedUpdate()
	{
		// Record camera position before it is inherited after FixedUpdate()
		if(oldPosUsed)
		{
			prePhysicsPosition = transform.position;
			oldPosUsed = false;
		}
	}

	// Update is called once per frame
	void Update () 
	{



		inputX = Input.GetAxis("Horizontal");
		inputY = Input.GetAxis("Vertical");

		//Debug.Log (inputX +" " +inputY);
		transform.position = prePhysicsPosition;
		oldPosUsed = true;
		targetPosition = new Vector3 (transform.parent.position.x, heightOffset+(Mathf.Sqrt(Mathf.Pow(inputX,2)+Mathf.Pow(inputY,2))*moveHeightScaler), transform.parent.position.z);

		transform.position = new Vector3(Mathf.Lerp(transform.position.x, targetPosition.x, .05f), Mathf.Lerp(transform.position.y, targetPosition.y, .01f), Mathf.Lerp(transform.position.z, targetPosition.z, .05f));
		//transform.position = Vector3.Lerp(transform.position, targetPosition, .01f);


		lookDirectionTargetZ = inputX * lookTiltScaler;
		lookDirectionTargetX = inputY * lookTiltScaler;

	}




	void LateUpdate()
	{
		// Rotate camera in the direction you're moving
		if(inputY > 0)
		{
			transform.parent.localRotation = Quaternion.Lerp(transform.parent.localRotation, Quaternion.Euler(-lookDirectionTargetX, 0, lookDirectionTargetZ), .01f);
		}
		else
		{
			transform.parent.localRotation = Quaternion.Lerp(transform.parent.localRotation, Quaternion.Euler(-10, 0, lookDirectionTargetZ), .01f);
		}
		// Keep the camera parent from rotating with MouseAim
		transform.parent.rotation = Quaternion.Euler(transform.parent.rotation.eulerAngles.x, 0, transform.parent.rotation.eulerAngles.z);


		// Cap the camera's max height
		/*if(transform.position.y > maxHeight)
		{
			transform.position = new Vector3(transform.position.x, maxHeight, transform.position.z);
		}
		else if(transform.position.y < maxHeight)
		{
			transform.localPosition = followOffset;
		}*/

		// Keep the camera from rotating around the Y axis
		//transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 0, transform.rotation.eulerAngles.z);





	}


}


