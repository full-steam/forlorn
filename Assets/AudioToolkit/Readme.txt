==============================================================
  Audio Toolkit v9.4 - (c) 2019 by ClockStone Software GmbH
==============================================================

Summary:
--------

Audio Toolkit provides an easy-to-use and performance optimized framework 
to play and manage music and sound effects in Unity.

Features include:

 - ease of use: play audio files with a simple static function call, creation 
    of required AudioSource objects is handled automatically 
 - conveniently define audio assets in categories
 - play audios from within the inspector
 - set properties such as the volume for the entire category
 - change the volume of all playing audio objects within a category at any time
 - define alternative audio clips that get played with a specified 
   probability or order
 - advanced audio pick modes such as "RandomNotSameTwice", "TwoSimultaneously", etc.
 - audio sequence modes such as
       + random looping
       + intro-loop
       + intro-loop-outro 
 - uses audio object pools for optimized performance particuliarly on mobile devices
 - set audio playing parameters conveniently, such as: 
       + random pitch & volume
       + minimum time difference between play calls
       + delay
       + looping
 - fade out / in 
 - special functions for music including cross-fading 
 - music track playlist management with shuffle, loop, etc.
 - delegate event call if audio was completely played
 - audio event log
 - audio overview window

Package Folders:
----------------

- AudioToolkit: The C# script files of the Audio Toolkit

- Shared Auxiliary Code: additional general purpose script files 
	required by the Audio Toolkit. These files might also be used 
	by other toolkits made by ClockStone available in the Unity Asset 
	Store.

- Reference Documentation file (Windows CHM format)

- Demo: A scene demonstrating the use of the toolkit



Quick Guide:
------------

We recommend to watch the video tutorial on http://unity.clockstone.com

Usage:
 - create a unique GameObject named "AudioController" with the 
   AudioController script component added.
 - Create an prefab named "AudioObject" containing the following components: Unity's AudioSource, 
   the AudioObject script, and the PoolableObject script (if pooling is wanted). 
   Then set your custom AudioSource parameters in this prefab. Next, specify this prefab 
   as the "Audio Object Prefab" in the AudioController.
 - create your audio categories in the AudioController using the Inspector, e.g. "Music", "SFX", etc.
 - for each audio to be played by a script create an 'audio item' with a unique name. 
 - specify any number of audio sub-items (= the AudioClip plus parameters in CLIP mode) 
   within an audio item. 
 - to play an audio item call the static function 
   AudioController.Play( "MyUniqueAudioItemName" )
 - Use AudioController.PlayMusic( "MusicAudioItemName" ) to play music. This function 
   assures that only one music file is played at a time and handles cross fading automatically 
   according to the configuration in the AudioController instance

For an up-to-date, detailed documentation please visit: http://unity.clockstone.com


Poolable Audio Objects: 
-----------------------

When audio object pooling is enabled you must be aware of the following:

- If audio object are attached to a parent object e.g. by calling AudioController.Play( audioID, parentTransform ) 
  then the parent object must be destroyed using ObjectPoolController.Destroy(), otherwise the 
  audio object will not be moved back to the pool correctly

- if you save an AudioObject reference and access it later in time, use PoolableReference to make sure the reference
  is not in the pool and still belongs to the original AudioObject

  Example:

  var soundFX = new PoolableReference( AudioController.Play( "someSFX" ) );

  // some other part of the code executed later when the sound may have stopped playing 
  // and was moved back to the pool
  AudioObject audioObject = soundFX.Get();
  if( audioObject.Get() != null )
  {
	// it is safe to access audioObject here
	audioObject.Stop();
  }


Memory / Audio Loading Management
---------------------------------

Any audio clip referenced in an Audio Controller will be loaded into memory (according to Unity's asset import options) 
the moment the Audio Controller is loaded into the scene. If you have a lot of audio files this can therefore have a 
significant impact on the loading times and the memory consumption. You can optimize in several ways:

1) Optimize your audio clip import settings. Here is a good guide I can recommend:
http://blog.theknightsofunity.com/wrong-import-settings-killing-unity-game-part-2/
You may also think about disabling the 'Preload Audio' import settings for some of your audio clips. However, be aware that 
loading an audio just before playing may result in performance hick-ups.

2) Split your audio into several Audio Controllers:

In the main Audio Controller specify all audio that is used throughout your entire application, such as a "mouse click", 
"error sound", etc. Keep it as small as possible and load it right in your app loading scene. Enable the "persist scene loading"
option so the Audio Controller remains in all your scenes.

If you have audio that is specific to certain levels/scenes of your game then create an Audio Controllers with the
level specific stuff for each of your levels. Mark this Audio Controller as "additional" in the Unity inspector and 
do NOT set the "persist scene loading" so all audio gets unloaded. You can use "Unload audio" to force Unity to unload 
all audios when the respective controller gets destroyed.

