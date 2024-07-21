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
        public const string BaseWeaponLayerName = "BaseWeapon";

        public static readonly int BulletLayer = LayerMask.NameToLayer(BulletLayerName);
        public static readonly int PickUpWeaponLayer = LayerMask.NameToLayer(PickUpWeaponLayerName);
        public static readonly int PickUpBoostLayer = LayerMask.NameToLayer(PickUpBoostLayerName);
        public static readonly int BaseWeaponLayer = LayerMask.NameToLayer(BaseWeaponLayerName);

        public static readonly int CharacterMask = LayerMask.GetMask(EnemyLayerName, PlayerLayerName);
        public static readonly int PickUpMask = LayerMask.GetMask(PickUpWeaponLayerName, PickUpBoostLayerName);

        public static bool IsBullet(GameObject other) => other.layer == BulletLayer;
    }
}
