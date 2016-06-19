using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
public class MovimientoBasico : MonoBehaviour {
	private Rigidbody2D rb; 
    
	public float speed = 2;
	private Transform transform;
    public PlayerScript ps;
	/// <summary>
    /// Inicializa las variables que sean componentes
    /// </summary>
	void Start () {
		rb = this.GetComponent<Rigidbody2D> ();
        ps = this.GetComponentInParent<PlayerScript>();
		transform = this.GetComponent<Transform> ();
	}
	
	/// <summary>
    /// Llama  a mover si alguno de los axis tiene valor y a rotar siempre.
    /// </summary>
	void FixedUpdate () {



		mover ();
		//rotar ();
		
		
	}


	
    /// <summary>
    /// Comprueba todas las posibilidades del movimiento, añadiendo fuerza al rigidBody si se cumplen las condiciones
    /// para que se siga aumentando la velocidad, y no haciendolo si no.
    /// El valor que rige la velocidad máxima es maxSpeed
    /// </summary>
	void mover(){ //Nunca sobrepasa la velocidad maxima, ni apretando dos botones a la vez. 
		/*#if (UNITY_IPHONE || UNITY_ANDROID)
		rb.velocity = new Vector2 (CrossPlatformInputManager.GetAxis ("Horizontal"), CrossPlatformInputManager.GetAxis ("Vertical")) * speed * ps.getSpeedMultiplier ();
#endif*/
       // Input.GetTouch()
//#if (!(UNITY_IPHONE || UNITY_ANDROID))
        rb.velocity = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical")) * speed * ps.getSpeedMultiplier ();

//#endif
    }


	void moverTeclado(){
		int auxX = 0;
		int auxY = 0;
		bool pru = false;
		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			auxY = -1;
			pru = true;
		}

		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			auxY = 1;
			pru = true;

		}
		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			auxX= 1;
			pru = true;

		}
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			auxX = -1;
			pru = true;

		}

		if (pru)
			rb.velocity = new Vector2 (auxX, auxY) * speed * ps.getSpeedMultiplier ();
		else {
			mover ();
		}
	}
    /// <summary>
    /// Basado en la posición del ratón en la pantalla y la del personaje, rota a este
    /// basandose en fórmulas trigonométricas para encarar la posición del ratón
    /// Dependiendo de como esté encarado el personaje, será necesario poner el valor del offset al negativo
    /// para que rote de acuerdo a su geometría. Y cambiar a mano valores en el editor hasta que encare a donde queremos.
    /// </summary>
	void rotar(){
		Vector3 mouse = Input.mousePosition;
		Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);
		Vector2 offset = new Vector2(mouse.x - screenPoint.x, mouse.y - screenPoint.y);
		float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0, 0, angle);
	}

	
	
}
	
	
