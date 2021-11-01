using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class transitions : MonoBehaviour
{
    private Animator _transitionsAnim;
    float secondsCounter=0;
   float secondsToCount=2F;


    // Start is called before the first frame update
    void Start()
    {
        _transitionsAnim= GetComponent<Animator>();
        StartCoroutine(Wait());

        
      
        
    }

   public void LoadScene(string scene){
       StartCoroutine(TransitionsTo(scene));
   }

   IEnumerator TransitionsTo(string scene){
       _transitionsAnim.SetTrigger("Salida");
       yield return new WaitForSeconds(1);
       SceneManager.LoadScene(scene);
   }

   IEnumerator Wait(){
       yield return new WaitForSeconds(3);
       LoadScene("PrincipalMenu");
   }
}
