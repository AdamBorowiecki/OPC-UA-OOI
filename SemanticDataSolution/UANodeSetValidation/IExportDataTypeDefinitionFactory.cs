﻿
using System.Xml;

namespace UAOOI.SemanticData.UANodeSetValidation
{

  /// <summary>
  /// Interface IExportDataTypeDefinitionFactory- This interface is used to define subtypes of the Structure or Enumeration standard DataTypes.
  /// It defines an abstract representation of a <see cref="IExportDataTypeFactory"/> that can be used by design tools to automatically create 
  /// serialization code.
  /// </summary>
  public interface IExportDataTypeDefinitionFactory
  {

    IExportDataTypeFieldFactory Field();
    XmlQualifiedName Name
    {
      set;
    }
    XmlQualifiedName BaseType
    {
      set;
    }
    string SymbolicName
    {
      set;
    }

  }
}
