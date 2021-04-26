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
using System.Collections.Generic;
using System.Linq;
using MessengerExtensions;
using System;

#pragma warning disable 1591 // undocumented XML code warning

namespace ClockStone
{
    /// <summary>
    /// Auxiliary class to overcome the problem of references to pooled objects that should become <c>null</c> when 
    /// objects are moved back to the pool after calling <see cref="ObjectPoolController.Destroy(GameObject)"/>.
    /// </summary>
    /// <typeparam name="T">A <c>UnityEngine.Component</c></typeparam>
    /// <example>
    /// Instead of a normal reference to a script component on a poolable object use 
    /// <code>
    /// MyScriptComponent scriptComponent = PoolableObjectController.Instantiate( prefab ).GetComponent&lt;MyScriptComponent&gt;();
    /// var myReference = new PoolableReference&lt;MyScriptComponent&gt;( scriptComponent );
    /// if( myReference.Get() != null ) // will check if poolable instance still belongs to the original object
    /// {
    ///     myReference.Get().MyComponentFunction();
    /// }
    /// </code>
    /// </example>
    public class PoolableReference<T> where T : Component
    {
        PoolableObject _pooledObj;
        int _initialUsageCount;

#if REDUCED_REFLECTION
    Component _objComponent;
#else
        T _objComponent;
#endif

        /// <summary>
        /// Initializes a new instance of the <see cref="PoolableReference&lt;T&gt;"/> class with a <c>null</c> reference.
        /// </summary>
        public PoolableReference()
        {
            Reset();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PoolableReference&lt;T&gt;"/> class with the specified reference.
        /// </summary>
        /// <param name="componentOfPoolableObject">The referenced component of the poolable object.</param>
#if REDUCED_REFLECTION
    public PoolableReference( Component componentOfPoolableObject )
#else
        public PoolableReference( T componentOfPoolableObject )
#endif
        {
            Set( componentOfPoolableObject, false );
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PoolableReference&lt;T&gt;"/> class from 
        /// a given <see cref="PoolableReference&lt;T&gt;"/>.
        /// </summary>
        /// <param name="poolableReference">The poolable reference.</param>
        public PoolableReference( PoolableReference<T> poolableReference )
        {
            _objComponent = poolableReference._objComponent;
            _pooledObj = poolableReference._pooledObj;
            _initialUsageCount = poolableReference._initialUsageCount;
        }

        /// <summary>
        /// Resets the reference to <c>null</c>.
        /// </summary>
        public void Reset()
        {
            _pooledObj = null;
            _objComponent = null;
            _initialUsageCount = 0;
        }

        /// <summary>
        /// Gets the reference to the script component, or <c>null</c> if the object was 
        /// already destroyed or moved to the pool.
        /// </summary>
        /// <returns>
        /// The reference to <c>T</c> or null
        /// </returns>
        public T Get()
        {
            if ( !_objComponent )
                return null;

            if ( _pooledObj ) // could be set to a none-poolable object
            {
                if ( _pooledObj._usageCount != _initialUsageCount || _pooledObj._isInPool )
                {
                    _objComponent = null;
                    _pooledObj = null;
                    return null;
                }
            }
            return ( T ) _objComponent;
        }

#if REDUCED_REFLECTION
    public void Set( Component componentOfPoolableObject, bool allowNonePoolable )
#else
        public void Set( T componentOfPoolableObject )
        {
            Set( componentOfPoolableObject, false );
        }

        /// <summary>
        /// Sets the reference to a poolable object with the specified component.
        /// </summary>
        /// <param name="componentOfPoolableObject">The component of the poolable object.</param>
        /// <param name="allowNonePoolable">If set to false an error is output if the object does not have the <see cref="PoolableObject"/> component.</param>
        public void Set( T componentOfPoolableObject, bool allowNonePoolable )
#endif
        {
            if ( !componentOfPoolableObject )
            {
                Reset();
                return;
            }
            _objComponent = ( T ) componentOfPoolableObject;
            _pooledObj = _objComponent.GetComponent<PoolableObject>();
            if ( !_pooledObj )
            {
                if ( allowNonePoolable )
                {
                    _initialUsageCount = 0;
                }
                else
                {
                    Debug.LogError( "Object for PoolableReference must be poolable" );
                    return;
                }
            }
            else
            {
                _initialUsageCount = _pooledObj._usageCount;
            }
        }
    }
}