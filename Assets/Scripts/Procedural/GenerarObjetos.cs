using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets._2D;
public class GenerarObjetos : MonoBehaviour {

	int [,] mapa;
	[Header("Objetos a generar")]
	public GameObject llave;
	public GameObject puerta;
	public GameObject moreSpeedPowerUp;
	[Header("Personaje")]
	public GameObject personaje;
	private GameObject[] easyArray;
	private float nodeSize;
    //Este script se encarga de instanciar los objetos del juego (los tres) en cada esquina
    // Aparte de los objetos, genera al propio personaje en la esquina que quede.

    // Use this for initialization
    public void onAwakeCall () {
		mapa = GetComponentInParent<RayasGrandesProceduralMap> ().getMapa ();
        nodeSize = GetComponentInParent<RayasGrandesProceduralMap> ().colisionAGenerar.transform.localScale.x;
		easyArray = new GameObject[4];
		easyArray [0] = puerta;
		easyArray [1] = llave;
		easyArray [2] = moreSpeedPowerUp;
		easyArray [3] = personaje;
		generacion ();
	}
	
	private void generacion(){

        //Instancia aleatoriamente todos los objetos y al personaje. Lo hace cogiendo un valor aleatorio del array, quitando ese valor del array (reduceArray) y seguir iterando 4 veces. 
		bool puertaConseguida = false;
		int aux = 0;
		for (int i = 0; i < mapa.GetLength(0)&& !puertaConseguida; i++) {
			for (int j = 0; j < mapa.GetLength(0) && !puertaConseguida; j++) {
				if (mapa [i, j] == 1) {
					aux = Random.Range (0, easyArray.Length);
					if (easyArray[aux] == personaje)
						GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera2DFollow> ().target = ((GameObject) Instantiate (easyArray[aux], getWorldPoint(i,j), Quaternion.identity)).transform;
					else
						Instantiate (easyArray[aux], getWorldPoint(i,j), Quaternion.identity);
					reduceArray (aux);
					puertaConseguida = true;
				}
			}

		}

		puertaConseguida = false;
		aux = 0;

		for (int i = mapa.GetLength(0)-1; i >= 0&& !puertaConseguida; i--) {
			for (int j = 0; j < mapa.GetLength(0) && !puertaConseguida; j++) {
				if (mapa [i, j] == 1) {
					aux = Random.Range (0, easyArray.Length);
					if (easyArray[aux] == personaje)
						GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera2DFollow> ().target = ((GameObject) Instantiate (easyArray[aux], getWorldPoint(i,j), Quaternion.identity)).transform;
					else
						Instantiate (easyArray[aux], getWorldPoint(i,j), Quaternion.identity);
					reduceArray (aux);
					puertaConseguida = true;
				}
			}

		}
		puertaConseguida = false;
		aux = 0;
		for (int i = 0; i < mapa.GetLength(0) && !puertaConseguida; i++) {
			for (int j = mapa.GetLength(1) -1 ; j >=  0 && !puertaConseguida; j--) {
				if (mapa [i, j] == 1) {
					aux = Random.Range (0, easyArray.Length);
					if (easyArray[aux] == personaje)
						GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera2DFollow> ().target = ((GameObject) Instantiate (easyArray[aux], getWorldPoint(i,j), Quaternion.identity)).transform;
					else
						Instantiate (easyArray[aux], getWorldPoint(i,j), Quaternion.identity);
					reduceArray (aux);
					puertaConseguida = true;
				}
			}

		}

		puertaConseguida = false;
		aux = 0;
		for (int i = mapa.GetLength(0)-1; i >= 0&& !puertaConseguida; i--) {
			for (int j = mapa.GetLength(1) -1 ; j >=  0 && !puertaConseguida; j--) {
				if (mapa [i, j] == 1) {
					aux = Random.Range (0, easyArray.Length);
					if (easyArray[aux] == personaje)
						GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera2DFollow> ().target = ((GameObject) Instantiate (easyArray[aux], getWorldPoint(i,j), Quaternion.identity)).transform;
					else
						Instantiate (easyArray[aux], getWorldPoint(i,j), Quaternion.identity);
					reduceArray (aux);
					puertaConseguida = true;
				}
			}

		}

        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera2DFollow>().startCamera();
    }

	public void reduceArray(int x){
		easyArray [x] = easyArray [easyArray.Length - 1];
		GameObject[] aux =new  GameObject [easyArray.Length - 1];

		for (int i = 0; i < aux.Length; i++) {
			aux [i] = easyArray [i];
		
		}

		easyArray = aux;
	}

	public  Vector2 getWorldPoint(int x, int y) {
		Vector2 aux = new Vector2 ();

		aux.x = x < mapa.GetLength(0) / 2 ? (x - mapa.GetLength(0) / 2) * (nodeSize) : (x - mapa.GetLength(0)/2) *nodeSize;
		aux.y = y < mapa.GetLength(1) / 2 ? (y - mapa.GetLength(1) / 2) * (-nodeSize) : -(y - mapa.GetLength(1) / 2) *nodeSize;
		aux.x += nodeSize/2;
		aux.y -= nodeSize/2;
		return aux;
	}
}
