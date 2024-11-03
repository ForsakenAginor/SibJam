using System;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class DeskSpriteChanger : MonoBehaviour
{
    [SerializeField] private UIElement _mark;

    [SerializeField] private Sprite _emptyDesk;
    [SerializeField] private Sprite _fullDesk;
    [SerializeField] private Sprite _oneQuestDesk;
    [SerializeField] private Sprite _threeQuestDesk;

    [SerializeField] private InteractableSprite _interactableSprite;

    private SpriteRenderer _renderer;
    private Desk _desk;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _interactableSprite.Pressed += OnContainChanged;
    }

    public void Init(Desk table)
    {
        _desk = table != null ? table : throw new ArgumentNullException(nameof(table));
        OnContainChanged();
        _desk.QuestPlaced += OnContainChanged;
        _desk.QuestRemoved += OnContainChanged;
    }

    private void OnContainChanged(Quest _)
    {
        OnContainChanged();
    }

    private void OnContainChanged()
    {
        if(_desk.Quests.Count() == 0)
        {
            _renderer.sprite = _emptyDesk;
            _mark.Disable();
        }
        else if(_desk.Quests.Count() == 1)
        {
            _renderer.sprite = _oneQuestDesk;
            _mark.Enable();
        }
        else if (_desk.Quests.Count() == 3)
        {
            _renderer.sprite = _threeQuestDesk;
            _mark.Enable();
        }
        else if (_desk.Quests.Count() > 3)
        {
            _renderer.sprite = _fullDesk;
            _mark.Enable();
        }
    }
}
