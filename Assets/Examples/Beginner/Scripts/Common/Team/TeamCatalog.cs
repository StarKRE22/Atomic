using UnityEngine;

namespace BeginnerGame
{
    [CreateAssetMenu(fileName = "TeamCatalog", menuName = "SampleGame/TeamCatalog")]
    public sealed class TeamCatalog : ScriptableObject
    {
        [SerializeField]
        public TeamInfo[] Teams;
    }
}