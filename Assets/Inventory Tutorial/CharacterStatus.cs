using UnityEngine;

[CreateAssetMenu(fileName = "HealthStatusData", menuName = "StatusObjects/Health", order = 1)]
public class CharacterStatus : ScriptableObject
{
    public string charName = "name";

    public string charClass = "Paladin";
    public int level = 1;
    public int xp = 0;

    public int maxhp = 100;
    public int hp = 100;

    public int base_spd = 0;
    public int base_atk = 0;
    public int base_def = 0;
    public int base_rec = 0;
    public int base_pty = 0;

    public int spd = 0;
    public int atk = 0;
    public int def = 0;
    public int rec = 0;
    public int pty = 0;

}