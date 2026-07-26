using UnityEngine;
using UnityEngine.SceneManagement;

namespace RTSFX {

public class RTSSceneSelect : MonoBehaviour
{
	public bool GUIHide = false;
	public bool GUIHide2 = false;
	public bool GUIHide3 = false;
	
    public void LoadRTSDemo01()
    {
        SceneManager.LoadScene("RTSDemo01");
    }
    public void LoadRTSDemo02()
    {
        SceneManager.LoadScene("RTSDemo02");
    }
	public void LoadRTSDemo03()
    {
        SceneManager.LoadScene("RTSDemo03");
    }
	public void LoadRTSDemo04()
    {
        SceneManager.LoadScene("RTSDemo04");
    }
	public void LoadRTSDemo05()
    {
        SceneManager.LoadScene("RTSDemo05");
    }
	public void LoadRTSDemo06()
    {
        SceneManager.LoadScene("RTSDemo06");
    }
	public void LoadRTSDemo07()
    {
        SceneManager.LoadScene("RTSDemo07");
    }

	void Update ()
	 {
 
     if(Input.GetKeyDown(KeyCode.J))
	 {
         GUIHide = !GUIHide;
     
         if (GUIHide)
		 {
             GameObject.Find("CanvasSceneSelect").GetComponent<Canvas> ().enabled = false;
         }
		 else
		 {
             GameObject.Find("CanvasSceneSelect").GetComponent<Canvas> ().enabled = true;
         }
     }
	      if(Input.GetKeyDown(KeyCode.K))
	 {
         GUIHide2 = !GUIHide2;
     
         if (GUIHide2)
		 {
             GameObject.Find("Canvas").GetComponent<Canvas> ().enabled = false;
         }
		 else
		 {
             GameObject.Find("Canvas").GetComponent<Canvas> ().enabled = true;
         }
     }
		if(Input.GetKeyDown(KeyCode.L))
	 {
         GUIHide3 = !GUIHide3;
     
         if (GUIHide3)
		 {
             GameObject.Find("CanvasTips").GetComponent<Canvas> ().enabled = false;
         }
		 else
		 {
             GameObject.Find("CanvasTips").GetComponent<Canvas> ().enabled = true;
         }
     }
}
}
}