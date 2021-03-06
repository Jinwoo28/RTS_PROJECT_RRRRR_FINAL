using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preview : MonoBehaviour
{
    private List<Collider> colliderList = new List<Collider>(); //충돌검사

    [SerializeField]
    private int layerGround = 14; //그라운드 레이어
    private const int IGNORE_LAYER = 2; //무시할 레이어

    [SerializeField] private Material green;    // 충돌이 없을 때 보여줄 초록색 프리뷰
    [SerializeField] private Material red;  // 충돌 물체가 있을 때 보여줄 빨간색 프리뷰

    private float LB;
    private float LU;
    private float RB;
    private float RU;

    private float sizeX = 0;
    private float sizeZ = 0;

    private float cellsize = 00;

    private bool isBuildable = false;



    void Start()
    {
        sizeX = this.transform.localScale.x;
        sizeZ = this.transform.localScale.z;

        cellsize = Grid.gridinstance.cellsize;
    }

    void Update()
    {
        ChangeColor();

        //int X;
        //int Z;

        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //RaycastHit hit;
        //int layerMask = (1 << 19) | (1 << 20);
        //Debug.Log(">???!!");
        //if (Physics.Raycast(ray, out hit, Mathf.Infinity,19))
        //{
        //    Vector3 PreveiewPos = hit.point;
        //    PreveiewPos.y += 0.5f;
        //    this.transform.position = PreveiewPos;
        //    Debug.Log(">???");
        //};

        // Debug.Log(CanBuildable());
    }

    private bool isSea()
    {

        int layerMask = 1 << LayerMask.NameToLayer("Sea");
        if (Physics.Raycast(this.transform.position, Vector3.down, 100f, layerMask))
        {
            return false;
        }
        else return true;
    }

    private void ChangeColor()
    {
        Debug.Log("1 : " + colliderList.Count); // 0
        Debug.Log("2 : " + CanBuildable()); //true
        Debug.Log("3 : " + isSea());    //false         green


        if (colliderList.Count > 0 || !CanBuildable() || isSea()) //충돌 물체가 하나 이상일 때 
        {
            //Debug.Log("업데이트");
            SetColor(red);
            isBuildable = false;

        }

        else
        {
            SetColor(green); /*Debug.Log("업데이트22222")*/
            isBuildable = true;
        }
    }

    private bool CanBuildable()
    {
        Vector3 LBpos = new Vector3(this.transform.position.x - sizeX / 2, this.transform.position.y, this.transform.position.z - sizeZ / 2);
        Vector3 LUpos = new Vector3(this.transform.position.x - sizeX / 2, this.transform.position.y, this.transform.position.z + sizeZ / 2);
        Vector3 RBpos = new Vector3(this.transform.position.x + sizeX / 2, this.transform.position.y, this.transform.position.z - sizeZ / 2);
        Vector3 RUpos = new Vector3(this.transform.position.x + sizeX / 2, this.transform.position.y, this.transform.position.z + sizeZ / 2);

        LB = Grid.gridinstance.NodePoint(LBpos, cellsize).YDepthLB;
        LU = Grid.gridinstance.NodePoint(LUpos, cellsize).YDepthLU;
        RB = Grid.gridinstance.NodePoint(RBpos, cellsize).YDepthRB;
        RU = Grid.gridinstance.NodePoint(RUpos, cellsize).YDepthRU;


        float X1 = Mathf.Abs(LB - LU);
        float X2 = Mathf.Abs(LU - RU);
        float X3 = Mathf.Abs(RU - RB);
        float X4 = Mathf.Abs(RB - LB);

        return X1 < 0.3f && X2 < 0.3f && X3 < 0.3f && X4 < 0.3f;

    }

    private void SetColor(Material mat)
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != layerGround && other.gameObject.layer != IGNORE_LAYER)
        {
            colliderList.Add(other);
            //  Debug.Log("충돌");
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer != layerGround && other.gameObject.layer != IGNORE_LAYER)
        {
            colliderList.Remove(other);
            //  Debug.Log("안충돌");
        }
    }

    public bool GetBuildable()
    {
        return isBuildable;
    }
}