using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName ="Shop/Item")] //dodanie do klikniêcia prawym przyciskiem opcji dodawania SO
public class Items : ScriptableObject
{
    public string Name; //nazwa przedmiotu
    public string Description; //opis przedmiotu
    public int Cost; //koszt przedmiotu
    public Sprite ImageItem; //obrazek przedmiotu
    public enum Type { Default, Potion, UpgradeSpell, UpgradePlayersStats, Herbs, Armor } //utworzony wlasny typ do wyboru jakiego typu jest to przedmiot
                                                                     //(eliksir/ulepszenie/zio³a(ulepsze statysyk gracza(AS/AD))/Zbroja)
    public Type type = Type.Default; //ustawienie domyœlnego typu na podstawowy

}
