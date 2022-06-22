using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public GameObject userObject = null;    // 이 웨이포인트를 이용할 오브젝트

    private IWaypointUser user = null;      // userObject가 가지는 IWaypointUser

    private void Awake()
    {
        user = userObject.GetComponent<IWaypointUser>();    // userObject에서 IWaypointUser 찾아오기
    }

    private void Start()
    {
        Waypoint[] ways = new Waypoint[transform.childCount];           // Waypoints의 자식 개수만큼 Waypoint 배열만큼
        for(int i=0; i< transform.childCount; i++)
        {
            ways[i] = transform.GetChild(i).GetComponent<Waypoint>();   // 자식들의 Waypoint 찾아서 배열에 넣음
        }

        // 찾아놓은 waypoint들의 필수 설정 진행
        for(int i=0; i<transform.childCount; i++)
        {
            ways[i].Next = ways[(i + 1) % transform.childCount].transform;  // 이 웨이포인트의 다음 웨이포인트 지정
            ways[i].OnWaypointArrive = user.SetNextWayPoint;                // 이 웨이포인트에 누군가 도착했을 때 실행될 델리게이트에 함수 등
        }

        user.SetNextWayPoint(ways[0].transform);    // userObject가 첫번째 웨이포인트로 이동하게 설정
    }

    public Transform GetNextWaypoint()
    {
        return null;
    }
}
