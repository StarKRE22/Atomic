using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ETFXSceneManager : MonoBehaviour
{
	public bool GUIHide = false;
	public bool GUIHide2 = false;
	public bool GUIHide3 = false;	
	public bool GUIHide4 = false;
	
    public void LoadScene2DDemo()  {
		SceneManager.LoadScene ("etfx_2ddemo");
	}
	public void LoadSceneCards()  {
		SceneManager.LoadScene ("etfx_cards");
	}
    public void LoadSceneCombat()  {
		SceneManager.LoadScene ("etfx_combat");
	}
	public void LoadSceneDecals()  {
		SceneManager.LoadScene ("etfx_decals");
	}
	public void LoadSceneDecals2()  {
		SceneManager.LoadScene ("etfx_decals2");
	}
	public void LoadSceneEmojis()  {
		SceneManager.LoadScene ("etfx_emojis");
	}
	public void LoadSceneEmojis2()  {
		SceneManager.LoadScene ("etfx_emojis2");
	}
	public void LoadSceneExplosions()  {
		SceneManager.LoadScene ("etfx_explosions");
	}
	public void LoadSceneExplosions2()  {
		SceneManager.LoadScene ("etfx_explosions2");
	}
	public void LoadSceneFire()  {
		SceneManager.LoadScene ("etfx_fire");
	}
	public void LoadSceneFire2()  {
		SceneManager.LoadScene ("etfx_fire2");
	}
	public void LoadSceneFire3()  {
		SceneManager.LoadScene ("etfx_fire3");
	}
	public void LoadSceneFireworks()  {
		SceneManager.LoadScene ("etfx_fireworks");
	}
	public void LoadSceneFlares()  {
		SceneManager.LoadScene ("etfx_flares");
	}
	public void LoadSceneMagic()  {
		SceneManager.LoadScene ("etfx_magic");
	}
	public void LoadSceneMagic2()  {
		SceneManager.LoadScene ("etfx_magic2");
	}
	public void LoadSceneMagic3()  {
		SceneManager.LoadScene ("etfx_magic3");
	}
	public void LoadSceneMainDemo()  {
		SceneManager.LoadScene ("etfx_maindemo");
	}
	public void LoadSceneMissiles()  {
		SceneManager.LoadScene ("etfx_missiles");
	}
	public void LoadScenePortals()  {
		SceneManager.LoadScene ("etfx_portals");
	}
	public void LoadScenePortals2()  {
		SceneManager.LoadScene ("etfx_portals2");
	}
	public void LoadScenePowerups()  {
		SceneManager.LoadScene ("etfx_powerups");
	}
	public void LoadScenePowerups2()  {
		SceneManager.LoadScene ("etfx_powerups2");
	}
	public void LoadSceneSparkles()  {
		SceneManager.LoadScene ("etfx_sparkles");
	}
	public void LoadSceneSwordCombat()  {
		SceneManager.LoadScene ("etfx_swordcombat");
	}
	public void LoadSceneSwordCombat2()  {
		SceneManager.LoadScene ("etfx_swordcombat2");
	}
	public void LoadSceneMoney()  {
		SceneManager.LoadScene ("etfx_money");
	}
	public void LoadSceneHealing()  {
		SceneManager.LoadScene ("etfx_healing");
	}
	public void LoadSceneWind()  {
		SceneManager.LoadScene ("etfx_wind");
	}
	
	void Update ()
	 {
 
     if(Input.GetKeyDown(KeyCode.L))
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
	      if(Input.GetKeyDown(KeyCode.J))
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
		if(Input.GetKeyDown(KeyCode.H))
	 {
         GUIHide3 = !GUIHide3;
     
         if (GUIHide3)
		 {
             GameObject.Find("ParticleSysDisplayCanvas").GetComponent<Canvas> ().enabled = false;
         }
		 else
		 {
             GameObject.Find("ParticleSysDisplayCanvas").GetComponent<Canvas> ().enabled = true;
         }
     }
	 	if(Input.GetKeyDown(KeyCode.K))
	 {
         GUIHide4 = !GUIHide4;
     
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