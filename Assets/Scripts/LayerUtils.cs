using UnityEngine;

namespace SampleArcade
{
    public class LayerUtils
    {
        public const string BulletLayerName = "Bullet";
        public const string EnemyLayerName = "Enemy";
        public const string PlayerLayerName = "Player";
        public const string PickUpWeaponLayerName = "PickUpWeapon";
        public const string PickUpBoostLayerName = "PickUpBoost";

        public static readonly int PlayerLayer = LayerMask.NameToLayer(PlayerLayerName);
        public static readonly int EnemyLayer = LayerMask.NameToLayer(EnemyLayerName);
        public static readonly int BulletLayer = LayerMask.NameToLayer(BulletLayerName);
        public static readonly int PickUpWeaponLayer = LayerMask.NameToLayer(PickUpWeaponLayerName);
        public static readonly int PickUpBoostLayer = LayerMask.NameToLayer(PickUpBoostLayerName);

        public static readonly int CharacterMask = LayerMask.GetMask(EnemyLayerName, PlayerLayerName);
        public static readonly int PickUpMask = LayerMask.GetMask(PickUpWeaponLayerName, PickUpBoostLayerName);

        public static bool IsBullet(GameObject other) => other.layer == BulletLayer;
        public static bool IsPickUp(GameObject other) => other.layer ==  PickUpWeaponLayer ||
                                                         other.layer == PickUpBoostLayer;

        public static bool IsWeaponPickUp(GameObject other) => other.layer == PickUpWeaponLayer;

        public static bool IsCharacter(GameObject other) => other.layer == PlayerLayer ||
                                                            other.layer == EnemyLayer;
    }
}
