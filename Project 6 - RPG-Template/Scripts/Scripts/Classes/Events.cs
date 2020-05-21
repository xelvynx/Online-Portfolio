
#region List of Events
public delegate void GameUpdated(GameState currentGameState, GameState futureGameState);
#endregion
#region List of Enumerators
public enum ItemType { Consumable, Key, Weapon, Helmet, Chest, Hand, Foot, None}
public enum AttackRange { Ranged, Melee }
public enum AttackAttribute { Physical, Magical }
public enum ElementalAttribute { Fire, Water, None }
public enum EquipmentSlot { Weapon, Helmet, Chest, Hand, Foot}
#endregion