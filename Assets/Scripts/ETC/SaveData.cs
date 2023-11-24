using System.Collections;
using System.Collections.Generic;
using System;
using VillageAdventure;
using UnityEngine;

[System.Serializable]

public class SaveData
{
    // InGameManager 재화 Time Score 등
    public float playerHP { get; set; } = 100f;
    public bool isDead = false;
    public float hour = 0;
    public float min = 0;
    public float time = 0;
    public int mine = 0;
    public int tree = 0;
    public int fish = 0;
    public int food = 0;
    public int score = 0;

    // 각 재화 holder들 활성화되어있는 num
    public List<int> mineNum = new List<int>();
    public List<int> treeNum = new List<int>();

    // Player Position
    public Vector3 transform;
    // 현재 Scene 
    public VillageAdventure.Enum.SceneType currentScene = new VillageAdventure.Enum.SceneType();

    // 모든 오브젝트 위치, 체력, 부모오브젝트
    // 몬스터, 워리어, 건설오브젝트, 플레이어
    // hp - int
    // transform - vector3
    // sd nickName - object Tag string
    // parent object - FieldObject / HomeObject / SilmeNormal / Warrior string
    public List<HomeObjectList> homeObjectList = new List<HomeObjectList>();
    public List<FieldObjectList> fieldObjectList = new List<FieldObjectList>();
    public List<MonsterList> monsterList = new List<MonsterList>();
    public List<WarriorList> warriorList = new List<WarriorList>();

    public float lastMonsterSpawnTime;
}

[System.Serializable]
public class HomeObjectList
{
    public float hp;
    public Vector3 transform;
    public string nickName;
    public string parent;
}
[System.Serializable]
public class FieldObjectList
{
    public float hp;
    public Vector3 transform;
    public string nickName;
    public string parent;
}
[System.Serializable]
public class MonsterList
{
    public float hp;
    public Vector3 transform;
    public string nickName;
    public string parent;
}
[System.Serializable]
public class WarriorList
{
    public float hp;
    public Vector3 transform;
    public string nickName;
    public string parent;
    public int index;
}