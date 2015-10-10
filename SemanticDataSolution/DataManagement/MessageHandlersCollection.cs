﻿
using System;
using System.Collections.Generic;
using UAOOI.SemanticData.DataManagement.Configuration;
using UAOOI.SemanticData.DataManagement.MessageHandling;

namespace UAOOI.SemanticData.DataManagement
{
  /// <summary>
  /// Class MessageHandlersCollection - represents collection of communication channels involved in handling selected message centric transport providers.
  /// </summary>
  internal class MessageHandlersCollection : Dictionary<string, IMessageHandler>
  {
    internal static MessageHandlersCollection CreateMessageHandlers
      (Configuration.MessageTransportConfiguration[] configuration, IMessageHandlerFactory messageHandlerFactory, Action<string, IMessageHandler> addMessageHandler)
    {
      MessageHandlersCollection _collection = new MessageHandlersCollection();
      foreach (Configuration.MessageTransportConfiguration item in configuration)
      {
        if (_collection.ContainsKey(item.Name))
          throw new ArgumentOutOfRangeException("Name", "Duplicated transport name");
        IMessageHandler _handler = null;
        switch (item.TransportRole)
        {
          case AssociationRole.Consumer:
            _handler = messageHandlerFactory.GetIMessageReader(item.Name, item.Configuration);
            break;
          case AssociationRole.Producer:
            _handler = messageHandlerFactory.GetIMessageWriter(item.Name, item.Configuration);
            break;
          default:
            break;
        }
        _collection.Add(item.Name, _handler);
        foreach (string _association in item.Associations)
          addMessageHandler(_association, _handler);
      }
      return _collection;
    }
    /// <summary>
    /// Handles the configuration modifications.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    /// <exception cref="System.NotImplementedException">It is intentionally not implemented</exception>
    internal void OnConfigurationChangeHandler(object sender, EventArgs e)
    {
      throw new NotImplementedException("It is intentionally not implemented");
    }
    /// <summary>
    /// Runs this instance.
    /// </summary>
    internal void Run()
    {
      foreach (IMessageHandler _mx in this.Values)
      {
        _mx.AttachToNetwork();
        _mx.State.Enable();
      }
    }
    private MessageHandlersCollection()
      : base()
    { }

  }
}
