﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {

	public GameObject fireBullet;
	
	// Update is called once per frame
	void Update () {
		ShootBullet();
	}

	void ShootBullet()
	{
		if (Input.GetKeyDown(KeyCode.J))
		{
			GameObject bullet = Instantiate(fireBullet, transform.position, Quaternion.identity);
			bullet.GetComponent<FireBullet>().speed *= transform.localScale.x;
		}
	}
}
