using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCard : MonoBehaviour
{
    //Material[] cardMaterials = new Material[3];
    Material cardFrontMaterial;
    
    // Start is called before the first frame update
    void Start()
    {
        cardFrontMaterial = GetComponent<MeshRenderer>().materials[2];
        //cardFrontMaterial.SetTexture("_CardFront", Resources.Load("B01-056", typeof(Texture)) as Texture);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
