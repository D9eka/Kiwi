using Sections;
using System;
using System.Collections;
using UnityEngine;

namespace Environment.Doors
{
    public class TutorialDoor : Door
    {
        [SerializeField] private bool _openOnAwake = true;

        public override void Initialize(DoorType doorType)
        {
            base.Initialize(doorType);
            if (_type == DoorType.End && _openOnAwake)
                Open();
        }

        protected override IEnumerator Subscribe()
        {
            yield return new WaitUntil(() => SectionTutorial.Instance != null);

            SectionTutorial.Instance.OnStartSpawnWaves += Section_OnStartSpawnWaves;
            SectionTutorial.Instance.OnEndSpawnWaves += Section_OnEndSpawnWaves;
        }

        protected override void Section_OnEndSpawnWaves(object sender, EventArgs e)
        {
            if (_type != DoorType.End)
                return;
            Open();
        }

        protected override void Enter()
        {
            if (_type == DoorType.End)
                SectionTutorial.Instance.EnterNextSection();
        }
    }
}
