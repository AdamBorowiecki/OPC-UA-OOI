﻿
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using UAOOI.SemanticData.DataManagement.Encoding;
using System.Xml;
using UAOOI.SemanticData.UANetworking.Configuration.Serialization;

namespace UAOOI.SemanticData.DataManagement.UnitTest
{
  [TestClass]
  public class UABinaryDecoderImplementationUnitTest
  {

    #region TestMethod
    [TestMethod]
    [TestCategory("DataManagement_UABinaryDecoderImplementationUnitTest")]
    [ExpectedException(typeof(NotImplementedException))]
    public void WriteDataValueTestMethod()
    {
      IDataValue _ReadDataValue;
      using (MemoryStream _stream = new MemoryStream())
      using (TestBinaryReader _buffer = new TestBinaryReader(_stream))
        _ReadDataValue = _buffer.ReadDataValue(_buffer);
    }
    [TestMethod]
    [TestCategory("DataManagement_UABinaryDecoderImplementationUnitTest")]
    public void GuidTestMethod()
    {
      Guid _Guid = Guid.NewGuid();
      MemoryStream _stream = new MemoryStream(_Guid.ToByteArray());
      TestBinaryReader _buffer = new TestBinaryReader(_stream);
      Assert.IsNotNull(_buffer);
      Guid _EncodedGuid = _buffer.ReadGuid();
      _buffer.Close();
      Assert.AreEqual<Guid>(_Guid, _EncodedGuid);
    }
    [TestMethod]
    [TestCategory("DataManagement_UABinaryDecoderImplementationUnitTest")]
    public void VariantGuidTestMethod()
    {
      using (MemoryStream _stream = new MemoryStream(CommonDefinitions.TestGuidVariant))
      using (TestBinaryReader _buffer = new TestBinaryReader(_stream))
      {
        Assert.IsNotNull(_buffer);
        IVariant _EncodedVGuid = _buffer.ReadVariant(_buffer);
        _buffer.Close();
        Assert.IsNotNull(_EncodedVGuid);
        Assert.AreEqual<BuiltInType>(BuiltInType.Guid, _EncodedVGuid.UATypeInfo.BuiltInType);
        Assert.AreEqual<int>(-1, _EncodedVGuid.UATypeInfo.ValueRank);
        Assert.AreEqual<Guid>(CommonDefinitions.TestGuid, (Guid)_EncodedVGuid.Value);
      }
    }
    [TestMethod]
    [TestCategory("DataManagement_UABinaryDecoderImplementationUnitTest")]
    public void ArrayOneDimensionTest()
    {
      byte[] _testArray = new byte[] { 134, 5, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 2, 0, 0, 0, 3, 0, 0, 0, 4, 0, 0, 0 };
      IVariant _EncodedArray = null;
      using (MemoryStream _stream = new MemoryStream(_testArray))
      using (TestBinaryReader _buffer = new TestBinaryReader(_stream))
      {
        Assert.IsNotNull(_buffer);
        _EncodedArray = _buffer.ReadVariant(_buffer);
        _buffer.Close();
      }
      Assert.IsNotNull(_EncodedArray);
      Assert.IsNotNull(_EncodedArray);
      Assert.AreEqual<BuiltInType>(BuiltInType.Int32, _EncodedArray.UATypeInfo.BuiltInType);
      Assert.AreEqual<int>(1, _EncodedArray.UATypeInfo.ValueRank);
      Assert.IsInstanceOfType(_EncodedArray.Value, typeof(IMatrix));
      IMatrix _value = _EncodedArray.Value as IMatrix;
      Assert.AreEqual<BuiltInType>(BuiltInType.Int32, _value.TypeInfo.BuiltInType);
      Assert.AreEqual<int>(1, _value.TypeInfo.ValueRank);
      Assert.AreEqual<int>(1, _value.Dimensions.Length);
      Assert.AreEqual<int>(5, _value.Dimensions[0]);
      CollectionAssert.AreEqual(new Int32[] { 0, 1, 2, 3, 4 }, _value.Elements);
    }
    [TestMethod]
    [TestCategory("DataManagement_UABinaryDecoderImplementationUnitTest")]
    public void VariantDateTimeTestMethod()
    {
      foreach (CommonDefinitions.DateTimeVariantEncoding _dtx in CommonDefinitions.DateTimeTestingValues)
      {
        using (MemoryStream _stream = new MemoryStream(_dtx.encoding))
        using (TestBinaryReader _buffer = new TestBinaryReader(_stream))
        {
          Assert.IsNotNull(_buffer);
          IVariant _variant = _buffer.ReadVariant(_buffer);
          _buffer.Close();
          Assert.AreEqual<BuiltInType>(BuiltInType.DateTime, _variant.UATypeInfo.BuiltInType);
          Assert.AreEqual<DateTime>(_dtx.dateTime, (DateTime)_variant.Value);
        }
      }
    }
    #endregion

