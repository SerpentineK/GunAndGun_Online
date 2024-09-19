using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardpoolDatabase : ScriptableObject
{
    [SerializeField] private string cardpoolName;
    [SerializeField] private string cardpoolNameENG;
    [SerializeField] private Sprite cardpoolLogo;
    [SerializeField] private GunnerDatabase gunnerDatabase;
    [SerializeField] private GunsDatabase gunsDatabase;
    [SerializeField] private BossData[] bosses;

    public string CardpoolName { get { return  cardpoolName; } }
    public string CardpoolNameENG { get { return cardpoolNameENG; } }
    public Sprite Logo { get { return cardpoolLogo; } }
    public GunnerDatabase GunnerDatabase { get { return gunnerDatabase; } }
    public GunsDatabase GunsDatabase { get { return gunsDatabase; } }
    public BossData[] Bosses { get {  return bosses; } }
}
