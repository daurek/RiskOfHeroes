﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectRarities : MonoBehaviour
{
    public static ObjectRarities instance;
    public Rarity[] objectRarities;

    public enum rarity
    {
        white,
        //orange,
        //purple,
        blue
        //green
    }

    void Awake()
    {
        

        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);



    }
   
}





[System.Serializable]
public class Rarity
{
    [Range(1, 100)]
    public int porcentage;

    public ObjectRarities.rarity rarities;
}

