/*************************************************************
 *           Unity Object Pool (c) by ClockStone 2017        *
 * 
 * Allows to "pool" prefab objects to avoid large number of
 * Instantiate() calls.
 * 
 * Usage:
 * 
 * Add the PoolableObject script component to the prefab to be pooled.
 * You can set the maximum number of objects to be be stored in the 
 * pool from within the inspector.
 * 
 * Replace all Instantiate( myPrefab ) calls with 
 * ObjectPoolController.Instantiate( myPrefab)
 * 
 * Replace all Destroy( myObjectInstance ) calls with 
 * ObjectPoolController.Destroy( myObjectInstance )
 * 
 * Replace all DestroyImmediate( myObjectInstance ) calls with 
 * ObjectPoolController.DestroyImmediate( myObjectInstance )
 * 
 * Note that Awake(), and OnDestroy() get called correctly for 
 * pooled objects. However, make sure that all component data that could  
 * possibly get changed during its lifetime get reinitialized by the
 * Awake() function.
 * The Start() function gets also called, but just after the Awake() function
 * during ObjectPoolController.Instantiate(...)
 * 
 * If a poolable objects gets parented to none-poolable object, the parent must
 * be destroyed using ObjectPoolController.Destroy( ... )
 * 
 * Be aware that OnDestroy() will get called multiple times: 
 *   a) the time ObjectPoolController.Destroy() is called when the object is added
 *      to the pool
 *   b) when the object really gets destroyed (e.g. if a new scene is loaded)
 *   
 * References to pooled objects will not change to null anymore once an object has 
 * been "destroyed" and moved to the pool. Use PoolableReference if you need such checks.
 * 
 * ********************************************************************
*/

#if UNITY_3_5 || UNITY_3_4 || UNITY_3_3 || UNITY_3_2 || UNITY_3_1 || UNITY_3_0
#define UNITY_3_x
#endif

using UnityEngine;
using UnityEngine.SceneManagement;

#pragma warning disable 1591 // undocumented XML code warning

namespace ClockStone
{
    /// <summary>
    /// Add this component to your prefab to make it poolable. 
    /// </summary>
    /// <remarks>
    /// See <see cref="ObjectPoolController"/> for an explanation how to set up a prefab for pooling.
    /// The following messages are sent to a poolable object:
    /// <list type="bullet">
    /// <item> 
    ///   <c>Awake()</c>, <c>Start()</c> and <c>OnDestroy()</c> whenever a poolable object is activated 
    ///   or deactivated from the pool (when the prefab used to instantiate is active itself). 
    ///   This way the same behaviour is simulated as if the object was instantiated respectively destroyed.
    ///   These messages are only sent when <see cref="sendAwakeStartOnDestroyMessage"/> is enabled.
    /// </item>
    /// <item>
    ///   <c>OnPoolableObjectActivated()</c> and <c>OnPoolableObjectDeactivated()</c> whenever a poolable 
    ///   object is activated or deactivated from the pool.
    ///   These messages are only sent when <see cref="sendPoolableActivateDeactivateMessages"/> is enabled.
    /// </item>
    /// </list>
    /// </remarks>
    /// <seealso cref="ObjectPoolController"/>
    [AddComponentMenu( "ClockStone/PoolableObject" )]
    public class PoolableObject : MonoBehaviour
    {
        [Tooltip("Specifies the maximum number of objects on the pool")]
        /// <summary>
        /// The maximum number of instances of this prefab to get stored in the pool.
        /// </summary>
        public int maxPoolSize = 10;

        [Tooltip("Specifies the number of objects that will be created on the pool at program start (improves speed of later instantiation)")]
        /// <summary>
        /// This number of instances will be preloaded to the pool if <see cref="ObjectPoolController.Preload(GameObject)"/> is called.
        /// </summary>
        public int preloadCount = 0;

        [Tooltip("If enabled the pool of deactivated objects will surivive a scene change")]
        /// <summary>
        /// If enabled the object will not get destroyed if a new scene is loaded
        /// </summary>
        public bool doNotDestroyOnLoad = false;

