using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisonCheck : MonoBehaviour
{

    [SerializeField] AudioClip finishAudio;
    [SerializeField] AudioClip crash;
    AudioSource audioSource;

    // This makes it so when we collide with an object, sound will not continually play after the first collision.
    bool isTransitioning = false;

    void Start() 
    {
        audioSource = GetComponent<AudioSource>();
    }


    void OnCollisionEnter(Collision other)
    //"other" means it references what it's colliding with, or what it is accessing (in this instance, it is a tag).

    {
        if (isTransitioning)
        {
            return;
        }
        switch (other.gameObject.tag)
        {
            case "Friendly":
            Debug.Log("You hit something nice!");
            break;
            //break means it's the end of this case, since the case has been satisfied.

            case "Finish":
            Debug.Log("You made it to the finish line, great!");
            NextLevelSequence();
            break;

            //default, in this instance, will be used to respawn the player (anytime the player collides with a tag NOT being used above, the player will respawn)
            default:
            Debug.Log("You died!");
            StartCrashSequence();
            //We are accessing the StartCrashSequence method BEFORE reload level, so that we can disable movement (and audio) on our player.
            break;
        }
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        GetComponent<Movement>().enabled = false;
        audioSource.PlayOneShot(crash);
        //Invoke allows us to add a delay to a method, in seconds (this instance, it's 1 second, using a float).
        Invoke ("ReloadLevel", 2f);
    }



    void NextLevelSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        GetComponent<Movement>().enabled = false;
        audioSource.PlayOneShot(finishAudio);
        //For Invoke, we are calling the "LoadNextLevel" method below, but with a slight delay.
        Invoke ("LoadNextLevel", 1f);
    }




    void LoadNextLevel()
    {
        //For the first two lines, we are going to load the next level by adding an int of 1 to the current scene index.
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
         
        //For the next two lines, we are checking the scene count in build settings (in unity)...
        //then, if our next scene is greater than the number of scenes in build settings, we're reloading to the first scene (in this case, that is scene 0).
        
        //== symbol means the same as what you are comparing it to.
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
    void ReloadLevel()
    {   
        //How this works below: we're getting the CURRENT Scene "Index" from Unity (in this case, index is an int value)
        //...then accessing the SceneManager from UnityEngine.SceneManagement;...
        //...then, getting the active scene from the build index, THEN reloading the CURRENT scene (thus respawning the player to the SAME "level" they were on).
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}