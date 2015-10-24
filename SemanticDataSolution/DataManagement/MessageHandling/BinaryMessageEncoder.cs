﻿
using System.IO;

namespace UAOOI.SemanticData.DataManagement.MessageHandling
{

  /// <summary>
  /// Class BinaryMessageEncoder - provides message content binary encoding functionality
  /// </summary>
  /// <remarks>
  /// <note>
  /// Implements only simple value types. Structural types must be implemented after more details will 
  /// be available in the spec.
  /// </note>
  /// </remarks>
  public abstract class BinaryMessageEncoder : MessageWriterBase, IBinaryHeaderWriter
  {
    
    #region IBinaryHeaderWriter
    /// <summary>
    /// Sets the position within the current stream.
    /// </summary>
    /// <param name="offset">
    /// A byte offset relative to origin.
    /// </param>
    /// <param name="origin">
    /// A field of <see cref="System.IO.SeekOrigin"/> indicating the reference point from which the new position is to be obtained..
    /// </param>
    /// <returns>The position with the current stream as <see cref="System.Int64"/>.</returns>
    public abstract long Seek(int offset, SeekOrigin origin);
    #endregion    

    #region MessageWriterBase
    /// <summary>
    /// Creates and prepares new the message.
    /// </summary>
    /// <param name="length">The length.</param>
    protected override void CreateMessage(int length)
    {
      //Create message header and placeholder for further header content.
      OnMessageAdding();
    }
    /// <summary>
    /// Sends the message - evaluates condition if send the package.
    /// </summary>
    /// <remarks>
    /// In current implementation one message per package is sent.
    /// </remarks>
    protected override void SendMessage()
    {
      OnMessageAdded();
      //TODO sign and encrypt the message.
    }
    #endregion

    #region private
    /// <summary>
    /// Called when new message is adding to the package payload.
    /// </summary>
    protected abstract void OnMessageAdding();

    /// <summary>
    /// Called when the current message has been added and is ready to be sent out.
    /// </summary>
    protected abstract void OnMessageAdded();
    #endregion    

  }

}
