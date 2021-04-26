using UnityEngine;
using System;
using System.Reflection;

namespace MessengerExtensions
{

    /// <summary>
    /// Broadcast messages between objects and components, including inactive ones (which Unity doesn't do)
    /// </summary>
    public static class MessengerThatIncludesInactiveElements
    {
        private static BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance | BindingFlags.Default;
        /// <summary>
        /// Determine if the object has the given method
        /// </summary>
        private static void InvokeIfExists( this object objectToCheck, string methodName, params object[] parameters )
        {
            MethodInfo mI = null;
            Type baseType = objectToCheck.GetType();

            while ( true )
            {
                mI = baseType.GetMethod( methodName, flags );
                baseType = baseType.BaseType;

                if ( mI != null )
                {
                    mI.Invoke( objectToCheck, parameters );
                    return;
                }

                if ( baseType == null )
                    return;
            }
        }

        /// <summary>
        /// Determine if the object has the given method
        /// </summary>
        private static void InvokeIfExists( this object objectToCheck, string methodName )
        {
            MethodInfo mI = null;
            Type baseType = objectToCheck.GetType();

            while ( true )
            {
                mI = baseType.GetMethod( methodName, flags );
                baseType = baseType.BaseType;

                if ( mI != null )
                {
                    mI.Invoke( objectToCheck, null );
                    return;
                }

                if ( baseType == null )
                    return;
            }
        }

        /// <summary>
        /// Invoke the method if it exists in any component of the game object, even if they are inactive
        /// </summary>
        public static void InvokeMethod( this GameObject gameobject, string methodName, bool includeInactive, params object[] parameters )
        {
            MonoBehaviour[] components = gameobject.GetComponents<MonoBehaviour> ();
            for ( int i = 0; i < components.Length; i++ )
            {
                var m = components[i];
                if ( includeInactive || m.isActiveAndEnabled )
                    m.InvokeIfExists( methodName, parameters );
            }
        }

        /// <summary>
        /// Invoke the method if it exists in any component of the game object, even if they are inactive
        /// </summary>
        public static void InvokeMethod( this GameObject gameobject, string methodName, bool includeInactive )
        {
            MonoBehaviour[] components = gameobject.GetComponents<MonoBehaviour> ();
            for ( int i = 0; i < components.Length; i++ )
            {
                var m = components[i];
                if ( includeInactive || m.isActiveAndEnabled )
                    m.InvokeIfExists( methodName );
            }
        }

        /// <summary>
        /// Invoke the method if it exists in any component of the component's game object, even if they are inactive
        /// </summary>
        public static void InvokeMethod( this Component component, string methodName, bool includeInactive, params object[] parameters )
        {
            component.gameObject.InvokeMethod( methodName, includeInactive, parameters );
        }

        /// <summary>
        /// Invoke the method if it exists in any component of the component's game object, even if they are inactive
        /// </summary>
        public static void InvokeMethod( this Component component, string methodName, bool includeInactive )
        {
            component.gameObject.InvokeMethod( methodName, includeInactive );
        }


        /// <summary>
        /// Invoke the method if it exists in any component of the game object and its children, even if they are inactive
        /// </summary>
        public static void InvokeMethodInChildren( this GameObject gameobject, string methodName, bool includeInactive, params object[] parameters )
        {
            MonoBehaviour[] components = gameobject.GetComponentsInChildren<MonoBehaviour> ( includeInactive );
            for ( int i = 0; i < components.Length; i++ )
            {
                var m = components[i];
                if ( includeInactive || m.isActiveAndEnabled )
                    m.InvokeIfExists( methodName, parameters );
            }
        }

        /// <summary>
        /// Invoke the method if it exists in any component of the game object and its children, even if they are inactive
        /// </summary>
        public static void InvokeMethodInChildren( this GameObject gameobject, string methodName, bool includeInactive )
        {
            MonoBehaviour[] components = gameobject.GetComponentsInChildren<MonoBehaviour> ( includeInactive );
            for ( int i = 0; i < components.Length; i++ )
            {
                var m = components[i];
                if ( includeInactive || m.isActiveAndEnabled )
                    m.InvokeIfExists( methodName );
            }
        }

        /// <summary>
        /// Invoke the method if it exists in any component of the component's game object and its children, even if they are inactive
        /// </summary>
        public static void InvokeMethodInChildren( this Component component, string methodName, bool includeInactive, params object[] parameters )
        {
            component.gameObject.InvokeMethodInChildren( methodName, includeInactive, parameters );
        }

        /// <summary>
        /// Invoke the method if it exists in any component of the component's game object and its children, even if they are inactive
        /// </summary>
        public static void InvokeMethodInChildren( this Component component, string methodName, bool includeInactive )
        {
            component.gameObject.InvokeMethodInChildren( methodName, includeInactive );
        }

        /// <summary>
        /// Invoke the method if it exists in any component of the game object and its ancestors, even if they are inactive
        /// </summary>
        public static void SendMessageUpwardsToAll( this GameObject gameobject, string methodName, bool includeInactive, params object[] parameters )
        {
            Transform tranform = gameobject.transform;
            while ( tranform != null )
            {
                tranform.gameObject.InvokeMethod( methodName, includeInactive, parameters );
                tranform = tranform.parent;
            }
        }
        /// <summary>
        /// Invoke the method if it exists in any component of the component's game object and its ancestors, even if they are inactive
        /// </summary>
        public static void SendMessageUpwardsToAll( this Component component, string methodName, bool includeInactive, params object[] parameters )
        {
            component.gameObject.SendMessageUpwardsToAll( methodName, includeInactive, parameters );
        }
    }
}