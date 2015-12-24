﻿
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UAOOI.SemanticData.DataManagement.Encoding;
using System.Xml;
using System.Net;

namespace UAOOI.SemanticData.UANetworking.ReferenceApplication.UnitTest
{
  [TestClass]
  public class BinaryUDPPackageReaderTestClass
  {

    #region TestMethod
    [TestMethod]
    [TestCategory("ReferenceApplication_BinaryUDPPackageReaderTestClass")]
    public void CreatorTestMethod()
    {
      bool _ExclusiveAddressUse = true;
      using (Consumer.BinaryUDPPackageReader _reader1 = new Consumer.BinaryUDPPackageReader(new UADecoder(), 4840, x => Console.WriteLine(x), null))
      {
        Assert.IsNotNull(_reader1);
        _reader1.ReuseAddress = _ExclusiveAddressUse;
        _reader1.MulticastGroup = IPAddress.Parse("239.0.0.1");
        _reader1.State.Enable();
        Assert.IsNotNull(_reader1.MulticastGroup);
      }
      using (Consumer.BinaryUDPPackageReader _reader1 = new Consumer.BinaryUDPPackageReader(new UADecoder(), 4840, x => Console.WriteLine(x), null))
      {
        Assert.IsNotNull(_reader1);
        _reader1.State.Enable();
      }
    }
    [TestMethod]
    [TestCategory("ReferenceApplication_BinaryUDPPackageReaderTestClass")]
    public void ExclusiveAddressUseTrueTestMethod()
    {
      bool _ExclusiveAddressUse = true;
      using (Consumer.BinaryUDPPackageReader _reader1 = new Consumer.BinaryUDPPackageReader(new UADecoder(), 4840, x => Console.WriteLine(x), null))
      {
        Assert.IsNotNull(_reader1);
        _reader1.ReuseAddress = _ExclusiveAddressUse;
        _reader1.State.Enable();
        using (Consumer.BinaryUDPPackageReader _reader2 = new Consumer.BinaryUDPPackageReader(new UADecoder(), 4840, x => Console.WriteLine(x), null))
        {
          Assert.IsNotNull(_reader2);
          _reader2.ReuseAddress = _ExclusiveAddressUse;
          _reader2.State.Enable();
        }
      }
    }
    [TestMethod]
    [TestCategory("ReferenceApplication_BinaryUDPPackageReaderTestClass")]
    [ExpectedException(typeof(System.Net.Sockets.SocketException))]
    public void ExclusiveAddressUseFalseTestMethod()
    {
      bool _ExclusiveAddressUse = false;
      using (Consumer.BinaryUDPPackageReader _reader1 = new Consumer.BinaryUDPPackageReader(new UADecoder(), 4840, x => Console.WriteLine(x), null))
      {
        Assert.IsNotNull(_reader1);
        _reader1.ReuseAddress = _ExclusiveAddressUse;
        _reader1.State.Enable();
        using (Consumer.BinaryUDPPackageReader _reader2 = new Consumer.BinaryUDPPackageReader(new UADecoder(), 4840, x => Console.WriteLine(x), null))
        {
          Assert.IsNotNull(_reader2);
          _reader2.ReuseAddress = _ExclusiveAddressUse;
          _reader2.State.Enable();
        }
      }
    }
    [TestMethod]
    [TestCategory("ReferenceApplication_BinaryUDPPackageReaderTestClass")]
    [ExpectedException(typeof(InvalidOperationException))]
    public void ExclusiveAddressOperationalTestMethod()
    {
      bool _ExclusiveAddressUse = true;
      using (Consumer.BinaryUDPPackageReader _reader1 = new Consumer.BinaryUDPPackageReader(new UADecoder(), 4840, x => Console.WriteLine(x), null))
      {
        Assert.IsNotNull(_reader1);
        _reader1.ReuseAddress = _ExclusiveAddressUse;
        _reader1.State.Enable();
        _reader1.ReuseAddress = _ExclusiveAddressUse;
      }
    }
    [TestMethod]
    [TestCategory("ReferenceApplication_BinaryUDPPackageReaderTestClass")]
    [ExpectedException(typeof(InvalidOperationException))]
    public void ExclusiveMulticastGroupTestMethod()
    {
      bool _ExclusiveAddressUse = true;
      using (Consumer.BinaryUDPPackageReader _reader1 = new Consumer.BinaryUDPPackageReader(new UADecoder(), 4840, x => Console.WriteLine(x), null))
      {
        try
        {
          Assert.IsNotNull(_reader1);
          _reader1.ReuseAddress = _ExclusiveAddressUse;
          _reader1.MulticastGroup = IPAddress.Parse("239.0.0.1");
          _reader1.State.Enable();
        }
        catch (Exception)
        {
          Assert.Fail();
        }
        _reader1.MulticastGroup = IPAddress.Parse("239.0.0.1"); 
      }
    }
    #endregion

    #region test instrumentation
    private class UADecoder : IUADecoder
    {
      public byte[] ReadByteString(IBinaryDecoder decoder)
      {
        throw new NotImplementedException();
      }

      public IDataValue ReadDataValue(IBinaryDecoder decoder)
      {
        throw new NotImplementedException();
      }

      public DateTime ReadDateTime(IBinaryDecoder decoder)
      {
        throw new NotImplementedException();
      }

      public IDiagnosticInfo ReadDiagnosticInfo(IBinaryDecoder decoder)
      {
        throw new NotImplementedException();
      }

      public IExpandedNodeId ReadExpandedNodeId(IBinaryDecoder decoder)
      {
        throw new NotImplementedException();
      }

      public IExtensionObject ReadExtensionObject(IBinaryDecoder decoder)
      {
        throw new NotImplementedException();
      }

      public Guid ReadGuid(IBinaryDecoder decoder)
      {
        throw new NotImplementedException();
      }

      public ILocalizedText ReadLocalizedText(IBinaryDecoder decoder)
      {
        throw new NotImplementedException();
      }

      public INodeId ReadNodeId(IBinaryDecoder decoder)
      {
        throw new NotImplementedException();
      }

      public IQualifiedName ReadQualifiedName(IBinaryDecoder decoder)
      {
        throw new NotImplementedException();
      }

      public IStatusCode ReadStatusCode(IBinaryDecoder decoder)
      {
        throw new NotImplementedException();
      }

      public IVariant ReadVariant(IBinaryDecoder decoder)
      {
        throw new NotImplementedException();
      }

      public XmlElement ReadXmlElement(IBinaryDecoder decoder)
      {
        throw new NotImplementedException();
      }
    }
    #endregion

  }
}
