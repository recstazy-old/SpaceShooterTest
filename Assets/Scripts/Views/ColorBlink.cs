using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorBlink
{
    public static IEnumerator Blink(SpriteRenderer sprite, Color blinkColor, float blinktime)
    {
        Color spriteColor = sprite.color;

        for (int i = 0; i < 4; i++)
        {
            sprite.color = blinkColor;
            yield return new WaitForSeconds(blinktime);
            sprite.color = spriteColor;
            yield return new WaitForSeconds(blinktime);
        }
    }
}
