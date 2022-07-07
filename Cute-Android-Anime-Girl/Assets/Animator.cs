using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animator : MonoBehaviour
{
    public float SmoothAnimationSpeed;
    bool animationCycleFinished = true;
    SpriteRenderer Renderer;
    private Sprite[] currentAnimation = null;
    public Sprite currentSprite;
    private void Start() {
        Renderer = GetComponent<SpriteRenderer>();
    }
    IEnumerator Animation(Sprite[] sprites)
    {
        currentAnimation = sprites;
        animationCycleFinished = false;
        foreach(Sprite sprite in sprites)
        {
            Renderer.sprite = sprite;
            currentSprite = sprite;
            yield return new WaitForSeconds(SmoothAnimationSpeed);
        }
        animationCycleFinished = true;
        currentAnimation = null;
    }
    public void AnimationSwapper(Sprite[]NewAnimation)
    {
        if(currentAnimation != NewAnimation || currentAnimation == null)
        {
            StopAllCoroutines();
            animationCycleFinished = true;
            StartCoroutine(Animation(NewAnimation));
        }
    }
}
