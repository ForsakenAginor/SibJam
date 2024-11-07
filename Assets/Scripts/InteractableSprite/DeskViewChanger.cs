using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class DeskViewChanger : MonoBehaviour
{
    [SerializeField] private UIElement _mark;
    [SerializeField] private UIElement _questionMark;

    [SerializeField] private Sprite _emptyDesk;
    [SerializeField] private Sprite _fullDesk;
    [SerializeField] private Sprite _oneQuestDesk;
    [SerializeField] private Sprite _threeQuestDesk;

    private SpriteRenderer _renderer;
    private int _quests;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    public void Init(int quests)
    {
        _quests = quests >= 0 ? quests : throw new ArgumentOutOfRangeException(nameof(quests));
        ChoseStarterSettings();
    }

    private void ChoseStarterSettings()
    {
        if (_quests == 0)
        {
            _renderer.sprite = _emptyDesk;
            _mark.Disable();
            return;
        }

        if (_quests >= 1 && _quests < 3)
            _renderer.sprite = _oneQuestDesk;
        else if (_quests == 3)
            _renderer.sprite = _threeQuestDesk;
        else
            _renderer.sprite = _fullDesk;

        _mark.Enable();
    }

    public void ChangeDesk()
    {
        _quests--;
        _mark.Disable();

        if (_quests < 0)
            return;

        if (_quests == 0)
            _renderer.sprite = _emptyDesk;
        else if (_quests >= 1 && _quests < 3)
            _renderer.sprite = _oneQuestDesk;
        else if (_quests == 3)
            _renderer.sprite = _threeQuestDesk;
        else
            _renderer.sprite = _fullDesk;

        _questionMark.Enable();
    }
}