using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class LootObject:MonoBehaviour{

    public static LootObject instance;

    [Header("1 active, 2 pasive")]
    [Range(1,2)]
    public int activeOrPasive;
    

    public ObjectRarities.rarity rarity;
    
    private ObjectRarities objectRaritiesClass;

   
}
