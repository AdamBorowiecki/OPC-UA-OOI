﻿
using System;
using UAOOI.SemanticData.UANetworking.Configuration.Serialization;

namespace UAOOI.SemanticData.UANetworking.Configuration.UnitTest
{
  internal static class ReferenceConfiguration
  {

    #region API
    /// <summary>
    /// Created the configuration from the local data.
    /// </summary>
    /// <remarks>In production release shall be replaced by reading from the file.</remarks>
    /// <returns>ConfigurationData.</returns>
    internal static ConfigurationData LoadConsumer()
    {
      return new ConfigurationData() { DataSets = GetDataSetConfigurations(AssociationRole.Consumer), MessageHandlers = GetMessageTransport(AssociationRole.Consumer) };
    }
    /// <summary>
    /// Created the configuration from the local data.
    /// </summary>
    /// <remarks>In production release shall be replaced by reading from the file.</remarks>
    /// <returns>ConfigurationData.</returns>
    internal static ConfigurationData LoadProducer()
    {
      return new ConfigurationData() { DataSets = GetDataSetConfigurations(AssociationRole.Producer), MessageHandlers = GetMessageTransport(AssociationRole.Producer) };
    }
    #endregion

    #region configuration
    private static MessageHandlerConfiguration[] GetMessageTransport(AssociationRole associationRole)
    {
      return new MessageHandlerConfiguration[] 
      { new MessageHandlerConfiguration()
        { AssociationNames = GetTransportAssociations(),
          Configuration = null,
          Name = "UDP",
          TransportRole = associationRole
        }
      };
    }
    private static string[] GetTransportAssociations()
    {
      return new string[] { AssociationConfigurationAlias };
    }
    private static DataSetConfiguration[] GetDataSetConfigurations(AssociationRole associationRole)
    {
      return new DataSetConfiguration[]
      { new DataSetConfiguration()
        { AssociationName = AssociationConfigurationAlias,
          AssociationRole = associationRole,
          DataSet = GetMembers(),
          DataSymbolicName = "DataSymbolicName",
          Id = DefaultAssociationConfigurationId,
          RepositoryGroup = m_RepositoryGroup,
          InformationModelURI= AssociationConfigurationInformationModelURI,
          ConfigurationGuid = m_ConfigurationGuid,
          ConfigurationVersion = new ConfigurationVersionDataType() { MajorVersion = 0x0, MinorVersion=0x0  },
          MaxBufferTime = 100,
          PublishingInterval = 1000,
          Root = new NodeDescriptor(  ) { NodeIdentifier = new System.Xml.XmlQualifiedName("NodeDescriptor", "NodeDescriptorNS")  }
        }
      };
    }
    private static FieldMetaData[] GetMembers()
    {
      return new FieldMetaData[]
      {
        new FieldMetaData() { ProcessValueName = "Value1", Encoding =  BuiltInType.DateTime, SymbolicName = "Value1",  ValueRank= -1, ArrayDimensions = new int[] { }  },
        new FieldMetaData() { ProcessValueName = "Value2", Encoding =  BuiltInType.Double, SymbolicName = "Value2",  ValueRank= -1, ArrayDimensions = new int[] { }  },
      };
    }
    #endregion

    #region preconfigured settings
    private const string AssociationConfigurationAlias = "Association1";
    private const string m_RepositoryGroup = "repositoryGroup";
    private const string AssociationConfigurationDataSymbolicName = "DataSymbolicName";
    private const string AssociationConfigurationInformationModelURI = @"https://github.com/mpostol/OPC-UA-OOI";
    private static readonly Guid DefaultAssociationConfigurationId = new Guid("C1F53FFB-6552-4CCC-84C9-F847147CDC85");
    private static Guid m_ConfigurationGuid = new Guid("D3DEA20A-1F65-4744-ABF5-3D8120960D7B");
    #endregion

  }
}
