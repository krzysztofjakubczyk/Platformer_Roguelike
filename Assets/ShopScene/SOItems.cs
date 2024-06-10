using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Shop/Item")] //dodanie do klikni�cia prawym przyciskiem opcji dodawania SO
public class Items : ScriptableObject
{
    [Header("Item's settings")]
    public string Name; //nazwa przedmiotu
    public string Description; //opis przedmiotu
    public int Cost; //koszt przedmiotu
    public Sprite ImageItem; //obrazek przedmiotu

    [Header("Type of upgrade")]
    public PlayerStatEnum stat;
    public float power; // ile dodaje

}