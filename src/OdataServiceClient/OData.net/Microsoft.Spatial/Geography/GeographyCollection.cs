﻿//---------------------------------------------------------------------
// <copyright file="GeographyCollection.cs" company="Microsoft">
//      Copyright (C) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.
// </copyright>
//---------------------------------------------------------------------

namespace Microsoft.Spatial
{
    using System.Collections.ObjectModel;
    using System.Linq;

    /// <summary>Represents the collection of geographies.</summary>
    public abstract class GeographyCollection : Geography
    {
        /// <summary>Initializes a new instance of the <see cref="Microsoft.Spatial.GeographyCollection" /> class.</summary>
        /// <param name="coordinateSystem">The coordinate system of this geography collection.</param>
        /// <param name="creator">The implementation that created this instance.</param>
        protected GeographyCollection(CoordinateSystem coordinateSystem, SpatialImplementation creator)
            : base(coordinateSystem, creator)
        {
        }

        /// <summary>Gets the collection of geographies.</summary>
        /// <returns>The collection of geographies.</returns>
        public abstract ReadOnlyCollection<Geography> Geographies { get; }

        /// <summary>Determines whether this instance and another specified geography instance have the same value.</summary>
        /// <returns>true if the value of the value parameter is the same as this instance; otherwise, false.</returns>
        /// <param name="other">The geography to compare to this instance.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", Justification = "null is a valid value")]
        public bool Equals(GeographyCollection other)
        {
            return this.BaseEquals(other) ?? this.Geographies.SequenceEqual(other.Geographies);
        }

        /// <summary>Determines whether this instance and the specified object have the same value.</summary>
        /// <returns>true if the value of the value parameter is the same as this instance; otherwise, false.</returns>
        /// <param name="obj">The object to compare to this instance.</param>
        public override bool Equals(object obj)
        {
            return this.Equals(obj as GeographyCollection);
        }

        /// <summary>Gets the hash code.</summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode()
        {
            return Microsoft.Spatial.Geography.ComputeHashCodeFor(this.CoordinateSystem, this.Geographies);
        }
    }
}