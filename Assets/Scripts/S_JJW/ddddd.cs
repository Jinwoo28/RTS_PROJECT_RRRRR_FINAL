using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ddddd : MonoBehaviour
{
    [SerializeField] private GameObject Mouse = null;
    [SerializeField] private Texture2D cursorImg = null;

    void Start()
    {
        Mouse.SetActive(false);
        Mouse.transform.localScale *= 15;

        Cursor.SetCursor(cursorImg, Vector2.zero, CursorMode.ForceSoftware);
    }



    // Update is called once per frame
    void Update()
    {
        MousePoint();
    }

    public void MousePoint()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {

            if (Input.GetMouseButtonDown(1))
            {
                //GameObject GO =  Instantiate(Mouse, hit.point, Quaternion.identity);
                // GO.transform.localScale *= 20;
                Mouse.SetActive(false);
                Mouse.transform.position = hit.point;
                Mouse.SetActive(true);

            }
        }
    }


}
