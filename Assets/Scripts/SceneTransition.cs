using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : Interactable
{
    public string targetScene;
    public override void Interact()
    {
        // start to fade out
        //GameObject.FindGameObjectWithTag("Crossfade").GetComponent<Animator>().SetTrigger("Out");

        // load the next scene
        SceneManager.LoadSceneAsync(targetScene, LoadSceneMode.Single);
    }
}
