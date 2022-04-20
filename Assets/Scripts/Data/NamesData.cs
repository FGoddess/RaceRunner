using UnityEngine;

[CreateAssetMenu(fileName = "Names Data", menuName = "Create Names Data")]
public class NamesData : ScriptableObject
{
    [SerializeField] private string[] _names = { "Blabbermouth", "Hercules", "Konversation", "Lollipop", "Hey", "Bigmouth", "Galore", "Jovial", "Supersmart", "Bleep Bloop", "The Monkey", "Volter", "Fintech", "SuchBot", "Neobot", "Funny Guy", "Dimwit", "Okay Sir", "Cybernetic", "Digital Dave", "Mr. Robot", "Pepper Bot", "Baxi", "Tutorial", "CloudBot", "Cometbot", "Comet Bot", "Albus", "Anthony", "Brooke", "Bumper", "Chandler", "Charlotte", "Smart One", "Abracadabra" };

    public string[] Names => _names;
}
