# BulletRush
  A mobile game that likes Bullet Rush by VooDoo. Main difference is enemy spawn base. There are spawn points in map those enemies can spawn. There is two types of enemy: Big and simple enemies. Big enemy has 200 max health and 1.5 scale, simple enemy has 100 max health and 1 scale. Bullets that player shot gives 100 damage. Main goal is kill enemies and blast spawn points. If any enemy can collide with player, player lose.   
 
 Enemies and bullets are held by a pool. GameController and ObjectPool classes are singletons. On the project, component based architecture implemented with SOLID princibles.
  
  URP implemented for pipeline and Lit Shader Graph implemented for water shader. TMP implemented for UI texts. For big enemy enemy type could be implemented command pattern for rotate around the player but I think the method that implemented currently is better way to do this. if there was no enemy spawn points, enemies could randomly spawn on NavMesh. That method avaliable in EnemySpawner class as a comment.
  
  Thanks for viewing.
