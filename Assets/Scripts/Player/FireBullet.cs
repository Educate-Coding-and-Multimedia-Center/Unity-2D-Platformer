using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour {

	public float speed = 10f;
	private Animator anim;

	private bool canMove = true;

	void Start ()
    {
		anim = GetComponent<Animator>();

		StartCoroutine(DisableBullet(5f));
    }

	// Use this for initialization
	void Update () {
		if (canMove)
		{
			Vector3 temp = transform.position;
			temp.x += speed * Time.deltaTime;
			transform.position = temp;
		}
	}
	
	public float Speed
    {
		get
        {
			return speed;
        }
        set
        {
			speed = value;
        }
    }

	IEnumerator DisableBullet(float timer)
    {
		yield return new WaitForSeconds(timer);
		gameObject.SetActive(false);
    }

	void OnTriggerEnter2D(Collider2D target)
    {
		if (target.gameObject.tag == "Beetle" || target.gameObject.tag == "Snail" || target.gameObject.tag == "Spider" || target.gameObject.tag == "Boss")
        {
			anim.Play("Explode");
			canMove = false;
			StartCoroutine(DisableBullet(0.1f));
        }
    }
}