    #region private
    private class TestBinaryReader : BinaryReader, IBinaryDecoder, IUADecoder
    {
      public TestBinaryReader(Stream input) : base(input) { }

      #region IUADecoder - not supported
      public byte[] ReadByteString(IBinaryDecoder decoder)
      {
        Assert.AreSame(this, decoder);
        return m_UABinaryDecoder.ReadByteString(decoder);
      }
      public IDataValue ReadDataValue(IBinaryDecoder decoder)
      {
        Assert.AreSame(this, decoder);
        return m_UABinaryDecoder.ReadDataValue(decoder);
      }
      public DateTime ReadDateTime(IBinaryDecoder decoder)
      {
        Assert.AreSame(this, decoder);
        return m_UABinaryDecoder.ReadDateTime(decoder);
      }
      public IDiagnosticInfo ReadDiagnosticInfo(IBinaryDecoder decoder)
      {
        Assert.AreSame(this, decoder);
        return m_UABinaryDecoder.ReadDiagnosticInfo(decoder);
      }
      public IExpandedNodeId ReadExpandedNodeId(IBinaryDecoder decoder)
      {
        Assert.AreSame(this, decoder);
        return m_UABinaryDecoder.ReadExpandedNodeId(decoder);
      }
      public IExtensionObject ReadExtensionObject(IBinaryDecoder decoder)
      {
        Assert.AreSame(this, decoder);
        return m_UABinaryDecoder.ReadExtensionObject(decoder);
      }
      public Guid ReadGuid(IBinaryDecoder decoder)
      {
        Assert.AreSame(this, decoder);
        return m_UABinaryDecoder.ReadGuid(decoder);
      }
      public ILocalizedText ReadLocalizedText(IBinaryDecoder decoder)
      {
        Assert.AreSame(this, decoder);
        return m_UABinaryDecoder.ReadLocalizedText(decoder);
      }
      public INodeId ReadNodeId(IBinaryDecoder decoder)
      {
        Assert.AreSame(this, decoder);
        return m_UABinaryDecoder.ReadNodeId(decoder);
      }
      public IQualifiedName ReadQualifiedName(IBinaryDecoder decoder)
      {
        Assert.AreSame(this, decoder);
        return m_UABinaryDecoder.ReadQualifiedName(decoder);
      }
      public IStatusCode ReadStatusCode(IBinaryDecoder decoder)
      {
        Assert.AreSame(this, decoder);
        return m_UABinaryDecoder.ReadStatusCode(decoder);
      }
      public IVariant ReadVariant(IBinaryDecoder decoder)
      {
        Assert.AreSame(this, decoder);
        return m_UABinaryDecoder.ReadVariant(decoder);
      }
      public XmlElement ReadXmlElement(IBinaryDecoder decoder)
      {
        Assert.AreSame(this, decoder);
        return m_UABinaryDecoder.ReadXmlElement(decoder);
      }
      public string ReadString(IBinaryDecoder decoder)
      {
        Assert.AreSame(this, decoder);
        return m_UABinaryDecoder.ReadString(decoder);
      }

      #endregion

      #region IBinaryDecoder
      public Guid ReadGuid()
      {
        return ReadGuid(this);
      }
      public DateTime ReadDateTime()
      {
        return ReadDateTime(this);
      }
      #endregion

      #region private
      private Helpers.UABinaryDecoderImplementation m_UABinaryDecoder = new Helpers.UABinaryDecoderImplementation();
      #endregion

    }

    #endregion

  }
}
