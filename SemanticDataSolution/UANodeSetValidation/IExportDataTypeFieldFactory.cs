﻿
using System.Xml;

namespace UAOOI.SemanticData.UANodeSetValidation
{
  /// <summary>
  /// Interface IExportDataTypeFieldFactory - The=is interface defines an abstract representation of a field within a 
  /// UADataType that can be used by design tools to automatically create serialization code.
  /// </summary>
  public interface IExportDataTypeFieldFactory
  {
    XmlQualifiedName DataType
    {
      set;
    }
    void AddDescription(string localeField, string valueField);
    int Identifier
    {
      set;
    }
    bool IdentifierSpecified
    {
      set;
    }
    string Name
    {
      set;
    }
    int ValueRank
    {
      set;
    }
    /// <summary>The field is a structure with a layout specified by the <see cref="IExportDataTypeDefinitionFactory"/>. 
    /// This field is optional.
    /// This field allows designers to create nested structures without defining a new DataType Node for each structure.
    /// This field is not specified for subtypes of Enumeration.
    /// </summary>
    /// <value>The definition.</value>
    IExportDataTypeDefinitionFactory NewDefinition();
    /// <summary>
    /// The value associated with the field. This field is only specified for subtypes of Enumeration.
    /// </summary>
    /// <value>The value.</value>
    int Value
    {
      set;
    }
    string SymbolicName
    {
      set;
    }
  }
}
