﻿
namespace UAOOI.SemanticData.UANodeSetValidation.InformationModelFactory
{
  internal class NodeFactoryBase : NodesContainer, IExportNodeFactory
  {

    public string BrowseName
    {
      set { }
    }
    public XML.LocalizedText[] Description
    {
      set { }
    }
    public XML.LocalizedText[] DisplayName
    {
      set { }
    }
    public IExportReferenceFactory NewReference()
    {
      return new ReferenceFactoryBase();
    }
    public System.Xml.XmlQualifiedName SymbolicName
    {
      set { }
    }
    public uint WriteAccess
    {
      set { }
    }
  }
}
