using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{

  public void PointColorChage(Material mat)
    {
        //  Debug.Log("0000");
        foreach (Transform thistransform in transform)
        {
            //   Debug.Log("1111");
            var newMaterials = new Material[thistransform.GetComponent<Renderer>().materials.Length];
            for (int i = 0; i < newMaterials.Length; i++)
            {
                //      Debug.Log("2222");
                newMaterials[i] = mat;
            }
            thistransform.GetComponent<Renderer>().materials = newMaterials;
        }
    }
}
