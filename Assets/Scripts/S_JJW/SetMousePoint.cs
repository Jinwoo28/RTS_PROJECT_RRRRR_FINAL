using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SetMousePoint : MonoBehaviour
{
    [SerializeField] private GameObject NomalMousePoint = null;
    [SerializeField] private GameObject AtkMousePoint = null;


    [SerializeField] private Texture2D NcursorImg = null;
    [SerializeField] private Texture2D AcursorImg = null;
    [SerializeField] private Texture2D HcursorImg = null;




    void Start()
    {
        NomalMousePoint.SetActive(false);
        NomalMousePoint.transform.localScale *= 15;
        Cursor.SetCursor(NcursorImg, new Vector2(13.5f,10), CursorMode.ForceSoftware);
   
    }



    // Update is called once per frame
    void Update()
    {
        MousePoint_();

        //mouseImg.GetComponent<RectTransfrom>().anchoredPoistion = Input.mousePosition + Screen.width * 0.5f
    }

    public void MousePoint_()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {

            if (Input.GetMouseButtonDown(1))
            {

                NomalMousePoint.SetActive(false);
                NomalMousePoint.transform.position = hit.point;
                NomalMousePoint.SetActive(true);

            }
        }
    }




}
