using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class PlayerScript : MonoBehaviour
{

    private int speedMultiplier = 1;
    private bool hasKey = false;
    public int invTime = 3;
    private float timer = 0;
	private float timerCu = 0, timerCa = 0;

    private GameObject llaveText;

	private SpriteRenderer sr;
	private Color co;
    void Start()
    {
		
		sr = this.GetComponent<SpriteRenderer> ();
		co = sr.color;
        llaveText = GameObject.FindGameObjectWithTag("LlaveText");
        llaveText.SetActive(false);

    }

    void Update()
    {
        timer += Time.deltaTime;

		changeOpacity ();

    }



	private void changeOpacity(){
		if (timer < invTime && sr.color.a!= 0.3f) {

			sr.color = new Color (co.r, co.g, co.b, Mathf.Lerp (1, 0.3f, timer-timerCa));
			timerCu = timer;



		} else if (timer < invTime) {
			sr.color = new Color (co.r, co.g, co.b, Mathf.Lerp (0, 0.3f, timer-timerCu));
			timerCa = timer;
		}

		else{
			sr.color = new Color (co.r, co.g, co.b, 1);

		}
	}
    public int getSpeedMultiplier()
    {
        return speedMultiplier;

    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Enemigo") && timer > invTime)
        {
			Generador.dificultad = 0;
			SceneManager.LoadScene ("Menu");



            
        }
        if (other.gameObject.CompareTag("Llave"))
        {
            hasKey = true;
            Destroy(other.gameObject);
            llaveText.SetActive(true);
        }

        else if (other.gameObject.CompareTag("Puerta") && hasKey)
        {
            print("He pasado el nivel");
			SceneManager.LoadScene ("InterLevel");
        }

        else if (other.gameObject.CompareTag("Velocidad"))
        {
            speedMultiplier = 2;
            Destroy(other.gameObject);
        }
        
    }

	IEnumerator comenzarMuerte(){
		yield return new WaitForSeconds (3);
		SceneManager.LoadScene ("Menu");
	}

}