3) If you require even more control create asset bundles for each of your Audio Controllers and load / unload them dynamically 
by script.



Usage with Java Script:
-----------------------
If you want to access the AudioToolkit from Java Scripts please move alle none-editor AudioToolkit script files
to a subfolder of either the Assets/Plugins or the Assets/Standard Assets directory. The "AudioController/Editor" 
subdirectory must not be moved to Plugins/Standard Assets directory. 
See http://docs.unity3d.com/Documentation/ScriptReference/index.Script_compilation_28Advanced29.html for
more infos about script compilation order.

Please note that Java script does not support default parameter values like C# (since Unity 3.1). There are 
overloads for the most common methods, but for all other cases you have to specify all parameters in Java Script.

e.g.: 
AudioController.Play( "MySFX", 0.5 ); // Play with volume=0.5, delay=0, startTime =0, possible only in C#
AudioController.Play( "MySFX", 0.5, 0, 0 ); // in Java Script you have to specify all default paramters


Changelog:
----------

v2.2: initial release on asset store 

v2.3:
- playlist with cross fading
- new function PlayPreviousMusicOnPlaylist()

v2.4:
- implementation without default method parameters for MonoDevelop .NET 3.5 compatibility

v2.5:
- several inspector view bugfixes

v2.6 
- new feature: AudioObjectPrefab can be set for each category
- new feature: OverrideClipLength
- new feature: "Add all items to playlist" - button
- inspector view bugfix: changed data not saves correctly 

v3.0
- maximum instance count
- audio log
- audio overview
- new subitem pick modes: random-not-same-twice, sequence, all, two
- subitem modes: CLIP (play audio clip) or ITEM (play audio item)
- reworked GUI design 
- play audio assets from within the Unity inspector

v3.1
- refined GUI design
- new subitem feature: Random Start Position
- new subitem feature: Start / End Position
- bugfix: audio log refreshed correctly

v3.2
- bugfix: inspector null reference errors on audio controllers without categories or items
- playlist can still be resumed after StopMusic() 

v3.3
- new subitem feature: Random Delay, Fade-in, Fade-out
- pooling system: bugfix for poolable object parented to other poolable object
- bugfix: probability not working with 'RandomNotSameTwice'-mode
- inspector: keyboard input focus released when changing items

v3.4
- Flash support
- new AudioController option: Persist Scene Load
- new AudioController functions: GetPlayingAudioObjectsInCategory, PauseAll, UnpauseAll,
  PauseCategory, UnpauseCategory, IsValidAudioID
- add new categories and audio items per script functions: 
  NewCategory, RemoveCategory, AddToCategory
- high precision system time used instead of Unity's game time ( e.g. for fading)
- Object Pooling System: poolable objects parented to poolable objects now correctly handled
- bugfix: music correctly handled when changing scene with none-persistent AudioController

v3.5
- AudioObject.Stop() fades out audio with sub item "Fade-Out" parameter 
- bugfix: FadeIn start volume 

v3.6
- bugfix: inspector changes saved correctly
- no error if pooling is disabled and AudioObject does not have the PoolableObject component 

v4.0
- Unity 4 compatibility
- new function AudioController.RemoveAudioItem

v5.0
- AudioSource override parameters (min/max distance)
- pooling system bugfixes and improvements (new messages)
- new parameter: AudioObject.Stop( float fadeOutLength, float startToFadeTime ) 
- new loop modes: Loop Sequence, Loop Sequence Gapless
- sub / parent categories
- music fade-in/out can be specified separately
- Unity inspector undo working

v5.1
- bugfixes for Flash build
- bugfix: volume change of parent category
- audio item is rename changes playlist accordingly

v5.2
- bugfix: "Loop Sequence" working correctly

v5.3
- AudioController automatically preloads poolable AudioObject prefabs if preloading is specified
  in the poolable AudioObject prefab
- AudioController.Stop function uses by default the fadeout as specified int the subitem  
- two Stop() calls with fadeout now combine the fade-out
- bugfix: incorrect parent category displayed in inspector

v6.0
- bugfix: Stopping sequence loop audio 
- new AudioObject functions: FadeOut, volumeItem
- volume changes of audio items / subitems in inspector take effect
- new feature: move audio item to a different category
- additional AudioController objects
- Pause/Unpause with fade-in/out
- Support for new Unity v4.1 features: PlayScheduled
- Loop sequence: gapless stitching, or overlap with fade
- tooltips in Unity Inspector for AudioController
- bugfixes when pausing / unpausing a fading audio
- AudioObject: performance improvements
- object pooling: performance improvements, pooled objects grouped in hierarchy
- MaxInstance count check: audio objects are destroyed immediatly if exceeded by more than one
- bugfix: UnpauseAll / UnpauseCategory
- item overview window: search audio item

