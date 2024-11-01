using System;
using UnityEngine;

[Serializable]
public class VillageEventSO
{
    [SerializeField] private string _name = "test";
    [SerializeField] private string _description = "dTest";
    [SerializeField] private string _failDescription = "fdTest";
    [SerializeField] private string _completeDescription = "cdTest";
    [SerializeField] private int _deadline = 3;
    [SerializeField] private Sprite _failSprite;
    [SerializeField] private Sprite _completeSprite;

    public string Name => _name;

    public string Description => _description;

    public string FailDescription => _failDescription;

    public string CompleteDescription => _completeDescription;

    public int Deadline => _deadline;

    public Sprite FailSprite => _failSprite;

    public Sprite CompleteSprite => _completeSprite;
}