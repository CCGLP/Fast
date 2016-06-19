using UnityEngine;
using System.Collections;

public class Generador : MonoBehaviour {
	spawnearEnemigos spaEn;
	RayasGrandesProceduralMap rayas;

    public int dificultadInicial;
	public static int dificultad = 0;
    public GameObject Astar;
	public int aumentoTamano = 10;
	public int aumentoRamas = 20;
	public int aumentoLongRamas = 2;
	public int aumentoEnemigos = 1;
	public int aumentoLineas = 1;
	// Este script llama desde su Awake al generador del mapa y al generador de objetos.
    //El generador de enemigos sucede automáticamente en el start, llamado por Unity.
	void Awake () {
        //Aumento la dificultad, cambiando las variables del mapa y del spawner de Enemigos.

        dificultad += dificultadInicial;
		spaEn = GetComponentInParent<spawnearEnemigos> ();
		rayas = GetComponentInParent<RayasGrandesProceduralMap> ();
		if (dificultad < 5) {
			rayas.lineasGeneradas += dificultad * aumentoLineas;
			spaEn.cantidadEnemigos += dificultad * aumentoEnemigos;
			rayas.worldSizeMapX += dificultad * aumentoTamano;
			rayas.worldSizeMapY += dificultad * aumentoTamano;
			rayas.cantidadRama += dificultad * aumentoRamas;
			rayas.longitudRama += dificultad * aumentoLongRamas;


		} else if (dificultad >= 5) {
			rayas.lineasGeneradas += 5 * aumentoLineas;
			spaEn.cantidadEnemigos += 5 * aumentoEnemigos;
			rayas.worldSizeMapX += 5 * aumentoTamano;
			rayas.worldSizeMapY += 5 * aumentoTamano;
			rayas.cantidadRama += 5 * aumentoRamas;
			rayas.longitudRama += 5* aumentoLongRamas;
			rayas.lineasGeneradas += (dificultad -5)/ 2* aumentoLineas;
			spaEn.cantidadEnemigos += (dificultad -5)/ 2* aumentoEnemigos;
			rayas.worldSizeMapX += (dificultad -5)/ 2 * aumentoTamano;
			rayas.worldSizeMapY += (dificultad -5) / 2 * aumentoTamano;
			rayas.cantidadRama += (dificultad -5) / 2 * aumentoRamas;
			rayas.longitudRama += (dificultad -5) /2 * aumentoLongRamas;

		}
		rayas.onAwake ();
       
        
        Instantiate(Astar);
        AstarPath.active.astarData.gridGraph.Width = rayas.worldSizeMapX;
        AstarPath.active.astarData.gridGraph.Depth = rayas.worldSizeMapY;
		AstarPath.active.astarData.gridGraph.nodeSize = rayas.colisionAGenerar.transform.localScale.x;

        AstarPath.active.astarData.gridGraph.UpdateSizeFromWidthDepth();
        AstarPath.active.Scan();

        this.GetComponentInParent<GenerarObjetos>().onAwakeCall();
       dificultad += 1;

	}
	

}
