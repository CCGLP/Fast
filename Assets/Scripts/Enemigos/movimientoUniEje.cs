using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class movimientoUniEje : MonoBehaviour {

	private Rigidbody2D rb;
	public bool walkHorizontal;
	public float speed = 4;
	// Use this for initialization
	void Start () {
	
		rb = this.GetComponent<Rigidbody2D> ();
		rb.velocity = walkHorizontal ? Vector2.right * speed : Vector2.up * speed; 
	}

	public void reSetearVelocidad(){
		rb = this.GetComponent<Rigidbody2D> ();

		rb.velocity = walkHorizontal ? Vector2.right * speed : Vector2.up * speed; 
	}
	void OnTriggerEnter2D(Collider2D coll) {
		rb.velocity = -rb.velocity;
	}
}
