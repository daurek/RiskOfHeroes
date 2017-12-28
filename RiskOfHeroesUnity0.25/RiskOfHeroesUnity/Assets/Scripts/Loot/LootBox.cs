
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBox : MonoBehaviour
{

    private bool bIsOpened;
    private int nRandom;
    private bool activeOrPasive; // if true active, if false pasive
    private GameObject spawnObject;

    void Start()
    {
       
        bIsOpened = false;
        spawnObject = LoadRandomObject();
    }


    void Update()
    {

        if (gameObject.tag == "LootBoxInactive")
        {
            Instantiate(spawnObject);
            gameObject.SetActive(false);
        }
    }


    private int randomActivePasive()
    {
        nRandom = Random.Range(0, 101);
        if (nRandom <= 69)
        {
            return 2;
        }

        else return 1;
    }


    private GameObject LoadRandomObject()
    {
        int[] porcentages;
        ObjectRarities.rarity[] rarities;
        ObjectRarities objectRaritiesClass;
        List<GameObject> objects = new List<GameObject>();

        objectRaritiesClass = ObjectRarities.instance;
        int size = objectRaritiesClass.objectRarities.Length;

        porcentages = new int[size];
        rarities = new ObjectRarities.rarity[size];

        nRandom = Random.Range(1, 100);

        int currentN = 1;
        bool found = false;

        for (int i = 0; i<size && !found; i++)
        {
            porcentages[i] = objectRaritiesClass.objectRarities[i].porcentage;
            rarities[i] = objectRaritiesClass.objectRarities[i].rarities;

            if (nRandom >= currentN && nRandom <= currentN + porcentages[i])
            {
                objects = takeAllObjectsByRarity(rarities[i]);
                found = true;
            }

            else currentN += porcentages[i];

        }

        print(objects[0] + "PUTA");
        return objects[Random.Range(0, objects.Count-1)];
        
    }


    private List<GameObject> takeAllObjectsByRarity(ObjectRarities.rarity r)
    {
        int pasiveOrActive = randomActivePasive();
        print(r);

        List<GameObject> objectG = new List<GameObject>();

        foreach (GameObject objectX in Resources.LoadAll("Objects", typeof(GameObject)))
        {
            print(objectX.GetComponent<LootObject>().rarity);
            if (objectX.GetComponent<LootObject>().activeOrPasive == pasiveOrActive && objectX.GetComponent<LootObject>().rarity == r)
            {
                objectG.Add(objectX);
                
            }
                
        }


        return objectG;
    }
    
}


