using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    //건물이 날고 있는지 아닌지 판단할 변수 선언
    private bool isFlyBuilding = false;

    //건물의 scale값을 저장할 변수 선언
    private float sizeX = 0;
    private float sizeZ = 0;

    private float cellsize = 0f;


    private void Start()
    {

        cellsize = Grid.gridinstance.cellsize;
        sizeX = this.transform.localScale.x;
        sizeZ = this.transform.localScale.z;


        //StartCoroutine("SetBuildingObs");
        SetObstacle(isFlyBuilding);
        Debug.Log(sizeX + " X size");
        Debug.Log(sizeZ + " Z size");
    }



    private void SetObstacle(bool isFlying)
    {

        Vector3 thisPos = this.transform.position;



        //건물의 크기만큼 장애물로 인식될 노드의 갯수 설정
        int Xpos = (int)(sizeX / cellsize);
        int Zpos = (int)(sizeZ / cellsize);

        for (int i = 0; i < Xpos; i++)
        {
            for (int j = 0; j < Zpos; j++)
            {
                Vector3 ObsPos = new Vector3((thisPos.x + i * cellsize), 0, (thisPos.z + j * cellsize));
                Grid.gridinstance.NodePoint(ObsPos, cellsize).walkable = isFlying;
                //   Debug.Log(Grid.gridinstance.NodePoint(ObsPos, cellsize).gridX + " : "+ Grid.gridinstance.NodePoint(ObsPos, cellsize).gridY);
            }
        }
    }

    private void Update()
    {
        //int Xpos = (int)(this.transform.localScale.x / cellsize);
        //int Zpos = (int)(this.transform.localScale.z / cellsize);

        //for (int i = 0; i < Xpos; i++)
        //{
        //    for (int j = 0; j < Zpos; j++)
        //    {
        //        //Grid.gridinstance.NodePoint(new Vector3(thisPos.x + i, 0, thisPos.z + j), cellsize);
        //        Debug.DrawLine(new Vector3(this.transform.position.x + i/2, 0, this.transform.position.z+j/2), new Vector3(this.transform.position.x+i/2, 10, this.transform.position.z + j/2), Color.blue);

        //    }

        //}
    }



    private void SetBuildingObs()
    {
        int X = (int)this.transform.localScale.x;
        int Z = (int)this.transform.localScale.z;


        for (int i = 0; i < X / cellsize; i++)
        {
            for (int j = 0; j < Z / cellsize; j++)
            {
                Vector3 BuildingPos = new Vector3(this.transform.position.x - (X / 2) + i, this.transform.position.y, this.transform.position.z - (Z / 2) + j);
                Node ObsBuildings = Grid.gridinstance.NodePoint(BuildingPos, cellsize);
                ObsBuildings.walkable = false;
            }
        }
    }

    IEnumerator SetObstacleNode()
    {
        //int Xscale = (int)(this.transform.position.x + this.transform.localScale.x);
        //int Zscale = (int)(this.transform.position.z + this.transform.localScale.z);

        int XPos = (int)this.transform.position.x;
        int ZPos = (int)this.transform.position.z;
        yield return null;
        int X = (int)this.transform.localScale.x;
        int Y = (int)this.transform.localScale.z;

        // 맵 사이즈 안에서 건물을 지을 때와 사이즈에 끝에 걸려서 지을 때를 나누어야 한다.
        int ObstacleRangeX = 0;
        int ObstacleRangeZ = 0;

        if (XPos > 0) ObstacleRangeX = XPos - 1;
        else ObstacleRangeX = XPos;

        if (ZPos > 0) ObstacleRangeZ = XPos - 1;
        else ObstacleRangeZ = ZPos;




        for (int i = -1; i <= X; i++) // 이동 불가 지역은 건물보다 조금 더 커야 한다.
        {
            for (int j = -1; j <= Y; j++)
            {
                Grid.gridinstance.ObstacleNode(XPos + i, ZPos + j);

            }
        }

    }
}
