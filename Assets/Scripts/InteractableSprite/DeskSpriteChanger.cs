using System;
using System.Collections;
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

    [SerializeField] private UIElement _questionMark;

    private SpriteRenderer _renderer;
    private Desk _desk;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();

        if (_interactableSprite != null)
            _interactableSprite.Pressed += OnContainChanged;
    }

    public void Init(Desk table)
    {
        _desk = table != null ? table : throw new ArgumentNullException(nameof(table));
        OnContainChanged();
        _desk.QuestPlaced += OnContainChanged;
        _desk.QuestRemoved += OnContainChanged;
    }

    public void Init(int quests)
    {
        OnContainChanged(quests, true);
    }

    private void OnContainChanged(Quest _)
    {
        OnContainChanged();
    }

    private void OnContainChanged()
    {
        if (_desk.Quests.Count() == 0)
        {
            _renderer.sprite = _emptyDesk;
            _mark.Disable();
        }
        else if (_desk.Quests.Count() >= 1 && _desk.Quests.Count() < 3)
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

    private void OnContainChanged(int quests, bool isFirstCall)
    {
        if (quests == 0 && isFirstCall == true)
        {
            _renderer.sprite = _emptyDesk;
            _mark.Disable();
        }
        else if (quests == 0 && isFirstCall == false)
        {
            _renderer.sprite = _emptyDesk;
            _mark.Disable();
            _questionMark.Enable();
        }
        else if (quests >= 1 && quests < 3)
        {
            _renderer.sprite = _oneQuestDesk;
            _mark.Enable();

            if (isFirstCall)
            {
                StartCoroutine(ChangeDesk(quests - 1));
            }
            else
            {
                _mark.Disable();
                _questionMark.Enable();
            }
        }
        else if (quests == 3)
        {
            _renderer.sprite = _threeQuestDesk;
            _mark.Enable();

            if (isFirstCall)
            {
                StartCoroutine(ChangeDesk(quests - 1));
            }
            else
            {
                _mark.Disable();
                _questionMark.Enable();
            }
        }
        else if (quests > 3)
        {
            _renderer.sprite = _fullDesk;
            _mark.Enable();

            if (isFirstCall)
            {
                StartCoroutine(ChangeDesk(quests - 1));
            }
            else
            {
                _mark.Disable();
                _questionMark.Enable();
            }
        }
    }

    private IEnumerator ChangeDesk(int quests)
    {
        WaitForSeconds delay = new WaitForSeconds(5f);
        yield return delay;
        OnContainChanged(quests, false);
    }
}
