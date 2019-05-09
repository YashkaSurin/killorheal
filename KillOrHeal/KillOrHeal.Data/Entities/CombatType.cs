using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace KillOrHeal.Data.Entities
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum CombatType: byte
    {
        Melee = 0,
        Ranged = 1
    }
}