v6.1
- added function versions without default paramters for Java script support
- new functions: AudioController.DetachAllAudios(), AudioObject.PlayNow()
- new Loop Mode: PlaySequenceAndLoopLast
- bugfix: when using additional AudioController the correct AudioObejctPrefab and PlayWithZeroVolume values are taken
- bugfix: when displaying Audio Item Overview of prefab

v6.2
- support for Win8 app and Windows Phone8
- added new property AudioObject.pan
- bugfix in pooling system: dummy parent objects recreated when loading scene
- bugfix when using gapless looping and 3D audio files (incorrect 3D parameters)
- bugfix: paused audios change volume correctly if global or category volume gets changed

v6.3
- fixed a bug when playing a sequence with subitem Play mode "Other Audio Item" 
- fixed a bug when unpausing an audio that is still fading out
- inspector: sort audio items and categories in alphabetical order

v6.4 
- bugfix: "Argument out of range" with zero elements in playlist
- bugfix: music is not faded-in anymore if crossfade is enabled but no music is playing
- new option: equal-power cross-fading

v6.5
- new feature: intro-loop-outro sequence mode
- new AudioObject method: FinishSequence()
- new feature: unload audio if audio controller is destroyed
- new AudioController function: GetAudioItemMaxDistance()

v6.6
- new method: AudioController.StopCategory(...)
- bugfix for Unity v4.5 error "Serialization depth limit exceeded"
- new subitem option: "Disable Other Subitems"
- bugfix isFadingIn / isFadingOut, new properties isFadeOutComplete, isFadingOutOrScheduled
- sound muting

v6.7
- bugfix: errors when selecting AudioObject in Unity
- bugfix: IsPlaylistPlaying now working correctly

v6.8
- new feature: audio skip, stop, and destroy logged
- bugfix: Audio item overview does not display names 
- bugfix: category volumes of additional AudioControllers adjust to main controller when enabled or instantiated
- added pitch to log
- new sub item pick mode: RandomNotSameTwiceOddsEvens:
- new features: loopSequenceRandomPitch, loopSequenceRandomVolume
- optimized C# code to reduce garbage collection issues, GetPlaying[...] methods now return a List instead of an Array

v6.9
- bugfix: fixing null ref exception when playing an dynamically added audio item from within the editor

v7.0
- Full Unity 5 compatibility (Unity 4 still supported)
- Unity 5 feature: spatialBlend

v7.1
- allowing the second Audio Source to be manually added to AudioObject prefab
- AudioSubItem StopTime now working even if no FadeOut is specified (0.1s fade out used by default)

v7.2
- moved AudioItem setting spatialBlend to "Override Audio Source Settings"
- AudioObjectPrefabs are now inherited by parent category
- assign AudioCategories to Unity 5 Audio Mixing Groups 

v7.3
- new method AudioItem.ResetSequence()

v8.0
- Support for multiple music playlists
- Audio Subitem batch change for random volume, pitch and delay
- Item Overview improved 
- drag-drag audio clips to create audio subitems
- Audio item copy/paste
- ambience sound methods

v8.1
- workaround for Unity5 Unload Audio 
- bugfix: additional AudioController can now persist scene loading
- bugfix: playing audio with fade-in does not produce clicks anymore 

v8.2
- new feature: Audio Category fade in/out
- bugfix: correct audio system time delta even if Time.maxDeltaTime was exceeded
- bugfix: music playlist not working when playing ambience sound

v8.3
- inspector: check for duplicate audio itemID
- "Unload Audio On Destroy" working again even if 'Preload Audio Data' is enabled in 
  the import settings
- fixed serialisation / undo issues
- introduced AudioChannelType (for music, ambience)
- new AudioController property: musicParent / ambienceParent

v8.4
- fixed Unity 2017 compatibility issues
- play playlist button in inspector

v8.5
- fixed initialisation order issue when the main AudioController gets instantiated after 
  additional controllers 
- improved pooling system (performance, etc.)

v9.0
- all scripts in namespace ClockStone,
  Upgrade notice: add "using ClockStone" to your code to access AudioToolkit classes!!
- AudioItem propery pitch shift
- AudioController: playlistFinishedEven
- resume playing audio for disabled objects when enabled again

v9.1
- fix missing properties in Unity 2018 (Spatial Blend, Preview button)

v9.2
- support for spatializer plugin with intro/loop/outro sequences

v9.3
- bugfix in pooling system that caused audio objects to keep playing after scene change
  even though "Stop When Scene Loads" was activated on the AudioObject
- fix null reference exception with empty playlists
- remove support for Ouya

v9.4
- fix playing audio clips in Editor
- button to stop playing audio clips in Editor
- CopyID button in item list (copy audioID to clipboard)
- "Browse Audio Items"-button in PlayAudio component
- select AudioController button in Item Overview
- create prefab menu (for AudioController and AudioObject)