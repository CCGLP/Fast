﻿using UnityEngine;
using System.Collections;

public class Line : MonoBehaviour {

	private Vector2 startPoint;
	private bool horizontal;
	private ProceduralMap map;
	private float nodeSize;

	public Line(Vector2 _startPoint, bool _horizontal, ProceduralMap _mapa, float _nodeSize)
	{
		this.startPoint = _startPoint;
		this.horizontal = _horizontal;
		this.map = _mapa;
		this.nodeSize = _nodeSize;

	}


	public Vector2 getStartPoint ()
	{
		return this.startPoint;

	}

	public bool getIfHorizontal()
	{
		return this.horizontal;
	}

	public Vector2 getWorldStartPoint(){
		Vector2 aux = new Vector2 ();

		aux.x = startPoint.x < (map.worldSizeMapX/ 2) ? (startPoint.x - (map.worldSizeMapX)/ 2 )* nodeSize : (startPoint.x -( map.worldSizeMapX)/2) * nodeSize;
		aux.y = startPoint.y < (map.worldSizeMapY / 2) ? (startPoint.y - (map.worldSizeMapY) / 2)* -nodeSize : -(startPoint.y - (map.worldSizeMapY) / 2) * nodeSize;
		return aux;


	}

}
