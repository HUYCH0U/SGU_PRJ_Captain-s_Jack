using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    private Animator anim;
    public static string Empty = "Empty";
    public static string Death = "Death";
    public static string Question = "QuestionMark";
    public static string ExcalamationMark = "ExcalamationMark";
    public static string Hi = "Hi";
    public static string Hello = "Hello";
    public static string Attack = "Attack";
    public static string Bomb = "Bomb";
    private bool isPlaying;
    private Quaternion initialRotation;

    void Start()
    {
        isPlaying = false;
        anim = GetComponent<Animator>();
         initialRotation = transform.rotation;
    }

    public void PlayDialogueOnce(String animName)
    {
        if (!isPlaying)
            StartCoroutine(Play(animName));
    }

    public IEnumerator Play(String animName)
    {
        isPlaying = true;
        anim.Play(animName);
        yield return new WaitForSeconds(1f);
        anim.Play(Empty);
    }

    public void resetDialogue()
    {
        isPlaying = false;
    }
      void Update()
    {
        transform.rotation = initialRotation;
    }
}
