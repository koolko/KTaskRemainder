using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTaskRemainder.Behavior
{
    /// <summary>
    /// Interface for dragable objects
    /// </summary>
    interface IDragDrop
    {
        /// <summary>
        /// Name of the collection to which the object belongs
        /// </summary>
        string CollectionName { get; }

        /// <summary>
        /// Index of the object in his collection
        /// </summary>
        int Index { get; }

        /// <summary>
        /// Remove object from his collection
        /// </summary>
        void Remove();

        /// <summary>
        /// Drop object to collection
        /// </summary>
        /// <param name="collection">Collection in which the object should be placed</param>
        /// <param name="index">Index location of the dropped object</param>
        void Drop(string collection, int index = -1);
    }
}
