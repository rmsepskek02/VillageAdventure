using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    Material mat;

    // Start is called before the first frame update
    void Start()
    {
        //mat.SetColor("_Color", Color.green);
    }

    // Update is called once per frame
    void Update()
    {
        //CheckSpriteName();
    }

    private void CheckSpriteName()
    {
        Debug.Log("update");
        if (this.gameObject.GetComponent<SpriteRenderer>().sprite.name == "MaterSimple_24")
        {
            Debug.Log("wellllll");
            if (Input.GetKey(KeyCode.W))
            {
                Debug.Log("Well");
            }
        }
    }
}
