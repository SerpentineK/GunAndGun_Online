using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour
{
    // 半透明の灰色オーバーレイを描画するRenderer
    [SerializeField] private GameObject greyCurtain;

    public int numberOfSelection;
    public SelectableObject[] selectables;
    public List<SelectableObject> selected;
}
