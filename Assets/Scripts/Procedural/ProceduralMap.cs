﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ProceduralMap : MonoBehaviour {
	private int[,] mapa;

	public GameObject colisionAGenerar;
	GameObject father;
	public int worldSizeMapX;
	public int worldSizeMapY;
	public int casillasLibresCentro;

	public int cantidadRama;
	public int longitudRama;

	public int lineasGeneradas;
	private List<Line> AlmacenLines;
	private float nodeSize;
	public bool drawGizmos;


	public GameObject suelo1;
	public GameObject suelo2;
	// Use this for initialization
	public void Awake () {
	    father = new GameObject ("map");
		father.transform.position = Vector3.zero;

		nodeSize = colisionAGenerar.transform.localScale.x;
		mapa = new int[worldSizeMapX, worldSizeMapY];
		AlmacenLines = new List<Line>();
		motherLines ();
		generarLines ();
		generarRamas();
		liberarCasillasCentro ();
		generarLimites();
		dibujarMapa();

	}

	public int[,] getMapa(){
		return mapa;
	}

	private void generarLines(){
		//Genera más lineas según la cantidad que diga la variable.
		int auxX;
		int auxY;

		for (int iteraciones = 0; iteraciones <= lineasGeneradas; iteraciones++) {
			if (Random.Range (0,100 ) <= 50) {
				auxX = Random.Range (0, worldSizeMapX);
				AlmacenLines.Add(new Line(new Vector2(auxX, 0), false,this,nodeSize));
				for (int j = 0; j < worldSizeMapY; j++) {
					mapa [auxX, j] = 1;
				}

			} else {
				auxY = Random.Range (0, worldSizeMapY);
				AlmacenLines.Add(new Line(new Vector2(0, auxY), true,this,nodeSize));
				for (int i = 0; i < worldSizeMapX; i++) {
					mapa [i, auxY] = 1;
				}
			}
		}
	}
	private void liberarCasillasCentro(){
		for (int i = worldSizeMapX/2 - casillasLibresCentro/2; i< worldSizeMapX/2 +casillasLibresCentro/2;i++){
			for(int j= worldSizeMapY/2 - casillasLibresCentro/2; j<worldSizeMapY/2 + casillasLibresCentro/2;j++){
				mapa[i,j] = 1;
			}
		}


	}
	private void generarLimites()
	{

		//Genera límites fuera del array, para librarnos de problemas.
		for (int i = -1; i <= worldSizeMapX; i++) {
			((GameObject)Instantiate (colisionAGenerar, getWorldPoint (i, -1), Quaternion.identity)).transform.SetParent(father.transform);
			((GameObject)Instantiate (colisionAGenerar, getWorldPoint (i, worldSizeMapY), Quaternion.identity)).transform.SetParent (father.transform);
		}
		for (int j= -1; j <= worldSizeMapY; j++) {
			((GameObject)Instantiate (colisionAGenerar, getWorldPoint (-1, j), Quaternion.identity)).transform.SetParent(father.transform);
			((GameObject)Instantiate (colisionAGenerar, getWorldPoint (worldSizeMapX, j), Quaternion.identity)).transform.SetParent(father.transform);
		}

	}

	//Método auxiliar que pasa una posición del array a posición del mundo.
	public  Vector2 getWorldPoint(int x, int y) {
		Vector2 aux = new Vector2 ();

		aux.x = x < mapa.GetLength(0) / 2 ? (x - mapa.GetLength(0) / 2) * (nodeSize) : (x - mapa.GetLength(0)/2) *nodeSize;
		aux.y = y < mapa.GetLength(1) / 2 ? (y - mapa.GetLength(1) / 2) * (-nodeSize) : -(y - mapa.GetLength(1) / 2) *nodeSize;
		aux.x += nodeSize/2;
		aux.y -= nodeSize/2;
		return aux;
	}

	private void generarRamas()
	{
		int aux;
		int iteracionY;
		int iteracionX;
		int iRama = 0;
		// Genero ramas aleatoriamente basado en la cantidad de ramas y su longitud. Es todo sencillo y aleatorio.
		for (int i= 0; i<cantidadRama; i++)
		{
			aux = Random.Range(0, AlmacenLines.Count);
			iRama = 0;
			if (AlmacenLines[aux].getIfHorizontal())
			{
				iteracionY = (int) AlmacenLines[aux].getStartPoint().y;
				iteracionX = Random.Range(0, worldSizeMapX);

				if (Random.Range(0,100)<= 50)
				{
					while (iRama < longitudRama && iteracionY < worldSizeMapY-1 && iteracionX < worldSizeMapX-1 )
					{
						iRama++;
						if (Random.Range(0,100)<= 50)
						{
							iteracionY++;
						}
						else
						{
							iteracionX++;
						}
						mapa[iteracionX, iteracionY] = 1;

					}


				}
				else
				{
					while (iRama < longitudRama && iteracionY > 0 && iteracionX > 0 )
					{
						iRama++;
						if (Random.Range(0, 100) <= 50)
						{
							iteracionY--;
						}
						else
						{
							iteracionX--;
						}
						mapa[iteracionX, iteracionY] = 1;

					}
				}

			}

			else
			{
				iteracionY = Random.Range(0, worldSizeMapY);
				iteracionX = (int)AlmacenLines[aux].getStartPoint().x;
				if (Random.Range(0, 100) <= 50)
				{
					while (iRama < longitudRama && iteracionY < worldSizeMapY - 1 && iteracionX < worldSizeMapX - 1)
					{
						iRama++;
						if (Random.Range(0, 100) <= 50)
						{
							iteracionY++;
						}
						else
						{
							iteracionX++;
						}
						mapa[iteracionX, iteracionY] = 1;

					}


				}
				else
				{
					while (iRama < longitudRama && iteracionY > 0 && iteracionX > 0)
					{
						iRama++;
						if (Random.Range(0, 100) <= 50)
						{
							iteracionY--;
						}
						else
						{
							iteracionX--;
						}
						mapa[iteracionX, iteracionY] = 1;

					}
				}


			}


		}

	}

	private void motherLines(){
		//Genera las líneas "Madre". Esta genera una línea horizontal y vertical. 
		if (Random.Range (0, 100) >= 50) {
			AlmacenLines.Add(new Line(new Vector2(0, worldSizeMapY / 2), true, this,nodeSize));
			for (int i = 0; i < worldSizeMapX; i++) {
				mapa [i, worldSizeMapY / 2] = 1;
			}


			int auxX = Random.Range (0, worldSizeMapX);
			AlmacenLines.Add(new Line(new Vector2(auxX, 0), false, this, nodeSize));
			for (int y = 0; y < worldSizeMapY; y++) {
				mapa [auxX, y] = 1;
			}

		} else {
			AlmacenLines.Add(new Line(new Vector2(worldSizeMapX/2, 0), false, this,nodeSize));
			for (int j = 0; j < worldSizeMapY; j++) {
				mapa [worldSizeMapX / 2, j] = 1;
			}
			int auxY = Random.Range (0, worldSizeMapY);
			AlmacenLines.Add(new Line(new Vector2(0, auxY), true, this,nodeSize));

			for (int x = 0; x < worldSizeMapX; x++) {
				mapa [x, auxY] = 1;
			}

		}
	}

	void OnDrawGizmos(){

		Gizmos.DrawWireCube (this.transform.position, new Vector3 (worldSizeMapX *nodeSize, worldSizeMapY*nodeSize,0));

		if (mapa != null && drawGizmos) {
			float auxX = 0- worldSizeMapX /2 + 0.5f;
			float auxY = worldSizeMapY/2-0.5f;
			for (int i = 0; i < mapa.GetLength (0); i++) {
				for (int j = 0; j < mapa.GetLength (1); j++) {
					Gizmos.color = mapa [i, j] == 1 ? Color.black : Color.red;
					Gizmos.DrawCube (new Vector3 (auxX, auxY), Vector3.one);
					auxY -= 1;
				}
				auxY = worldSizeMapY/2-0.5f;
				auxX += 1;
			}
		}
	}

	public List<Line> getLines(){
		return this.AlmacenLines;
	}

	//Dibuja el mapa instanciando los objetos 
	void dibujarMapa()
	{
		int rand;
		float auxX = 0 - (worldSizeMapX * nodeSize) / 2 + nodeSize*0.5f;
		float auxY = (worldSizeMapY*nodeSize) / 2 - nodeSize*0.5f;


		for (int i = 0; i < mapa.GetLength(0); i++)
		{
			for (int j = 0; j < mapa.GetLength(1); j++)
			{

				if (mapa [i, j] == 0) {
					   ((GameObject) Instantiate (colisionAGenerar, new Vector3 (auxX, auxY, 0), Quaternion.identity)).transform.SetParent(father.transform);
				} else {
					rand = Random.Range (0, 100);

					if (rand < 50) {
						((GameObject)Instantiate (suelo1, new Vector3 (auxX, auxY, 0), Quaternion.identity)).transform.SetParent(father.transform);
					} else {
						((GameObject)Instantiate (suelo2, new Vector3 (auxX, auxY, 0), Quaternion.identity)).transform.SetParent (father.transform);
					}

				}
				auxY -= nodeSize;
			}
			auxY = (worldSizeMapY * nodeSize) / 2 - nodeSize*0.5f;
			auxX += nodeSize;
		}

	}
}
