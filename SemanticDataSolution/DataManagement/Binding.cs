﻿
using System;
using System.Globalization;
using System.Windows.Data;

namespace UAOOI.SemanticData.DataManagement
{

  /// <summary>
  /// Class Binding - a generic implementation of the <see cref="IBinding"/> interface. 
  /// The instance of this class is used to update a destination variable by an owner of this object.
  /// It captures an association targeted a variable that is to be updated by the user of this instance.
  /// It is assumed that the repository implements the <see cref="IBindingFactory"/> interface and is the factory of this instance.
  /// </summary>
  public class Binding<type> : IBinding
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="Binding{type}"/> class.
    /// </summary>
    /// <param name="assign">This parameters captures a delegate that will be used to update the destination variable hosted by a repository. 
    /// </param>
    public Binding()
    {
      m_TargetType = typeof(type);
    }
    #region IBinding
    /// <summary>
    /// Gets the type of the repository a variable that is to be updated using <see cref="IBinding.Assign2Repository" />.
    /// </summary>
    /// <value>The type of the repository target variable of the binding.</value>
    Type IBinding.TargetType { get { return m_TargetType; } }
    /// <summary>
    /// Sets the converter, which is used to provide a way to apply custom logic to a binding.
    /// </summary>
    /// <value>The converter as an instance of the <see cref="IValueConverter" />.</value>
    IValueConverter IBinding.Converter { set { m_Converter = value; } }
    /// <summary>
    /// Sets an optional parameter to be used in the converter logic.
    /// </summary>
    /// <value>The parameter to be used by the <see cref="IBinding.Converter" />.</value>
    object IBinding.Parameter
    {
      set { m_Parameter = value; }
    }
    /// <summary>
    /// Sets the culture of the conversion.
    /// </summary>
    /// <value>The culture as an instance of the <see cref="CultureInfo" /> to be used by the <see cref="IBinding.Converter" />.</value>
    CultureInfo IBinding.Culture
    {
      set { m_Culture = value; }
    }
    /// <summary>
    /// Marks the process value enabled - signal that the update of the value is expected.
    /// </summary>
    void IBinding.OnEnabling()
    {
      RaiseHandlerState(HandlerState.Operational);
    }
    /// <summary>
    /// Marks the process value disabled - signal that the value will not be updated.
    /// </summary>
    void IBinding.OnDisabling()
    {
      RaiseHandlerState(HandlerState.Disabled);
    }
    #endregion

    #region public API
    /// <summary>
    /// Gets the state of the handler.
    /// </summary>
    /// <value>The state of the handler.</value>
    public HandlerState HandlerState { get { return m_HandlerState; } }
    /// <summary>
    /// Occurs when state changes].
    /// </summary>
    public event EventHandler<AssociationStateChangedEventArgs> StateChangedEventHandler;
    #endregion

    #region private
    protected Type m_TargetType;
    protected IValueConverter m_Converter;
    protected CultureInfo m_Culture;
    private HandlerState m_HandlerState = HandlerState.Operational;
    protected object m_Parameter;
    private void RaiseHandlerState(HandlerState state)
    {
      EventHandler<AssociationStateChangedEventArgs> _hc = StateChangedEventHandler;
      if (_hc != null)
        _hc(this, new AssociationStateChangedEventArgs(state));
    }
    #endregion
  }

}
