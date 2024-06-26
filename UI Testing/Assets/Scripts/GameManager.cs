using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public GameObject MainCamera;
    public GameObject TimerTextObject;
    public GameObject TempTilemap;
    public GameObject StarPrefab;

    [Space(20)]
    public GameObject UI;
    public GameObject Transition;
    public GameObject EndButtons;

    [Space(20)]
    public float TargetTime = 60.0f;

    public int SpaceBetweenStars = 3;

    [Space(20)]
    public string UIFadeOutAnim;
    public string DimGameplayAnim;
    public string SceneTransitionInAnim;

    //Determines if the game is in "play" or "grade" mode. Grading disables all player interactability.
    public static bool GridIsActive;

    //Contain's Phil's positive and negative furniture tags within a Dictionary.
    private PhilTagsClass philTagsClass = new PhilTagsClass();

    private bool TimerIsOn = true;

    private float Score = 0;

    [Space(20)]
    public float MaxScore = 35;
    private float MinScore;

    public delegate void GMEvent();
    public GMEvent OnGrade;

    #region Unity Methods

    void Start()
    {
        MinScore = -MaxScore;
        StartCoroutine(SceneIntro());
    }

    void Update()
    {
        if (TimerIsOn)
        {
            Timer();
        }

        if(Input.GetKeyDown(KeyCode.C))
        {
            //Resets the score and each current value of Tags.
            Score = 0;
            Debug.Log("Current Score is " + Score);
            
            List<string> KeysToUpdate = new List<string>(philTagsClass.Tags.Keys);
            foreach(string tag in KeysToUpdate)
            {
                philTagsClass.Tags[tag.ToLower()] = philTagsClass.TagsMaxValue[tag.ToLower()];
            }
        }
    }

    #endregion

    #region Timer Methods

    void Timer()
    {
        if (TargetTime > 0.0f)
        {
            TargetTime -= Time.deltaTime;

            int TargetTimeInt = (int)TargetTime;

            TextMeshProUGUI TimerText = TimerTextObject.GetComponent<TextMeshProUGUI>();
            TimerText.text = TargetTimeInt.ToString();
        }
        else
        {
            Grade();
        }
    }

    #endregion

    #region Grading Functionality

    public void Grade()
    {
        //Stops the timer and all functionality.
        TimerIsOn = false;
        GridIsActive = false;
        Destroy(TempTilemap);

        //Unzooms the camera.
        CameraFunctionality cameraFunctionality = MainCamera.GetComponent<CameraFunctionality>();
        if(cameraFunctionality.GetCurrentCameraSize() != cameraFunctionality.OriginalSize)
        {
            cameraFunctionality.ManualCameraZoom(false);
        }

        //Removes all gameplay UI.
        GetAnimation(UI, UIFadeOutAnim);

        //Dims the background.
        Transition.SetActive(true);
        GetAnimation(Transition, DimGameplayAnim);

        //Prevents the player's score from exceeding the maximum score.
        if(Score > MaxScore)
        {
            Score = MaxScore;
        }

        //Generates stars based on player's performance.
        float StarCount = 0;
        if(Score != 0)
        {
            StarCount = (Score + MaxScore) / ((MaxScore + Mathf.Abs(MinScore)) / 3);
        }
        StartCoroutine(DrawStars(StarCount));

        //Alerts subscribers of method.
        OnGrade?.Invoke();
    }

    public void ChangeScore(string tag)
    {
        //Checks the amount of points/negative points a furniture's tag gives the player.
        if (philTagsClass.Tags.ContainsKey(tag.ToLower()))
        {
            Score += philTagsClass.Tags[tag.ToLower()];
            if(philTagsClass.Tags[tag.ToLower()] >= 0)
            {
                philTagsClass.Tags[tag.ToLower()] -= 2;
            }
        }
        else
        {
            Debug.Log("Tag not found in GameManager.ChangeScore()");
        }

        Debug.Log("Current Score is " + Score);
    }

    #endregion

    #region Button Functionality
    
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    
    #endregion

    #region Animations

    void GetAnimation(GameObject obj, string animName)
    {
        obj.GetComponent<Animator>().Play(animName);
    }
    
    IEnumerator SceneIntro()
    {
        Transition.SetActive(true);
        GetAnimation(Transition, SceneTransitionInAnim);
        yield return new WaitForSeconds(1);
        Transition.SetActive(false);
        GridIsActive = true;
        TimerIsOn = true;
    }

    IEnumerator DrawStars(float starCount)
    {
        yield return new WaitForSeconds(1);
        for (int i = 0; i < (int)starCount; i++)
        {
            Instantiate(StarPrefab, new Vector3(-SpaceBetweenStars + (i * SpaceBetweenStars), 0, 0), Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
        }
        EndButtons.SetActive(true);
    }
    #endregion
}
