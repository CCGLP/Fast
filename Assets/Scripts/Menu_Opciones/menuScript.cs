using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class menuScript : MonoBehaviour {

	void Start(){
		Camera.main.aspect = 0.5f;
		Camera.main.ResetAspect ();
	}


	public void onPlayClick(){
		Generador.dificultad = 0;
		SceneManager.LoadScene ("level");

	}

	void Update(){
		if (Input.anyKeyDown) {
			

			onPlayClick ();
		}

	}
}
