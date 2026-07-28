using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Shaders;

public class StageGenerator : MonoBehaviour
{
    const int StageChipSize = 30;

    public Transform Chractor;
    public GameObject[] StageChips;
    public int StartChipIndex;
    public int PreInstantiate;
    public List<GameObject> GeneratedStageList = new List<GameObject>();

    int _currentchipIndex;

    void Start()
    {
        _currentchipIndex = StartChipIndex - 1;
        UpdateStage(PreInstantiate);
    }

    void Update()
    {
        int charaPositionIndex = (int)(Chractor.position.z / StageChipSize);

        if (charaPositionIndex + PreInstantiate > _currentchipIndex)
        {
            UpdateStage(charaPositionIndex + PreInstantiate);
        }
    }

    void UpdateStage(int toChipIndex)
    {
        if (toChipIndex <= _currentchipIndex) return;

        for (int i = _currentchipIndex + 1; i <= toChipIndex; i++)
        {
            GameObject stageObject = GenerateStage(i);

            GeneratedStageList.Add(stageObject);
        }

        while (GeneratedStageList.Count > PreInstantiate + 2)
        {
            DestroyOldestStage();
        }
        _currentchipIndex = toChipIndex;
    }
    GameObject GenerateStage(int chipIndex)
    {
        int nextStageChip = Random.Range(0, StageChips.Length);
        GameObject stageObject = (GameObject)Instantiate(
            StageChips[nextStageChip],
            new Vector3(0, 0, chipIndex * StageChipSize),
            Quaternion.identity
        );
        return stageObject;
    }

    void DestroyOldestStage()
    {
        GameObject oldStage = GeneratedStageList[0];
        GeneratedStageList.RemoveAt(0);
        Destroy(oldStage);
    }
}
