﻿using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Building : MonoBehaviour
{
    public bool construction;
    public float interpol;
    protected bool builded;
    [SerializeField] SpriteRenderer spriteRend;
    [SerializeField] NavMeshObstacle navObstacle;
    [SerializeField] Collider2D col2d;
    public int buildersNeed;

    public  IEnumerator Construct(float addInterpol)
    {
        yield return new WaitUntil(() => construction);
        col2d.enabled = true;
        BuildingManager.dictoConstructions.Add(gameObject.transform.position, buildersNeed);
        Debug.Log("BuildingPresent1 : " + BuildingManager.dictoConstructions.Count);
        spriteRend.color = Color.clear;     
        while (spriteRend.color.a < 1)
        {
            spriteRend.color = Vector4.Lerp(Color.clear, Color.magenta, interpol);
            interpol += addInterpol * Time.deltaTime;
            Debug.Log("BuildingPresent2 : " + BuildingManager.dictoConstructions.Count);
            navObstacle.enabled = true;
            yield return new WaitForFixedUpdate();
        }
        builded = true;
        //BuildingManager.listConstructions.Remove(gameObject.transform.position);
        BuildingManager.dictoConstructions.Remove(gameObject.transform.position);
    }
}
