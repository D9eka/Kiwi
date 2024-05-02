using Sections;
using UnityEngine;

public class ControlPanel : MonoBehaviour
{
    private Door _secretDoor;

    private void Start()
    {
        _secretDoor = Section.Instance.SecretDoor;

        SectionManager sectionManager = SectionManager.Instance;
        if (sectionManager.SecretDoorSectionIndex == -1)
            sectionManager.SecretDoorSectionIndex = sectionManager.CurrentSectionIndex;
        else
            gameObject.SetActive(false);
    }

    public void TryOpenSecretDoor()
    {
        if (_secretDoor.IsOpened || !MyGameManager.TryUseKeyCard(1))
            return;
        _secretDoor.Open();
    }
}