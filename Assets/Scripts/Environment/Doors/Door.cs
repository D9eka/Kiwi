using Components.Interactables;
using Sections;
using System;
using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private RectTransform _interactableUI;

    private SpriteRenderer _rendered;
    private BoxCollider2D _collider;

    protected DoorType _type;

    private const float OPEN_HEIGHT = 2f;
    private const float OPEN_TIME = 1f;

    public enum DoorType
    {
        Start,
        End,
        Secret
    }

    public bool IsOpened { get; private set; }

    private void Awake()
    {
        _rendered = GetComponent<SpriteRenderer>();
        _collider = GetComponent<BoxCollider2D>();
        IsOpened = false;
        StartCoroutine(Subscribe());
    }

    protected virtual IEnumerator Subscribe()
    {
        yield return new WaitUntil(() => Section.Instance != null);

        Section.Instance.OnStartSpawnWaves += Section_OnStartSpawnWaves;
        Section.Instance.OnEndSpawnWaves += Section_OnEndSpawnWaves;
    }

    public virtual void Initialize(DoorType doorType)
    {
        _type = doorType;
        _interactableUI.anchoredPosition = new Vector2(_type == DoorType.Start ? 2 : -2, _interactableUI.anchoredPosition.y);
    }

    protected virtual void Section_OnStartSpawnWaves(object sender, EventArgs e)
    {
        if (_type == DoorType.Secret)
            return;
        Close();
    }

    protected virtual void Section_OnEndSpawnWaves(object sender, EventArgs e)
    {
        if (_type == DoorType.Secret || (_type == DoorType.Start && SectionManager.Instance.CurrentSectionIndex <= 1))
            return;
        Open();
    }

    public virtual void Open()
    {
        if (IsOpened)
            return;

        GetComponent<InteractableComponent>().Activate();
        StartCoroutine(LerpDoor(Vector2.up, true));
    }

    public virtual void Close()
    {
        if (!IsOpened)
            return;

        GetComponent<InteractableComponent>().Deactivate();
        StartCoroutine(LerpDoor(Vector2.down, false));
    }

    private IEnumerator LerpDoor(Vector2 direction, bool doorState)
    {
        if (_collider == null)
            yield return new WaitUntil(() => _collider != null);
        _collider.isTrigger = direction.y == 1;
        Vector3 initialPosition = transform.position;
        float initialHeight = _rendered.size.y;
        float initialOffset = _collider.offset.y;

        Vector3 endPosition = new Vector3(initialPosition.x, initialPosition.y + OPEN_HEIGHT / 2f * direction.y, initialPosition.z);
        float endHeight = initialHeight + OPEN_HEIGHT * direction.y * -1f;
        float endOffset = initialOffset + OPEN_HEIGHT / 2f * direction.y * -1f;
        float startTime = Time.time;

        while (transform.position != endPosition)
        {
            float elapsedTime = (Time.time - startTime) / OPEN_TIME;
            transform.position = Vector3.Lerp(initialPosition, endPosition, elapsedTime);
            _rendered.size = new Vector2(_collider.size.x, Mathf.Lerp(initialHeight, endHeight, elapsedTime));
            _collider.offset = new Vector2(_collider.offset.x, Mathf.Lerp(initialOffset, endOffset, elapsedTime));
            yield return new WaitForFixedUpdate();
        }
        IsOpened = doorState;
    }

    public void TryEnter()
    {
        if (!IsOpened)
            return;
        Enter();
    }

    protected virtual void Enter()
    {
        SectionManager sectionManager = SectionManager.Instance;
        switch (_type)
        {
            case DoorType.Start:
                sectionManager.EnterPreviousSection();
                break;
            case DoorType.End:
                sectionManager.EnterNextSection();
                break;
            case DoorType.Secret:
                sectionManager.EnterSecretSection();
                break;
            default:
                throw new NotImplementedException();
        }
    }
}