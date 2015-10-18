﻿
using System;
using UAOOI.SemanticData.DataManagement.DataRepository;

namespace UAOOI.SemanticData.DataManagement.MessageHandling
{
  /// <summary>
  /// Class MessageReaderBase - helper class providing basic implementation of the <see cref="IMessageReader"/> interface
  /// </summary>
  public abstract class MessageReaderBase : IMessageReader, IPeriodicDataMessage
  {

    #region IMessageReader
    /// <summary>
    ///  If implemented in derived class gets the state machine for this instance.
    /// </summary>
    /// <value>An object of <see cref="IAssociationState" /> providing implementation of the state machine governing this instance behavior.</value>
    public abstract IAssociationState State
    {
      get;
      protected set;
    }
    /// <summary>
    /// Attaches to network - initialize the underlying protocol stack and establish the connection with the broker is applicable.
    /// </summary>
    /// <remarks>Depending on the message transport layer type implementation of this function varies.</remarks>
    public abstract void AttachToNetwork();
    /// <summary>
    /// Occurs when an asynchronous operation to read a new message completes.
    /// </summary>
    public event EventHandler<MessageEventArg> ReadMessageCompleted;
    #endregion

    #region IPeriodicDataMessage
    /// <summary>
    /// Check if the message destination is the data set described by the <paramref name="dataId" /> of type <see cref="ISemanticData" />.
    /// </summary>
    /// <param name="dataId">The data identifier <see cref="ISemanticData" />.</param>
    /// <returns><c>true</c> if <paramref name="dataId" /> is the destination of the message, <c>false</c> otherwise.</returns>
    public abstract bool IAmDestination(ISemanticData dataId);
    /// <summary>
    /// Updates my values using inverse of control pattern.
    /// </summary>
    /// <param name="update">Captures a delegated used to update the consumer variables using values decoded form the message.</param>
    /// <param name="length">Number of items in the data set.</param>
    void IPeriodicDataMessage.UpdateMyValues(Func<int, IConsumerBinding> update, int length)
    {
      UInt64 _mask = 0x1;
      int _associationIndex = 0;
      for (int i = 0; i < length; i++)
      {
        if ((ContentFilter & _mask) > 0)
        {
          IConsumerBinding _binding = update(_associationIndex);
          Read(_binding);
        }
        _associationIndex++;
        _mask = _mask << 1;
      }
    }
    #endregion

    #region private

    #region Reader
    protected abstract object ReadUInt64();
    protected abstract object ReadUInt32();
    protected abstract object ReadUInt16();
    protected abstract object ReadString();
    protected abstract object ReadSingle();
    protected abstract object ReadSByte();
    protected abstract object ReadInt64();
    protected abstract object ReadInt32();
    protected abstract object ReadInt16();
    protected abstract object ReadDouble();
    protected abstract object ReadChar();
    protected abstract object ReadByte();
    protected abstract object ReadBoolean();
    #endregion

    protected abstract ulong ContentFilter { get; set; }
    protected void RaiseReadMessageCompleted()
    {
      EventHandler<MessageEventArg> _handler = ReadMessageCompleted;
      if (_handler == null)
        return;
      ReadMessageCompleted(this, new MessageEventArg(this));
    }
    private void Read(IConsumerBinding binding)
    {
      if (!IsValueIConvertible(binding))
        throw new ArgumentOutOfRangeException(string.Format("Impossible to convert the type {0}", binding.TargetType.Name));
    }
    private bool IsValueIConvertible(IConsumerBinding binding)
    {
      object _value = null;
      System.IO.BinaryReader _r = null;
      switch (Type.GetTypeCode(binding.TargetType))
      {
        case TypeCode.Boolean:
          _value = ReadBoolean();
          break;
        case TypeCode.Byte:
          _value = ReadByte();
          break;
        case TypeCode.Char:
          _value = ReadChar();
          break;
        case TypeCode.DBNull:
          return false;
        case TypeCode.DateTime:
          _value = ReadDateTime();
          break;
        case TypeCode.Decimal:
          return false;
        case TypeCode.Double:
          _value = ReadDouble();
          break;
        case TypeCode.Empty:
          return false;
        case TypeCode.Int16:
          _value = ReadInt16();
          break;
        case TypeCode.Int32:
          _value = ReadInt32();
          break;
        case TypeCode.Int64:
          _value = ReadInt64();
          break;
        case TypeCode.Object:
          return false;
        case TypeCode.SByte:
          _value = ReadSByte();
          break;
        case TypeCode.Single:
          _value = ReadSingle();
          break;
        case TypeCode.String:
          _value = ReadString();
          break;
        case TypeCode.UInt16:
          _value = ReadUInt16();
          break;
        case TypeCode.UInt32:
          _value = ReadUInt32();
          break;
        case TypeCode.UInt64:
          _value = ReadUInt64();
          break;
        default:
          return false;
      }
      binding.Assign2Repository(_value);
      return true;
    }

    protected abstract DateTime ReadDateTime();
    #endregion

  }
}
