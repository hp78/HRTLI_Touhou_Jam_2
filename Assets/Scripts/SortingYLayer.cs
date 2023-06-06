using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
[RequireComponent(typeof(Renderer))]
public class SortingYLayer : MonoBehaviour {

    private const float IsometricRangePerYUnit = 100;
    Renderer rend;



    [Tooltip("Use this to offset the object slightly in front or behind the Target object")]
    public float TargetOffset = 0;

    // Use this for initialization
    void Start () {

        rend = GetComponent<Renderer>();


    }
	
	// Update is called once per frame
	void Update () {

        int realsort = (int)((transform.position.y * IsometricRangePerYUnit) + TargetOffset);
        rend.sortingOrder = -realsort;

    }
}
