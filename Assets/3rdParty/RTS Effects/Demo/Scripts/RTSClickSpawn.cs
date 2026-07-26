using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

namespace RTSFX
{
public class RTSClickSpawn : MonoBehaviour
{

Ray myRay;
RaycastHit hit;
public GameObject clickEffect1;
public GameObject clickEffect2;
public GameObject clickEffect3;
public GameObject clickEffect4;
public GameObject clickEffect5;
public GameObject clickEffect6;

//public float collideOffset = 0.15f;
	 
 void Update ()
 {
     myRay = Camera.main.ScreenPointToRay (Input.mousePosition);
	
	if (!EventSystem.current.IsPointerOverGameObject())
	{
     if (Physics.Raycast (myRay, out hit))
	 {

         if (Input.GetMouseButtonDown (0))
		 {
             Instantiate(clickEffect1, hit.point, Quaternion.identity);
         }
		 if (Input.GetMouseButtonDown (1))
		 {
             Instantiate(clickEffect2, hit.point, Quaternion.identity);
         }
		 if (Input.GetMouseButtonDown (2))
		 {
             Instantiate(clickEffect3, hit.point, Quaternion.identity);
         }
		 if (Input.GetMouseButtonDown (4))
		 {
             Instantiate(clickEffect4, hit.point, Quaternion.identity);
         }
		 if (Input.GetMouseButtonDown (5))
		 {
             Instantiate(clickEffect5, hit.point, Quaternion.identity);
         }
		 if (Input.GetMouseButtonDown (6))
		 {
             Instantiate(clickEffect6, hit.point, Quaternion.identity);
         }
     }
	}
 }
}
}