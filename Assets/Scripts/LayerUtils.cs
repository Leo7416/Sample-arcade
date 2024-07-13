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

        public static readonly int BulletLayer = LayerMask.NameToLayer(BulletLayerName);
        public static readonly int PickUpWeaponLayer = LayerMask.NameToLayer(PickUpWeaponLayerName);
        public static readonly int PickUpBoostLayer = LayerMask.NameToLayer(PickUpBoostLayerName);

        public static readonly int EnemyMask = LayerMask.GetMask(EnemyLayerName, PlayerLayerName);

        public static bool IsBullet(GameObject other) => other.layer == BulletLayer;
        public static bool IsPickUpWeapon(GameObject other) => other.layer == PickUpWeaponLayer;

        public static bool IsPickUpBoost(GameObject other) => other.layer == PickUpBoostLayer;
    }
}
