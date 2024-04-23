using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerAnimationManager : MonoBehaviour
{
    public GameObject GameManagerObject;
    public Sprite EndSprite;
    //public string TimerAnimName;

    private Animator animator;
    private SpriteRenderer sRenderer;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        sRenderer = GetComponent<SpriteRenderer>();
        gameManager = GameManagerObject.GetComponent<GameManager>();

        gameManager.OnGrade += StopAnim;
    }

    void StopAnim()
    {
        animator.enabled = false;
        sRenderer.sprite = EndSprite;
        gameManager.OnGrade -= StopAnim;
    }
}
