﻿
using CAS.UA.IServerConfiguration;
using System;
using System.Collections.Generic;
using System.Xml;

namespace UAOOI.DataBindings
{
  /// <summary>
  /// Class NodeDescriptorBase - provides description of the node to be configured.
  /// </summary>
  [Serializable]
  public abstract class NodeDescriptorBase : INodeDescriptor, IComparable<INodeDescriptor>, IEqualityComparer<NodeDescriptorBase>
  {

    #region INodeDescriptor
    /// <summary>
    /// Gets the binding description that allows the editor to create automatically bindings.
    /// </summary>
    /// <value>The binding description.</value>
    public abstract string BindingDescription { get; set; }
    /// <summary>
    /// Gets the type of the node of the Variable NodeClass
    /// </summary>
    /// <value>The type of the data as instance of <see cref="XmlQualifiedName"/>.</value>
    public abstract XmlQualifiedName DataType { get; set; }
    /// <summary>
    /// Gets a value indicating whether it is instance declaration - may have many instances in the created address space.
    /// </summary>
    /// <value><c>true</c> if it is instance declaration; otherwise, <c>false</c>.</value>
    public abstract bool InstanceDeclaration { get; set; }
    /// <summary>
    /// Gets the node class.
    /// </summary>
    /// <value>The node class <see cref="InstanceNodeClassesEnum"/>.</value>
    public abstract InstanceNodeClassesEnum NodeClass { get; set; }
    /// <summary>
    /// Gets the node unique identifier, i.e. the symbolic path.
    /// </summary>
    /// <value>The node identifier.</value>
    public abstract XmlQualifiedName NodeIdentifier { get; set; }
    #endregion

    #region operators
    /// <summary>
    /// Implements the == operator.
    /// </summary>
    /// <param name="x">The first object of type <see cref="NodeDescriptorBase"/> to compare.</param>
    /// <param name="y">The second object of type <see cref="NodeDescriptorBase"/> to compare.</param>
    /// <returns><c>true</c> if the specified objects are equal; otherwise, false.</returns>
    public static bool operator ==(NodeDescriptorBase x, NodeDescriptorBase y)
    {
      if (x.Equals(null) && y.Equals(null))
        return true;
      if (Object.Equals(x, null) || Object.Equals(y, null))
        return false;
      return x.CompareTo(y) == 0;
    }
    /// <summary>
    /// Implements the !=.
    /// </summary>
    /// <param name="x">The first object of type <see cref="NodeDescriptorBase"/> to compare.</param>
    /// <param name="y">The second object of type <see cref="NodeDescriptorBase"/> to compare.</param>
    /// <returns><c>true</c> if the specified objects are not equal; otherwise, false.</returns>
    public static bool operator !=(NodeDescriptorBase x, NodeDescriptorBase y)
    {
      if (x.Equals(null) && y.Equals(null))
        return false;
      if (Object.Equals(x, null) || Object.Equals(y, null))
        return true;
      return x.CompareTo(y) != 0;
    }
    #endregion

    #region IComparable
    /// <summary>
    /// Compares the current instance with another <see cref="INodeDescriptor"/> and returns an integer that indicates whether the current instance precedes, 
    /// follows, or occurs in the same position in the sort order as the other object.
    /// </summary>
    /// <param name="other">An instance of <see cref="INodeDescriptor"/> to compare with this instance.</param>
    /// <returns>
    /// A <see cref="int"/> signed integer that indicates the relative order of the objects being compared. The return value has these meanings:
    /// Value, Meaning
    /// Less than zero:  This instance is less than <paramref name="other"/>.
    /// Zero: This instance is equal to <paramref name="other"/>.
    /// Greater than zero: This instance is greater than <paramref name="other"/>.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    /// 	<paramref name="other"/> is not the same type as this instance.
    /// </exception>
    public int CompareTo(INodeDescriptor other)
    {
      if (other == null)
        throw new ArgumentNullException(nameof(other), "Parameter cannot be null");
      if (this.NodeIdentifier == null || other.NodeIdentifier == null)
        throw new ArgumentNullException("NodeIdentifier cannot be null.");
      if (this.NodeIdentifier.IsEmpty || other.NodeIdentifier.IsEmpty)
        throw new ArgumentNullException("NodeIdentifier cannot be empty.");
      if (String.IsNullOrEmpty(this.NodeIdentifier.Namespace) || String.IsNullOrEmpty(other.NodeIdentifier.Namespace))
        throw new ArgumentNullException("NodeIdentifier Namespace cannot be null.");
      int ret = NodeIdentifier.Namespace.CompareTo(other.NodeIdentifier.Namespace);
      if (ret != 0)
        return ret;
      if (String.IsNullOrEmpty(this.NodeIdentifier.Name) || String.IsNullOrEmpty(other.NodeIdentifier.Namespace))
        throw new ArgumentNullException("NodeIdentifier Name cannot be null.");
      return NodeIdentifier.Name.CompareTo(other.NodeIdentifier.Name);
    }
    #endregion

    #region IEqualityComparer
    /// <summary>
    /// Determines whether the specified objects are equal.
    /// </summary>
    /// <param name="x">The first object of type <see cref="NodeDescriptorBase"/> to compare.</param>
    /// <param name="y">The second object of type <see cref="NodeDescriptorBase"/> to compare.</param>
    /// <returns><c>true</c> if the specified objects are equal; otherwise, false.</returns>
    public bool Equals(NodeDescriptorBase x, NodeDescriptorBase y)
    {
      return x.CompareTo(y) == 0;
    }
    /// <summary>
    /// Returns a hash code for this instance.
    /// </summary>
    /// <param name="obj">The object.</param>
    /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
    /// <exception cref="System.ArgumentNullException">if <paramref name="obj"/> is null</exception>
    public int GetHashCode(NodeDescriptorBase obj)
    {
      if (obj == null)
        throw new ArgumentNullException(nameof(obj));
      return obj.GetHashCode();
    }
    #endregion

    #region object
    /// <summary>
    /// Returns a hash code for this instance.
    /// </summary>
    /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
    public override int GetHashCode()
    {
      int _hash = NodeIdentifier.ToString().GetHashCode();
      return _hash;
    }
    /// <summary>
    /// Returns a <see cref="System.String" /> that represents this instance.
    /// </summary>
    /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
    public override string ToString()
    {
      return NodeIdentifier.ToString();
    }
    /// <summary>
    /// Determines whether the specified <see cref="System.Object" /> is equal to this instance.
    /// </summary>
    /// <param name="obj">The object to compare with the current object.</param>
    /// <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
    public override bool Equals(object obj)
    {
      return CompareTo((INodeDescriptor)obj) == 0;
    }
    #endregion

  }
}
