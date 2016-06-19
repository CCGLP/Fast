using UnityEngine;
using System.Collections;

public class movimientoRandom : MonoBehaviour {

    private Rigidbody2D rb;
    public float speed = 4;
    private float timer = 0;
    public float tiempoCol = 0.6f;
    private float tamanoMundoYPart;
    private float tamanoMundoXPart; 
    // Use this for initialization
    void Start()
    {
        RayasGrandesProceduralMap ray = GameObject.FindGameObjectWithTag("Generador").GetComponent<RayasGrandesProceduralMap>();
        rb = this.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.right * speed;
        tamanoMundoXPart= ray.worldSizeMapX * ray.colisionAGenerar.transform.localScale.x * 0.5f;
        tamanoMundoYPart = ray.worldSizeMapY * ray.colisionAGenerar.transform.localScale.y * 0.5f;
    }
    // Haze un random cada X tiempo, con 4 posibilidades de movimiento. Checkea si se ha salido del mapa cada frame para cambiar la dirección si lo hace. 
    void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        comprobarLimite();

        if (timer > tiempoCol)
        {
            Vector2 aux = rb.velocity;
            int auxR = Random.Range(0, 100);
             if (auxR < 25)
            {
                rb.velocity = -aux;
            }
             if (auxR < 50)
            {
                rb.velocity = new Vector2(aux.y, aux.x); // si la velocidad es 2, 0 pasa a 0,2 , si es -2, 0 pasa a 0.-2 y vicerversa 
            }
            else if (auxR< 75)
            {
                rb.velocity = new Vector2(-aux.y, -aux.x);
            }
            timer = 0;
        }
    }
    public void comprobarLimite()
    {
      if (timer > 0.1f && (transform.position.x < -tamanoMundoXPart && rb.velocity.x < 0)  || (transform.position.x > tamanoMundoXPart && rb.velocity.x > 0) || 
            (transform.position.y < -tamanoMundoYPart && rb.velocity.y < 0) || (transform.position.y > tamanoMundoYPart && rb.velocity.y > 0))
        {
            rb.velocity = -rb.velocity;
            timer = 0; 
        }

    




    }
    
}
