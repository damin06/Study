using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteCut : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            CutSprite();
    }

    private void CutSprite()
    {
        Texture2D tex = _spriteRenderer.sprite.texture;
        int width = (int)_spriteRenderer.sprite.rect.width;
        int height = (int)_spriteRenderer.sprite.rect.height;

        Texture2D newTex = new Texture2D(width, height);
        newTex.filterMode = FilterMode.Point;

        Rect rect = _spriteRenderer.sprite.rect;
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                Color c = tex.GetPixel(j + (int)rect.x, i + (int)rect.y);
                if (j > i)
                {
                    c.a = 0;
                }
                newTex.SetPixel(j, i, c);
            }
            newTex.Apply();

            Sprite s = Sprite.Create(newTex, new Rect(0, 0, width, height), Vector2.one * 0.5f, 16);

            GameObject obj = new GameObject();
            obj.AddComponent<SpriteRenderer>().sprite = s;
            obj.AddComponent<Rigidbody2D>();
            obj.AddComponent<PolygonCollider2D>();
            obj.transform.position = transform.position + new Vector3(0, 0, 3);
        }
    }
}
