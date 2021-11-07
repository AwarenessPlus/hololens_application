using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{

    public string scene;
    public void startProcedureScene(){
        
         SceneManager.LoadScene(scene);

   }

}
