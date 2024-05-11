using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BossCardButton : MonoBehaviour
{
    [SerializeField] private SpriteRenderer buttonSpriteRenderer;
    [SerializeField] private Sprite activeButtonSprite;
    [SerializeField] private Sprite dormantButtonSprite;
    [SerializeField] private SpriteRenderer buttonMarker;
    [SerializeField] private Canvas canvas;
    [SerializeField] private TMP_Text effectTextArea;
}
