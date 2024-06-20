# MagneticAim-Unity
Aim Assist system for Unity that increases assist strength the closer aiming is to the target.

## How it works:
The GetAssistedAim() function in AimAssist.cs receives the raw aim vector as an argument. This function returns the modified aim-assisted vector.
If aiming angle is within the user-set maximum threshold, then this vector will be corrected towards the nearest target (in angle).

## How to use 
- Add the AimAssist.cs script to the player game object.
- Set up assist parameters:
  - Aim Assist Strength: Animation curve to define strength of aim assist in relation to angle towards nearest target. Best to have the strength be 1 at 0 degrees-from-target and a gradual fall-off toward 0 as the angle approaches the MaxAngle.
  - MaxAngle: Maximum angle at which to assist aim, if angle-from-nearest-target is greater than this value then the assist strength will be 0 and the assisted aim will be equal to the raw aim (function argument).
- In your player controller, substitute the player input aim for the output of the GetAssistedAim() function. This function requires a List<Transform> containing all the transforms of the targetable objects in the player character's vicinity as well as the raw player input aim vector.
- If more/less aim is needed then modify the y values of the Aim Assist Strength curve.
- If you need the assist to help with targets further away/closer to the player's raw aim input then increase/decrease the MaxAngle value.
