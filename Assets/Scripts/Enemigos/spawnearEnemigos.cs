using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class spawnearEnemigos : MonoBehaviour {

	private List<Linea> lineas;
    private int[] usados;
	public int cantidadEnemigos;
	public GameObject enemigo;
	public GameObject enemigoCirc;
    public GameObject enemigoIA;
    public GameObject enemigoRandom;
	public float chanceEnemNormal = 80;
	private float nodeSize;
	private float tamanoMundoX;
	private float tamanoMundoY;
    
    /// <summary>
    /// Utilizamos el start (funciona después de que el generador haga sus cosas) 
    /// (Si la dificultad es mayor de 1 (esto es, hemos superado el primer nivel, donde no hay enemigos) genera enemigos básicos. 
    /// Si la cantida de enemigos (que seteamos en el generador) supera a las líneas que hay en el nivel genera enemigos básicos extra (en una línea podrá haber más de un enemigo básico).
    /// </summary>
	// Use this for initialization
	void Start () {
        usados = new int[cantidadEnemigos];
		lineas = this.GetComponentInParent<RayasGrandesProceduralMap> ().getLineas ();
		nodeSize = this.GetComponentInParent<RayasGrandesProceduralMap> ().colisionAGenerar.transform.localScale.x;
        if (Generador.dificultad > 1)
            generarEnemigosIniciales();

        if (cantidadEnemigos > lineas.Count)
        {
            cantidadEnemigos -= lineas.Count;
            generarEnemigosEnMapa();
            
        }
		tamanoMundoX = this.GetComponentInParent<RayasGrandesProceduralMap>().worldSizeMapX * nodeSize;
		tamanoMundoY = this.GetComponentInParent<RayasGrandesProceduralMap>().worldSizeMapY* nodeSize;

		generarEnemigosCirculares ();

	}

	private void generarEnemigosCirculares(){
        //Los random tienen las mismas posibilidades de entrar que los enemigos circulares (a partir de nivel 3, uno en el 3, 2 en el 4...)
		for (int i = 3; i < Generador.dificultad; i++) {
			Instantiate (enemigoCirc, new Vector3 (Random.Range (-tamanoMundoX / 2, tamanoMundoX / 2), Random.Range (-tamanoMundoY / 2, tamanoMundoY / 2), 0), Quaternion.identity);
            Instantiate(enemigoRandom, new Vector3(Random.Range(-tamanoMundoX / 2, tamanoMundoX / 2), Random.Range(-tamanoMundoY / 2, tamanoMundoY / 2), 0), Quaternion.identity);
		}


	}




	private void generarEnemigosEnMapa( ){
        int contadorArray = 0;
		GameObject aux;
        int sumadorPosicion = 4;
		int auxLinea;
		Vector2 auxResta;

        //Metemos a enemigos en una línea random. Primero calculamos la posición inicial según la línea (vertical o horizontal) y luego veremos si es un enemigo básico normal o uno que use pathfinding.
		for (int i = 0; i < cantidadEnemigos; i++) {
			auxLinea = Random.Range (0, lineas.Count);
            
            if (buscarEnArray(auxLinea))
            {
				sumadorPosicion += 2 * (int)nodeSize;
				auxResta = lineas[auxLinea].getIfHorizontal() ? new Vector2(-32 - sumadorPosicion, nodeSize/2) : new Vector2(-nodeSize/2, 32 + sumadorPosicion);
            }
            else {
                usados[contadorArray] = auxLinea;
                contadorArray++;
				auxResta = lineas[auxLinea].getIfHorizontal() ? new Vector2(-32, nodeSize/2) : new Vector2(-nodeSize/2, 32);
            }


			if (Random.Range (0,100) < chanceEnemNormal)
            {
                aux = (GameObject)Instantiate(enemigo, lineas[auxLinea].getWorldStartPoint() - auxResta, Quaternion.identity);
                aux.GetComponent<movimientoUniEje>().walkHorizontal = lineas[auxLinea].getIfHorizontal();
                aux.GetComponent<movimientoUniEje>().reSetearVelocidad();
            }
            else
            {
                aux = (GameObject)Instantiate(enemigoIA, lineas[auxLinea].getWorldStartPoint() - auxResta, Quaternion.identity);
                aux.GetComponent<AILerp>().target = GameObject.FindGameObjectWithTag("Player").transform;
            }
			
			



		}



	}

    private void generarEnemigosIniciales()
    {
     
        GameObject aux;
       // Genera un enemigo básico por cada línea.
        int auxLinea;
        Vector2 auxResta;
        for (int i = 0; i < lineas.Count; i++)
        {
            auxLinea = i;

           
           
            auxResta = lineas[auxLinea].getIfHorizontal() ? new Vector2(-16, nodeSize / 2) : new Vector2(-nodeSize / 2, 16);
            



            aux = (GameObject)Instantiate(enemigo, lineas[auxLinea].getWorldStartPoint() - auxResta, Quaternion.identity);

            aux.GetComponent<movimientoUniEje>().walkHorizontal = lineas[auxLinea].getIfHorizontal();
            aux.GetComponent<movimientoUniEje>().reSetearVelocidad();



        }



    }

    private bool buscarEnArray(int x)
    {
        bool aux = false;
        for (int i= 0; i < usados.Length; i++)
        {
            if (usados[i] == x)
            {
                aux = true;
                break;
            }

        }

        return aux;


    }
}