        /// <summary>
        /// If enabled Awake(), Start(), and OnDestroy() messages are sent to the poolable object if the object is set 
        /// active respectively inactive whenever <see cref="ObjectPoolController.Destroy(GameObject)"/> or 
        /// <see cref="ObjectPoolController.Instantiate(GameObject)"/> is called. <para/>
        /// This way it is simulated that the object really gets instantiated respectively destroyed.
        /// </summary>
        /// <remarks>
        /// The Start() function is called immedialtely after Awake() by <see cref="ObjectPoolController.Instantiate(GameObject)"/> 
        /// and not next frame. So do not set data after <see cref="ObjectPoolController.Instantiate(GameObject)"/> that Start()
        /// relies on. In some cases you may not want the  Awake(), Start(), and OnDestroy() messages to be sent for performance 
        /// reasons because it may not be necessary to fully reinitialize a game object each time it is activated from the pool. 
        /// You can still use the <c>OnPoolableObjectActivated</c> and <c>OnPoolableObjectDeactivated</c> messages to initialize 
        /// specific data.
        /// The Awake() and Start() Messages only get sent when the instantiated object got instantiated from an active prefab
        /// parent. If the prefab was deactivated then the instantiated object will also be inactive and the Methods aren't called. 
        /// </remarks>
        public bool sendAwakeStartOnDestroyMessage = true;

        /// <summary>
        /// If enabled a <c>OnPoolableObjectActivated</c> and <c>OnPoolableObjectDeactivated</c> message is sent to 
        /// the poolable instance if the object is activated respectively deactivated by the <see cref="ObjectPoolController"/>
        /// </summary>
        public bool sendPoolableActivateDeactivateMessages = false;

        /// <summary>
        /// If enabled reflection gets used to invoke the <c>Awake()</c>, <c>Start()</c>, <c>OnDestroy()</c>, 
        /// <c>OnPoolableObjectActivated()</c> and <c>OnPoolableObjectDeactivated()</c> Methods instead of using the Unity-
        /// Messaging-System. This is useful when objects are instantiated as inactive or deactivated before they are destroyed.
        /// (Unity-Messaging-System works on active components and GameObjects only!)
        /// </summary>
        /// <remarks>
        /// * Invocations when an object gets instantiated (taken from pool):
        ///   - <c>Awake()</c> on active Components
        ///   - <c>Start()</c> on active Components
        ///   - <c>OnPoolableObjectActivated()</c> on all Components (also inactive)
        ///     (when an object is instantiated as inactive <c>Awake()</c> and <c>Start()</c> are never called)
        /// 
        /// * Invocations when an object gets destroyed (moved to pool):
        ///   - <c>OnPoolableObjectDeactivated()</c> on all Components (also inactive)
        ///   - <c>OnDestroy()</c> on all Components (also inactive)
        /// </remarks>
        public bool useReflectionInsteadOfMessages = false;

        internal bool _isInPool = false;

        /// <summary>
        /// if null - Object was not created from ObjectPoolController
        /// </summary>
        internal ObjectPoolController.ObjectPool _pool = null;

        internal int _serialNumber = 0;
        internal int _usageCount = 0;

        //needed when an object gets instantiated deactivated to prevent double awake
        internal bool _awakeJustCalledByUnity = false;
        public bool isPooledInstance
        {
            get
            {
                return _pool != null;
            }
        }

        internal bool _wasInstantiatedByObjectPoolController = false;

        private bool _justInvokingOnDestroy = false;

        protected void Awake()
        {
            _awakeJustCalledByUnity = true;

#if UNITY_EDITOR
            if ( !isPooledInstance && !ObjectPoolController._isDuringInstantiate && !_wasInstantiatedByObjectPoolController )
                Debug.LogWarning( "Poolable object " + name + " was instantiated without ObjectPoolController" );
#endif

        }

        protected void OnDestroy()
        {
            //only if destroy message comes from unity and not from invocation
            if ( !_justInvokingOnDestroy && _pool != null )
            {
                // Poolable object was destroyed by using the default Unity Destroy() function -> Use ObjectPoolController.Destroy() instead
                // This can also happen if objects are automatically deleted by Unity e.g. due to level change or if an object is parented to an object that gets destroyed
                _pool.Remove( this );
            }
        }

