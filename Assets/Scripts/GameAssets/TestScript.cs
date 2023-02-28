using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAssets;
using UnityEditor;

public class TestScript : MonoBehaviour {
    public GameObject testAddObject;
    public GameObject testReadObject;
    // Start is called before the first frame update
    void Start() {
        Assets.AddAsset("rope", testAddObject);
        //Debug.Log( Assets.GetAsset<GameObject>("rope"));
        testReadObject = Assets.GetAsset<GameObject>("rope");
        //testReadObject = Assets.GetAsset<GameObject>("ro");
       // Debug.Log(Assets.GetAsset<GameObject>("ro"));
        //Sprite wepSprite2 = Assets.GetAsset("WeaponSprite") as Sprite;
        //Sprite wepSprite3 = Assets.GetAsset<Sprite>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
