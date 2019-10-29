using System.Collections;
using UnityEngine;

public class rotation : MonoBehaviour {

	public float xRotation = 0F;
	public float yRotation = 0F;
	public float zRotation = 0F;
	void OnEnable(){
		InvokeRepeating("Rotate", 0f, 0.0167f);
	}
	void OnDisable(){
		CancelInvoke();
	}
	void Rotate(){
		transform.localEulerAngles += new Vector3(xRotation,yRotation,zRotation);
	}
}