        /// <summary>
        /// Gets the object's pool serial number. Each object has a unique serial number. Can be useful for debugging purposes.
        /// </summary>
        /// <returns>
        /// The serial number (starting with 1 for each pool).
        /// </returns>
        public int GetSerialNumber() // each new instance receives a unique serial number
        {
            return _serialNumber;
        }

        /// <summary>
        /// Gets the usage counter which gets increased each time an object is re-used from the pool.
        /// </summary>
        /// <returns>
        /// The usage counter
        /// </returns>
        public int GetUsageCount()
        {
            return _usageCount;
        }

        /// <summary>
        /// Moves all poolable objects of this kind (instantiated from the same prefab as this instance) back to the pool. 
        /// </summary>
        /// <returns>
        /// The number of instances deactivated and moved back to its pool.
        /// </returns>
        public int DeactivateAllPoolableObjectsOfMyKind()
        {
            if ( _pool != null )
            {
                return _pool._SetAllAvailable();
            }
            return 0;
        }

        /// <summary>
        /// Checks if the object is deactivated and in the pool.
        /// </summary>
        /// <returns>
        /// <c>true</c> if the object is in the pool of deactivated objects, otherwise <c>false</c>.
        /// </returns>
        public bool IsDeactivated()
        {
            return _isInPool;
        }

        internal void _PutIntoPool()
        {
            if ( _pool == null )
            {
                Debug.LogError( "Tried to put object into pool which was not created with ObjectPoolController", this );
                return;
            }

            if ( _isInPool )
            {
                if ( transform.parent != _pool.poolParent )
                {
                    Debug.LogWarning( "Object was already in pool but parented to Pool-Parent. Reparented.", this );
                    transform.parent = _pool.poolParent;

                    if ( transform.parent != _pool.poolParent )
                    {
                        Debug.LogError( "Object couldn´t be reparented. Deleted" );
                        DestroyImmediate( gameObject );
                    }

                    return;
                }

                Debug.LogWarning( "Object is already in Pool", this );
                return;
            }

            //dont fire callbacks when object is put into pool initially
            if ( !ObjectPoolController._isDuringInstantiate )
            {
                if ( sendAwakeStartOnDestroyMessage )
                {
                    _justInvokingOnDestroy = true;
                    _pool.CallMethodOnObject( gameObject, "OnDestroy", true, true, useReflectionInsteadOfMessages );
                    _justInvokingOnDestroy = false;
                }

                if ( sendPoolableActivateDeactivateMessages )
                    _pool.CallMethodOnObject( gameObject, "OnPoolableObjectDeactivated", true, true, useReflectionInsteadOfMessages );
            }

            _isInPool = true;
            transform.SetParent( _pool.poolParent, true );
            //transform.parent = _pool.poolParent;

            gameObject.SetActive( false );
        }

        internal void TakeFromPool( Transform parent, bool activateObject )
        {
            if ( !_isInPool )
            {
                Debug.LogError( "Tried to take an object from Pool which is not available!", this );
                return;
            }

            _isInPool = false;

            _usageCount++;
            transform.SetParent( parent, true );
            if( parent == null /*&& doNotDestroyOnLoad*/ )
            {
                // make sure that the object is not in the DontDestroyOnLoadScene when taken from pool
                SceneManager.MoveGameObjectToScene( gameObject, SceneManager.GetActiveScene() );
            }
            //transform.parent = parent;

            if ( activateObject )
            {
                //this may be set to true when unity calls Awake after gameObject.SetActive(true);
                _awakeJustCalledByUnity = false;
                gameObject.SetActive( true );

                if ( sendAwakeStartOnDestroyMessage )
                {
                    //when an instance gets activated not the first time Awake() wont be called again. so we call it here via reflection!
                    if ( !_awakeJustCalledByUnity )
                    {
                        _pool.CallMethodOnObject( gameObject, "Awake", true, false, useReflectionInsteadOfMessages );

                        if ( gameObject.activeInHierarchy ) // Awake could deactivate object
                            _pool.CallMethodOnObject( gameObject, "Start", true, false, useReflectionInsteadOfMessages );
                    }
                }

                if ( sendPoolableActivateDeactivateMessages )
                {
                    _pool.CallMethodOnObject( gameObject, "OnPoolableObjectActivated", true, true, useReflectionInsteadOfMessages );
                }
            }
        }
    }
}