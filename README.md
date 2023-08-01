# SpaceRocketBuilder
Game about building rockets and flying them, created as part of the engineering project (WIP)

**Controls:**
* Flight mode:
  * Rocket rotating:
    * W, S - rocket yaw axis rotation
    * A, D - rocket pitch axis rotation
    * Q, E - rocket roll axis rotation
  * Engine throttle management:
    * Left Shift - increase engine's throttle
    * Left Ctrl - decrease engine's throttle
    * Z - set engine's throttle to 100%
    * X - set engine's throttle to 0%
  * Camera movement:
    * RMB - camera rotation
    * Scroll - zoom in/out the view
  * Other:
    * Space - activate next stage

**Current status of progress:**
* Basic 2 body physics between a planet and other rigidbody objects ie. Rocket
* Basic rocket behaviour
  * Working engine behaviour (throttle, fuel consuming)
  * Working multistaging behaviour (stages can be separated and exist as independent objects)
* Working rocket editor mode
  * User is able to select various components and stack them into a rocket model
  * User is able to save and load his own models
* Both rocket editor and flight mode are integrated and may be accessible through a hub - Space Center
