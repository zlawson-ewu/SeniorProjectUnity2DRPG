Enemy Prefab and Waypoint Manager/Follower Schemes v1

-------
How to Test:

Open EnemyTest scene
	If Needed:
	(Add ExampleWorld prefab)
	(Add Enemy prefab)
	(Add Player prefab)
Play and observe the enemy follow the collider path
	Get within chase range to observe chase behavior
	Exit chase range to observe enemy return to route
	Edit the EnemyPath's EdgeCollider2D in EnemyPath if desired
	Mess with Waypoint Follower values in EnemySprite
	Change the triggerable CircleCollider2D's radius to adjust chase range
-------

The enemy prefab has two game objects attached to it: one containing the sprite, rigidbodies, enemy controller script, and a waypoint follower script, and the other containing a waypoint manager script and an edge collider 2d.

The waypoint manager script simply indexes the verticies of the attached edge collider 2d and makes them available for a waypoint follower to use.

The waypoint follower script needs a waypoint manager to tell it where the waypoints are. The waypoints are created by drawing the collider (edit collider) with its vertexes as waypoints.

The waypoint follower script handles travelling and chasing behaviors for its attached object, and is where the movement of that object is defined. This is to allow for more behaviors than just enemy chasing, for example, an object can be moved to unblock a passage (or something) using this script plus a trigger of some sort. It could also be used for moving platforms, though with current edge cases this should be treated with care (locking player movement, for example).

The enemy controller script simply defines which behaviors to use and when for the waypoint follower script. OriginalBehavior defines what the enemy should be doing if not chasing the player. WaitDelayAfterHit defines how many seconds the enemy should pause after collision (which will trigger the combat scene). An object with the enemy controller script must have a collider for its 'body' (which triggers combat when it collides with the player), and a circle collider 2d (set to trigger) for its chase range. If a player is within this chase range, the enemy will move toward the player.

Details about certain variables are commented within their relevant scripts. 