using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnProjectilesScript : MonoBehaviour
{
	public GameObject firePoint;
    public GameObject VFX;

	public void SpawnVFX () {
		GameObject vfx;

		if (firePoint != null) {
			vfx = Instantiate (VFX, firePoint.transform.position, Quaternion.identity);
		}
		else
			vfx = Instantiate (VFX);
	}
}
