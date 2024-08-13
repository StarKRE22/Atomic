using UnityEngine;

namespace ReadMe
{
    [CreateAssetMenu(menuName = "ScriptableObject/Readme")]
    public class ReadMe : ScriptableObject
    {
        public string Title = "Welcome to the Atomic Framework";

        public readonly Section[] Sections =
        {
            new()
            {
                TextSubheading = "What is Atomic Framework?",
                TextBody =
                    "\nAtomic Framework is a solution that is designed for developing games in C#. Integrated with Unity. The main concept is reduce OOP towards reactive procedural and functional programming\n\n" +
                    "Since state and behavior rigidly separated from each other then it makes possible to use game mechanics more flexible and reusable\n\n" +
                    "Thus, the developer focuses on functions and interactions instead of object-oriented design. He can assemble game objects as a constructor and test a lot of game mechanics",
                LinkName = "Github repository",
                LinkUrl = "https://github.com/StarKRE22/unity-atomic"
            },

            new()
            {
                TextSubheading = "Add Odin Inspector",
                TextBody =
                    "Note: To use Atomic Framework more effectively and unlock advanced features, we recommend adding the Odin-Inspector plugin to the project",
                LinkName = "Look Odin Inspector at Asset Store",
                LinkUrl = "https://assetstore.unity.com/packages/tools/utilities/odin-inspector-and-serializer-89041"
            },

            new()
            {
                TextSubheading = "Framework modules",
                TextBody = "- <b>Atomic.Elements</b> — library with reactive data structures\n" +
                           "- <b>Atomic.Entities</b> — library for developing game objects\n" +
                           "- <b>Atomic.Context</b> — library for developing game systems\n" +
                           "- <b>Atomic.UI</b> — library for developing UI controllers\n" +
                           "- <b>Atomic.AI</b> — library for developing AI (coming soon)\n" +
                           "- <b>Atomic.Extensions</b> — extra extensions\n" +
                           "\nFor more information you can look at the folder: <i>Assets/Plugins/Atomic</i>\n" +
                           "\nFor advanced using we recommend look at EditMode and PlayMode Tests that covers different cases"
            },
            new()
            {
                TextSubheading = "Game Example",
                TextBody =
                    "The project also has an example of developing a mini-project for two players using an atomic approach. " +
                    "The folder is <i>Assets/GameExample</i>. Note: tutorial made without Odin Inspector"
            },
            new()
            {
                TextSubheading = "Step by Step Tutorial",
                TextBody =
                    "There is also a step-by-step tutorial in the project, which shows the development of a mini-game step by step. " +
                    "Folder: <i>Assets/Tutorial (Walkthrough)</i>. Note: example made without Odin Inspector"
            },
            new()
            {
                TextSubheading = "API Consoles",
                TextBody =
                    "API Consoles allow you to configure various data identifiers and tags of game entities and systems through code generation and Unity. " +
                    "This is a very useful tool, which we recommend that you familiarize yourself with in the Tutorial Walkthrough. "
            },
            new()
            {
                TextBody = "<b>Entity Value Console</b>",
                MenuItemName = "Open Console",
                MenuItemPath = "Window/Atomic/Entities/Show Value Console"
            },
            
            new()
            {
                TextBody = "<b>Entity Tag Console</b>",
                MenuItemName = "Open Console",
                MenuItemPath = "Window/Atomic/Entities/Show Tag Console"
            },
            new()
            {
                TextBody = "<b>Context Value Console</b>",
                MenuItemName = "Open Console",
                MenuItemPath = "Window/Atomic/Context/Show Console"
            },
        };

        public class Section
        {
            public string TextHeading;
            public string TextSubheading;
            public string TextBody;

            public string LinkName;
            public string LinkUrl;

            public string MenuItemName;
            public string MenuItemPath;
        }
    }
}